using System.Collections;
using System.Collections.Generic;
using EventCallbacks;
using Scenario;
using UnityEngine;

public class SystemObjective : MonoBehaviour, iObjective 
{
    private iTechSystem system;

    private void Start() {
        system = GetComponent<iTechSystem>();
    }
    public void listenForObjective(ObjectiveListener listener){
        system.registerForActivation(ObjectiveReach);
    }
     public void resetObjective(){
        system.unregisterForActivation(ObjectiveReach);
    }
    public void ObjectiveReach(){
        EventSystem.Current.FireEvent((new ObjectiveInfo(this,true)));
    }
}
