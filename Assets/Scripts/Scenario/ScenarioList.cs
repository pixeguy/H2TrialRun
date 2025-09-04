using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class ScenarioList : MonoBehaviour
{
    public List<Scenario> scenarios;
    private void Start()
    {
        Addressables.LoadAssetsAsync<Scenario>("Scenarios", item =>
        {
            scenarios.Add(item);
        });
        Debug.Log("Loaded " + scenarios.Count + " scenarios.");
    }
}
