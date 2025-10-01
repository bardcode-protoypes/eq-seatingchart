using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGuestWave", menuName = "Wedding/Guest Wave")]
public class GuestWaveSO : ScriptableObject
{
    [Tooltip("Guests to spawn in this wave.")]
    public List<GuestSO> guests = new();

    [Tooltip("Optional note keys to spawn at the same time.")]
    public List<string> noteKeys = new();

    [Tooltip("Should the next wave spawn automatically after this one is solved?")]
    public bool autoAdvance = false;
}