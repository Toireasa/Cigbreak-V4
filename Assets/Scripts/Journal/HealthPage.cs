using UnityEngine;
using System.Collections;
using System.Linq;
using CigBreak;
using UnityEngine.UI;

public class HealthPage : MonoBehaviour
{
    [SerializeField]
    private  HealthDataSet m_HealthDataSet = null;

    [SerializeField]
    private HealthMetterObject m_HealthMeterObjest = null;

    [SerializeField]
    private GameObject displayParent = null;

    // Use this for initialization
    void OnEnable()
    {
        foreach (Transform child in displayParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for (int i = 0; i < m_HealthDataSet.HelathsData.Count<HealthData>(); i++)
        {
            HealthMetterObject icon = Instantiate(m_HealthMeterObjest) as HealthMetterObject;
            icon.transform.SetParent(displayParent.transform, false);
            icon.SetHealthData = m_HealthDataSet.HelathsData[i];
        }
    }
}
