using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechLaser :  MonoBehaviour, IWeapon
{

    public float damage = 1f;
    public GameObject actualLaser;

    public Transform effectPosition;
    private bool isShooting;
    public LineRenderer lineEffect;
    public GameObject impactEffect;

    public GameObject raycastStart;

    private void OnEnable() {
        ReleaseTrigger();
    }
    public void PullTrigger(){
        isShooting = true;
        actualLaser.SetActive(true);
    }

    public void ReleaseTrigger(){
        isShooting = false;
        actualLaser.SetActive(false);
    }

    public void Desactivate(){
        Destroy(this.gameObject);
    }

    public void PlaceInHand(Transform hand){
        transform.parent = hand;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void FixedUpdate() {
        
        if(isShooting){
            RaycastHit hit;
            Vector3 straightDirection = effectPosition.forward;
            straightDirection.y = 0f;
            if(Physics.Raycast(raycastStart.transform.position,straightDirection,out hit))
            {
                lineEffect.SetPosition(1,effectPosition.InverseTransformPoint(hit.point));
                impactEffect.transform.position = hit.point;
                if(hit.collider){
                    iLazerable lazerable = hit.collider.gameObject.GetComponent<iLazerable>();
                    if(lazerable != null){
                        lazerable.Lazer(Time.fixedDeltaTime * damage);
                    }
                }
            }
            else{
                Vector3 positionFarAway = raycastStart.transform.position + straightDirection*5000;
                lineEffect.SetPosition(1,effectPosition.InverseTransformPoint(positionFarAway));
                impactEffect.transform.position = positionFarAway;
            }
        }
    }
}

