using System.Collections.Generic;
using UnityEngine;

//dijkstra path finding script

public class DijkstraPathfindin : MonoBehaviour {
    //start and finish objects
    [HideInInspector] public GameObject startObject; 
    [HideInInspector] public GameObject endObject; 

    //ui panels for error catching
    [SerializeField] GameObject warningPanel; 
    [SerializeField] GameObject noConnectionWarning;

    //list objects - names are mainly self explanatory
    private List<GameObject> connectedObjects = new List<GameObject>(); //this is named this way as it will contain the ConnectedObjects list from another script
    private List<GameObject> unvisited = new List<GameObject>(); 
    private List<GameObject> path = new List<GameObject>(); 

    //dictionaries for referencing
    private Dictionary<GameObject, float> distances = new Dictionary<GameObject, float>(); 
    private Dictionary<GameObject, GameObject> previous = new Dictionary<GameObject, GameObject>(); 
    private ConnectObjects ConnectScript; //class with connections

    //misc variables
    [SerializeField] Material lineMat; //for the finished path
    [HideInInspector] public bool doPathfinding = true;  //to enable and disable path finding - to make it dependent on the button

    void Start() {
        unvisited = connectedObjects; //no planets visited yet so all connections are unvisited
    }

    void PathFinding() {
        YoinkConnectedObjects(startObject);  //yoink connected objects from class that connects objects thats on the planets
        InitDicts(); //initialises dictionaries - i just find this name funnier
        CalcDist(); //calculates alllll the distances
        Reconstructor(); 

        // print shortest path 
        foreach (GameObject waypoint in path) {
            Debug.Log(waypoint.name); //ignore this 
        }

        MRLinemaker(); //create the shortest path object and make the lineRenderer component
    }


    void YoinkConnectedObjects(GameObject start) {
        connectedObjects.Add(start); //make sure our start point is in the list
        ConnectScript = start.GetComponent<ConnectObjects>(); //the yoinking
        if (ConnectScript != null) {// if the class is actually there 
            foreach (GameObject neighbor in ConnectScript.connectedObjects) {//for each connected object
                if (!connectedObjects.Contains(neighbor)) {// only runs if neighbour isn't in connectedObjects
                    YoinkConnectedObjects(neighbor); //find all connections to neighbour
                }
            }
        }
    }

    void InitDicts() {
        distances.Clear(); // Clear the distances dictionary
        previous.Clear(); // Clear the previous dictionary
        foreach (GameObject obj in connectedObjects) {
            distances[obj] = float.MaxValue; // Set initial distance to each object as maximum
            previous[obj] = null; // Set initial previous object to null for each object
        }
    }

    void CalcDist() {
        distances[startObject] = 0.0f; //start object distance is 0 as its the start point

        while (unvisited.Count > 0) {//so long as there is an unvisited star
            GameObject current = DragClosestHere(unvisited); //make the closest object current
            unvisited.Remove(current); // mark it as visited by making it not in unvisited

            if (current == endObject) {
                break; //stop checking once we've reached the end object
            }

            ConnectObjects CurrentConnectScript = current.GetComponent<ConnectObjects>(); //get the ConnectedObject class from the current object
            if (CurrentConnectScript != null) {//run so long as the class is there
                foreach (GameObject neighbor in CurrentConnectScript.connectedObjects) {//for each of its neighbours
                    float AltDist = distances[current] + Vector3.Distance(current.transform.position, neighbor.transform.position); //calculate alternative distance 
                    if (AltDist < distances[neighbor]) {//if its smaller than the distance currently recorded - if it is it means that the new path is shorter than the one we had before
                        distances[neighbor] = AltDist; //update dictionary by assigning the new distance to the neighbour
                        previous[neighbor] = current; // assign as previous obj on shortest path  - keep track of previous objects that lead to shortest path for each obj
                    }
                }
            }
        }
    }

    void Reconstructor() {//the mighty
        path.Clear(); //clear the path list jussst in case

        try {// try and catch here as it causes an error if there is no line connection between the start and end
            GameObject j = endObject;
            while (j != null) {
                //goes backwards through the previous objs to reconstruct the path + makes suure correct order for shortest path
                path.Insert(0, j); // inserts j at start of path 
                j = previous[j]; // moves onto next previous obj
            }
        }
        catch (KeyNotFoundException e) {
            noConnectionWarning.SetActive(true); // Display the no connection warning
            if (e != null) {
                return; //this if statement is just here as it gives the "e is not actually used warning"
            }
        }
    }

    void MRLinemaker() {//a distinguished gentleman
        GameObject shortObj = new GameObject("ShortestPath"); //make object for the path
        shortObj.tag = "shortestPath"; //tag the object if it needs to be found in another script - this tag may need to be recreated as unity doesnt want to save it for some reason
        LineRenderer shortLine = shortObj.AddComponent<LineRenderer>(); // give it the ability to make lines
        shortLine.positionCount = path.Count; // set the amount of planets in the path as amount of points
        shortLine.startWidth = 0.1f; //width things
        shortLine.endWidth = 0.1f; 
        shortLine.material.color = Color.red; //so its obvious
        shortLine.material = lineMat; //so its not pink

        Vector3[] pathPosi = new Vector3[path.Count]; //array for path positions (pathPosi because pathPoses isnt right so i went with cactus logic (cactuses -> cacti))
        for (int i = 0; i < path.Count; i++) {
            pathPosi[i] = path[i].transform.position; // set each element of the pathPosi as each path elements positon
        }
        shortLine.SetPositions(pathPosi); // set the line to the positions of the path objects through the path posi
    }

    GameObject DragClosestHere(List<GameObject> objects) {
        GameObject smallestObject = objects[0]; // smallest obj is the first to begin with
        foreach (GameObject obj in objects) {//for each obj in the list given
            if (distances[obj] < distances[smallestObject]) {// if the distance of current obj is smaller than the smallllest object currently
                smallestObject = obj; //update it as the smallest is clearly the smallest no longer
            }
        }
        return smallestObject; //the tiniest of them all
    }

    public void PathGenChecker() {
        //check whether to do path finding yet - i want it to only do it after the button is pressed
        if (doPathfinding) {
            try {//try except here is for the same reason as before
                if (startObject != null && endObject != null) {
                    PathFinding(); // perform pathfinding only if both are there, otherwise whats the point?
                }
                else {
                    warningPanel.SetActive(true); //if they try give them a warning 
                    return;
                }
            }
            catch (KeyNotFoundException ex) {
                noConnectionWarning.SetActive(true); // Display the no connection warning
                if (ex != null) {
                    return;
                }
            }
        }
        doPathfinding = false; // only one run allowed till scene reload
    }

    public void ClosePanel() {
        warningPanel.SetActive(false); // hide the warning panel
    }
}
