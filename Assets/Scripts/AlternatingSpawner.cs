using UnityEngine;

public class AlternatingSpawner : MonoBehaviour
{
    public string eagleTag = "Eagle";
    public string birdTag = "Bird";
    
    private LevelConfig config;
    private float timer = 0f;
    private bool isEagleTurn = true;

    void Start()
    {
        GameObject logicObj = GameObject.FindGameObjectWithTag("Logic");
        if (logicObj != null)
        {
            config = logicObj.GetComponent<LogicScript>().config;
        }
    }

    void Update()
    {
        if (config == null) return;

        if (timer < config.spawnRate)
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

        if (roll <= config.doubleSpawnChance)
        {
            // СПАВНИМО ДВОХ
            SpawnSingleEnemy(Random.Range(transform.position.y + 1f, transform.position.y + config.heightOffset));
            SpawnSingleEnemy(Random.Range(transform.position.y - config.heightOffset, transform.position.y - 1f));
        }
        else
        {
            // СПАВНИМО ОДНУ
            SpawnSingleEnemy(Random.Range(transform.position.y - config.heightOffset, transform.position.y + config.heightOffset));
        }
    }

    void SpawnSingleEnemy(float yPosition)
    {
        string tagToSpawn = isEagleTurn ? eagleTag : birdTag;
        Vector3 spawnPosition = new Vector3(transform.position.x, yPosition, 0);

        if (ObjectPooler.Instance != null)
        {
            ObjectPooler.Instance.SpawnFromPool(tagToSpawn, spawnPosition, transform.rotation);
        }

        isEagleTurn = !isEagleTurn; 
    }
}