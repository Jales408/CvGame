using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent( typeof(Animator))]
[RequireComponent( typeof(CharacterController))]
public class AstronauteControler : MonoBehaviour
{
    public float speed = 3f,stunTime = 1.8f,turnSmoothTime = 0.1f,gravity=1.0f;
    public Transform weaponPlacement;
    
    private bool dab, shoot, sprint, stun;
    private float turnSmoothVelocity, actualStunTime;
    private Vector2 movingDirection;
    private IWeapon weapon;
    private CharacterController controller;
    private Animator animator;

    private MenuManager menuManager;

    private DialogManager dialogManager;

    private bool isDialoguing;

    private List<IDabbable> dabbablesInRange = new List<IDabbable>();
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        menuManager = FindObjectOfType<MenuManager>();
        dialogManager = FindObjectOfType<DialogManager>();
        dialogManager.SaveInteractor(this);
    }

    public void TakeWeapon(IWeapon newWeapon){
        if(weapon!=null){
            weapon.desactivate();
        }        
        else{
            animator.SetBool("CarryingWeapon",true);
        }
        weapon = newWeapon;
        weapon.placeInHand(weaponPlacement);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(stun){
            actualStunTime-= Time.fixedDeltaTime;
            if(actualStunTime<0f){
                stun = false;
                animator.SetBool("Stun",false);
            }
            return;
        }
        Move();
        if(dab){
            dab=false;
            Dab();
        }
        if(shoot){
            shoot = false;
            if(weapon!=null){
                weapon.pullTrigger();
            }
        }
    }

    void OnDab(){
        if(Time.timeScale==0f || isDialoguing){
            return;
        }
        dab = true;
    }

    void OnShoot(){
        if(Time.timeScale==0f){
        return;
        }
        if(isDialoguing) {
            dialogManager.DisplayNextSentence();
            return;
        }
        shoot = true;
    }

    void OnSprint(){
        if(Time.timeScale==0f || isDialoguing){
            return;
        }
        sprint = !sprint;
    }

    void OnEnnemiTouch(){
        if(stun){
            return;
        }
        actualStunTime = stunTime;
        stun = true;
        animator.SetBool("Stun",true);
    }

    void OnMove(InputValue axis){
        if(Time.timeScale==0f || isDialoguing){
            
            return;      
        }
        movingDirection = axis.Get<Vector2>();
    }

    void OnPause(){
        menuManager.TogglePause();
    }

    private void Move(){
        Vector3 direction = new Vector3(movingDirection.x,0f,movingDirection.y).normalized;

        if(direction.magnitude>0f){
            float targetAngle = Mathf.Atan2(direction.x,direction.z)*Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            if(!sprint){
                direction /=2f;
            }
        }
        controller.Move((direction+ Vector3.down*gravity*9.81f)*Time.deltaTime*speed);
        animator.SetFloat("Speed",direction.magnitude);
    }

    private void Dab(){
        if(!animator.GetCurrentAnimatorStateInfo(1).IsName("Dab")){
            animator.SetTrigger("Dab");
            foreach(IDabbable dabbable in dabbablesInRange){
                dabbable.Dab(gameObject);
            }
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

    public void OnDialog(bool isActive){
        isDialoguing = isActive;
        movingDirection = new Vector2();
    }
}
