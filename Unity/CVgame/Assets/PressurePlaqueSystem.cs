using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlaqueSystem : MonoBehaviour, iTechSystem
{
    private int pressureMaxCount;
    private int pressureCount = 0;

    public event ActivationDelegate OnActivation;
    void Start()
    {
        pressureMaxCount = GetComponentsInChildren<PressurePlaque>().Length;
    }

    // Update is called once per frame
    public void HandlePressureModification(bool isPressureAdded)
    {
        pressureCount += isPressureAdded?1:-1;
        if(pressureCount==pressureMaxCount && OnActivation!=null){
            OnActivation();
        }
    }

    public void registerForActivation(ActivationDelegate activation){
        OnActivation += activation;
    }

    public void unregisterForActivation(ActivationDelegate activation){
        OnActivation -= activation;
    }
}
