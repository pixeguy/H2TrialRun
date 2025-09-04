using System.Text.RegularExpressions;
using UnityEngine;

public class ToggleUIGroups : MonoBehaviour
{
    CanvasGroup[] canvasGroups;
    TapToInteractObject[] tapToInteractObjects;
    CanvasGroup currentGroup;

    public void DisableAllOtherGroups()
    {
        DisableAllOtherGroups(null);
    }

    public void DisableAllOtherGroups(GameObject specificGroup)
    {
        canvasGroups = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None);
        if (specificGroup != null)
        {
            var specGroup = specificGroup.GetComponent<CanvasGroup>();
            currentGroup = specGroup;
        }
        else
        {
            currentGroup = GetComponent<CanvasGroup>();
        }
        foreach (CanvasGroup group in canvasGroups)
        {
            if (group != currentGroup)
            {
                group.interactable = false;
                group.blocksRaycasts = false;
                group.alpha = 0.5f; // Optional: visually indicate it's disabled
            }
            else
            {
                group.interactable = true;
                group.blocksRaycasts = true;
                group.alpha = 1f; // Optional: visually indicate it's enabled
            }
        }
        tapToInteractObjects = FindObjectsByType<TapToInteractObject>(FindObjectsSortMode.None);
        foreach(TapToInteractObject obj in tapToInteractObjects)
        {
            obj.canInteract = false;
        }
    }

    public void ToggleGroups()
    {
        ToggleGroups(null);
    }

    public void ToggleGroups(GameObject specificGroup = null)
    {
        canvasGroups = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None);

        if (specificGroup != null)
        {
            currentGroup = specificGroup.GetComponent<CanvasGroup>();
        }
        else
        {
            currentGroup = GetComponent<CanvasGroup>();
        }

        // Check if any other group is enabled
        bool isAnyOtherEnabled = false;
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            if (canvasGroups[i] == currentGroup) continue;

            if (canvasGroups[i].interactable)
            {
                isAnyOtherEnabled = true;
                break;
            }
        }

        bool newState = !isAnyOtherEnabled;

        // Correctly update current group
        if (newState == false)
        {
            currentGroup.interactable = !newState;
            currentGroup.blocksRaycasts = !newState;
            currentGroup.alpha = !newState ? 1f : 0.5f;
        }
        else {             currentGroup.interactable = true;
            currentGroup.blocksRaycasts = true;
            currentGroup.alpha = 1f;
        }
        // Update all other groups
        foreach (CanvasGroup group in canvasGroups)
            {
                if (group == currentGroup) continue;

                group.interactable = newState;
                group.blocksRaycasts = newState;
                group.alpha = (newState) ? 1f : 0.5f;
            }

        // Toggle all interactable objects
        tapToInteractObjects = FindObjectsByType<TapToInteractObject>(FindObjectsSortMode.None);
        foreach (TapToInteractObject obj in tapToInteractObjects)
        {
            obj.canInteract = newState;
        }
    }

    public void EnableAllGroups()
    {
        canvasGroups = FindObjectsByType<CanvasGroup>(FindObjectsSortMode.None);
        foreach (CanvasGroup group in canvasGroups)
        {
            group.interactable = true;
            group.blocksRaycasts = true;
            group.alpha = 1f; // Optional: visually indicate it's enabled
        }
        tapToInteractObjects = FindObjectsByType<TapToInteractObject>(FindObjectsSortMode.None);
        foreach(TapToInteractObject obj in tapToInteractObjects)
        {
            obj.canInteract = true;
        }
    }
}
