using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillRandomly : MonoBehaviour
{
    private Image image;
    private bool isFilling;
    private bool isUnfilling;

    public float timeFilling = 0.5f;
    public float averageTimeBetweenFill = 5.0f;

    private float actualtimeBeforeFill;
    private float actualTimeFill;

    private void Start() {
        image = GetComponent<Image>();
        actualtimeBeforeFill = averageTimeBetweenFill * Random.Range(0.6f,1.4f);
        image.fillAmount = 0f;
    }
    

    private void Update(){
        if(!isFilling && !isUnfilling){
            actualtimeBeforeFill-= Time.deltaTime;
            if(actualtimeBeforeFill<0f){
                isFilling = true;
                actualTimeFill = timeFilling;
                image.fillOrigin = (int)Image.OriginHorizontal.Left;
            }
        }
        else if(isFilling){
            actualTimeFill-= Time.deltaTime;
            if(actualTimeFill<0f){
                image.fillAmount = 1.0f;
                isFilling = false;
                isUnfilling = true;
                actualTimeFill = timeFilling;
                image.fillOrigin = (int)Image.OriginHorizontal.Right;
            }
            else{
                image.fillAmount = 1f-actualTimeFill/timeFilling;
            }
        }
        else if(isUnfilling){
            actualTimeFill-= Time.deltaTime;
            if(actualTimeFill<0f){
                image.fillAmount = 0f;
                isUnfilling = false;
                actualtimeBeforeFill = averageTimeBetweenFill * Random.Range(0.1f,1.2f);
            }
            else{
                image.fillAmount = actualTimeFill/timeFilling;
            }
        }
    }
}
