using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TropheeSelecter : MonoBehaviour
{
    public string contentBeforeTitle = ">.";
    public Text title;
    public Text description;
    public Text competence1;
    public Text competence2;

    public bool isInitialized;
    private Dictionary<string,TropheeSelectable> selectables = new Dictionary<string, TropheeSelectable>();

    [Space(20)]
    public TropheeItem defaultTrophee;

    private void Awake() {
        TropheeSelectable[] selectablesList = GetComponentsInChildren<TropheeSelectable>();
        foreach(TropheeSelectable selectable in selectablesList){
            selectables.Add(selectable.identifier,selectable);
        }
    }

    public void DisplaySelection(TropheeItem item){
        title.text = contentBeforeTitle + item.Title;
        description.text = item.Description;
        competence1.text = item.Competence1;
        competence2.text = item.Competence2;
    }

    public void CleanDisplay(){
        title.text = "";
        description.text = "";
        competence1.text = "";
        competence2.text = "";
    }

    private void OnEnable() {
        if(!isInitialized){
            TropheeSelectable[] selectablesList = GetComponentsInChildren<TropheeSelectable>();
            foreach(TropheeSelectable selectable in selectablesList){
                selectable.RegisterSelecter(this,defaultTrophee);
            } 
            isInitialized = true;
        }
        CleanDisplay();
        foreach(KeyValuePair<string, TropheeSelectable> keyValue in selectables)
        {
            TropheesStat tStats = TropheesStat.getInstance();
            keyValue.Value.changeTropheeContent(tStats.getTropheeItem(keyValue.Key));
        }
    }
}
