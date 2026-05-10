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
        
        // Шукаємо LogicScript на сцені
        GameObject logicObject = GameObject.FindGameObjectWithTag("Logic");
        if (logicObject != null)
        {
            logic = logicObject.GetComponent<LogicScript>();
        }
        else
        {
            Debug.LogError("ОБ'ЄКТ З ТЕГОМ 'Logic' НЕ ЗНАЙДЕНО!");
        }
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && birdIsAlive)
        {
            myRigidbody.velocity = Vector2.up * flapStrength;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && birdIsAlive)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) && birdIsAlive)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die() викликано!"); // Перевірка в консолі
        birdIsAlive = false;

        // Викликаємо Game Over
        if (logic != null)
        {
            logic.gameOver();
        }
        else
        {
            Debug.LogError("Скрипт Logic не призначено!");
        }

        // Ефекти
        if (deathParticles != null)
        {
            deathParticles.transform.position = transform.position + Vector3.forward * -5;
            deathParticles.transform.parent = null; 
            var main = deathParticles.main;
            main.useUnscaledTime = true;
            deathParticles.Play();
            Destroy(deathParticles.gameObject, 2f);
        }

        // ПОВНЕ ВИМКНЕННЯ
        // Вимикаємо рендерер (щоб пташка зникла візуально)
        GetComponent<SpriteRenderer>().enabled = false;
        // Вимикаємо фізику (щоб вона не падала)
        myRigidbody.simulated = false;
        // Вимикаємо сам об'єкт
        gameObject.SetActive(false); 
    }
}