using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingDistance : MonoBehaviour
{

    private GameObject[] planetarium;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] planetarium = GameObject.FindGameObjectsWithTag("planet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
