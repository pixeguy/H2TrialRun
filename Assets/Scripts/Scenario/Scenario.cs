using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "Scriptable Objects/Scenario")]
public class Scenario : ScriptableObject
{
    public string scenarioDescription;
    public Item[] items;
}
