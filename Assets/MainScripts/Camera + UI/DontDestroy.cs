using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attempt script to make the starting panel ui not show up upon reload

public class DontDestroy : MonoBehaviour {
    public bool spawnStartingUI = false;

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this.gameObject);
        spawnStartingUI = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) //on escape quit
        {
            // Quit the application
            Application.Quit();
        }
    }   
}