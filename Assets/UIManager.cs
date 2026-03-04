using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public static float highScore = 0f;

    private InputAction pauseAction;

    [SerializeField] private Player player;
    [SerializeField] private Ball ball;

    public int scoreMultiplier;
    public float score = 0f;
    public bool isGameOver = false;
    public bool isGameWon = false;
    public bool isGamePaused = false;

    private List<GameObject> playerLives;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject pausedPanel;
    [SerializeField] private GameObject healthPanel;
    [SerializeField] private GameObject healthPrefab;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreTextWin;
    [SerializeField] private TextMeshProUGUI highScoreTextWin;
    [SerializeField] private TextMeshProUGUI highScoreTextGameOver;
    [SerializeField] private TextMeshProUGUI comboText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        playerLives = new List<GameObject>();
        pauseAction = InputSystem.actions["Pause"];
    }

    private void Start()
    {
        score = 0f;
        highScore = PlayerPrefs.GetFloat(highScoreTextWin.text, highScore);
        highScoreTextWin.text = highScore.ToString();
        highScoreTextGameOver.text = highScore.ToString();

        for (int i = 0; i < player.playerLives; i++)
        {
            GameObject newHeart = Instantiate(healthPrefab);
            newHeart.transform.SetParent(healthPanel.transform, false);
            playerLives.Add(newHeart);
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        comboText.text = "Combo " + scoreMultiplier.ToString() + "x";

        if (isGameWon)
        {
            victoryPanel.SetActive(true);
            scoreTextWin.text = "Score: " + score.ToString();
            if (score > highScore)
            {
                highScore = score;
                highScoreTextWin.text = "High Score: " + highScore.ToString();

                PlayerPrefs.SetFloat(highScoreTextWin.text, highScore);
                PlayerPrefs.Save();
            }
        }
        else if (!isGameWon)
        {
            victoryPanel.SetActive(false);
        }

        if (isGameOver)
        {
            gameOverPanel.SetActive(true);
            highScoreTextGameOver.text = "High Score: " + highScore.ToString();
        }
        else if (!isGameOver)
            gameOverPanel.SetActive(false);

        if (pauseAction.triggered && !isGameOver && !isGameWon)
        {
            pausedPanel.SetActive(!pausedPanel.activeSelf);
            Time.timeScale = pausedPanel.activeSelf ? 0.0f : 1.0f;
        }

        if (ball.isLifeLost)
        {
            ReduceLife();
            ball.isLifeLost = false;
        }

    }

    private void ReduceLife()
    {
        GameObject lastLife = playerLives.LastOrDefault();
        Destroy(lastLife);
        playerLives.Remove(lastLife);
    }
}
