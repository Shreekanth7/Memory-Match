using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    private int comboCount = 0;
    public static Action<int> OnScoreChanged; // Event to notify UI of score changes

    /// <summary>
    /// Add points to the score. Positive for match, negative for mismatch.
    /// </summary>
    public void AddPoints(int points)
    {
        if (points > 0)
        {
            comboCount++;
            // Optional: reward combos
            score += points + (comboCount - 1) * 2;
        }
        else
        {
            comboCount = 0;
            score += points;
        }
        
        OnScoreChanged?.Invoke(Score);
    }

    /// <summary>
    /// Reset score and combo count.
    /// </summary>
    public void ResetScore()
    {
        score = 0;
        comboCount = 0;
        OnScoreChanged?.Invoke(Score);
    }

    /// <summary>
    /// Set score directly (used when loading saved progress).
    /// </summary>
    public void SetScore(int value)
    {
        score = value;
        OnScoreChanged?.Invoke(Score);
    }

    /// <summary>
    /// Get current score.
    /// </summary>
    public int Score
    {
        get
        {
            return score;
        }
    }

}