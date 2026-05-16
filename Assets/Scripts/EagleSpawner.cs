using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    [Header("Налаштування пулу об'єктів")]
    public string eagleTag = "Eagle 1"; // Тег, який прописаний в ObjectPooler для орла

    [Header("Параметри спавну")]
    public float spawnRate = 3;    // Як часто орел з'являється (кожні 3 сек)
    private float timer = 0;
    public float heightOffset = 3; // Коливання висоти

    void Start()
    {
        // При старті обнуляємо таймер
        timer = 0;
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnEagle();
            timer = 0; // Скидаємо таймер
        }
    }

    void SpawnEagle()
    {
        // Вираховуємо випадкову висоту
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);

        // --- МАГІЯ ПУЛУ ЗАМІСТЬ INSTANTIATE ---
        if (ObjectPooler.Instance != null)
        {
            // Беремо готового орла з пулу
            ObjectPooler.Instance.SpawnFromPool(eagleTag, spawnPosition, transform.rotation);
        }
        else
        {
            // Якщо раптом пулу на сцені немає (для підстраховки)
            Debug.LogWarning("ObjectPooler не знайдено на сцені! Спавню через Instantiate.");
            // Тут використовуємо старий eaglePrefab, але ми його приберемо, щоб не смітити
        }
    }
}