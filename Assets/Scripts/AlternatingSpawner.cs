using UnityEngine;

public class AlternatingSpawner : MonoBehaviour
{
    public string eagleTag = "Eagle";
    public string birdTag = "Bird";

    [Header("Частота появи (менше = частіше)")]
    public float spawnRate = 1.5f; 
    private float timer = 0f;

    [Header("Налаштування висоти")]
    public float heightOffset = 5f;

    [Header("Налаштування пар")]
    [Range(0, 100)]
    public float doubleSpawnChance = 75f; // Шанс у %, що вилетять двоє

    private bool isEagleTurn = true;

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnLogic();
            timer = 0f;
        }
    }

    void SpawnLogic()
    {
        float roll = Random.Range(0f, 100f);

        if (roll <= doubleSpawnChance)
        {
            // СПАВНИМО ДВОХ ОДНОЧАСНО
            // Перша пташка (зверху)
            SpawnSingleEnemy(Random.Range(transform.position.y + 1f, transform.position.y + heightOffset));
            // Друга пташка (знизу)
            SpawnSingleEnemy(Random.Range(transform.position.y - heightOffset, transform.position.y - 1f));
        }
        else
        {
            // СПАВНИМО ОДНУ (як зазвичай)
            SpawnSingleEnemy(Random.Range(transform.position.y - heightOffset, transform.position.y + heightOffset));
        }
    }

    // Окремий метод для створення однієї одиниці
    void SpawnSingleEnemy(float yPosition)
    {
        string tagToSpawn = isEagleTurn ? eagleTag : birdTag;
        Vector3 spawnPosition = new Vector3(transform.position.x, yPosition, 0);

        if (ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.SpawnFromPool(tagToSpawn, spawnPosition, transform.rotation);
        }

        // Міняємо чергу після кожного створення
        isEagleTurn = !isEagleTurn; 
    }
}