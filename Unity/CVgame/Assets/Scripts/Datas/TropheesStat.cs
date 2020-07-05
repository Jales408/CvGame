using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropheesStat : MonoBehaviour
{
    public static TropheesStat tropheeStatInstance;
    public TropheeItem[] tropheeList;

    public PopTropheeHelper tPop;
    private Dictionary<string,TropheeItem> trophees = new Dictionary<string, TropheeItem>();

    public static TropheesStat getInstance(){
        return tropheeStatInstance;
    }

    public void registerInstance(){
        tropheeStatInstance = this;
    }

    private void Awake() {
        if(tropheeStatInstance == null){
            registerInstance();
        } 
        else{
            Debug.Log("Multiple Instance of TropheesStat exist");
        }
        foreach(TropheeItem item in tropheeList){
            trophees.Add(item.Identifier,item);
            item.isUnlocked = false;
        }
    }

    public TropheeItem getTropheeItem(string identifier){
        if(trophees.ContainsKey(identifier)){
            return trophees[identifier];
        }
        else{
            Debug.Log("WARNING wrong identifier when accessing trophees");
            return null;
        }
    }

    public void unlockTropheeItem(string identifier){
        if(trophees.ContainsKey(identifier)){
            trophees[identifier].isUnlocked = true;
            tPop.popTrophee(trophees[identifier]);
        }
        else{
            Debug.Log("WARNING wrong identifier when accessing trophees");
        }
    }
}
