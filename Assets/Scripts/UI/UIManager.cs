using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text scoreText; // or TMP_Text if using TextMeshPro
    [SerializeField] private ToggleGroup gridToggleGroup;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        ShowMenu();
    }

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScoreUI;
        //gridToggleGroup.ActiveToggles().FirstOrDefault().GetComponent<>
    }

    /// <summary>
    /// Show the menu panel and hide the game panel.
    /// </summary>
    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    /// <summary>
    /// Start a new game with chosen layout.
    /// </summary>
    public void StartGame(int rows, int cols)
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        gameManager.StartNewGame(rows, cols);
        UpdateScoreUI();
    }
    
    public void LoadGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        UpdateScoreUI();
    }

    public void OnClickPlayButton()
    {
        // Get selected layout from toggles
        Toggle selectedToggle = gridToggleGroup.ActiveToggles().FirstOrDefault();
        if (selectedToggle != null)
        {
           GridData gridData = selectedToggle.GetComponentInParent<GridData>();
           StartGame(gridData.grid.rows, gridData.grid.cols);
        }
    }
    /// <summary>
    /// Restart the current game.
    /// </summary>
    public void RestartGame()
    {
        // Restart with the last chosen layout
        int rows = gameManager.LastRows;
        int cols = gameManager.LastCols;

        gameManager.StartNewGame(rows, cols);
        gameOverPanel.SetActive(false);
        UpdateScoreUI();
    }
    
    public void BackToMenu()
    {
        RestartGame();
        gamePanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    /// <summary>
    /// Update score display.
    /// </summary>
    public void UpdateScoreUI(int score = 0)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}