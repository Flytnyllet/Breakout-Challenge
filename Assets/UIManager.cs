using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public static float highScore = 0f;

    [SerializeField] private Player player;
    [SerializeField] private Ball ball;

    public float score = 0f;
    public bool isGameOver = false;
    public bool isGameWon = false;

    private List<GameObject> playerLives;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject healthPanel;
    [SerializeField] private GameObject healthPrefab;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreTextWin;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText2;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        playerLives = new List<GameObject>();
    }

    private void Start()
    {
        score = 0f;
        highScore = PlayerPrefs.GetFloat(highScoreText.text, highScore);
        highScoreText.text = highScore.ToString();
        highScoreText2.text = highScore.ToString();

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

        if (isGameWon)
        {
            victoryPanel.SetActive(true);
            scoreTextWin.text = "Score: " + score.ToString();
            if (score > highScore)
            {
                highScore = score;
                highScoreText.text = "High Score: " + highScore.ToString();

                PlayerPrefs.SetFloat(highScoreText.text, highScore);
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
            highScoreText2.text = "High Score: " + highScore.ToString();
        }
        else if (!isGameOver)
            gameOverPanel.SetActive(false);

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
