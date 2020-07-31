using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;

namespace Scenario{
    public class SpawnerListener : MonoBehaviour, ScenarioListener
    {
        private ScenarioManager currentManager;
        private int keyOfEventListener;
        public EndDialogInfo dialogInfo;
        public ScenarioStep nextStep;
        
        public void StartListeningFor(ScenarioManager manager){
            keyOfEventListener = EventSystem.Current.RegisterListener<SpawnerInfo>(CompareInfo);
            currentManager = manager;
        }

        public void CompareInfo(SpawnerInfo info){
            if(info.hasEnded){
                NotifyManager();
            }
        }

        public void NotifyManager(){
            currentManager.NotifyProgress(nextStep);
        }
        public void StopListening(){
            EventSystem.Current.UnregisterListener<SpawnerInfo>(keyOfEventListener);
        }
    }
}
