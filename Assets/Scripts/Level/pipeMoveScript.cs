using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -28;    
    private LevelConfig config;

    void Start()
    {
        // Отримуємо конфіг через LogicScript
        GameObject logicObj = GameObject.FindGameObjectWithTag("Logic");
        if (logicObj != null)
        {
            config = logicObj.GetComponent<LogicScript>().config;
            if (config != null)
            {
                moveSpeed = config.globalMoveSpeed;
                deadZone = config.deadZone;
            }
        }
    }

    void Update()
    {
        // Рухаємо об'єкт вліво
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        // Перевіряємо deadZone
        if (transform.position.x < deadZone)
        {
            gameObject.SetActive(false); 
        }
    }

    private void OnEnable()
    {
        // Оновлюємо швидкість при кожному ввімкненні з пулу
        if (config != null) moveSpeed = config.globalMoveSpeed;

        if (moveSpeed <= 0) 
        {
            moveSpeed = 5f; 
        }
    }
}