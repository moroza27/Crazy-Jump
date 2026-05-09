using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private float width;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x <= startPos.x - width)
        {
            transform.position += new Vector3(width * 2, 0, 0);
        }
    }
}