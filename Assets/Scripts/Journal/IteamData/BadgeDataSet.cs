using UnityEngine;
using System.Collections;
using System;

public class BadgeDataSet : ScriptableObject
{
    [SerializeField]
    private BadgeData[] badgeData = null;
    public BadgeData[] BadgeData { get { return badgeData; } }

    // Supply a separate IndexOf function, since it's not accessible via IEnumerable<>
    public int IndexOf(BadgeData data)
    {
        return Array.IndexOf(badgeData, data);
    }
}

