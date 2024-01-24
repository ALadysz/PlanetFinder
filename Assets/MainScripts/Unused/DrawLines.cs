using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
   public float radius = 5f; // radius to check for objects
    public LayerMask objectLayer; // layer of objects to check for
    public Color lineColor = Color.white; // color of the line

    private LineRenderer[] lineRenderers;
    private Collider[] objectsInRange;

    private void Start()
    {
        objectsInRange = Physics.OverlapSphere(transform.position, radius, objectLayer);
        lineRenderers = new LineRenderer[objectsInRange.Length];

        for (int i = 0; i < objectsInRange.Length; i++)
        {
            GameObject lineObj = new GameObject("LineRenderer");
            lineRenderers[i] = lineObj.AddComponent<LineRenderer>();
            lineRenderers[i].startColor = lineColor;
            lineRenderers[i].endColor = lineColor;
            lineRenderers[i].positionCount = 2;
            lineRenderers[i].SetPosition(0, transform.position);
            lineRenderers[i].SetPosition(1, objectsInRange[i].transform.position);
        }
    }


    private void Update()
    {
        // check for objects within the specified radius
        objectsInRange = Physics.OverlapSphere(transform.position, radius, objectLayer);

        // remove any lines that are no longer needed
        if (lineRenderers.Length > objectsInRange.Length)
        {
            for (int i = objectsInRange.Length; i < lineRenderers.Length; i++)
            {
                Destroy(lineRenderers[i].gameObject);
            }
        }

        // add any new lines needed
        if (lineRenderers.Length < objectsInRange.Length)
        {
            LineRenderer[] newLineRenderers = new LineRenderer[objectsInRange.Length];
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                newLineRenderers[i] = lineRenderers[i];
            }
            for (int i = lineRenderers.Length; i < objectsInRange.Length; i++)
            {
                GameObject lineObj = new GameObject("LineRenderer");
                newLineRenderers[i] = lineObj.AddComponent<LineRenderer>();
                newLineRenderers[i].startColor = lineColor;
                newLineRenderers[i].endColor = lineColor;
                newLineRenderers[i].positionCount = 2;
                newLineRenderers[i].SetPosition(0, transform.position);
                newLineRenderers[i].SetPosition(1, objectsInRange[i].transform.position);
            }
            lineRenderers = newLineRenderers;
        }

        // update the positions of the lines
        for (int i = 0; i < objectsInRange.Length; i++)
        {
            lineRenderers[i].SetPosition(0, transform.position);
            lineRenderers[i].SetPosition(1, objectsInRange[i].transform.position);
        }
    }
}
