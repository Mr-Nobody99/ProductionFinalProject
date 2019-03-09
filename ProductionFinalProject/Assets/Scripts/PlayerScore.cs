using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerScore
{
    static int highScore = PlayerPrefs.GetInt("highScore");
    static int currentScore = 0;

    public static int GetHighScore()
    {
        return highScore;
    }
    
    public static void SetHighScore(int x)
    {
        highScore = x;
    }

    public static void UpdateHighScore()
    {
        highScore = currentScore;
        PlayerPrefs.SetInt("highScore", highScore);
    }

    public static int GetCurrentScore()
    {
        return currentScore;
    }

    public static void AddToCurrentScore(int x)
    {
        currentScore += x;
    }
}
