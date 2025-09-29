using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Tables;

[CreateAssetMenu(fileName = "NewGuest", menuName = "Wedding/Guest")]
public class GuestSO : ScriptableObject
{
    public TableEntryReference guestNameRef;
    public Sprite portrait;
    public TableEntryReference descriptionRef;

    [TextArea]
    public List<string> naturalLanguageRules; 
    
    public List<GuestRule> ruleData; 
}

public class GuestRule
{
    // ToDo: implement 
}
