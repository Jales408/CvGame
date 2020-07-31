using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Tourelle : MonoBehaviour, iLazerable
{
    
    public float maxCharge = 2.0f;
    [GradientUsageAttribute(true)] public Gradient colorOverCharge;
    [GradientUsageAttribute(true)] public Gradient colorOverDisCharge;
    [GradientUsageAttribute(true)] public Gradient colorOverCooldown;
    [GradientUsageAttribute(true)] private Gradient actualColor;
    public float initialVerticalChargePosition = 0.55f;
    public float chargeHigh = 0.4f;
    public float dischargePerSecondAmount = 0.2f;

    public float coolDownTime = 5f;
    private float actualCooldownTime = 0f;
    private float charge = 0.0f;

    public GameObject[] visualChargeObjects;
    private Renderer[] visualChargeRenderers;

    private InteractableState state = InteractableState.Ready;

    [Space(20)]

    private TechLaser techLaser;

    public float timeShooting;


    public void Lazer(float amount){
        if(state!=InteractableState.Ready){
            return;
        }
        charge += amount;
        if(charge > maxCharge){
            charge=maxCharge;
            state = InteractableState.Proccesing;
            actualColor = colorOverDisCharge;
        }
        techLaser = GetComponent<TechLaser>();
    }
    void Start()
    {
        actualColor = colorOverCharge;
        visualChargeRenderers = new Renderer[visualChargeObjects.Length];
        for (int i = 0; i < visualChargeObjects.Length; i++)
        {
            visualChargeRenderers[i] = visualChargeObjects[i].GetComponent<Renderer>();
        }
    }

    private void Update()
    {
        switch (state)
        {
            case InteractableState.Ready :
                charge = Mathf.Max(charge - dischargePerSecondAmount * Time.deltaTime,0f);
                break;
            case InteractableState.Proccesing :
                charge = Mathf.Max(charge - dischargePerSecondAmount*50f * Time.deltaTime,0f);
                if(charge==0f){
                    state = InteractableState.Cooling;
                    actualColor = colorOverCooldown;
                    actualCooldownTime = 0f;
                    StartCoroutine(LaunchLaserEffect(timeShooting));
                }
                break;
            case InteractableState.Cooling :
                actualCooldownTime+= Time.deltaTime;
                if(actualCooldownTime>coolDownTime){
                    state = InteractableState.Ready;
                    actualColor = colorOverCharge;
                }
                break;
            default:
                break;
        }
        foreach(Renderer renderer in visualChargeRenderers){
            renderer.material.SetColor("_GlowColor", actualColor.Evaluate(charge/maxCharge));
            Vector3 localPosition = renderer.transform.localPosition;
            renderer.transform.localPosition = new Vector3(localPosition.x,localPosition.y,initialVerticalChargePosition + chargeHigh* charge/maxCharge); 
        }

        
    }

    IEnumerator LaunchLaserEffect(float timeShooting){
        techLaser.PullTrigger();
        yield return new WaitForSeconds(timeShooting);
        techLaser.ReleaseTrigger();
    }
}