using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    private int chapter;
    private Dictionary<int,Vector3> cameraPositions;
    public bool isPositionOffsetBasedOnScene = true;
    public Vector3 positionOffset;

    public float relativePositionFactor = 0.25f;
    // Start is called before the first frame update
    [Space(20)]
    public GameObject target;
    void Start()
    {
        cameraPositions = new Dictionary<int, Vector3>();
        CameraPositionning[] cameraPositionnings = FindObjectsOfType<CameraPositionning>();
        foreach(CameraPositionning cameraPositionning in cameraPositionnings){
            cameraPositions.Add(cameraPositionning.chapter,cameraPositionning.transform.position);
        }
        if(isPositionOffsetBasedOnScene){
            positionOffset = transform.position - target.transform.position;
        }
    }

    void FixedUpdate()
    {
        Vector3 differencePosition = target.transform.position - cameraPositions[chapter];
        Vector3 relativePosition = differencePosition * relativePositionFactor;

        transform.position = cameraPositions[chapter] + positionOffset + relativePosition;
    }

    public void ChangeChapter(int chapter){
        this.chapter = chapter;
    }
}
