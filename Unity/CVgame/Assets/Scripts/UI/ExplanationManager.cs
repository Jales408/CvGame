using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationManager : MonoBehaviour
{
    public EasyTween dialogAnimationTween;
    public Text content;

    public void ShowExplication(Explanation explanation){
        if(!dialogAnimationTween.IsObjectOpened()){
            dialogAnimationTween.OpenCloseObjectAnimation();
        }
        content.text = explanation.text;

    }

    public void HideExplanation(){
        if(dialogAnimationTween.IsObjectOpened()){
            dialogAnimationTween.OpenCloseObjectAnimation();
        }
    }
}
