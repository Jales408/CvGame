using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DabbableOpeningDoor : MonoBehaviour, IDabbable
{
    public GameObject visualEffect;

    public MoveSideWay doorController;
    public void Dab(GameObject dabAuthor){
        visualEffect.SetActive(false);
        doorController.SmoothlyMoveObjects();
    }
}
