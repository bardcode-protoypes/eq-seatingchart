using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class GuestCardView : MonoBehaviour
{
    [SerializeField] private Image portraitImage;
    public Image PortraitImage => this.portraitImage;

    [SerializeField] private LocalizeStringEvent firstNameText;
    public LocalizeStringEvent FirstNameText => this.firstNameText;
    
    [SerializeField] private LocalizeStringEvent lastNameText;
    public LocalizeStringEvent LastNameText => this.lastNameText;

    [SerializeField] private RectTransform cardRect;
    public RectTransform CardRect => this.cardRect;
    
}
