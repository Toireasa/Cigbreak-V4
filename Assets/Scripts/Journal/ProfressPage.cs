using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;

public class ProfressPage : MonoBehaviour
{

    [SerializeField]
    GameObject m_Index = null;

    [Header("Pages")]
    [SerializeField]
    GameObject m_HealthPage = null;

    [SerializeField]
    GameObject m_MoneyPage = null;

    [SerializeField]
    GameObject m_TimePage = null;

    [SerializeField]
    Text m_TimeText = null;

    GameObject m_CurrentPage = null;

    

    //Buttons Function
    public void HealthBT()
    {
        m_HealthPage.SetActive(true);
        m_CurrentPage = m_HealthPage;
        m_Index.SetActive(false);
    }

    public void MoneyBT()
    {
        m_MoneyPage.SetActive(true);
        m_CurrentPage = m_MoneyPage;
        m_Index.SetActive(false);
    }

    public void TimeBT()
    {
        m_TimePage.SetActive(true);
        m_CurrentPage = m_TimePage;
        m_Index.SetActive(false);

        System.TimeSpan ts = System.DateTime.Now - PlayerProfile.GetProfile().QuitDate.Value;

        int years = ts.Days / 365;
        int months = (ts.Days - (years * 365) )/ 30;

        int days = ts.Days - (years * 365) - months * 30;

        m_TimeText.text = years + " Years \n" +
                          months + " Months \n" +
                          days + " Days \n";
    }

    public void BackBT()
    {
        m_CurrentPage.SetActive(false);
        m_Index.SetActive(true);
    }

}
