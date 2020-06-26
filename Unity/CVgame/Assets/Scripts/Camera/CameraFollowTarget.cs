using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public bool isPositionOffsetBasedOnScene = true;
    public Vector3 positionOffset;
    // Start is called before the first frame update
    [Space(20)]
    public GameObject target;
    void Start()
    {
        if(isPositionOffsetBasedOnScene){
            positionOffset = transform.position - target.transform.position;
        }
    }

    void FixedUpdate()
    {
        transform.position = target.transform.position + positionOffset;
    }
}
