using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideWay : MonoBehaviour
{
    public GameObject[] objectsToMoveRight;
    public GameObject[] objectsToMoveLeft;
    public Vector3 rightDirection = new Vector3(0,0,1);
    [Space(20)]

    public float movingTime;
    public float movingDistance;

    private float movingSpeed;

    private float movingTimePass;

    private bool isMoving;
    private bool isOpen;

    private void Start() {
        movingSpeed = movingDistance/movingTime;
    }
    private void FixedUpdate() {
        if(isMoving){
            movingTimePass+=Time.fixedDeltaTime;
            Vector3 movingPartielDistance = (isOpen?1:-1)*rightDirection * Time.fixedDeltaTime * movingSpeed;
            foreach (GameObject objectR in objectsToMoveRight)
            {
                objectR.transform.position+= movingPartielDistance;
            }
            foreach (GameObject objectL in objectsToMoveLeft)
            {
                objectL.transform.position-= movingPartielDistance;
            }
            if(movingTimePass>movingTime){
                isMoving = false;
                isOpen = !isOpen;
            }
        }
        else{
        }
    }
    
    public void SmoothlyMoveObjects(){
        movingTimePass = 0f;
        isMoving = true;
    }
}
