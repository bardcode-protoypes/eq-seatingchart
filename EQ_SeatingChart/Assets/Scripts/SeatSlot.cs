using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeatSlot : MonoBehaviour, IDropHandler
{
    public GuestCard CurrentGuestCard;
    public int seatIndex;
    public List<SeatSlot> nextTo;
    public SeatSlot oppositeTo;
    public List<SeatSlot> sameTable;
    
    public List<RoomZone> roomZones => GetComponentInParent<TableController>().roomZones;

    public void OnDrop(PointerEventData eventData)
    {
        GuestCard guestCard = eventData.pointerDrag.GetComponent<GuestCard>();
        if (guestCard != null)
        {
            // Snap to slot
            RectTransform guestCardRect = guestCard.GetComponent<RectTransform>();
            guestCardRect.position = this.GetComponent<RectTransform>().position;

            // Register occupant
            this.CurrentGuestCard = guestCard;
        }
    }
}
