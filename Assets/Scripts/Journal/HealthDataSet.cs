using UnityEngine;
using System.Collections;
using CigBreak;
using System;

public class HealthDataSet : ScriptableObject
{
    [SerializeField]
    private HealthData[] healthData = null;
    public HealthData[] HelathsData { get { return healthData; } }

    // Supply a separate IndexOf function, since it's not accessible via IEnumerable<>
    public int IndexOf(HealthData data)
    {
        return Array.IndexOf(healthData, data);
    }
}
