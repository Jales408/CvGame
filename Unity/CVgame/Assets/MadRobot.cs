using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

public class MadRobot : MonoBehaviour, iLazerable
{
    private Collider bCollider;
   
    private Animator animator;

    public float life = 1;

    public float timeBeforeDesactivation = 0.5f;

    public bool destroyed;

    void Start()
    {
        bCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    private void Explode(){
        EventSystem.Current.FireEvent(new KillInfo(EnnemiType.Robot));
        animator.SetBool("Explode",true);
        Destroy(bCollider,timeBeforeDesactivation);
    }

    public void Lazer(float amount){
        life-=amount;
        if(life<0f && !destroyed){
            destroyed = true;
            Explode();
        }
    }
}
