using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private UIManager uiManager;

    private List<CardController> flippedCards = new List<CardController>();

    // Store last chosen layout
    public int LastRows { get; private set; }
    public int LastCols { get; private set; }

    // 🚫 Input lock flag
    private bool isCheckingMatch = false;

    /// <summary>
    /// Called by UIManager when a new game starts.
    /// </summary>
    public void StartNewGame(int rows, int cols)
    {
        flippedCards.Clear();
        scoreManager.ResetScore();

        LastRows = rows;
        LastCols = cols;

        boardManager.GenerateBoard(rows, cols, OnCardFlipped);

        StartCoroutine(ShowAllCardsAtStart(.375f)); // .375 second preview
    }
    
    public void LoadGame()
    {
        SaveData data = saveSystem.Load();
        uiManager.LoadGame();
        if (data != null)
        {
            LastRows = data.rows;
            LastCols = data.cols;
            scoreManager.SetScore(data.score);

            boardManager.GenerateBoard(data.rows, data.cols, OnCardFlipped);
            StartCoroutine(ShowAllCardsAtStart(.375f));
        }
    }
    
    private IEnumerator ShowAllCardsAtStart(float delay)
    {
        // Reveal all cards
        foreach (CardController card in boardManager.allCards)
        {
            card.ForceFlipFront();
        }

        yield return new WaitForSeconds(delay);

        // Reset all cards back to hidden
        foreach (CardController card in boardManager.allCards)
        {
            card.ResetCard();
        }
    }

    private void OnCardFlipped(CardController card)
    {
        if (isCheckingMatch) return;

        flippedCards.Add(card);
        audioManager.PlayFlip();

        if (flippedCards.Count == 2)
        {
            isCheckingMatch = true;
            CardController.InputLocked = true; // 🚫 lock clicks globally
            CheckMatch();
        }
    }

    private void CheckMatch()
    {
        if (flippedCards[0].GetCardId() == flippedCards[1].GetCardId())
        {
            flippedCards[0].SetMatched();
            flippedCards[1].SetMatched();
            scoreManager.AddPoints(10);
            audioManager.PlayMatch();
            flippedCards.Clear();

            isCheckingMatch = false;
            CardController.InputLocked = false; // ✅ unlock immediately
        }
        else
        {
            StartCoroutine(ResetAfterDelay(0.75f));
            scoreManager.AddPoints(-2);
            if (scoreManager.Score < 0) scoreManager.SetScore(0);
            audioManager.PlayMismatch();
        }

        if (boardManager.AllCardsMatched())
        {
            audioManager.PlayGameOver();
            uiManager.ShowGameOverPanel();
            saveSystem.Save(new SaveData
            {
                score = scoreManager.Score,
                rows = LastRows,
                cols = LastCols
            });
        }
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        flippedCards[0].ResetCard();
        flippedCards[1].ResetCard();
        flippedCards.Clear();

        isCheckingMatch = false;
        CardController.InputLocked = false; // ✅ unlock input after reset
    }
}