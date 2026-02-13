using UnityEngine;

public enum ItemType
{
    Gold,
    Exp,
    Expendable,
    Buff
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Unit/Item", order = 1)]
public class ItemData : ScriptableObject
{
    public GameObject item;
    public string itemName;
    public ItemType type;
    public float rate;
}
