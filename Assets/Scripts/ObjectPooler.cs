using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; // Дозволяє іншим скриптам звертатися до пулу

    [System.Serializable]
    public class Pool {
        public string tag;          // Назва (наприклад, "Eagle")
        public GameObject prefab;   // Що саме копіюємо
        public int size;            // Скільки штук створити при старті
    }

    public List<Pool> pools; 
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake() { Instance = this; }

    void Start() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false); // Спочатку всі пташки вимкнені
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!poolDictionary.ContainsKey(tag)) return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true); // "Витягуємо з шафи"
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn); // Кладемо в кінець черги
        return objectToSpawn;
    }
}