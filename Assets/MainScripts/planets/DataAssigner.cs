using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script to assign data elsewhere

public class DataAssigner : MonoBehaviour {
    private GameObject[] planetarium; //array to store planets in scene
    private string[] NamesOfPlanets = new string[] { //array of names - this took so long to type and format :(
        "Vulroitania",
        "Henzeahiri",
        "Nidroth",
        "Runkilia",
        "Zuria",
        "Molara",
        "Civethea",
        "Biutov",
        "Zyria OVGO",
        "Morix 1F2",
        "Hechaeruta",
        "Ibbonides",
        "Ridriea",
        "Iccars",
        "Henus",
        "Soulea",
        "Tramurilia",
        "Lakecarro",
        "Lion QOKR",
        "Byke 7Q",
        "Thonvoitis",
        "Dogralia",
        "Latrion",
        "Chibiri",
        "Yuagawa",
        "Ruhiri",
        "Bapagawa",
        "Gricuhiri",
        "Tharvis Q2R",
        "Larvis 13R",
        "Vucciruta",
        "Ubbogawa",
        "Revinda",
        "Tudrorix",
        "Neophus",
        "Toalara",
        "Trohater",
        "Dolotera",
        "Cov E9",
        "Lade X",
        "Chulmimia",
        "Zuphatune",
        "Zennoth",
        "Tognone",
        "Bestea",
        "Bolia",
        "Striatune",
        "Bixinus",
        "Lliri F91",
        "Vars 5A8"
    };

    //functions to call from ui script

    public void GiveNames() { 
        GameObject[] planetarium = GameObject.FindGameObjectsWithTag("planet"); // this is called here instead of start as this object is always in scene and in the beinning there is no planets
        foreach(GameObject planet in planetarium) {//for each planet
            planet.name = NamesOfPlanets[Random.Range(0, NamesOfPlanets.Length)]; //rename it
        }
    }

    public string GenerateTemp() {
        float temperature = Random.Range(-60.0f, 60.0f); //randomise temperature
        return temperature.ToString();
    }
    
    public string GeneratePopulation() {
        int population = Random.Range(0, 10000); //randomise population
        return population.ToString();
    }



}
