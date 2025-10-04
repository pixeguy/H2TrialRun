using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ScenarioObjectPosition
{
    public Item itemType;
    public Vector3 position;
    public Quaternion rotation;
    public GameObject objectPrefab;
}

[Serializable]
public class MappedInstance
{
    public ItemInstance instance;
    public ScenarioObjectPosition position;
}

[CreateAssetMenu(fileName = "Scenario", menuName = "Scriptable Objects/Scenario")]
public class Scenario : ScriptableObject
{
    public string scenarioDescription;
    public Inventory SRInventory;
    public Inventory TFAInventory;
    public List<ScenarioObjectPosition> TFAPositions;
    
    
    public List<MappedInstance> TFAMappedPositions;

    private void OnEnable()
    {
        TFAMappedPositions = GetMappedInstances(TFAInventory, TFAPositions);
    }

    public List<MappedInstance> GetMappedInstances(Inventory inventory, List<ScenarioObjectPosition> positions)
    {
        List<MappedInstance> result = new();
        int totalInstanceCount = inventory.itemInventory.Sum(kvp => kvp.Value.Count);

        // Check 1: Overall count mismatch
        if (totalInstanceCount != positions.Count)
        {
            Debug.LogWarning("Instance count and position count do not match." + name);
            return null;
        }

        // Optional: Validate per-itemType match
        var groupedPositions = positions
            .GroupBy(pos => pos.itemType)
            .ToDictionary(g => g.Key, g => g.Count());

        foreach (var kvp in inventory.itemInventory)
        {
            int instanceCount = kvp.Value.Count;

            // If no entry or count mismatch
            if (!groupedPositions.TryGetValue(kvp.Key, out int expectedCount) || expectedCount != instanceCount)
            {
                Debug.LogWarning($"Item type count mismatch for {kvp.Key.name}");
                return null;
            }
        }

        // Proceed with assignment
        int positionIndex = 0;
        foreach (var kvp in inventory.itemInventory)
        {
            foreach (var instance in kvp.Value)
            {
                var pos = positions[positionIndex++];
                result.Add(new MappedInstance
                {
                    instance = instance,
                    position = pos
                });
            }
        }

        return result;
    }
}