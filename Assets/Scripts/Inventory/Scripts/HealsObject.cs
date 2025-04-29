using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Object", menuName = "Inventory System/Items/Heals")]

public class HealsObject : ItemObject
{
    public int restoreHealthValue;
    public void Awake()
    {
        type = ItemType.Heals;
    }
}
