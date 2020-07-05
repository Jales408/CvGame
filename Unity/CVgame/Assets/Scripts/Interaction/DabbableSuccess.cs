using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DabbableSuccess : MonoBehaviour, IDabbable
{
    public string identifier;
    public void Dab(GameObject dabAuthor){
        TropheesStat tStats = TropheesStat.getInstance();
        tStats.unlockTropheeItem(identifier);
    }
}
