using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [Header("Board Settings")]
    [SerializeField] private RectTransform boardContainer; 
    [SerializeField] private GameObject cardPrefab;        
    [SerializeField] private CardsData cardsData;          // Updated: reference to CardsData ScriptableObject

    internal List<CardController> allCards = new List<CardController>();

public void GenerateBoard(int rows, int cols, Action<CardController> onCardFlipped)
{
    ClearBoard();
    allCards.Clear();
    
    GridLayoutGroup gridLayout = boardContainer.GetComponent<GridLayoutGroup>();
    RectTransform rt = boardContainer.GetComponent<RectTransform>();

// Get available width/height
    float width = rt.rect.width;
    float height = rt.rect.height;

// Calculate cell size so all items fit
    float cellWidth = width / cols;
    float cellHeight = height / rows;

// Pick the smaller dimension so cards stay square
    float cellSize = Mathf.Min(cellWidth, cellHeight);

    gridLayout.cellSize = new Vector2(cellSize, cellSize);
    gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
    gridLayout.constraintCount = cols;
    gridLayout.childAlignment = TextAnchor.MiddleCenter; // ✅ center the grid
    
    gridLayout.spacing = new Vector2(10, 10); // small gap between cards
    gridLayout.padding = new RectOffset(10, 10, 10, 10); // margin around grid

    int totalCards = rows * cols;
    if (totalCards % 2 != 0)
    {
        Debug.LogError("Board must have an even number of cards!");
        return;
    }

    // Safety check: do we have enough unique cards?
    int pairsNeeded = totalCards / 2;
    if (pairsNeeded > cardsData.cards.Count)
    {
        Debug.LogError($"Not enough unique cards in CardsData! Need {pairsNeeded}, but only have {cardsData.cards.Count}.");
        return;
    }

    // Step 1: Copy all available cards
    List<CardData> availableCards = new List<CardData>(cardsData.cards);

    // Step 2: Shuffle available cards
    for (int i = 0; i < availableCards.Count; i++)
    {
        CardData temp = availableCards[i];
        int randomIndex = UnityEngine.Random.Range(i, availableCards.Count);
        availableCards[i] = availableCards[randomIndex];
        availableCards[randomIndex] = temp;
    }

    // Step 3: Select only as many pairs as needed
    List<CardData> selectedCards = new List<CardData>();
    for (int i = 0; i < pairsNeeded; i++)
    {
        selectedCards.Add(availableCards[i]);
        selectedCards.Add(availableCards[i]); // add its pair
    }

    // Step 4: Shuffle the final list so pairs aren’t adjacent
    for (int i = 0; i < selectedCards.Count; i++)
    {
        CardData temp = selectedCards[i];
        int randomIndex = UnityEngine.Random.Range(i, selectedCards.Count);
        selectedCards[i] = selectedCards[randomIndex];
        selectedCards[randomIndex] = temp;
    }

    // Step 5: Instantiate cards
    GridLayoutGroup grid = boardContainer.GetComponent<GridLayoutGroup>();
    if (grid != null)
    {
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;
    }

    foreach (CardData data in selectedCards)
    {
        GameObject cardObj = Instantiate(cardPrefab, boardContainer);
        CardController card = cardObj.GetComponent<CardController>();
        card.Initialize(data);
        card.OnCardFlipped += onCardFlipped;
        allCards.Add(card);
    }
}

    private void ClearBoard()
    {
        foreach (Transform child in boardContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public bool AllCardsMatched()
    {
        foreach (CardController card in allCards)
        {
            if (!card.IsMatched()) return false;
        }
        return true;
    }

    private void Shuffle(List<CardData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            CardData temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}