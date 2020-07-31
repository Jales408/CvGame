using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    void PullTrigger();
    void ReleaseTrigger();
    void Desactivate();
    void PlaceInHand(Transform hand);
}

