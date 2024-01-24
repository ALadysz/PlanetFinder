using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


//make it obious which planets is currently selected


public class UIManager : MonoBehaviour {
    //variables for setting ui
    [SerializeField] GameObject BackButton;
    [SerializeField] GameObject planetData;
    [SerializeField] GameObject SelectButton;
    [SerializeField] GameObject UndoButton;
    [SerializeField] GameObject StartingPanel;
    [SerializeField] GameObject ResetButton;
    [SerializeField] GameObject GenButton;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject startingCam;

    [SerializeField] TMP_Text planetName;
    [SerializeField] TMP_Text planetPopulation;
    [SerializeField] TMP_Text planetTemp;

    [SerializeField] private TMP_Text selectedPlanetA;
    [SerializeField] private TMP_Text selectedPlanetB;    

    [SerializeField] private DataAssigner DataScript; //script that generates planet info
    [SerializeField] private CamManager CamScript; //script to move the camera + access the raycast
    [SerializeField] private DijkstraPathfindin PathScript; //script to set the starting and ending object of pathfinding
    [SerializeField] private planets PlanetScript; //script to set the distance and amount
    [SerializeField] private DontDestroy NoDestroyScript; //script that was supposed to prevent the gen config panel from appearing but DOESNT :(
    [SerializeField] private AudioManager AudioScript; //script to play audios

    //variables needed for selecting starting and ending planet for path finding
    [SerializeField] private GameObject planetA;
    [SerializeField] private GameObject planetB;
    [HideInInspector]public int selectTrack = 0; //track what has been selected + for undoing

    private string currentSceneName; //needed for scene reloading


    private void Start() {
        ToggleUI(false); //make sure toggleable ui isn't active at start
        currentSceneName = SceneManager.GetActiveScene().name; //needed for scene reloading
        //if(NoDestroyScript.spawnStartingUI == true) //my attempt at disabling the gen config ui after reload
        //{
            StartingPanel.SetActive(true); //in the start gen config needs to be active
            startingCam.SetActive(true); //gen config camera needs to be on
            mainCam.SetActive(false);// and the main cam which moves around off
            ResetButton.SetActive(false); // reset button and gen path button not needed when gen config is active
            GenButton.SetActive(false);
       // }
       /* else //if the gen config panel wasnt suppsed to be on make sure needed things are active
        {
            ResetButton.SetActive(true);
            GenButton.SetActive(true);
            StartingPanel.SetActive(false);
            startingCam.SetActive(false);
        }
        */
    }

    private void Update() {
        ChangeCamPos(); //move the camera to a planet if needed
    }

    void ChangeCamPos() {//to call when clicking on planet
        if(CamScript.updateCamPosition == true) { //when triggered to move camera
            CamScript.TargetOfCam.transform.position = CamScript.newCameraTargetPosition; // move the target of the camera to the planet 
            CamScript.targetDistance = 5.0f; // make the distance shorter so the planet is in clear view
            GenerateUI(); // generate the ui for the current planet
            CamScript.doRaycast = false; // don't let the user click on other planets whilst looking at a planet
        }
        CamScript.updateCamPosition = false; //after this runs make it so it can be called again later
    }

    private void GenerateUI() {
        ToggleUI(true); //toggle all toggleable uis
        planetName.text = CamScript.currentPlanetName; //get current planets name and set its text in the ui
        planetTemp.text = "Temperature = " + DataScript.GenerateTemp(); //set the temp using the datascript
        planetPopulation.text = "Population = " + DataScript.GeneratePopulation(); //set the population using the datascript
    }

    public void GenPlanetsButton() { 
        //function for the generate planets button
        startingCam.SetActive(false);
        mainCam.SetActive(true); //activate main camera and disable starting camera
        StartingPanel.SetActive(false); //disable config panel
        ResetButton.SetActive(true);
        GenButton.SetActive(true); //activate generate path button and reset button as they now have a purpose
        NoDestroyScript.spawnStartingUI = false; // continuation of my attempt to prevent config panel from showing up
    }

    public void SetDistance(int distance) { //function for distances buttons on gen config
        AudioScript.GenConfigButtons(); //audio is called here instead of in inspector because there is 8 different of these buttons and that is too many clicks
        PlanetScript.distance = distance; //set the clicked distance as the distance for the planet generator
    }

    public void SetAmount(int amount) { //function for amounts buttons on gen config
        AudioScript.GenConfigButtons();
        PlanetScript.amount = amount; //set the clicked amount as the amount for the amount generator
    }

    public void BackToOriginal() { // function for back button
        CamScript.TargetOfCam.transform.position = CamScript.originalCameraTargetPosition; //resets camera target position
        CamScript.targetDistance = 20.0f; //sets the distance to the target to the normal
        ToggleUI(false); //disable toggleables
        CamScript.doRaycast = true; //enable raycast again so planets can be clicked on again

    }
    
    public void Select() {//function for select button
        if(selectTrack == 0) {//if  nothing has been selected yet
            selectTrack = 1; // enable selecting second planet
            planetA = CamScript.clickedPlanet;  //set the starting planet as the last clicked planet
            selectedPlanetA.text = planetA.name; //in the selected window add the planets name
            PathScript.startObject = planetA; //set the starting planet as our selected planet
        }
        else if(selectTrack == 1) {//can only select second if first has been selected
            selectTrack = 2; //update select tracking
            planetB = CamScript.clickedPlanet; //set the ending planet as the last clicked planet
            selectedPlanetB.text = planetB.name; //add the ending planets name
            PathScript.endObject = planetB; //set the ending planet as this selected planet
        }
    }

    public void UndoSelect() { //function of undo button 
         if(selectTrack == 1) { //if the undo button is pressed after the first planet has been selected
            selectedPlanetA.text = " "; //clear selected panel
            selectTrack = 0; //reset selection tracking
            PathScript.startObject = null; //clear the starting object
         }
         else if(selectTrack == 2) { //if the undo button is pressed after the second
            selectedPlanetB.text = " "; //clear panel
            selectTrack = 1; //set selected tracking back one so the second can be selected again
            PathScript.endObject = null; //clear the ending object
         }
         else {
            return; //only other thing it can be is 0 which then theres nothing to undo
         }
    }

    void ToggleUI(bool toggle) { //function to toggle ui by parsing in bool into a bunch of set active statements
        BackButton.SetActive(toggle);
        planetData.SetActive(toggle);
        SelectButton.SetActive(toggle);
        UndoButton.SetActive(toggle);
    }

    public void Reset() { //reset button function - reloads scene
        SceneManager.LoadScene(currentSceneName);
    }
}
