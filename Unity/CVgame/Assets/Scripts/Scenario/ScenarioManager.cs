using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scenario{
    public class ScenarioManager : MonoBehaviour
    {
        public float ChillTime = 2f;
        private int actualStep;
        private int actualChapter = -1;
        private bool isRestarting;
        private int restartId =1;
        private ScenarioListener[] listeners;

        private HashSet<int> endedChapters;

        void Start()
        {
            endedChapters = new HashSet<int>();
            StartChapter(0);
        }

         IEnumerator LetThePlayerChill(float waitTime)
        {
         yield return new WaitForSeconds(waitTime);
         BeginStep();
        }

        private void ReplaceAndSetListenersStep(Transform actualStepTransform){
            if(listeners!=null){
                foreach(ScenarioListener listener in listeners){
                listener.StopListening();
                }
            }
            listeners = actualStepTransform.GetComponents<ScenarioListener>();
             foreach(ScenarioListener listener in listeners)
                {
                listener.StartListeningFor(this);
                }
        }



        public void NotifyProgress(ScenarioStep nextStep){
            if(nextStep == ScenarioStep.Continue){
                actualStep++;
                BeginStep();
            }
            else{
                RestartChapter();
            }

        }

        public void StartChapter(int id){
            if(id==actualChapter || endedChapters.Contains(id)){
                return;
            }
            if(actualChapter<transform.childCount){
                actualChapter = id;
                actualStep = 0;
                StartCoroutine(LetThePlayerChill(ChillTime));
            }
        }

        public void EndChapter(){
            endedChapters.Add(actualChapter);
        }

        private void BeginStep(){

        Transform actualStepTransform = transform.GetChild(actualChapter);
        if(isRestarting)
        {
            actualStepTransform = actualStepTransform.GetChild(restartId);
        }
        if(!(actualStep<actualStepTransform.childCount))
        {
            if(isRestarting){
                isRestarting= false;
                actualStepTransform = actualStepTransform.parent;
                actualStep=restartId;
            }
        }
        Debug.Log("SCENAR" + actualStep + " : " + isRestarting);
        actualStepTransform = actualStepTransform.GetChild(actualStep);
        TriggerStep(actualStepTransform);
        ReplaceAndSetListenersStep(actualStepTransform);
        }

        private void TriggerStep(Transform actualStepTransform){
        foreach(ScenarioTrigger trigger in actualStepTransform.GetComponents<ScenarioTrigger>())
            {
            trigger.Trigger();
            }
        }

        private void RestartChapter(){
            isRestarting=true;
            actualStep = 0;
            BeginStep();
        }
    }

    public enum ScenarioStep
    {
        Continue,
        Restart
    }
}


