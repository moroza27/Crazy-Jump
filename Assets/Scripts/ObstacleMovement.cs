using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f; // Швидкість орла
    private float leftEdge;

    void Start()
    {
        // Визначаємо лівий край екрана, щоб знати, коли видаляти орла
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    void Update()
    {
        // Орел постійно летить вліво
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Якщо орел залетів далеко за лівий край - знищуємо його
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}