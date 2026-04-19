using UnityEngine;

public class Birdscript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength = 10;

    void Start()
    {
        // Пташка просто бере своє тіло. Ніякого пошуку логіки.
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Стрибок
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }
}