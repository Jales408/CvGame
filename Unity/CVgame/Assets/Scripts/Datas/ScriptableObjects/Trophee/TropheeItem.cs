
using UnityEngine;

[CreateAssetMenu(fileName = "TropheeItemObject", menuName = "ScriptableObjects/TropheeItem", order = 1)]
public class TropheeItem : ScriptableObject
{
    public bool isUnlocked;
    public string Identifier;
    public Sprite Logo;
    public string Title;
    public string Description;
    public string Competence1;
    public string Competence2;
    public string ShortDescription;
}
