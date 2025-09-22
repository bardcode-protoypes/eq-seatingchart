using UnityEngine;
using UnityEngine.UI;

public class GuestCard : MonoBehaviour
{
    public GuestSO guestData;

    public Image portraitImage;
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI description;

    void Start()
    {
        if (guestData != null)
        {
            portraitImage.sprite = guestData.portrait;
            nameText.text = guestData.guestName;
            description.text = guestData.description;
        }
    }
}
