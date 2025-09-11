using UnityEngine;

public abstract class CutsceneAction : ScriptableObject
{
    public abstract void PlayAction(GameObject target);
    public abstract void EndAction(GameObject target);
}

[System.Serializable]
public class CA_Option
{ 
    public GameObject target;
    public CutsceneAction action;

    public void Play() => action.PlayAction(target);
    public void End() => action.EndAction(target);
}