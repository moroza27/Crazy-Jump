using UnityEngine;

public class Birdscript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength = 5;
    public LogicScript logic; // Посилання на наш менеджер
    public bool birdIsAlive = true;

    void Start()
    {
        // Пташка просто бере своє тіло. Ніякого пошуку логіки.
        myRigidbody = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        // Пташка може стрибати ТІЛЬКИ якщо вона жива
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            birdIsAlive = false;

            Instantiate(deathEffect, transform.position + Vector3.forward, Quaternion.identity);

            logic.gameOver();

           Destroy(gameObject);
        }
    }
    public GameObject deathEffect;
}