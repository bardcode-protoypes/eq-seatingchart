using System;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;
using UnityEngine.Localization.Tables;
using Random = UnityEngine.Random;

public class NotesSystem : MonoBehaviour
{
    [SerializeField] private NoteView noteTemplate;
    [SerializeField] private Transform deskArea;

    private void Awake()
    {
        if (noteTemplate != null)
            noteTemplate.NoteCanvasGroup.alpha = 0; 
    }

    public void CreateNote(string localizationKey)
    {
        NoteView note = Instantiate(this.noteTemplate, this.deskArea);

        // Localize
        note.Message.SetReference(default, localizationKey);

        note.PaperObject.SpawnOnDesk();
    }

}