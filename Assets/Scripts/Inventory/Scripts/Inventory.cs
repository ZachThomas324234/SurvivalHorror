using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<List<ItemSlot>> slots = new List<List<ItemSlot>>();
}

[Serializable]
public class ItemSlot
{
    public Igem item;
    public int quantity;
}

[Serializable]
[CreateAssetMenu(fileName = "Igem", menuName = "Igem")]
public class Igem : ScriptableObject
{

}