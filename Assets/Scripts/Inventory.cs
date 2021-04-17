using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory
{
    public List<ItemDescription> content;

    private int size;
    private float maxWeight;
    private float currentWeight;

    public float Weight { get => currentWeight; }
    public event System.Action<InventoryChangeDescription> InventoryChanged;

    public Inventory(int size = -1, float maxWeight = -1)
    {
        if (size > 0)
            content = new List<ItemDescription>(size);
        else
            content = new List<ItemDescription>();

        this.size = size;
        this.maxWeight = maxWeight;
    }

    public bool TryAdd(ItemDescription item)
    {
        //null failure
        if (item == null)
            return false;


        //overweight failure, skip if maxWeight <=0
        if (maxWeight > 0 && currentWeight + item.Weight > maxWeight)
            return false;

        content.Add(item);
        UpdateWeight();

        InventoryChangeDescription desc = new InventoryChangeDescription()
        {
            Inventory = this,
            ElementModified = item,
            Type = InventoryChangeDescription.ChangeType.Addition
        };
        InventoryChanged?.Invoke(desc);

        return true;

    }

    public void UpdateWeight()
    {
        currentWeight = content.Sum((t) => Mathf.Max(0, t.Weight));
    }
}

public struct InventoryChangeDescription
{
    public Inventory Inventory;
    public ItemDescription ElementModified;
    public ChangeType Type;

    public enum ChangeType
    {
        Addition,
        Removal
    }
}