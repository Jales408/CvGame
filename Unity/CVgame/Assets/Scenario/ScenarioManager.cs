using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scenario{
    public class ScenarioManager : MonoBehaviour
    {
        public float ChillTime = 2f;
        private int actualStep;
        private int actualChapter;
        private bool isRestarting;
        private int restartId =1;
        private ScenarioListener[] listeners;

        void Start()
        {
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
            if(actualChapter<transform.childCount){
                actualChapter = id;
                StartCoroutine(LetThePlayerChill(ChillTime));
            }
        }

        private void BeginStep(){

        Transform actualStepTransform = transform.GetChild(actualChapter);
        if(isRestarting)
        {;
            if(!(restartId<actualStepTransform.childCount))
            {
                return;
            }
            actualStepTransform = actualStepTransform.GetChild(restartId);
        }
        if(!(actualStep<actualStepTransform.childCount))
        {
            if(isRestarting){

            }
            else{
                
                isRestarting= false;
                actualStepTransform = transform.parent;
                actualStep=restartId;
                return;
            }
        }
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
        }
    }

    public enum ScenarioStep
    {
        Continue,
        Restart
    }
}


