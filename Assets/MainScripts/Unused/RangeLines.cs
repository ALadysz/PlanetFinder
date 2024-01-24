using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.reddit.com/r/Unity3D/comments/2sun9c/find_an_object_within_a_range/

public class RangeLines : LineControl
{
    List<GameObject> planetsInRange = new List<GameObject>();
    [SerializeField] float range;
    private GameObject newerLine;
    private LineRenderer drawingLine;

    void Start()
    {
        newerLine = new GameObject();
        drawingLine = newerLine.AddComponent<LineRenderer>();
    }

    void Update()
    {
        GameObject[] planetarium = GameObject.FindGameObjectsWithTag("planet");
        foreach(GameObject g in planetarium)
        {
            for (int i = 0; i < planetarium.Length; i++)
            {
                float dist = Vector3.Distance(g.transform.position, planetarium[i].transform.position);
                if ( dist <= range )
                {
                    planetsInRange.Add(g);
                    linePoints.Add(g.transform.position);
                    Debug.Log(planetsInRange[i].name);
                }
            }
            
        }
        drawingLine.positionCount = linePoints.Count;
        drawingLine.SetPositions(linePoints.ToArray());

    }
}
