using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDescription : ScriptableObject
{
    public string ItemName;
    public float Weight;
    public float Value;

    [TextArea(3,5)]
    public string Description;

    public ItemCategory Category;


}
public enum ItemCategory
{

}