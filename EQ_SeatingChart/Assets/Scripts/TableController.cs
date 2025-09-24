using System;
using UnityEngine;
using System.Collections.Generic;

public enum TableType { Round, Rectangular }
public enum RoomZone { HeadTable, Front, Middle, Back, Windows, Doors }

[ExecuteInEditMode]
public class TableController : MonoBehaviour
{
    [Header("Table Settings")] 
    
    [SerializeField] private TableType tableType;
    [SerializeField] public List<RoomZone> roomZones;
    
    [SerializeField] private float spacing;
    [SerializeField] private float oppositeSpacing;
    
    [SerializeField] [Range(2, 20)] private int seatCount = 6;
    [SerializeField] private GameObject seatPrefab;
    [SerializeField] private List<SeatSlot> seats = new();
    
    public void BuildTable()
    {
        // clear existing seats
        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        
        foreach (var go in children)
            DestroyImmediate(go);
        
        seats.Clear();

        // Generate seats based on type
        if (this.tableType == TableType.Round)
            BuildRound();
        else
            BuildRectangular();

        // Assign relationships
        LinkRelationships();
        
    }

    void BuildRound()
    {
        for (int i = 0; i < this.seatCount; i++)
        {
            float angle = (360f / this.seatCount) * i;
            float radius = this.oppositeSpacing / 2; 
            
            Vector3 pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;
            var seatObj = Instantiate(seatPrefab, transform);
            seatObj.transform.localPosition = pos;

            SeatSlot seat = seatObj.GetComponent<SeatSlot>();
            //seat.tableId = GetInstanceID();
            seat.seatIndex = i;
            seatObj.name = $"Seat_{i}";
            seats.Add(seat);
        }
    }

    void BuildRectangular()
    {
        float topRowYPos = this.oppositeSpacing / 2;
        float bottomRowYPos = -topRowYPos;
        
        int seatsPerSide = this.seatCount / 2;
        for (int i = 0; i < seatsPerSide; i++)
        {
            // Top row
            float topRowXPos = i * this.spacing;
            var top = Instantiate(seatPrefab, transform);
            top.transform.localPosition = new Vector3(topRowXPos, topRowYPos, 0);
            var seatTop = top.GetComponent<SeatSlot>();
            //seatTop.tableId = GetInstanceID();
            seatTop.seatIndex = i;
            seatTop.name = $"Seat_{i}";
            seats.Add(seatTop);
        }
        for (int i = seatsPerSide; i < this.seatCount; i++)
        {
            // Bottom row
            float bottomRowXpos = (i - seatsPerSide) * this.spacing;
            var bottom = Instantiate(seatPrefab, transform);
            bottom.transform.localPosition = new Vector3(bottomRowXpos, bottomRowYPos, 0);
            var seatBottom = bottom.GetComponent<SeatSlot>();
            //seatBottom.tableId = GetInstanceID();
            seatBottom.seatIndex = i;
            seatBottom.name = $"Seat_{i}";
            seats.Add(seatBottom);
            
        }
    }

    void LinkRelationships()
    {
        if (this.tableType == TableType.Round)
        {
            int count = seats.Count;
            for (int i = 0; i < count; i++)
            {
                SeatSlot s = seats[i];
                s.nextTo.Clear();
                s.nextTo.Add(seats[(i + 1) % count]);
                s.nextTo.Add(seats[(i - 1 + count) % count]);
                
                if (count % 2 == 0)
                {
                    int oppositeIndex = (i + count / 2) % count;
                    s.oppositeTo = seats[oppositeIndex];
                }

                s.sameTable = new List<SeatSlot>(seats);
            }
        }
        else if (this.tableType == TableType.Rectangular)
        {
            int half = seats.Count / 2;
            for (int i = 0; i < seats.Count; i++)
            {
                SeatSlot s = seats[i];
                s.nextTo.Clear();
                s.sameTable = new List<SeatSlot>(seats);

                if (i < half) // top row
                {
                    s.oppositeTo = seats[i + half];
                    if (i > 0) s.nextTo.Add(seats[i - 1]);
                    if (i < half - 1) s.nextTo.Add(seats[i + 1]);
                }
                else // bottom row
                {
                    s.oppositeTo = seats[i - half];
                    if (i > half) s.nextTo.Add(seats[i - 1]);
                    if (i < seats.Count - 1) s.nextTo.Add(seats[i + 1]);
                }
            }
        }
    }

    public void OnValidate()
    {
        if (this.seatCount % 2 != 0)
        {
            this.seatCount++;
        }
    }
}
