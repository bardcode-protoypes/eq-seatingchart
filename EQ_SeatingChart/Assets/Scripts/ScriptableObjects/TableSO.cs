using UnityEngine;

public enum TableType { Round, Rectangular }
public enum RoomZone { headTable, front, middle, back }

[CreateAssetMenu(fileName = "NewTable", menuName = "Wedding/Table")]
public class TableSO : ScriptableObject
{
    public RoomZone roomZone;
    public TableType tableType;
    [Range(2, 20)] public int seatCount;
    private void OnValidate()
    {
        // Force seatCount to even
        if (seatCount % 2 != 0)
            seatCount += 1;
    }

}

