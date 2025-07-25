using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;
    public static GameManager Instance { get { return gameManager; } }
    private int currentScore = 0;
    private int maxScore = 0;
    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }
    private const string FlappyBestScoreKey = "FlappyBestScore";
    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        uiManager.UpdateScore(0);
    }
    public void GameOver()
    {
        PlayerPrefs.SetInt(FlappyBestScoreKey, currentScore);
        uiManager.SetRestart(currentScore);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
    }
}
