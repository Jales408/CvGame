using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopTropheeHelper : MonoBehaviour
{
    public TropheeSelecter selecter;

    [Space(20)]
    
    public Image tropheeImage;
    public Text tropheeTitle;
    public Text tropheeDescription;

    [Space(20)]
    public EasyTween EasyTweenTropheeOpenClose;
    public float timeTapping = 0.5f;
    public float timeWaiting = 5.0f;

    public string preDeco = "Progrès débloqué < ";
    public string postDeco = " >";

    private TropheeItem unlockedTrophee;
    IEnumerator AnimateUnlock(Text Titletext, Text DescriptionText, string finalTitleText, string finalDescriptionText, string preTitleDeco, string postTitleDeco){
        EasyTweenTropheeOpenClose.OpenCloseObjectAnimation();
        Titletext.text = preDeco + postDeco;
        DescriptionText.text = "";
        yield return new WaitForSecondsRealtime(EasyTweenTropheeOpenClose.GetAnimationDuration()*2.0f);
        int numberOfAdds = 0;
        while(numberOfAdds<finalTitleText.Length){
            Titletext.text = preDeco + finalTitleText.Substring(0,++numberOfAdds)+ postDeco;
            yield return new WaitForSecondsRealtime(Random.Range(0.2f,2.0f)*timeTapping);
        }
        numberOfAdds = 0;
        while(numberOfAdds<finalDescriptionText.Length){
            DescriptionText.text = finalDescriptionText.Substring(0,++numberOfAdds);
            yield return new WaitForSecondsRealtime(Random.Range(0.2f,2.0f)*timeTapping);
        }
        yield return new WaitForSecondsRealtime(timeWaiting);
        EasyTweenTropheeOpenClose.OpenCloseObjectAnimation();
    }

    public void popTrophee(TropheeItem item){
        unlockedTrophee = item;
        tropheeImage.sprite = unlockedTrophee.Logo;
        StartCoroutine(AnimateUnlock(tropheeTitle,tropheeDescription,unlockedTrophee.Title,unlockedTrophee.ShortDescription,preDeco,postDeco));
    }

}
