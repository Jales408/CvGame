using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    void pullTrigger();
    void desactivate();
    void placeInHand(Transform hand);
}

