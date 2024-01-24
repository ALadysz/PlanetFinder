using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesInBetween : MonoBehaviour
{
    private GameObject[] planetarium;
    private GameObject newLine;
    private LineRenderer lineDrawer;
    private List<Vector3> linePoints;
    [SerializeField] private int numberOfConnections;
    [SerializeField] private Material lineMat;
    int i = 0;
    int otherPlanet;


    void Update()
    {
        GameObject[] planetarium = GameObject.FindGameObjectsWithTag("planet");
        //if (i <= numberOfConnections)
        //{
            foreach(GameObject planet in planetarium)
            {
                LineData();
                linePoints.Clear();
                lineDrawer.positionCount = 0;
                if(i != planetarium.Length)
                {
                    linePoints.Add(planetarium[i].transform.position);
                }
                else
                {
                    return;
                }
                
                for (int j = 0; j < numberOfConnections; j++)
                {
                    otherPlanet = Random.Range(i, planetarium.Length);
                    linePoints.Add(planetarium[otherPlanet].transform.position);
                }

                if(linePoints.Count == 3)
                {
                    linePoints.RemoveAt(2);
                }

                lineDrawer.positionCount = linePoints.Count;
                lineDrawer.SetPositions(linePoints.ToArray());
                Debug.Log(Vector3.Distance(planetarium[i].transform.position,planetarium[otherPlanet].transform.position));

                if(i < planetarium.Length)
                {
                    i++;
                }
                
            }

        //}

    }

    void LineData()
    {
        linePoints = new List<Vector3>();
        if(i < 50)
        {
            newLine = new GameObject();
            newLine.tag = "line";
            newLine.transform.parent = transform;
            lineDrawer = newLine.AddComponent<LineRenderer>();
            lineDrawer.material = lineMat;
            lineDrawer.textureMode = LineTextureMode.Tile;
            lineDrawer.startWidth = 0.15f;
            lineDrawer.endWidth = 0.15f;
            lineDrawer.startColor = Color.white;
            lineDrawer.endColor = Color.white;
        }
        else
        {
            return;
        }
        
    }

}
