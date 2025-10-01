using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Tables;

[CreateAssetMenu(fileName = "NewGuest", menuName = "Wedding/Guest")]
public class GuestSO : ScriptableObject
{
    [Header("Guest Identity")] 
    public string guestId;
    
    private TableEntryReference guestNameRef;
    public TableEntryReference GuestNameRef => this.guestNameRef;
    
    private TableEntryReference descriptionRef;
    public TableEntryReference DescriptionRef => this.descriptionRef;
    
    [SerializeField] private Sprite portrait;
    public Sprite Portrait => this.portrait;
    
    [Header("Ruleset")]
    public List<GuestRule> rules = new List<GuestRule>();

    private void OnValidate()
    {
        this.guestNameRef = "guest_" + guestId + "_name";
        this.descriptionRef = "guest_" + guestId + "_desc";
    }
}