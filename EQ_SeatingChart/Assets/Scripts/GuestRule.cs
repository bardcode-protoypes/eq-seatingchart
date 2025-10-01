using UnityEngine;

public enum RuleType
{
    MustBeNextTo,
    MustNotBeNextTo,
    MustBeOpposite,
    MustNotBeOpposite,
    MustBeAtSameTable,
    MustNotBeAtSameTable,
    MustBeInZone,
    MustNotBeInZone
}

[System.Serializable]
public class GuestRule
{
    public RuleType type;
    public GuestSO targetGuest;    // optional (e.g. "must sit next to Lord Hawthorne")
    // public TableSO targetTable;    // optional (e.g. "must sit at the head table")
    public RoomZone targetZone;    // optional (e.g. "must sit in the front")
} 