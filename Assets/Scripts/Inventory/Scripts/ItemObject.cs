using UnityEngine;

public enum ItemType
{
    Default,
    KeyItem,
    Weapon,
    Heals,
    Miscellaneous
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;

}
