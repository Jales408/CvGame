using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventCallbacks;


namespace Scenario{
    public class DialogListener : MonoBehaviour, ScenarioListener
    {
        private ScenarioManager currentManager;
        private int keyOfEventListener;
        public EndDialogInfo dialogInfo;
        public ScenarioStep nextStep;
        
        public void StartListeningFor(ScenarioManager manager){
            keyOfEventListener = EventSystem.Current.RegisterListener<EndDialogInfo>(CompareInfo);
            currentManager = manager;
        }

        public void CompareInfo(EndDialogInfo info){
            NotifyManager();
        }

        public void NotifyManager(){
            currentManager.NotifyProgress(nextStep);
        }
        public void StopListening(){
            EventSystem.Current.UnregisterListener<EndDialogInfo>(keyOfEventListener);
        }
    }

}
