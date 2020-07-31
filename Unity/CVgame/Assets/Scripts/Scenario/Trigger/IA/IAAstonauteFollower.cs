using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAAstonauteFollower : MonoBehaviour
{

    public int followingDistance;
 
    public int precision;

    private List<Vector3> successivesPositions = new List<Vector3>();

    private bool isFollowing;

    private float stepSize;
    private int numberOfFollowers;

    void Start()
    {
        stepSize = ((float)followingDistance)/precision;
    }

    public int FollowTarget(GameObject follower){
        isFollowing = true;
        Vector3 initialPosition = follower.transform.position;
        Vector3 targetPosition = (successivesPositions.Count==0)?transform.position:successivesPositions[0];
        numberOfFollowers++;
        int stepCountToAdd = precision;
        int stepOnTheWay = Mathf.Min(Mathf.CeilToInt((initialPosition-targetPosition).magnitude/stepSize),stepCountToAdd);
        Vector3 direction = (targetPosition-initialPosition).normalized;
        
        if(stepOnTheWay!=stepCountToAdd){
            successivesPositions.InsertRange(0,Enumerable.Repeat(initialPosition,stepCountToAdd-stepOnTheWay));
        }
        for (int i = 0; i < stepOnTheWay; i++)
        {
            successivesPositions.Insert(0,targetPosition-i*direction*stepSize);
        }

        return(numberOfFollowers);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isFollowing){
                    ActualizePosition();
        }
    }

    private void ActualizePosition(){
        if((successivesPositions.Last() - transform.position).magnitude>stepSize)
        {
            successivesPositions.RemoveAt(0);
            successivesPositions.Add(transform.position);
        }
    }

    public Vector3 getNextPosition(int positionFollowing){
    return successivesPositions[(numberOfFollowers - positionFollowing)*precision];
    }

    public float getFollowedDistance(){
        return stepSize;
    }
}
