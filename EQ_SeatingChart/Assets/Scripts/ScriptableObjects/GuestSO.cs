using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGuest", menuName = "Wedding/Guest")]
public class GuestSO : ScriptableObject
{
    public string guestName;
    public Sprite portrait;
    public string description;

    [TextArea]
    public List<string> naturalLanguageRules; 
    
    public List<GuestRule> ruleData; 
}

public class GuestRule
{
    // ToDo: implement 
}
