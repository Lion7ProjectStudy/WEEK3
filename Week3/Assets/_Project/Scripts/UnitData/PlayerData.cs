using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Unit/Player", order = 1)]
public class PlayerData : ScriptableObject
{
    public float level; 
    public float power;
    public float spd;
    public int maxJumpCount;
}
