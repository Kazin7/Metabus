using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using Unity.VisualScripting;

public enum UIState
{
    Home,
    Game,
    Score,
}
public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    public GameObject endPanel;
    static UIManager instance;
    public TextMeshProUGUI FlappyBestScoreText;
    public TextMeshProUGUI StackBestScoreText;
    public static UIManager Instance
    {
        get { return instance; }
    }

    UIState currentState = UIState.Home;
    HomeUI homeUI = null;
    GameUI gameUI = null;
    ScoreUI scoreUI = null;

    TheStack thestack = null;
    public Transform playerTransform;
    [SerializeField] private GameObject flappyExplainPanel;
    [SerializeField] private GameObject stackExplainPanel;
    private string currentTargetSceneName;
    private bool isPaused = false;
    private const string FlappyBestScoreKey = "FlappyBestScore";
    private const string StackBestScoreKey = " BestScore";
    private const string StackBestComboKey = " BestCombo";
    private void Awake()
    {
        instance = this;
        thestack = FindObjectOfType<TheStack>();

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI?.Init(this);

        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI?.Init(this);

        scoreUI = GetComponentInChildren<ScoreUI>(true);
        scoreUI?.Init(this);

        if (flappyExplainPanel != null)
            flappyExplainPanel.SetActive(false);

        ChangeState(UIState.Home);
    }
    void Start()
    {
        if (endPanel != null)
            endPanel.gameObject.SetActive(false);
        int flappyBestScore = PlayerPrefs.GetInt(FlappyBestScoreKey, 0);
        int stackBestScore = PlayerPrefs.GetInt(StackBestScoreKey, 0);

        if (FlappyBestScoreText != null)
            FlappyBestScoreText.text = flappyBestScore.ToString();

        if (StackBestScoreText != null)
            StackBestScoreText.text = stackBestScore.ToString();
    }
    public void SetRestart(int currentScore)
    {
        if (endPanel != null)
        {
            endScoreText.text = currentScore.ToString();
            endPanel.gameObject.SetActive(true);
        }
            
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }
    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI?.SetActive(currentState);
        gameUI?.SetActive(currentState);
        scoreUI?.SetActive(currentState);
    }
    public void OnClickStart()
    {
        thestack.Restart();
        ChangeState(UIState.Game);
    }
//     public void OnClickExit()
//     {
// #if UNITY_EDITOR
//         UnityEditor.EditorApplication.isPlaying = false;
// #else
//         Application.Quit();
// #endif
//     }
    public void UpdateScore()
    {
        gameUI.SetUI(thestack.Score, thestack.Combo, thestack.MaxCombo);
    }
    public void SetScoreUI()
    {
        scoreUI.SetUI(thestack.Score, thestack.Combo, thestack.BestScore, thestack.BestCombo);
        ChangeState(UIState.Score);
    }
    public void ShowExplain(string targetSceneName)
    {
        currentTargetSceneName = targetSceneName;
        if (string.IsNullOrEmpty(targetSceneName) || playerTransform == null)
            return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(playerTransform.position);

        if (targetSceneName == "FlappyScene")
        {
            flappyExplainPanel.SetActive(true);
            MovePanelToScreenPosition(flappyExplainPanel, screenPos);
        }
        else if (targetSceneName == "StackScene")
        {
            stackExplainPanel.SetActive(true);
            MovePanelToScreenPosition(stackExplainPanel, screenPos);
        }

        Time.timeScale = 0f;
        isPaused = true;
    }
    private void MovePanelToScreenPosition(GameObject panel, Vector3 screenPosition)
    {
        RectTransform rectTransform = panel.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform,
                screenPosition,
                Camera.main,
                out localPoint);

            rectTransform.localPosition = localPoint;
        }
    }
    public void CloseExplain()
    {
        flappyExplainPanel.SetActive(false);
        stackExplainPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }
    public void StartGame()
    {
        if (string.IsNullOrEmpty(currentTargetSceneName))
            return;

        if (currentTargetSceneName == "FlappyScene")
        {
            flappyExplainPanel.SetActive(false);
        }
        else if (currentTargetSceneName == "StackScene")
        {
            stackExplainPanel.SetActive(false);
        }
        Time.timeScale = 1f;
        isPaused = false;

        UnityEngine.SceneManagement.SceneManager.LoadScene(currentTargetSceneName);
    }
    public void ReturnLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
    }
}
