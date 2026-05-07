using UnityEngine;

public class Birdscript : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;
    public Rigidbody2D myRigidbody;
    public float flapStrength = 5;
    public LogicScript logic;
    public bool birdIsAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
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



            if (deathParticles != null)
            {
                deathParticles.transform.position = transform.position+Vector3.forward*-5;
                var main = deathParticles.main;
                main.useUnscaledTime = true;
                deathParticles.Play();
                Destroy(deathParticles.gameObject, main.duration + main.startLifetime.constantMax + 0.25f);
            }
            else
            {
                Debug.LogWarning("Death particles are not assigned.");
            }

            logic.gameOver();

            Destroy(gameObject);
        }
    }
}