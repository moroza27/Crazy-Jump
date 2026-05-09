using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public Text progressText;
    public GameObject gameOverScreen; // —юди в Unity перет€гнемо панель Game Over
    public float levelDuration = 60f;
    private float currentTime = 0f;
    private bool isGameActive = true;
    public int playerScore;
    public TextMeshProUGUI scoreText;

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
        if (isGameActive) // ўоб не викликати сто раз≥в посп≥ль
        {
            isGameActive = false;
            gameOverScreen.SetActive(true); // ѕоказуЇмо екран програшу
            Time.timeScale = 0;
            Debug.Log("√ру зупинено! ѕташка вр≥залас€.");
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1; // ќЅќ¬'я« ќ¬ќ повертаЇмо швидк≥сть часу в 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ѕерезапуск
    }

    void WinLevel()
    {
        isGameActive = false;
        Time.timeScale = 0;
        if (progressText != null) progressText.text = "100% - ѕ≈–≈ћќ√ј!";
    }
    public void MoveToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}