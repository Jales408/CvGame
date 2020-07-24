using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeDetector : MonoBehaviour
{
    public EnnemiController controler;
    private void OnTriggerEnter(Collider other) {
        EnnemiObjective objective = other.gameObject.GetComponent<EnnemiObjective>();
        if( objective!=null){
            controler.changeObjective(other.transform,objective.isMoving);
        }
    }
}
