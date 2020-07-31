using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent( typeof(Animator))]
[RequireComponent( typeof(CharacterController))]
public class IAAstonauteController : MonoBehaviour
{
    private CharacterController controller;
    private IAAstonauteFollower IAfollower;
    private Animator animator;

    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;
    private bool isFollowingTarget;
    private bool isStaticPositionReach = true;

    private Vector3 positionToKeep;

    private int positionFollowing;

    [Space(20)]
    public string[] randomIdleName;

    private int actualIdle = 0;
    public float randomIdleTimeMax = 10.0f;

    private IEnumerator idleCoroutine;

    public AstronauteSpawner spawner;

    IEnumerator changeIdle(float time){
        
        animator.SetBool(randomIdleName[actualIdle],false);
        actualIdle = Random.Range(0,randomIdleName.Count());
        animator.SetBool(randomIdleName[actualIdle],true);
        yield return new WaitForSeconds(time);
        idleCoroutine = changeIdle(Random.Range(0.4f*randomIdleTimeMax,randomIdleTimeMax));
        StartCoroutine(idleCoroutine);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        positionToKeep = transform.position;
        idleCoroutine = changeIdle(0.0f);
        StartCoroutine(idleCoroutine);
        spawner = GetComponentInParent<AstronauteSpawner>();
    }

    public void StartFollow(GameObject target){
        if(!isFollowingTarget){
            spawner.NotifyActivation();
            isFollowingTarget = true;
            IAfollower = target.GetComponent<IAAstonauteFollower>();
            positionFollowing = IAfollower.FollowTarget(this.gameObject);
            //effect
            StopCoroutine(idleCoroutine);
            animator.SetBool(randomIdleName[actualIdle],false);
        }
    }

    public void StopFollow(Vector3 positionToKeep){
        isFollowingTarget = false;
        StartCoroutine(changeIdle(0.0f));
    }

    // Update is called once per frame
    void Update()
    {
            if(!isStaticPositionReach||isFollowingTarget){
                MoveToNextPosition();
            }    
    }

    private void MoveToNextPosition(){
        Vector3 followedPosition =(isFollowingTarget)?IAfollower.getNextPosition(positionFollowing): positionToKeep;
        Vector3 positionGap = followedPosition - transform.position;
        float minDistanceToMove = speed/5f;
        if(positionGap.magnitude>minDistanceToMove){
            Vector3 direction = positionGap.normalized;
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            controller.Move(direction*Time.deltaTime*speed);
            animator.SetFloat("Speed",controller.velocity.magnitude);
        }
        else{
            animator.SetFloat("Speed",0f);
            isStaticPositionReach = true;
        } 
    }
}
