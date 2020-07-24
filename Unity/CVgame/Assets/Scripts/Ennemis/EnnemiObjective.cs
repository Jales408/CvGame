using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiObjective : MonoBehaviour
{
    public bool isMoving;
    private bool isAvaillable = true;

    public void ennemiHasTouch(){
        SendMessage("OnEnnemiTouch");
    }

    public void SetActive(bool active){
        isAvaillable = active;
    }

    public bool GetAvaillability(){return(isAvaillable);}
}
