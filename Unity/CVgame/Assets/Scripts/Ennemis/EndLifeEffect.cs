using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EndLifeEffect : MonoBehaviour
{
    public GameObject effectGameObject;
    private VisualEffect effect;
    public float delayBeforeDestruction;
    public string eventName;

    private void Start() {
        effect = effectGameObject.GetComponent<VisualEffect>();
    }
    public void DestroyGameObjectWithEffect(){
        effectGameObject.transform.parent = null;
        effect.SendEvent(eventName);
        Destroy(gameObject, delayBeforeDestruction);
        Destroy(effectGameObject, delayBeforeDestruction*10.0f);
    }
}
