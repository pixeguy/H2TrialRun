using System;
using UnityEngine;

public class SetUpTFA : MonoBehaviour
{
    private Scenario scenario;
    private bool called = false;
    public Action<GameObject> onCreateDropSlot;
    private void Update()
    {
        if (!called && ScenarioPicker.instance.currentScenario != null)
        {
            scenario = ScenarioPicker.instance.currentScenario;
            SetUp();
            called = true;
        }
    }

    public void SetUp()
    {
        if (scenario == null || scenario.TFAPositions == null)
        {
            Debug.LogWarning("Scenario or positions not assigned.");
            return;
        }

        foreach (var positionData in scenario.TFAPositions)
        {
            if (positionData.objectPrefab == null)
            {
                Debug.LogWarning($"No prefab assigned for item: {positionData.itemType?.name}");
                continue;
            }

            GameObject obj = Instantiate(
                positionData.objectPrefab,
                positionData.position,
                positionData.rotation
            );

            var objSprite = obj.GetComponent<SpriteRenderer>();
            var objCover = obj.GetComponentInChildren<SpriteRenderer>();
            var objDropper = obj.GetComponent<DropGameObject>();
            objSprite.sprite = positionData.itemType.Sprite;
            objCover.sprite = positionData.itemType.Sprite;
            objDropper.itemType = positionData.itemType;

            onCreateDropSlot?.Invoke(obj);

            obj.name = $"{positionData.itemType.name}_Instance";
        }
    }
}
