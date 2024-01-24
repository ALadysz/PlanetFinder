using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLines : LineControl
{
    
    // Update is called once per frame
    void Update()
    {
        GameObject[] planetarium = GameObject.FindGameObjectsWithTag("planet");
        while(i != planetarium.Length)
        {
            foreach (GameObject p in planetarium)
            {     
                linePoints.Add(planetarium[i].transform.position);
                i++;
            }
            drawLine.positionCount = linePoints.Count;
            drawLine.SetPositions(linePoints.ToArray());
        }
    }
}