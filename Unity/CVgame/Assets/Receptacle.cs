using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Receptacle : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Transform weaponPlacement;
    public VisualEffect activationEffect;
    private IWeapon weapon;
    public float rotationSpeed;
    void Start()
    {
        GetComponent<iTechSystem>().registerForActivation(PlaceWeapon);
    }

    public void PlaceWeapon(){
        weapon = Instantiate(weaponPrefab,weaponPlacement).GetComponent<IWeapon>();
        activationEffect.Play();
    }

    public IWeapon GetWeapon(){
        IWeapon returnedWeapon = weapon;
        weapon = null;
        return weapon;
    }
    void FixedUpdate()
    {
        if(weapon!=null){
            weaponPlacement.Rotate(0f,rotationSpeed * Time.fixedDeltaTime,0f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        AstronauteControler aController = other.gameObject.GetComponent<AstronauteControler>();
        if(weapon!=null && aController!=null){
            aController.TakeWeapon(weapon);
            weapon = null;
        }
    }
}
