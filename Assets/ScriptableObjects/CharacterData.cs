using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    public int characterId;
    public string characterName;
    public int maxHealth;
    public int maxMP;
    public float jumpPower;
    public string defaultProjectileType;
    public Sprite characterSprite;
    public GameObject characterPrefab;
}
