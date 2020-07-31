using System;
using System.Collections;
using System.Collections.Generic;
using EventCallbacks;
using Scenario;
using UnityEngine;
using UnityEngine.Playables;

public class LazerableDirector : MonoBehaviour, iLazerable,iObjective
{
    private float charge;
    public float maxCharge = 4.0f;
    [GradientUsageAttribute(true)] public Gradient colorOverCharge;
    public float dischargePerSecondAmount = 0.2f;
    private Renderer mRenderer;
    private bool isListening;

    public void Lazer(float amount){
        if(!isListening){
            return;
        }
        charge += amount;
        if(charge > maxCharge){
            TriggerDirector();
        }
    }

    private void TriggerDirector()
    {
        GetComponent<PlayableDirector>().Play();
        EventSystem.Current.FireEvent(new ObjectiveInfo(this,true));
    }

    private void Start() {
        mRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if(charge >0f){
            charge = Mathf.Max(charge - dischargePerSecondAmount * Time.deltaTime,0f);
        }
        mRenderer.materials[1].SetColor("_GlowColor", colorOverCharge.Evaluate(charge/maxCharge));
    }

    public void listenForObjective(ObjectiveListener listener)
    {
        isListening = true;
    }

    public void resetObjective()
    {
        isListening = false;
    }
}
