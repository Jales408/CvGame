using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    public float speed = 10f;
    public bool trigonometricDirection = false;

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0f,0f,transform.rotation.eulerAngles.z +((trigonometricDirection)?1:-1)* speed*Time.unscaledDeltaTime);
    }
}
