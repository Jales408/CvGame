using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAAstonauteFollower : MonoBehaviour
{
    public GameObject targetToFollow;
    [Space(10)]
    public float stepBehindLenght;

    public int precision;

    private List<Vector3> successivesPositions = new List<Vector3>();

    private bool isFollowing;

    private float stepSize;

    private Vector3 staticPosition;
    void Start()
    {
        staticPosition = transform.position;
        stepSize = stepBehindLenght/precision;
    }

    public void FollowTarget(GameObject target, int placeInQueue){
        isFollowing = true;
        targetToFollow = target;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = target.transform.position;
        int stepCount = placeInQueue*precision;
        int stepOnTheWay = Mathf.Min(Mathf.CeilToInt((initialPosition-targetPosition).magnitude/stepSize),stepCount);
        Vector3 direction = (targetPosition-initialPosition).normalized;
        successivesPositions = new List<Vector3>();
        if(stepOnTheWay!=stepCount){
            successivesPositions.AddRange(Enumerable.Repeat(initialPosition,stepCount-stepOnTheWay));
        }
        
        for (int i = stepOnTheWay-1; i >= 0 ; i--)
        {
            successivesPositions.Add(targetPosition-i*direction*stepSize);
        }
    }

    public void KeepPosition(Vector3 positionToKeep){
        isFollowing =false;
        staticPosition = positionToKeep;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isFollowing){
                    ActualizePosition();
        }
    }

    private void ActualizePosition(){
        if((successivesPositions.Last() - targetToFollow.transform.position).magnitude>stepSize)
        {
            successivesPositions.RemoveAt(0);
            successivesPositions.Add(targetToFollow.transform.position);
        }
    }

    public Vector3 getNextPosition(){
        if(isFollowing){
            return successivesPositions[0];
        }
        else{
            return staticPosition;
        }

    }

    public float getFollowedDistance(){
        return stepSize;

    }
}
