using UnityEngine;
using System.Collections.Generic;

public class GuestSpawner : MonoBehaviour
{
    [SerializeField] private GuestCardController guestCardPrefab;
    [SerializeField] private Transform deskArea;
    [SerializeField] private NotesSystem notesSystem;

    private readonly List<GuestCardController> activeCards = new();

    public void SpawnWave(GuestWaveSO wave)
    {
        foreach (var guest in wave.guests)
        {
            var card = Instantiate(guestCardPrefab, deskArea);
            card.Init(guest);
            
            card.PaperObject.SpawnOnDesk();
            activeCards.Add(card);
        }

        // Spawn notes
        foreach (var noteKey in wave.noteKeys)
        {
            notesSystem.CreateNote(noteKey);
        }
    }

    public List<GuestCardController> GetActiveCards()
    {
        return activeCards;
    }
}