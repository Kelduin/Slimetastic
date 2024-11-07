using UnityEngine;

public class LineCastDown : MonoBehaviour
{
    public LineRenderer lineRenderer; // Reference to the LineRenderer component
    public float lineLength = 10f; // The length of the line to cast down

    void Start()
    {
        // Check if lineRenderer is assigned, and if not, get the component on the same GameObject
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        // Set the number of points for the line (2 points: start and end)
        lineRenderer.positionCount = 2;

        // Optional: Set initial line properties (color, width, etc.)
        // lineRenderer.startWidth = 0.1f;
        // lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        // Get the current position of the GameObject
        Vector3 startPosition = transform.position;

        // Set the end position a fixed distance down (based on lineLength)
        Vector3 endPosition = startPosition - new Vector3(0, lineLength, 0);

        // Update the positions of the LineRenderer
        lineRenderer.SetPosition(0, startPosition); // Set the start point of the line
        lineRenderer.SetPosition(1, endPosition);   // Set the end point of the line
    }
}
