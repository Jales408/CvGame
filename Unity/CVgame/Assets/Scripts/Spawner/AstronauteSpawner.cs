using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

public class AstronauteSpawner : MonoBehaviour, iSpawner
{
    private IAAstonauteController[] astronautes;
    private int activationCount;
    private void Start() {
        astronautes = GetComponentsInChildren<IAAstonauteController>();
        activationCount=0;
    }
    public void SpawnUnit(){
        //Set up in the editor
    }
    public void CleanUnit(){
        //Nothing to clean
    }

    public void NotifyActivation(){
        if(++activationCount == astronautes.Length){
            SpawnerInfo sInfo = new SpawnerInfo();
            sInfo.EventDescription = "Astronautes are gathered";
            sInfo.hasEnded = true;
            sInfo.spawner = this;
            EventCallbacks.EventSystem.Current.FireEvent(sInfo);
        }
    }
}
