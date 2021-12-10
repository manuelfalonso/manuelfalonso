using UnityEngine;

/// <summary>
/// Debugging class to create an up straight line over the object.
/// </summary>
public class UsingLineRendererForDebugging : MonoBehaviour
{
    private LineRenderer m_lineRenderer;
    private Vector3[] m_positions;

    void Awake()
    {
        //we would want to cache our reference to the line renderer component to avoid having to do expensive component lookup
        m_lineRenderer = gameObject.AddComponent<LineRenderer>();

        //we know that we're going to be drawing a line between two points, so we need 
        m_positions = new Vector3[2];
    }

    void Update()
    {
        //set the start and end positions of our line
        m_positions[0] = this.transform.position;
        m_positions[1] = this.transform.position + Vector3.up * 2f;

        //set the beginning and ending width of the line in units, 0.1f corresponding to 10cm.
        m_lineRenderer.startWidth = 0.1f;
        m_lineRenderer.endWidth = 0.1f;

        //feed the values to the line renderer
        m_lineRenderer.SetPositions(m_positions);

        //and ensure that the line renderer knows to draw in world space
        m_lineRenderer.useWorldSpace = true;
    }
}