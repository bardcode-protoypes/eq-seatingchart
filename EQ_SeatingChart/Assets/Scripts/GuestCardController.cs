using System;
using Unity.VisualScripting;
using UnityEngine;

public class GuestCardController : MonoBehaviour
{
    [SerializeField] private GuestSO guestData;
    public GuestSO GuestData => this.guestData;

    [SerializeField] private PaperObject paperObject;
    public PaperObject PaperObject => this.paperObject;
    
    [SerializeField] private GuestCardView guestCardView;

    private SeatSlot currentSeat;
    public SeatSlot CurrentSeat
    {
        get => currentSeat;
        set => currentSeat = value;
    }

    void Start()
    {
        this.SetReferences();
    }

    public void Init(GuestSO guest)
    {
        this.guestData = guest;
        this.SetReferences();
    }


    private void SetReferences()
    {
        if (guestData != null)
        {
            this.guestCardView.PortraitImage.sprite = guestData.Portrait;
            this.guestCardView.NameText.StringReference.SetReference("UITexts", guestData.GuestNameRef);
            this.guestCardView.Description.StringReference.SetReference("UITexts", guestData.DescriptionRef);
        }
    }

    public void PlaceAtSeat(SeatSlot seat)
    {
        if (this.currentSeat != null)
            this.currentSeat.CurrentGuestCard = null;

        this.currentSeat = seat;
        seat.CurrentGuestCard = this;
        
        // Snap to slot
        this.guestCardView.CardRect.position = seat.SeatRect.position;
        this.guestCardView.CardRect.localRotation = seat.SeatRect.localRotation;
    }

}
