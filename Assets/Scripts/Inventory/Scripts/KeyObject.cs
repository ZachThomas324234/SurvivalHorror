using UnityEngine;

[CreateAssetMenu(fileName = "New Key Object", menuName = "Inventory System/Items/Key")]

public class KeyObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.KeyItem;
    } 
}
