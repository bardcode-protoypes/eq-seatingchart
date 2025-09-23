using System;
using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class TableController : MonoBehaviour
{
    public GameObject seatPrefab;
    [SerializeField] private TableSO tableData;
    private List<SeatSlot> seats = new List<SeatSlot>();

    [SerializeField] private float spacing;
    [SerializeField] private float radius;

    public void BuildTable()
    {
        // clear existing seats
        var children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        
        foreach (var go in children)
            DestroyImmediate(go);
        
        seats.Clear();

        if (tableData == null) return;

        // Generate seats based on type
        if (tableData.tableType == TableType.Round)
            BuildRound();
        else
            BuildRectangular();

        // Assign relationships
        LinkRelationships();

        // Assign zone
        foreach (var seat in seats)
            seat.roomZone = tableData.roomZone;
    }

    void BuildRound()
    {
        for (int i = 0; i < tableData.seatCount; i++)
        {
            float angle = (360f / tableData.seatCount) * i;
            Vector3 pos = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;
            var seatObj = Instantiate(seatPrefab, transform);
            seatObj.transform.localPosition = pos;

            SeatSlot seat = seatObj.GetComponent<SeatSlot>();
            //seat.tableId = GetInstanceID();
            seat.seatIndex = i;
            seats.Add(seat);
        }
    }

    void BuildRectangular()
    {
        int seatsPerSide = tableData.seatCount / 2;
        for (int i = 0; i < seatsPerSide; i++)
        {
            // Top row
            var top = Instantiate(seatPrefab, transform);
            top.transform.localPosition = new Vector3(i * spacing, 50f, 0);
            var seatTop = top.GetComponent<SeatSlot>();
            //seatTop.tableId = GetInstanceID();
            seatTop.seatIndex = i;
            seats.Add(seatTop);
        }
        for (int i = seatsPerSide; i < tableData.seatCount; i++)
        {
            // Bottom row
            var bottom = Instantiate(seatPrefab, transform);
            bottom.transform.localPosition = new Vector3((i - seatsPerSide) * spacing, -50f, 0);
            var seatBottom = bottom.GetComponent<SeatSlot>();
            //seatBottom.tableId = GetInstanceID();
            seatBottom.seatIndex = i + seatsPerSide;
            seats.Add(seatBottom);
        }
    }

    void LinkRelationships()
    {
        if (tableData.tableType == TableType.Round)
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
        else if (tableData.tableType == TableType.Rectangular)
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

}
