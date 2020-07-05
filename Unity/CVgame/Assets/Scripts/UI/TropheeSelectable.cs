using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TropheeSelectable : MonoBehaviour
{
    public string identifier;
    public Image logo;
    public Color unlockedColor;
    public Color defaultColor;
    private TropheeSelecter selecter;
    private TropheeItem itemDisplay;
    private TropheeItem itemLockDisplay;
    
    public void Select(){
            selecter.DisplaySelection(itemDisplay);
    }

    public void RegisterSelecter(TropheeSelecter selecter, TropheeItem itemLockDisplay){
        this.selecter = selecter;
        this.itemLockDisplay = itemLockDisplay;
    }

    public void changeTropheeContent(TropheeItem itemToDisplay){
        logo.color = (itemToDisplay.isUnlocked)?unlockedColor:defaultColor;
        if(itemToDisplay.isUnlocked){
            this.itemDisplay = itemToDisplay;
            logo.sprite = itemToDisplay.Logo;
        }
        else{
            this.itemDisplay = itemLockDisplay;
            logo.sprite = itemLockDisplay.Logo;
        }
    }
}
