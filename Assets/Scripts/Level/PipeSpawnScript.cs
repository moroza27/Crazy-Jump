using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2f;
    public float heightOffset = 7;
    public float startDelay = 2f;

    void Start()
    {
        // Ця одна команда замінює весь таймер. 
        // Вона почекає startDelay (2 секунди) перед першою пташкою,
        // а потім буде викликати spawnPipe кожні spawnRate (2 секунди).
        InvokeRepeating("spawnPipe", startDelay, spawnRate);
    }

    // Зверни увагу: ми ПОВНІСТЮ видалили метод Update і змінну timer, 
    // щоб вони не створювали "подвійних" ворогів.

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        // Створюємо ворога на випадковій висоті
        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
    }
}