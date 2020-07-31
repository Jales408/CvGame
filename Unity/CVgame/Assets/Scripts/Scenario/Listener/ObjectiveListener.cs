using System.Collections;
using System.Collections.Generic;
using EventCallbacks;
using UnityEngine;

namespace Scenario{


    public class ObjectiveListener : MonoBehaviour, ScenarioListener
    {
        public GameObject objectiveContainer;
        private int keyOfEventListener;
        private iObjective objective;
        private ScenarioManager currentManager;
        public void StartListeningFor(ScenarioManager manager){
            currentManager = manager;
            objective = objectiveContainer.GetComponent<iObjective>();
            keyOfEventListener = EventSystem.Current.RegisterListener<ObjectiveInfo>(CompareInfo);
            objective.listenForObjective(this);
        }

        public void CompareInfo(ObjectiveInfo info){
            if(info.objective == objective){
                currentManager.NotifyProgress(info.success?ScenarioStep.Continue:ScenarioStep.Restart);
            }
        }
        public void StopListening(){
            objective.resetObjective();
        }
    }
}