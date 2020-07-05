﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public GameObject PausePanel;

    [Space(20)]

    private bool pause;


    public void TogglePause(){
        pause = !pause;
        PausePanel.SetActive(pause);
        if(pause){
            Time.timeScale = 0f; 
        }
        else{
            Time.timeScale = 1f;  
        }
    }

    private void Update() {
        if(Input.GetButtonDown("Cancel")){
            TogglePause();
        }
    }
}