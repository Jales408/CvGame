using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scenario;
using EventCallbacks;

public class RobotObjective : MonoBehaviour, iObjective
{
    private int robotCount;
    private int robotObjectiveCount;
    private ObjectiveListener objectiveListener;
    public void listenForObjective(ObjectiveListener listener){
        objectiveListener = listener;
    }

     public void resetObjective(){
        objectiveListener = null;
        robotCount=0;
    }

    private void Start() {
        EventSystem.Current.RegisterListener<KillInfo>(OnRobotKilled);
        robotObjectiveCount = transform.childCount;
    }

    public void OnRobotKilled(KillInfo info){
        if(info.type == EnnemiType.Robot && ++robotCount==robotObjectiveCount){
            AllRobotsKilled();
        }
    }

    public void AllRobotsKilled(){
        EventSystem.Current.FireEvent(new ObjectiveInfo(this,true));
    }
}
