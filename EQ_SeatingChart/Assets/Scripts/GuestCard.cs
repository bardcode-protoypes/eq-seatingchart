using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class GuestCard : MonoBehaviour
{
    public GuestSO guestData;

    public Image portraitImage;
    public LocalizedString nameText;
    public LocalizedString description;

    void Start()
    {
        if (guestData != null)
        {
            portraitImage.sprite = guestData.portrait;
            nameText.SetReference(default, guestData.guestNameRef);
            description.SetReference(default, guestData.descriptionRef);
        }
    }
}
