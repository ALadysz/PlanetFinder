using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//manager for the camera
//source - https://gamedevacademy.org/unity-3d-first-and-third-person-view-tutorial/

public class CamManager : MonoBehaviour {

    //variables for camera rotation 
    private float turnSpeed = 6.0f;
    private float minTurnAngle = -90.0f;
    private float maxTurnAngle = 0.0f;
    private float rotX;

    //variables for finding where to put camera
    [SerializeField] private planets ScriptObj; //script that has two opposite corners of area of planet spawn
    [HideInInspector] public float targetDistance = 20.0f; //distance of the camera away from the target obj
    public GameObject TargetOfCam; //object that the camera is focused on - set apart by distance
    private GameObject pointOne; //centre object
    private GameObject pointTwo; //opposite centre - these are used to calculate the centre point of where the planets were generated

    //variables for raycasting and switching camera to clicked on planet
    [HideInInspector] public Vector3 originalCameraTargetPosition; //centre point of planets is saved under this
    [HideInInspector] public Vector3 newCameraTargetPosition; //this is set to the planet when its clicked on
    [HideInInspector] public bool doRaycast; //bool that enables and disables raycasting so its not on when its focused on a planet
    [HideInInspector] public bool updateCamPosition = false; //triggers the switch to the planet inside of UI manager
    [HideInInspector] public GameObject clickedPlanet; //current planet - used to reference the raycasted planet in other scripts
    Camera cam; //main camera

    //misc variables
    [HideInInspector] public string currentPlanetName; //needed in other scripts - set using raycast
    [SerializeField] private AudioSource MouseClick; //sound to play when planet clicked on
    private float zoomOutLimit; //limit for zooming out as it needed to be changed the higher the distance got



    
    public void StartCamera() { //runs after planet generation
        //putting object camera focuses on in centre of planets
        pointOne = ScriptObj.centre; //getting opposite ends of planet generation space
        pointTwo = ScriptObj.oppositeCentre;
        ScriptObj.oppositeCentre.transform.position = new Vector3(ScriptObj.distance, ScriptObj.distance, ScriptObj.distance); //setting the opposite end of the planet generation space
        originalCameraTargetPosition = (pointOne.transform.position + pointTwo.transform.position) / 2f;//finding centre of planets by adding and dividing the opposite ends of the space
        TargetOfCam.transform.position = originalCameraTargetPosition; //setting the targets positon as the centre

        cam = Camera.main; //setting camera to the main camera
        doRaycast = true; //enabling raycast
    }

    void Update() {
        RotateCam();
        //move the camera to be at target distance
        transform.position = TargetOfCam.transform.position - (transform.forward * targetDistance);
        if(doRaycast == true) {//only do raycasting and zooming if in the original position and not focused on a planet
            RaycastDetect(); 
            Zoom();
        }
    }


    void RotateCam() {//rotate camera according to mouse position
        //get mouse inputs
        float rotY = Input.GetAxis("Mouse X") * turnSpeed; //set rotation values to be the based on the mouse
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;

        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle); //clamp vertical rotation

        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + rotY, 0); //actually rotate the camera
    }





    void RaycastDetect() { //raycast mechanics and checlomg what is clicked on
        if (Input.GetMouseButtonDown(0)) {//if the user clicks
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //raycast is created
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) //if it hits something (only planets are possible to click anyway)
            {
                MouseClick.Play(); //play click sound
                newCameraTargetPosition = hit.collider.gameObject.transform.position; //set camera target position as the planet
                currentPlanetName = hit.collider.gameObject.name; //set current name as the planets name
                clickedPlanet = hit.collider.gameObject; //set clicked planet to the current planet
                updateCamPosition = true; //tells the ui manager script to switch the cameras
            }
        }
    }

    void Zoom() { //adds the zoom in and out functionality
        //limit of zoom out is dependent on the distance the planets were generated
        if(ScriptObj.distance > 50) {
            zoomOutLimit = 80.0f;
        }
        else {
            zoomOutLimit = 40.0f;
        }

        if(targetDistance > 10.0f) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) { //zoom in with up arrow key
                targetDistance = targetDistance - 5;
            }
        }
        if(targetDistance < zoomOutLimit) {
            if (Input.GetKeyDown(KeyCode.DownArrow)) { //zoom out with down arrow key
                targetDistance = targetDistance + 5;
            }
        }        
        
        if(Input.GetKeyDown(KeyCode.Space)) { //reset zoom with space
            targetDistance = 20.0f;
        }
       
    }

}
