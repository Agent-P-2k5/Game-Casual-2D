using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
    }

    public void DrawLine(List<Vector2Int> path)
    {
        if (path == null || path.Count == 0) return;

        lineRenderer.positionCount = path.Count;

        for (int i = 0; i < path.Count; i++)
        {
            // convert tọa độ lưới -> world (nếu bạn có cách convert riêng thì sửa ở đây)
            Vector3 pos = new Vector3(path[i].x, path[i].y, 0);
            lineRenderer.SetPosition(i, pos);
        }

        CancelInvoke(nameof(ClearLine));
        Invoke(nameof(ClearLine), 0.5f); // Xóa sau 0.5s
    }

    void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }
}
