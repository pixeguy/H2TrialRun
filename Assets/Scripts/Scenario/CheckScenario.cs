using System.Collections.Generic;
using UnityEngine;

public class CheckScenario : MonoBehaviour
{
    private Scenario scenario;
    public Inventory inventory;

    public void SRCheckScenario()
    {
        scenario = ScenarioPicker.instance.currentScenario;
        var a = scenario.SRInventory.itemInventory;
        var b = inventory.itemInventory;

        if (a.Count != b.Count) {
            ErrorManager.instance.Init("You do not have the correct items!"); return;
        }

        foreach (var kvA in a)
        {
            var match = b.Find(kvB => kvB.Key.itemName == kvA.Key.itemName);
            if (match.Key == null) { ErrorManager.instance.Init("Not all the objects have been placed!"); return; }

            if (kvA.Value.Count != match.Value.Count)
            { ErrorManager.instance.Init("Not all the objects have been placed!"); return; }

        }

        return;
    }

    public void TFACheckScenario()
    {
        var slotList = new ActualItemCover[] { };
        slotList = FindObjectsByType<ActualItemCover>(FindObjectsSortMode.None);

        foreach (var slot in slotList)
        {
            if(slot.occupied == false)
            {
                ErrorManager.instance.Init("Not all the objects have been placed!"); return;
            }
        }
        Debug.Log("Scenario check passed!");
        return;
    }
}
