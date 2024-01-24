using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLines : LineControl
{
    
    //generates random amount of lines goin from random stars
    // int amount;
    // int numOfLines;
    // int RandomStar;
    // int RandomNum;

    // void Start() 
    // {
    //     planets planets = GetComponent<planets>();
    //     amount = planets.amount;
    //     RandomNum = Random.Range(0, amount);
    // }
    
    public int numOfLines;
    void Update()
    {
        GameObject[] planetarium = GameObject.FindGameObjectsWithTag("planet");
        for (int i=0; i>= numOfLines; i++)
            {
                linePoints.Add(planetarium[Random.Range(0,planetarium.Length)].transform.position);
            }
            drawLine.positionCount = linePoints.Count;
            drawLine.SetPositions(linePoints.ToArray());
       
    }

}
