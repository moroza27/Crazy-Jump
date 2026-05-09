using UnityEngine;

public class EagleSpawner : MonoBehaviour
{
    public GameObject eaglePrefab; // Сюди покладемо префаб твого орла
    public float spawnRate = 3;    // Як часто вони з'являтимуться (кожні 2 сек)
    private float timer = 0;
    public float heightOffset = 3; // Наскільки високо/низько вони можуть з'являтися

    void Start()
    {

    }

    void Update()
    {
        // Таймер з відео: рахує час, і коли він доходить до spawnRate, створює орла
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnEagle();
            timer = 0; // Скидаємо таймер
        }
    }

    void SpawnEagle()
    {
        // Визначаємо випадкову висоту для появи орла (як труби у відео)
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(eaglePrefab, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}