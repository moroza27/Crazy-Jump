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
                obj.SetActive(false); // Спочатку всі об'єкти вимкнені
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if (!poolDictionary.ContainsKey(tag)) return null;

        // --- ЗАХИСТ ВІД ЗАВИСАННЯ (ПОРОЖНЬОЇ ЧЕРГИ) ---
        if (poolDictionary[tag].Count == 0)
        {
            // Варіант А: Якщо черга пуста, динамічно створюємо ОДИН новий об'єкт, щоб гра не вилітала
            Pool currentPool = pools.Find(p => p.tag == tag);
            if (currentPool != null)
            {
                Debug.LogWarning($"Пул для тегу {tag} закінчився! Створюємо додатковий об'єкт.");
                GameObject newObj = Instantiate(currentPool.prefab);
                newObj.SetActive(true);
                newObj.transform.position = position;
                newObj.transform.rotation = rotation;
                return newObj; // Повертаємо його відразу в гру
            }
            return null;
        }
        // ------------------------------------------------

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true); // "Витягуємо з шафи"
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn); // Кладемо в кінець черги
        return objectToSpawn;
    }

    // Новий корисний метод: повернення об'єкта в пул вручну (коли стовпчик вилітає за екран)
    public void ReturnToPool(string tag, GameObject obj)
    {
        obj.SetActive(false);
        if (poolDictionary.ContainsKey(tag) && !poolDictionary[tag].Contains(obj))
        {
            poolDictionary[tag].Enqueue(obj);
        }
    }
}