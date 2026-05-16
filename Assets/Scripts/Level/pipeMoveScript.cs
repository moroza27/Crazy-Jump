using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -28;    
    private LevelConfig config;

    // Переносимо логіку сюди — Awake спрацьовує ТОЧНО при Instantiate, навіть якщо об'єкт вимкнений
    void Awake()
    {
        FindConfig();
    }

    void Update()
    {
        // Рухаємо об'єкт вліво
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        // Перевіряємо deadZone
        if (transform.position.x < deadZone)
        {
            gameObject.SetActive(false); // Повертаємо в пул
        }
    }

    private void OnEnable()
    {
        // Якщо раптом при старті конфіг не знайшовся, шукаємо його знову
        if (config == null) 
        {
            FindConfig();
        }

        // Оновлюємо швидкість та deadZone при кожному ввімкненні з пулу
        if (config != null)
        {
            moveSpeed = config.globalMoveSpeed;
            deadZone = config.deadZone;
        }

        // Підстраховка, якщо швидкість чомусь збилася
        if (moveSpeed <= 0) 
        {
            moveSpeed = 5f; 
        }
    }

    // Окремий зручний метод для пошуку конфігу
    private void FindConfig()
    {
        GameObject logicObj = GameObject.FindGameObjectWithTag("Logic");
        if (logicObj != null)
        {
            LogicScript logicScript = logicObj.GetComponent<LogicScript>();
            if (logicScript != null)
            {
                config = logicScript.config;
            }
        }
    }
}