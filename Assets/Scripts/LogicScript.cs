using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public LevelConfig config; // ПЕРЕТЯГНИ ФАЙЛ КОНФІГУ СЮДИ В ІНСПЕКТОРІ
    public Text progressText;
    public GameObject gameOverScreen; 
    private float levelDuration = 60f; // Буде перезаписано з конфігу
    private float currentTime = 0f;
    private bool isGameActive = true;
    public int playerScore;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Ініціалізуємо тривалість з конфігу при старті
        if (config != null)
        {
            levelDuration = config.levelDuration;
        }
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
            gameOverScreen.SetActive(true); 
            Time.timeScale = 0;
            Debug.Log("Гра закінчена! Рахунок збережено.");
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
        if (progressText != null) progressText.text = "100% - Перемога!";
    }

    public void MoveToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}