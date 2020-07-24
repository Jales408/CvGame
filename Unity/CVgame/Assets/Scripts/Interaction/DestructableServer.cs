using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableServer : MonoBehaviour
{
    public float life = 10;

    void OnEnnemiTouch(){
        life--;
        if(life==0f){
            //destroy
        }
    }
}
