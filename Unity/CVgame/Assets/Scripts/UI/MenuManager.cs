using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public GameObject PausePanel;

    public Button firstButtonSelected;

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
        firstButtonSelected.Select();
    }

}
