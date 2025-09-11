using UnityEngine;

[CreateAssetMenu(fileName = "CA_ActivateObject", menuName = "Scriptable Objects/CA_ActivateObject")]
public class CA_ActivateObject : CutsceneAction
{
    public bool activate = true;

    public override void PlayAction(GameObject target) => target?.SetActive(activate);
    public override void EndAction(GameObject target) => target?.SetActive(!activate);
}
