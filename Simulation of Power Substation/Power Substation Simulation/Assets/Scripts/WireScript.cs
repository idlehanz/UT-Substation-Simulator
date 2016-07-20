using UnityEngine;
using System.Collections;

public class WireScript: MonoBehaviour
{
    public int lengthOfLineRenderer = 29;
    public Material newMaterialRef;
    public float yIntc = -51.58F;
    public float coeff = 35.5F;
    public float jcoeff = 0.014F;
    public float jIntc = 67.05F;

    

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.material = newMaterialRef;
        lineRenderer.SetColors(Color.gray, Color.gray);
        lineRenderer.SetWidth(.2F, .2F);
        lineRenderer.SetVertexCount(lengthOfLineRenderer);
    }
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        Vector3[] points = new Vector3[lengthOfLineRenderer];
        int i = 0;
        int j = 0;
        while (i < lengthOfLineRenderer)
        {
            points[i] = new Vector3(i * 2F, yIntc + coeff * (Mathf.Exp(jcoeff * (j - jIntc)) + Mathf.Exp(-jcoeff * (j - jIntc))) * 0.5F, 0);
            i++;
            j += 1;
        }
        lineRenderer.SetPositions(points);
    }
}
