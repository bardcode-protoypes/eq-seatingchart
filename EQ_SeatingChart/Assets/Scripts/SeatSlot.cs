using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeatSlot : MonoBehaviour, IDropHandler
{
    public GuestCardController CurrentGuestCard;
    public int seatIndex;
    public List<SeatSlot> nextTo;
    public SeatSlot oppositeTo;
    public List<SeatSlot> sameTable;

    [SerializeField] private RectTransform seatRect;
    public RectTransform SeatRect => this.seatRect;
    
    public List<RoomZone> roomZones => GetComponentInParent<TableController>().roomZones;

    public void OnDrop(PointerEventData eventData)
    {
        GuestCardController guestCard = eventData.pointerDrag.GetComponent<GuestCardController>();
        if (guestCard != null)
        {
            guestCard.PlaceAtSeat(this);
        }
    }
}
