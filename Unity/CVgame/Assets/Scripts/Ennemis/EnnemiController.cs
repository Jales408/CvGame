using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{

    public float speed = 10.0f;

    public float distancefromTargetToExpload = 0.5f;
    public float gravity = 9.81f;
    public float stunningTime = 5.0f;
    private Rigidbody rb;
    private Vector3 movement;
    private bool isStunned;
    private bool shouldMove = true;
    private bool targetCanMove = true;

    private bool isFollowingDirectly;
    private float movingTime;
    public float refreshTargetTime;
    private GridPathFinder pathFinder;
    private Transform target;
    public List<Vector3> path; //should be private
    private Vector3 nextPosition;

    private Animator animator;

    private bool isInitialize = false;

    private float turnSmoothVelocity;

    private float YOffset = 90f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    public void Initialize(GridPathFinder pathFinder){
        this.pathFinder = pathFinder;
        chooseRandomObjective();
        isInitialize = true;
    }

    void FixedUpdate()
    {
        if(!isInitialize)
        {
            return;
        }
        if(!isStunned){
            if(shouldMove){
                movingTime+=Time.deltaTime;
                moveCharacter();
            }
            Vector3 targetPosition = target.transform.position;
            Vector3 position = transform.position;
            targetPosition.y = 0;
            position.y = 0;
            if((targetPosition-position).magnitude<distancefromTargetToExpload){
                Explode();
            }
        }
        if(movingTime>refreshTargetTime && targetCanMove){
            movingTime =0;
            refreshPath();
        }
    }

    private void Explode(){
        speed/=2f;
        animator.SetBool("Explode",true);
    }

    private void CheckExplosionContact(){
        Vector3 targetPosition = target.transform.position;
        Vector3 position = transform.position;
        targetPosition.y = 0;
        position.y = 0;
        if((targetPosition-position).magnitude<distancefromTargetToExpload*1.1f){
            target.GetComponent<EnnemiObjective>().ennemiHasTouch();
        }
    }

    private void refreshPath(){
        path = pathFinder.findPath(transform.position,target.transform.position);
            if(path.Count>1){
                nextPosition = path[path.Count-1];
                isFollowingDirectly = false;
            }
            else{
                isFollowingDirectly = true;
            }
    }

    void moveCharacter()
    {
        
        Vector3 positionDifference = ((isFollowingDirectly)? target.position:nextPosition)-transform.position;
        positionDifference.y = 0f;
        if(positionDifference.magnitude<1.0f){
            if(path.Count>1){
                path.RemoveAt(path.Count-1);
                nextPosition = path[path.Count-1];
            }
            else{
                refreshPath();
            }
        }
        rb.MovePosition(transform.position + (positionDifference.normalized * speed * Time.fixedDeltaTime));
        float targetAngle = Mathf.Atan2(positionDifference.x,positionDifference.z)*Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, 0.01f);
        transform.rotation = Quaternion.Euler(0f,angle+YOffset,0f);
    }

    public void PushAndStun(Vector3 direction, float force){
        rb.AddForce(direction *force);
        isStunned = true;
        StartCoroutine(WaitAndWakeUp(stunningTime*force));
    }

    IEnumerator WaitAndWakeUp(float time){
        yield return new WaitForSeconds( time);
        isStunned = false;
    }

    public void changeObjective(Transform target, bool isMoving){
        this.target = target;
        this.targetCanMove = isMoving;
        refreshPath();
    }

    public void chooseRandomObjective(){
        EnnemiObjective objective = pathFinder.selectRandomObjective();
        if(objective!=null){
            this.target = objective.transform;
            this.targetCanMove = objective.isMoving;
            refreshPath();
        }
    }
}
