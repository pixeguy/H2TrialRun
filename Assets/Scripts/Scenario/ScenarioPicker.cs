using UnityEngine;

public class ScenarioPicker : MonoBehaviour
{
    public ScenarioList scenarioList;
    public Scenario currentScenario;

    public static ScenarioPicker instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if (scenarioList.scenarios.Count != 0 && instance.currentScenario == null)
        {
            instance.currentScenario = scenarioList.scenarios[Random.Range(0, scenarioList.scenarios.Count)];
            Debug.Log("SDF");
        }
    }
}
