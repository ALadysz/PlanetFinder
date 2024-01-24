using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script that spins the planets at random rates and makes them random sizes

public class SpinPlanet : MonoBehaviour {
    //variables
    private float yRotation;
    private float xRotation;
    private float Size;

    
    void Start() {
        //randomise planet size and rotation axis
        yRotation = Random.Range(0f, 0.2f);
        xRotation = Random.Range(0f, 0.15f);
        Size = Random.Range(0.5f, 2f);
    }

    void Update() {
        //set planets rotation and size
        transform.Rotate(0f ,yRotation, Time.deltaTime, Space.Self);
        transform.Rotate(xRotation ,0f, Time.deltaTime, Space.Self);
        transform.localScale = new Vector3(Size, Size, Size);
        
    }

   

}
