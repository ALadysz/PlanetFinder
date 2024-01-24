using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tutorial used to learn line renderer stuff - https://www.youtube.com/watch?v=mgmfZRllpzs
//fix for array stuff - https://answers.unity.com/questions/810590/getting-all-objects-with-a-tag-and-inserting-them.html

public class LineControl : MonoBehaviour
{
    //variables
    [HideInInspector] public GameObject[] planetarium;
    [HideInInspector] public GameObject newLine;
    [HideInInspector] public LineRenderer drawLine;
    [HideInInspector] public int i= 0;
    [HideInInspector] public List<Vector3> linePoints;
    
    

    void Start() => LineData();
    

    // Update is called once per frame

    Color randomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void LineData()
    {
        linePoints = new List<Vector3>();
        newLine = new GameObject();
        drawLine = newLine.AddComponent<LineRenderer>();
        drawLine.material = new Material(Shader.Find("Sprites/Default"));
        drawLine.startWidth = 0.25f;
        drawLine.endWidth = 0.25f;
        drawLine.startColor = Color.white;
        drawLine.endColor = Color.white;
    }
}
