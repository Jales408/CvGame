using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlaque : MonoBehaviour
{
    private bool canBeModified = true;
    public int activationMaterialIndex = 1;
    public Renderer activationRenderer;

    public float desactivationTime = 1f;

    public bool On;

    public LayerMask playerLayer;

    [ColorUsage(true,true)]public Color activatedColor;
    [ColorUsage(true,true)]public Color unactivatedColor;

    private PressurePlaqueSystem system;
    private List<Collider> collidersOnPlaque;
    void Start()
    {
        activationRenderer.materials[activationMaterialIndex].SetColor("_GlowColor",unactivatedColor);
        collidersOnPlaque = new List<Collider>();
        system = GetComponentInParent<PressurePlaqueSystem>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other) {
        if(!canBeModified){
            return;
        }
        if (playerLayer.value == 1<<other.gameObject.layer )
        {
            collidersOnPlaque.Add(other);
            if(collidersOnPlaque.Count==1){
                setPlaqueOn();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(!canBeModified){
            return;
        }
        if (playerLayer.value == 1<<other.gameObject.layer )
        {
            collidersOnPlaque.Remove(other);
            if(collidersOnPlaque.Count ==0){
                StartCoroutine(WaitAndSetPlaqueOff(desactivationTime));
            }
        }
    }

    private void setPlaqueOn(){
        if(On){
            StopAllCoroutines();
            return;
        }
        On = true;
        activationRenderer.materials[activationMaterialIndex].SetColor("_GlowColor",activatedColor);
        system.HandlePressureModification(true);
    }

    private void setPlaqueOff(){
        On = false;
        activationRenderer.materials[activationMaterialIndex].SetColor("_GlowColor",unactivatedColor);
        system.HandlePressureModification(false);
    }

    IEnumerator WaitAndSetPlaqueOff(float timeWaiting){
        yield return new WaitForSeconds(timeWaiting);
        setPlaqueOff();
    }

    public void AllowModification(bool isAllowed){
        canBeModified = isAllowed;
    }
}
