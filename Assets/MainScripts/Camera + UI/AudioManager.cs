using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//audio manager to call audios

public class AudioManager : MonoBehaviour {

    //audio sources
    [SerializeField] private AudioSource ResetButtonAudio;
    [SerializeField] private AudioSource GenerateButtonsAudio;
    [SerializeField] private AudioSource BasicButtonAudio;
    [SerializeField] private AudioSource GenConfigButtonsAudio;

    //functions to call from buttons and scripts
    public void ResetButton() {
        ResetButtonAudio.Play();
    }

    public void GenerateButtons() {
        GenerateButtonsAudio.Play();
    }

    public void BasicButtons() {
        BasicButtonAudio.Play();
    }

    public void GenConfigButtons() {
        GenConfigButtonsAudio.Play();
    }


}
