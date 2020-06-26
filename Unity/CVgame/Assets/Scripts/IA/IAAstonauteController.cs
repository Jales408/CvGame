using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent( typeof(Animator))]
[RequireComponent( typeof(CharacterController))]
[RequireComponent( typeof(IAAstonauteFollower))]
public class IAAstonauteController : MonoBehaviour
{
    private CharacterController controller;
    private IAAstonauteFollower IAfollower;
    private Animator animator;

    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    private bool isFollowingTarget;
    private bool isStaticPositionReach;
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        IAfollower = GetComponent<IAAstonauteFollower>();
    }

    public void StartFollow(GameObject target, int placeInQueue){
        isFollowingTarget = true;
        IAfollower.FollowTarget(target, placeInQueue);
    }

    public void StopFollow(Vector3 positionToKeep){
        isFollowingTarget = false;
        IAfollower.KeepPosition(positionToKeep);
    }

    // Update is called once per frame
    void Update()
    {
        if(isFollowingTarget){
            MoveToNextPosition();
        }
        else{
            if(!isStaticPositionReach){
                MoveToNextPosition();
            }
        }       
    }

    private void MoveToNextPosition(){
        Vector3 followedPosition = IAfollower.getNextPosition();
        Vector3 positionGap = followedPosition - transform.position;
        float minDistanceToMove = IAfollower.getFollowedDistance();
        if(positionGap.magnitude>minDistanceToMove){
            Vector3 direction = positionGap.normalized;
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            controller.Move(direction*Time.deltaTime*speed);
            animator.SetFloat("Speed",1f);
        }
        else{
            
            animator.SetFloat("Speed",0f);
            isStaticPositionReach = true;
        } 
    }
}
