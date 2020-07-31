using System;
using System.Collections;
using System.Collections.Generic;
using EventCallbacks;
using Scenario;
using UnityEngine;

public class DestructableServer : MonoBehaviour, iObjective
{
    private float life = 10;

    public float maxLife = 10;

    public Vector3 initialPosition,finalPosition;

    public GameObject risingGameObject;

    public float timeRising;

    [GradientUsage(true)] public Gradient lifeColor;

    private float actualTimeRising;
    private bool isRising;
    private Vector3 directionRising;
    private bool isFalling;

    public Renderer lRenderer;

    private bool isListening;

    public void listenForObjective(ObjectiveListener listener)
    {
        
        actualTimeRising=0f;
        isRising = true;
        life = maxLife;
        lRenderer.materials[1].SetColor("_GlowColor", lifeColor.Evaluate(life/maxLife));
        isListening=true;
    }

    public void resetObjective()
    {
        isListening=false;
        lRenderer.materials[1].SetColor("_GlowColor", lifeColor.Evaluate(life/maxLife));
    }

    void OnEnnemiTouch(){
        life--;
        if(life==0f){
            if(isListening){
                ObjectiveReach(false);
            }

        }
        lRenderer.materials[1].SetColor("_GlowColor", lifeColor.Evaluate(life/maxLife));
    }

    private void Update() {
        if(isRising){
            actualTimeRising += Time.deltaTime;
            if(actualTimeRising>=timeRising){
                actualTimeRising=timeRising;
                if(isListening){
                    ObjectiveReach(true);
                }
            }
        }
        else if(isFalling){
            actualTimeRising -= Time.deltaTime * 4f;
            if(actualTimeRising<=0f){
                actualTimeRising=0f;
                isFalling = false;
            }
        }
        else{
            return;
        }
        risingGameObject.transform.localPosition = initialPosition + directionRising * (actualTimeRising/timeRising);
    }

    private void ObjectiveReach(bool success)
    {
        EventSystem.Current.FireEvent(new ObjectiveInfo(this,success));
        isRising = false;
        if(success){
            life = maxLife;
        }
        else{
            isFalling = true;
        }
    }

    private void Start() {
        directionRising = finalPosition - initialPosition;

    }
}
