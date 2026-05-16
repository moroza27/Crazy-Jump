using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public Text progressText;
    public GameObject gameOverScreen;
    public float levelDuration = 60f;
    private float currentTime = 0f;
    private bool isGameActive = true;
    public int playerScore;

    // Залишаємо тільки цей один config!
    public LevelConfig config;

    public TextMeshProUGUI scoreText;

    [Header("Game Over UI")]
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverBestText;
    private int bestScore;

    void Start()
    {
        // Завантажуємо рекорд із пам'яті при запуску
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        if (isGameActive)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp((currentTime / levelDuration) * 100f, 0, 100);

            if (progressText != null)
                progressText.text = progress.ToString("F0") + "%";

            if (progress >= 100f) WinLevel();
        }
    }

    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void gameOver()
    {
        if (isGameActive)
        {
            isGameActive = false;

            // 1. ПЕРЕВІРКА ТА ЗБЕРЕЖЕННЯ РЕКОРДУ
            if (playerScore > bestScore)
            {
                bestScore = playerScore;
                PlayerPrefs.SetInt("HighScore", bestScore);
                PlayerPrefs.Save();
            }

            // 2. ВИВЕДЕННЯ ЦИФР НА ЕКРАН GAME OVER
            if (gameOverScoreText != null)
                gameOverScoreText.text = playerScore.ToString();

            if (gameOverBestText != null)
                gameOverBestText.text = bestScore.ToString();

            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Гру зупинено! Пташка врізалася.");
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void WinLevel()
    {
        isGameActive = false;
        Time.timeScale = 0;
        if (progressText != null) progressText.text = "100% - ПЕРЕМОГА!";
    }

    public void MoveToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}