using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scenario{
    public class TimeListener : MonoBehaviour, ScenarioListener
    {
        private ScenarioManager currentManager;
        public float timeWaiting;
        public ScenarioStep nextStep;
        public void StartListeningFor(ScenarioManager manager){
            StartCoroutine(WaitFor(timeWaiting));
            currentManager = manager;
        }

        IEnumerator WaitFor(float time){
            yield return new WaitForSeconds(time);
            NotifyManager();
        }

        public void NotifyManager(){
            currentManager.NotifyProgress(nextStep);
        }
        public void StopListening(){
            StopAllCoroutines();
        }
    }
}

