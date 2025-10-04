using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CableLineManager : MonoBehaviour
{
    public static CableLineManager Instance;
    public MouseObserver mouseObserver;
    public GameObject cableLinePrefab;
    public List<DragLineRenderer> Cables;

    private void Start()
    {
        Instance = this;
        var cables = FindObjectsByType<DragLineRenderer>(FindObjectsSortMode.None);
        Cables = cables.ToList();
    }

    public void LeaveActiveCable()
    {
        foreach(var cable in Cables)
        {
            if (!cable.leaveLine)
            {
                cable.leaveLine = true;
                CreateNewCable();
                break;
            }
        }
    }

    public void CreateNewCable()
    {
        var cable = Instantiate(cableLinePrefab, Vector3.zero, Quaternion.identity);
        var cableLineRenderer = cable.GetComponent<DragLineRenderer>();
        cableLineRenderer.mouseObserver = mouseObserver;
        cableLineRenderer.Init();
        Cables.Add(cableLineRenderer);
    }
}
