using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class DragLineRenderer : MonoBehaviour
{
    public MouseObserver mouseObserver;
    private LineRenderer lineRenderer;
    private Camera cam;

    public Material lineMaterial;     // Must use a texture with wrap mode: Repeat
    public float lineWidth = 0.1f;
    public float tilingFactor = 1f;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private bool isDragging = false;

    private Transform objRef;

    public bool leaveLine;

    private void OnEnable()
    {
        if (mouseObserver != null)
        {
            mouseObserver.onMouseClickEvent += OnClick;
            mouseObserver.onMouseReleaseEvent += OnRelease;
        }
    }

    public void Init()
    {
        mouseObserver.onMouseClickEvent += OnClick;
        mouseObserver.onMouseReleaseEvent += OnRelease;
    }

    private void OnDisable()
    {
        mouseObserver.onMouseClickEvent -= OnClick;
        mouseObserver.onMouseReleaseEvent -= OnRelease;
    }

    void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.textureMode = LineTextureMode.Tile;
    }

    private void OnClick(Vector3 mousePos)
    {
        if (leaveLine) { return; }
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);

        foreach (var hit in hits)
        {
            var sprite = hit.collider.GetComponent<DragCables>();
            if (sprite != null)
            {
                isDragging = true;
                startPoint = sprite.transform.position;
                objRef = sprite.transform;
                break;
            }
        }
    }

    private void OnRelease(Vector3 mousePos)
    {
        if (leaveLine) { return; }
        isDragging = false;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (isDragging || leaveLine)
        {
            endPoint = objRef.position;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);

            // Update texture tiling based on length
            float distance = Vector3.Distance(startPoint, endPoint);
            lineRenderer.material.mainTextureScale = new Vector2(distance * tilingFactor, 1);
        }
    }
}
