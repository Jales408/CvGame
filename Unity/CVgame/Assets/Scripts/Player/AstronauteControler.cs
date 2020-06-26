using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Animator))]
[RequireComponent( typeof(CharacterController))]
public class AstronauteControler : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;

    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    private List<IDabbable> dabbablesInRange = new List<IDabbable>();
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Input.GetButtonDown("Jump")){
            Dab();
        }
    }

    private void Move(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;

        if(direction.magnitude>0f){
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            controller.Move(direction*Time.deltaTime*speed);
        }
        animator.SetFloat("Speed",direction.magnitude);
    }

    private void Dab(){
        animator.SetTrigger("Dab");
        foreach(IDabbable dabbable in dabbablesInRange){
            dabbable.Dab(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        IDabbable dabbable = other.gameObject.GetComponent<IDabbable>();
        if(dabbable!=null){
            dabbablesInRange.Add(dabbable);
        }
    }

    private void OnTriggerExit(Collider other) {
        IDabbable dabbable = other.gameObject.GetComponent<IDabbable>();
        if(dabbable!=null){
            dabbablesInRange.Remove(dabbable);
        }
    }

}
