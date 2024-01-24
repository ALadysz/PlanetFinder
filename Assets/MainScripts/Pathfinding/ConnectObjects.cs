using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//planets class - connects planets and also stores neighbours

public class ConnectObjects : MonoBehaviour {
    //variables
    [HideInInspector] public List<GameObject> connectedObjects; //list that stores planets connected to it

    private float maxDistance; // max distance to detect planets from
    private LineRenderer lineRenderer; // linerenderer component to make the lines
    public planets PlanetScript; //planet script to get distance 


    private void Start() {
        //initialise things needed
        PlanetScript = GameObject.FindObjectOfType<planets>();
        connectedObjects = new List<GameObject>();
        lineRenderer = GetComponent<LineRenderer>();

        //dependent on distance in planets, distance can only be set to 4 different distances - set maxdistance dependent on distance
        switch(PlanetScript.distance) {
            case 25:
                maxDistance = 7;
                break;
            case 50:
                maxDistance = 14;
                break;
            case 75:
                maxDistance = 21;
                break;
            case 100:
                maxDistance = 28;
                break;
                
            default:
            //there is literally no other option - if this happens something has gone very very wronng
                break;
        }

        FindObjects(); 
        LineSetup();
    }

    private void FindObjects() { //find near objects 
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxDistance); //put any colliders in range into the colliders array

        foreach (Collider collider in colliders) {
            if (collider.gameObject.CompareTag("planet")) {//if the object in range is a planet
                connectedObjects.Add(collider.gameObject); // add its game object to the list
            }
        }
    }

    private void LineSetup() {//set up lines
        lineRenderer.positionCount = connectedObjects.Count + 1; //add all connected objects to the linerenderer count - plus one to add the planet this class is on
        lineRenderer.SetPosition(0, transform.position); // make the start object this current object

        for (int i = 0; i < connectedObjects.Count; i++) {//for each connected planet
            lineRenderer.SetPosition(i + 1, connectedObjects[i].transform.position); //set line position
        }
    }

    private void NewObjects() {// this function isn't used but it's called at update to add more objects if anything comes in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxDistance); //on each update frame check if something new is overlapping

        foreach (Collider collider in colliders) {//for each object found
            if (!connectedObjects.Contains(collider.gameObject)) {// if the object found isn't already in list 
                //add a line connected to it
                connectedObjects.Add(collider.gameObject);
                lineRenderer.positionCount = connectedObjects.Count + 1;
                lineRenderer.SetPosition(connectedObjects.Count, collider.gameObject.transform.position);
            }
        }
    }
}