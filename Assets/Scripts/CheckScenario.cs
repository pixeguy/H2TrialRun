using System.Collections.Generic;
using UnityEngine;

public class CheckScenario : MonoBehaviour
{
    private Scenario scenario;
    public Inventory inventory;

    public void SRCheckScenario()
    {
        scenario = ScenarioPicker.instance.currentScenario;
        var items = scenario.items;
        var itemInventory = inventory.itemInventory;
        // 1. Count items in the array
        Dictionary<Item, int> itemArrayCounts = new();
        foreach (var item in items)
        {
            if (!itemArrayCounts.ContainsKey(item))
                itemArrayCounts[item] = 0;
            itemArrayCounts[item]++;
        }

        // 2. Count items in the inventory list
        Dictionary<Item, int> inventoryCounts = new();
        foreach (var kvp in itemInventory)
        {
            var item = kvp.Key;
            int count = kvp.Value?.Count ?? 0;

            if (!inventoryCounts.ContainsKey(item))
                inventoryCounts[item] = 0;
            inventoryCounts[item] += count;
        }

        // 3. Compare counts
        if (itemArrayCounts.Count != inventoryCounts.Count)
        {
            Debug.Log("Scenario check failed: Different number of unique items.");
            return;
        }
        foreach (var pair in itemArrayCounts)
        {
            if (!inventoryCounts.TryGetValue(pair.Key, out int invCount))
            {
                Debug.Log("Scenario check failed: Different number of unique items.");
                return;
            }

            if (invCount != pair.Value)
            {
                Debug.Log("Scenario check failed: Different number of unique items.");
                return;
            }
        }

        Debug.Log("Scenario check passed!");
        return;
    }
}
