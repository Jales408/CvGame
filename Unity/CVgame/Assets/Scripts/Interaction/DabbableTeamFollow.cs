using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DabbableTeamFollow : MonoBehaviour, IDabbable
{
    public IAAstonauteController controller;

    public void Dab(GameObject dabAuthor){
        controller.StartFollow(dabAuthor);
        Destroy(this);
    }
}
