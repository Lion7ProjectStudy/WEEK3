using UnityEngine;

public enum ItemType
{
    Gold,
    Exp,
    Expendable,
    Mag
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Unit/Item", order = 1)]
public class ItemData : ScriptableObject
{
    public int itemCode;
    public GameObject item;
    public string itemName;
    public int itemCount;
    public ItemType type;
    public float rate;
}
