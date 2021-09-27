using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaypoinType
{
    room1,
    room2,
    room3,
    room4,
    room5,
    room6,
    room7,
    room8,
    room9,
    room10,
}

public class WaypointInfo : MonoBehaviour
{
    public WaypoinType m_type;

    [System.NonSerialized]
    public bool Catched = false;
}
