using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//https://answers.unity.com/questions/914923/standard-shader-emission-control-via-script.html
//make it select as soon as one is selected
//make it so you can undo the texture
//make it so it can be fully reset
public class PlanetGlower : MonoBehaviour
{
    public UIManager UIScript;
    private Renderer startRen;
    private Renderer endRen;
    private Material[] startMats;
    private Material[] endMats;
    private Material startMat;
    private Material endMat;
    [SerializeField] Material GlowMat;
    public DijkstraPathfindin PathScript;
    

    // Update is called once per frame
    void Update()
    {
        try
        {
            try
            {
                Material mat = startRen.material;
                float emission = Mathf.PingPong (Time.time, 1.0f);
                Color baseColor = Color.red; //Replace this with whatever you want for your base color at emission level '1'
        
                Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
        
                mat.SetColor ("_EmissionColor", finalColor);
            }
            catch(UnassignedReferenceException ex)
            {
                if(ex != null)
                {
                    return;
                }
                return;
            }
        }
        catch(NullReferenceException exo)
        {
            if(exo != null)
            {
                return;
            }
            return;
        }
        
        /*
        {
            if( UIScript.planetA != null || UIScript.planetB != null)
            {
                startRen = UIScript.planetA.GetComponent<Renderer>();
                endRen = UIScript.planetB.GetComponent<Renderer>();

                startMat = startRen.materials[2];
                endMat = endRen.materials[2];

                startMats = startRen.materials;
                endMats = endRen.materials;
                startMats[2] = GlowMat;
                endMats[2] = GlowMat;
                startRen.materials = startMats;
                endRen.materials = endMats;

            }
            else if(UIScript.planetB != null)
            {

            }
        }
        catch(UnassignedReferenceException ex)
        {
            return;
        }
        */
        
    }   
}