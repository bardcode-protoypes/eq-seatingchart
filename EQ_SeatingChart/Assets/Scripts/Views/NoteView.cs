using UnityEngine;
using UnityEngine.Localization;

public class NoteView: MonoBehaviour
{
    [SerializeField] private RectTransform noteRect;
    public RectTransform NoteRect => this.noteRect;
    [SerializeField] private LocalizedString message;

    [SerializeField] private PaperObject paperObject;
    public PaperObject PaperObject => this.paperObject;

    public LocalizedString Message => this.message;

}
