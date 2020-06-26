using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GetDownOnPlayerDetection : MonoBehaviour
{
    private bool isPlayerDetected;

    public LayerMask playerLayer;

    public Vector3 distanceDownMax;

    public float ratioToBeDown = 0.2f;
    private Vector3 initialPosition;

    private bool goDown;
    private float maxMagnitude=1.0f;

    private List<GameObject> astronautesInRange = new List<GameObject>();
    private void Start() {
        initialPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
    }
    private void OnTriggerEnter(Collider other) {
        if (playerLayer.value == 1<<other.gameObject.layer )
        {
            astronautesInRange.Add(other.gameObject);
            maxMagnitude = (other.gameObject.transform.position-initialPosition).magnitude;
        }
    }

    private void FixedUpdate() {
        if(astronautesInRange.Count>0){
            float distanceRatio = astronautesInRange.Select((astronaute) => (astronaute.transform.position-initialPosition).magnitude/maxMagnitude).Min();
            float distanceRatioCorrected = (distanceRatio < ratioToBeDown)?1.0f:(distanceRatio-1.0f)/(ratioToBeDown-1);
            transform.position =  initialPosition - distanceDownMax*distanceRatioCorrected;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (playerLayer.value == 1<<other.gameObject.layer )
        {
            astronautesInRange.Remove(other.gameObject);
        }
    }
}
