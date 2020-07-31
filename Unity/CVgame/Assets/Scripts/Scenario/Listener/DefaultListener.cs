using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario{
    public class DefaultListener : MonoBehaviour, ScenarioListener
    {
        public void StartListeningFor(ScenarioManager manager){
            manager.NotifyProgress(ScenarioStep.Continue);
        }

        public void StopListening(){
        }
    }
}

