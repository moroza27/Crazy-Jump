using UnityEngine;
using UnityEngine.SceneManagement; // Обов'язкова бібліотека для перезавантаження сцени

public class PlayerCollision : MonoBehaviour
{
    // Якщо твої вороги налаштовані як Trigger (галочка Is Trigger), то використовуй цю функцію замість верхньої:
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        // Отримуємо ім'я поточної сцени і завантажуємо її наново
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}