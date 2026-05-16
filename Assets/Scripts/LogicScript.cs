using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicScript : MonoBehaviour
{
    public LevelConfig config; // РџР•Р Р•РўРЇР“РќР Р¤РђР™Р› РљРћРќР¤Р†Р“РЈ РЎР®Р”Р Р’ Р†РќРЎРџР•РљРўРћР Р†
    public Text progressText;
<<<<<<< HEAD
    public GameObject gameOverScreen;
    public float levelDuration = 60f;
=======
    public GameObject gameOverScreen; 
    private float levelDuration = 60f; // Р‘СѓРґРµ РїРµСЂРµР·Р°РїРёСЃР°РЅРѕ Р· РєРѕРЅС„С–РіСѓ
>>>>>>> origin/dev
    private float currentTime = 0f;
    private bool isGameActive = true;
    public int playerScore;
    public TextMeshProUGUI scoreText; // Срахунок під час гри

    [Header("Game Over UI")]
    public TextMeshProUGUI gameOverScoreText; // Текст для "YOUR SCORE" на екрані смерті
    public TextMeshProUGUI gameOverBestText;  // Текст для "YOUR BEST" на екрані смерті
    private int bestScore; // Змінна для зберігання рекорду

    void Start()
    {
        // Завантажуємо рекорд із пам'яті при запуску
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        // Р†РЅС–С†С–Р°Р»С–Р·СѓС”РјРѕ С‚СЂРёРІР°Р»С–СЃС‚СЊ Р· РєРѕРЅС„С–РіСѓ РїСЂРё СЃС‚Р°СЂС‚С–
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
<<<<<<< HEAD
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
=======
        if (isGameActive) 
        {
            isGameActive = false;
            gameOverScreen.SetActive(true); 
>>>>>>> origin/dev
            Time.timeScale = 0;
            Debug.Log("Р“СЂР° Р·Р°РєС–РЅС‡РµРЅР°! Р Р°С…СѓРЅРѕРє Р·Р±РµСЂРµР¶РµРЅРѕ.");
        }
    }

    public void restartGame()
    {
<<<<<<< HEAD
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
=======
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
>>>>>>> origin/dev
    }

    void WinLevel()
    {
        isGameActive = false;
        Time.timeScale = 0;
        if (progressText != null) progressText.text = "100% - РџРµСЂРµРјРѕРіР°!";
    }

    public void MoveToMenu()
    {
        Time.timeScale = 1; // Додав сюди, щоб меню не "замерзало"
        SceneManager.LoadScene("MainMenu");
    }
}