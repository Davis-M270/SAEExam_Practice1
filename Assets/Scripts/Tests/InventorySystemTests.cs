using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventorySystemTests
{

    [Test]
    public void EmptyInventorySanity()
    {
        Inventory inventory = new Inventory();

        Assert.Zero(inventory.Weight, "Empty inventory has non zero weight");
    }

    [Test]
    public void InventoryHasInfiniteCapacity()
    {
        Inventory inventory = new Inventory(-1, -1);

        ItemDescription testItem = ScriptableObject.CreateInstance<ItemDescription>();
        testItem.ItemName = "TestItem";
        testItem.Weight = float.PositiveInfinity;

        bool success = inventory.TryAdd(testItem);

        Assert.IsTrue(success, "Failed to add infintely weighting item");
    }

    [Test]
    public void WeightIsCalculatedCorrectly()
    {
        Inventory inventory = new Inventory(-1, -1);

        inventory = new Inventory(-1, -1);
        int iterations = 1000;
        float sum = 0;
        ItemDescription[] cacheTemporaryItems = new ItemDescription[iterations];

        for (int i = 0; i < iterations; i++)
        {
            ItemDescription tempItem = ScriptableObject.CreateInstance<ItemDescription>();
            tempItem.ItemName = i.ToString();
            tempItem.Weight = Random.Range(0, 10000);
            cacheTemporaryItems[i] = tempItem;
            sum += tempItem.Weight;

            bool success = inventory.TryAdd(tempItem);
            Assert.IsTrue(success, $"Failed to add item to bag. At iteration {i}");
        }

        Assert.AreEqual(sum, inventory.Weight);

        for (int i = 0; i < iterations; i++)
        {
            ScriptableObject.DestroyImmediate(cacheTemporaryItems[i]);
        }
    }

    [Test]
    public void NegativeWeightIs0()
    {
        Inventory inventory = new Inventory(100, 100);

        ItemDescription testItem = ScriptableObject.CreateInstance<ItemDescription>();
        testItem.ItemName = "TestItem";
        testItem.Weight = -10;

        bool success = inventory.TryAdd(testItem);

        Assert.IsTrue(success, "Failed to add item to bag.");

        Assert.AreEqual(0, inventory.Weight, $"Weights do not match ");


        for (int i = 0; i < 4; i++)
        {
            success = inventory.TryAdd(testItem);
            Assert.IsTrue(success, "Failed to add item to bag.");
        }

        Assert.AreEqual(0, inventory.Weight, $"Weights do not match ");

        ScriptableObject.DestroyImmediate(testItem);
    }

}
