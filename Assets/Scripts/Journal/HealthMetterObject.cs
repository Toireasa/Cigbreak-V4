using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;

public class HealthMetterObject : MonoBehaviour
{
    [SerializeField]
    private Text m_TitleText = null;

    [SerializeField]
    private Text m_DescriptionText = null;

    [SerializeField]
    private Image barFill = null;

    [SerializeField]
    private Text m_PercentageText = null;

    private HealthData m_HealthData = null;
    public HealthData SetHealthData
    {
        set { m_HealthData = value; }
    }

    void Start()
    {
        m_TitleText.text = m_HealthData.Title;
        m_DescriptionText.text = m_HealthData.Statement;

        System.TimeSpan ts = System.DateTime.Now - PlayerProfile.GetProfile().QuitDate.Value;

        float percentage = (float)ts.Days / m_HealthData.DaysToAchive;
        barFill.fillAmount = (percentage <= 1) ? percentage : 1;

        if (percentage>=1)
        {
            m_PercentageText.text = "100%";
        }
        else
        {
            m_PercentageText.text = (percentage * 100).ToString("F2") + "%";
        }
    }

}
