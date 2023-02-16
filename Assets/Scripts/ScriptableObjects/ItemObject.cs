using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Items/SpawnNewItem")]
public class ItemObject : ScriptableObject
{
    public string itemName;
    public float weight;
    public int value;
    public string description;
    public string category;
}
