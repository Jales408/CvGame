using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ActivationDelegate();
public interface iTechSystem
{
    void registerForActivation(ActivationDelegate activation);

    void unregisterForActivation(ActivationDelegate activation);
}
