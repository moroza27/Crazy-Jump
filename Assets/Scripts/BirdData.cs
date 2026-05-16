using UnityEngine;

[CreateAssetMenu(fileName = "NewBird", menuName = "Birds/BirdData")]
public class BirdData : ScriptableObject
{
    public string birdName;      // Назва пташки для магазину
    public Sprite birdSprite;    // Картинка пташки
    public int scoreRequired;    // Скільки перешкод треба пролетіти (наприклад, 10)
    public string birdId;        // Унікальний ID (наприклад, "bird_blue")
}