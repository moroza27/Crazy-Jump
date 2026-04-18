using UnityEngine;

public class AlternatingSpawner : MonoBehaviour
{
    public GameObject[] eaglePrefabs;
    public GameObject[] newBirdPrefabs;

    public float spawnRate = 2f;          // Час між появою ворогів
    public float heightOffset = 4f;       // На скільки вгору/вниз може зміщуватися ворог
    private float timer = 0f;

    private bool isEagleTurn = true;

    void Start()
    {
        SpawnEnemy(); // Створюємо першого відразу, щоб не чекати 2 секунди
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // Вибираємо префаб
        GameObject enemyToSpawn = isEagleTurn ?
            eaglePrefabs[Random.Range(0, eaglePrefabs.Length)] :
            newBirdPrefabs[Random.Range(0, newBirdPrefabs.Length)];

        // --- ГЕНЕРУЄМО ВИПАДКОВУ ВИСОТУ ---
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0);

        // Створюємо ворога
        Instantiate(enemyToSpawn, spawnPosition, transform.rotation);

        isEagleTurn = !isEagleTurn; // Зміна черги
    }
}