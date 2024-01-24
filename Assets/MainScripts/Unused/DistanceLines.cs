using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DistanceLines : LineControl
{

    List<GameObject> paths = new List<GameObject>();
    int n = 0;
    int m = 0;
    [SerializeField] int range;

    void Update()
    {
        GameObject[] planetarium = GameObject.FindGameObjectsWithTag("planet");
        foreach (GameObject t in planetarium)
        {
            if((n+1) < planetarium.Length)
            {
                float dist = Vector3.Distance(planetarium[n].transform.position, planetarium[n + 1].transform.position);
                Debug.Log(dist + " " + n);

                if (dist <= range)
                {
                    paths.Add(planetarium[n]);
                    paths.Add(planetarium[n + 1]);
                }
                n++;
            }
            else
            {
                float dist = Vector3.Distance(planetarium[n].transform.position, planetarium[0].transform.position);
                Debug.Log(dist + " " + n);

                if (dist <= range)
                {
                    paths.Add(planetarium[n]);
                    paths.Add(planetarium[0]);
                }
            }
        }

        foreach(GameObject j in paths)
        {
            linePoints.Add(paths[m].transform.position);
            m++;
        }
        drawLine.positionCount = linePoints.Count;
        drawLine.SetPositions(linePoints.ToArray());

    }
}
