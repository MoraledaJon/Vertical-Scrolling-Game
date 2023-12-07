using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public Camera mainCamera;
    public float maxLineLength = 10f;
    public RectTransform panelRect;

    private LineRenderer currentLine;
    private List<Vector2> linePoints;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Check if the touch position is within the panel boundaries
                    if (IsInsidePanel(touch.position))
                    {
                        StartDrawing(touch.position);
                    }
                    break;

                case TouchPhase.Moved:
                    ContinueDrawing(touch.position);
                    break;

                case TouchPhase.Ended:
                    StopDrawing();
                    break;
            }
        }
    }

    bool IsInsidePanel(Vector2 touchPos)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRect, touchPos, mainCamera, out Vector2 localPoint);
        return panelRect.rect.Contains(localPoint);
    }

    void StartDrawing(Vector2 touchPos)
    {
        GameObject lineGO = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        currentLine = lineGO.GetComponent<LineRenderer>();

        // Add EdgeCollider2D
        EdgeCollider2D edgeCollider = lineGO.AddComponent<EdgeCollider2D>();

        linePoints = new List<Vector2>();
        linePoints.Add(mainCamera.ScreenToWorldPoint(touchPos));
        currentLine.positionCount = 1;
        currentLine.SetPosition(0, linePoints[0]);

        // Add the first point to the EdgeCollider2D
        edgeCollider.points = new Vector2[] { mainCamera.ScreenToWorldPoint(touchPos) };
    }

    void ContinueDrawing(Vector2 touchPos)
    {
        if (currentLine != null)
        {
            Vector2 point = mainCamera.ScreenToWorldPoint(touchPos);

            // Check if the line length exceeds the maximum length
            if (GetLineLength(linePoints) + Vector2.Distance(point, linePoints[linePoints.Count - 1]) > maxLineLength)
            {
                StopDrawing();
                return;
            }

            // Ensure that points are not added too close to each other
            if (Vector2.Distance(point, linePoints[linePoints.Count - 1]) > 0.1f)
            {
                // Check if the new point is inside the panel boundaries
                if (IsInsidePanel(touchPos))
                {
                    linePoints.Add(point);
                    currentLine.positionCount++;
                    currentLine.SetPosition(currentLine.positionCount - 1, point);

                    // Update the points of the EdgeCollider2D
                    currentLine.GetComponent<EdgeCollider2D>().points = linePoints.ToArray();
                }
                else
                {
                    // Stop drawing if the new point is outside the panel
                    StopDrawing();
                }
            }
        }
    }

    void StopDrawing()
    {
        currentLine = null;
        linePoints = null;
    }

    float GetLineLength(List<Vector2> points)
    {
        float length = 0f;
        for (int i = 1; i < points.Count; i++)
        {
            length += Vector2.Distance(points[i - 1], points[i]);
        }
        return length;
    }
}
