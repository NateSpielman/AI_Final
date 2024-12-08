using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<ControlPoint> controlPoints;
    [SerializeField] private int controlCount;

    private int segments = 30;
    private float pointDistance = 2f;

    private Vector2[] points;
    private List<Vector2> pointPositions;

    [SerializeField]
    LineRenderer lineRenderer;

    public void CalculatePath()
    {
        if (controlCount > 1)
        {
            InterpolatePath();
            WalkLine(points, pointDistance);
            DrawPath();
        }
    }

    private void InterpolatePath()
    {
        controlCount = controlPoints.Count - 1;
        int totalSegments = (segments + 1) * controlCount;
        points = new Vector2[totalSegments];

        int currentSegment = 0;

        for (int p = 0; p < controlCount; p++)
        {
            float step = 1f / segments;
            for (int i = 0; i <= segments; i++)
            {
                float t = i * step;
                points[currentSegment] = Mathf.Pow(1 - t, 3) * controlPoints[p].anchor.position +
                    3 * Mathf.Pow(1 - t, 2) * t * controlPoints[p].startAngle.position +
                    3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[p + 1].endAngle.position +
                    Mathf.Pow(t, 3) * controlPoints[p + 1].anchor.position;
                currentSegment++;

            }
        }
    }

    private void WalkLine(Vector2[] points, float spacing, float offset = 0f)
    {
        pointPositions = new List<Vector2>();
        spacing = spacing > 0.00001f ? spacing : 0.00001f;

        float distanceNeeded = offset;
        while (distanceNeeded < 0)
        {
            distanceNeeded += spacing;
        }

        Vector2 current = points[0];
        Vector2 next = points[1];
        int i = 1;
        int last = points.Length - 1;
        while (true)
        {
            Vector2 diff = next - current;
            float dist = diff.magnitude;

            if (dist >= distanceNeeded)
            {
                current += diff * (distanceNeeded / dist);
                pointPositions.Add(current);
                distanceNeeded = spacing;
            }
            else if (i != last)
            {
                distanceNeeded -= dist;
                current = next;
                next = points[++i];
            }
            else
            {
                break;
            }
        }
    }

    private void DrawPath()
    {
        lineRenderer.positionCount = pointPositions.Count;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 position = new Vector3(pointPositions[i].x, pointPositions[i].y, 0f);
            lineRenderer.SetPosition(i, position);
        }
        float width = lineRenderer.startWidth;
        lineRenderer.material.mainTextureScale = new Vector2(1f / width, 1.0f);
    }

    public void SetControlPoints(List<ControlPoint> cPoints)
    {
        controlPoints.Clear();
        controlCount = 0;
        foreach (ControlPoint c in cPoints)
        {
            controlPoints.Add(c);
            controlCount++;
        }
    }

    public int GetNumPoints()
    {
        return pointPositions.Count;
    }

    public Vector2 GetPointPos(int index)
    {
        Vector2 point;
        point = pointPositions[index];

        return point;
    }

    public void ShowPath(bool shouldShow)
    {
        lineRenderer.enabled = shouldShow;
    }
}
