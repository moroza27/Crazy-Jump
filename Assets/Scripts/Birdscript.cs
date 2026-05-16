using UnityEngine;

public class Birdscript : MonoBehaviour
{
    [SerializeField] private ParticleSystem deathParticles;
    public Rigidbody2D myRigidbody;
    public float flapStrength = 5;
    public LogicScript logic;
    public bool birdIsAlive = true;

    [Header("Магазин пташок у грі")]
    public SpriteRenderer birdSpriteRenderer; // Посилання на відображення картинки пташки
    public BirdData[] allBirds;               // Сюди скинемо ті ж самі конфіги пташок, що й в магазині

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        
        // Автоматично беремо SpriteRenderer, якщо забули перетягнути в інспекторі
        if (birdSpriteRenderer == null)
        {
            birdSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        // --- ЛОГІКА МАГАЗИНУ: Зміна вигляду пташки ---
        // Отримуємо ID вибраної в магазині пташки. Якщо ще нічого не вибирали — береться "default"
        string selectedBirdId = PlayerPrefs.GetString("SelectedBird", "default");

        // Шукаємо пташку з таким ID серед усіх наших конфігів
        if (allBirds != null && allBirds.Length > 0)
        {
            for (int i = 0; i < allBirds.Length; i++)
            {
                if (allBirds[i] != null && allBirds[i].birdId == selectedBirdId)
                {
                    // Знайшли! Замінюємо спрайт ігрового персонажа на картинку цієї пташки
                    birdSpriteRenderer.sprite = allBirds[i].birdSprite;
                    break;
                }
            }
        }
        // ----------------------------------------------

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
        if (birdSpriteRenderer != null)
        {
            birdSpriteRenderer.enabled = false;
        }
        
        // Вимикаємо фізику (щоб вона не падала)
        myRigidbody.simulated = false;
        // Вимикаємо сам об'єкт
        //gameObject.SetActive(false); 
    }
}