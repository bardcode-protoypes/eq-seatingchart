using UnityEngine;
using UnityEngine.EventSystems;

public class SeatSlot : MonoBehaviour, IDropHandler
{
    public GuestCard CurrentGuestCard;

    public void OnDrop(PointerEventData eventData)
    {
        GuestCard guestCard = eventData.pointerDrag.GetComponent<GuestCard>();
        if (guestCard != null)
        {
            // Snap paperObject to this slot
            RectTransform guestCardRect = guestCard.GetComponent<RectTransform>();
            guestCardRect.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            // Register occupant
            this.CurrentGuestCard = guestCard;
        }
    }
}
