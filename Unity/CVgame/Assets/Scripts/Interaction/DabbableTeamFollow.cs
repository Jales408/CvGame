using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DabbableTeamFollow : MonoBehaviour, IDabbable
{
    public GameObject visualEffect;

    public IAAstonauteController controller;

    public void Dab(GameObject dabAuthor){
        visualEffect.SetActive(false);
        visualEffect.SetActive(true);
        controller.StartFollow(dabAuthor);
    }
}
