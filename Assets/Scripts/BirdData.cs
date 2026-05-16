using UnityEngine;

[CreateAssetMenu(fileName = "NewBird", menuName = "Birds/BirdData")]
public class BirdData : ScriptableObject
{
    public string birdName;
    public Sprite birdSprite;
    public int scoreRequired; 
    public string birdId;
}