using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script that generates planets + makes sure they stay after scene reload


public class planets : MonoBehaviour {
    //variables for randomising location + c variable to track looping
    private float randomx;
    private float randomy; 
    private float randomz;
    private int c;

    private planets[] thisScripts; //to make sure that multiple centre objects aren't created
    [SerializeField] private GameObject[] PlanetPrefabs; // array to put planet prefabs in inspector

    [HideInInspector] public GameObject centre; //prefab to make the planets as child to it
    [HideInInspector] public GameObject oppositeCentre; // is put at the max distance on all axis in a different script - so the camera can be in the centre
    private GameObject newplanet; //object to make new planets as

    [HideInInspector] public float distance = 0; //distance from centre to generate planets
    [HideInInspector] public int amount = 0; //amount of planets to generate

    // Update is called once per frame
    void Awake() {
        DontDestroyOnLoad(centre);  //make sure object is not destroyed so planets stay the same      
    }

    void GenerateRandoms() {
        //generate random coordinates for the planet
        randomx = Random.Range(0.0f, distance);
        randomz = Random.Range(0.0f, distance);
        randomy = Random.Range(0.0f, distance);
    }

    void GeneratePlanets() {
        while(c!= amount) { //generate planets so long as the amount isn't reached
            GenerateRandoms();

            //instantiate planet at random coords
            newplanet = Instantiate(PlanetPrefabs[Random.Range(0, PlanetPrefabs.Length)], new Vector3(randomx, randomz, randomy), Quaternion.identity, centre.transform);
            c++; //increment

           

        }
    }

    //this function was made to prevent multiple centres getting instantiated and more planets getting made
    public void CheckIfGen() { //check whether to generate yet - amount and distance are set in different scripts so can't run at awake
        thisScripts = FindObjectsOfType<planets>(); //check how many centres are in scene
        if(thisScripts.Length < 2) { //so long as theres only one generate planets
            GeneratePlanets();

        } 
    }

}
