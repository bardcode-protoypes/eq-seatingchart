using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class GuestCardView : MonoBehaviour
{
    [SerializeField] private Image portraitImage;
    public Image PortraitImage => this.portraitImage;

    [SerializeField] private LocalizeStringEvent nameText;
    public LocalizeStringEvent NameText => this.nameText;
    
    [SerializeField] private LocalizeStringEvent description;
    public LocalizeStringEvent Description => this.description;

    [SerializeField] private RectTransform cardRect;
    public RectTransform CardRect => this.cardRect;
    
}
