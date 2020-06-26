using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DabbableTeamFollow : MonoBehaviour, IDabbable
{
    public GameObject visualEffect;

    public List<IAAstonauteController> followers = new List<IAAstonauteController>();
    public void Dab(GameObject dabAuthor){
        visualEffect.SetActive(false);
        for(int i=0; i<followers.Count;i++){
            followers[i].StartFollow(dabAuthor,i+1);
        }
        Destroy(this);
    }
}
