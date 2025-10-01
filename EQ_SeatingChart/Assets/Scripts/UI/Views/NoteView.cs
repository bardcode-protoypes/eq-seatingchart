using System;
using UnityEngine;
using UnityEngine.Localization;

public class NoteView: MonoBehaviour
{
    [SerializeField] private CanvasGroup noteCanvasGroup;
    public CanvasGroup NoteCanvasGroup => this.noteCanvasGroup;
    
    [SerializeField] private RectTransform noteRect;
    public RectTransform NoteRect => this.noteRect;

    [SerializeField] private LocalizedString message;
    public LocalizedString Message => this.message;
    
    [SerializeField] private PaperObject paperObject;
    public PaperObject PaperObject => this.paperObject;

}
