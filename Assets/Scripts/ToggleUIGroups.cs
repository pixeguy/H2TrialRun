using System.Text.RegularExpressions;
using UnityEngine;

public class ToggleUIGroups : MonoBehaviour
{
    CanvasGroup[] canvasGroups;
    TapToInteractObject[] tapToInteractObjects;
    TapToInteractObject[] currentTapToInteractObjects;
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
            currentTapToInteractObjects = specificGroup.GetComponentsInChildren<TapToInteractObject>(true);
            currentGroup = specGroup;
        }
        else
        {
            currentGroup = GetComponent<CanvasGroup>();
            currentTapToInteractObjects = GetComponentsInChildren<TapToInteractObject>(true);
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
        foreach(TapToInteractObject obj in currentTapToInteractObjects)
        { obj.canInteract = true; }
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
            currentTapToInteractObjects = specificGroup.GetComponentsInChildren<TapToInteractObject>(true);
        }
        else
        {
            currentGroup = GetComponent<CanvasGroup>();
            currentTapToInteractObjects = GetComponentsInChildren<TapToInteractObject>(true);
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
            foreach (TapToInteractObject obj in currentTapToInteractObjects)
            { obj.canInteract = !newState; }
        }
        else {             
            currentGroup.interactable = true;
            currentGroup.blocksRaycasts = true;
            currentGroup.alpha = 1f;
            foreach (TapToInteractObject obj in currentTapToInteractObjects)
            { obj.canInteract = true; }
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
