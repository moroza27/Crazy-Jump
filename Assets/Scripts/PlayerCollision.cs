using UnityEngine;
using UnityEngine.SceneManagement; // Обов'язкова бібліотека для перезавантаження сцени

public class PlayerCollision : MonoBehaviour
{
    // Ця функція спрацьовує, коли пташка фізично вдаряється об інший колайдер
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Перевіряємо, чи має об'єкт, з яким ми зіткнулися, тег "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            RestartLevel();
        }
    }

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