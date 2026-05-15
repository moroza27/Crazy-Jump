using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Configs/Level Configuration")]
public class LevelConfig : ScriptableObject
{
    [Header("Налаштування руху")]
    public float globalMoveSpeed = 5f;
    public float deadZone = -28f;

    [Header("Налаштування спавну")]
    public float spawnRate = 2f;
    public float heightOffset = 7f;
    public float startDelay = 2f;

    [Header("Складність")]
    public float levelDuration = 60f;
    [Range(0, 100)]
    public float doubleSpawnChance = 75f;
}