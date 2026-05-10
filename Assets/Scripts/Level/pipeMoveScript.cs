using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipeMoveScript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -28;    

    void Update()
    {
        // 1. Рухаємо об'єкт вліво
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;

        // 2. Перевіряємо, чи вилетів він за межі екрана (deadZone)
        if (transform.position.x < deadZone)
        {
            // Debug.Log("Enemy returned to pool"); // Можна залишити для тесту
            
            // ЗАМІСТЬ Destroy(gameObject); пишемо:
            gameObject.SetActive(false); 
        }
    }
    private void OnEnable()
    {
        // Кожного разу, коли пташка з'являється з пулу, 
        // ми можемо примусово задати їй швидкість, якщо вона раптом застрягла
        if (moveSpeed <= 0) 
        {
            moveSpeed = 5f; 
        }
    }
}