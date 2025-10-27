using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Tables;

[CreateAssetMenu(fileName = "NewGuest", menuName = "Wedding/Guest")]
public class GuestSO : ScriptableObject
{
    [Header("Guest Identity")] 
    public string guestId;
    
    private TableEntryReference guestFirstNameRef;
    public TableEntryReference GuestFirstNameRef => this.guestFirstNameRef;
    
    private TableEntryReference guestLastNameRef;
    public TableEntryReference GuestLastNameRef => this.guestLastNameRef;
    
    [SerializeField] private Sprite portrait;
    public Sprite Portrait => this.portrait;
    
    [Header("Ruleset")]
    public List<GuestRule> rules = new List<GuestRule>();

    private void OnValidate()
    {
        this.guestFirstNameRef = "guest_" + guestId + "_firstname";
        this.guestLastNameRef = "guest_" + guestId + "_lastname";
    }
}