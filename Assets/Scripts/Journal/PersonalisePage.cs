using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using CigBreakGUI;

public class PersonalisePage : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PhotoPage = null;

    [SerializeField]
    Button m_ScrollButtonPefab = null;

    [SerializeField]
    private Journal m_Journal = null;

    [Header("Pages Field")]
    [SerializeField]
    private InputField m_NameField = null;

    [SerializeField]
    private InputField m_CigPerDayField = null;

    [SerializeField]
    private InputField m_CostPerPack = null;

    [SerializeField]
    private InputField m_CifInPackField = null;

    [Header("Day")]
    [SerializeField]
    Button DayBT = null;

    [SerializeField]
    GameObject m_DayScrollbar = null;

    [SerializeField]
    GameObject m_DayScrollBarContaint = null;

    [Header("Month")]
    [SerializeField]
    Button m_Month = null;

    [SerializeField]
    GameObject m_MonthScrollbar = null;

    [SerializeField]
    GameObject m_MonthScrollBarContaint = null;

    [Header("Year")]
    [SerializeField]
    Button m_YearBT = null;

    [SerializeField]
    GameObject m_YearScrollbar = null;

    [SerializeField]
    GameObject m_YearScrollBarContaint = null;

    InputField test;

    PlayerProfile m_Profile = null;

    protected void OnEnable()
    {
        m_Profile = PlayerProfile.GetProfile();
        if (PlayerProfile.GetProfile().QuitDate != null)
        {
            m_NameField.text = m_Profile.PlayerName;
            m_CigPerDayField.text = m_Profile.NumOfCigPerDay.ToString();
            m_CostPerPack.text = m_Profile.CostOfPack.ToString();
            m_CifInPackField.text = m_Profile.NumOfCigPerPack.ToString();

            DayBT.GetComponentInChildren<Text>().text = m_Profile.QuitDate.Value.Day.ToString();
            m_Month.GetComponentInChildren<Text>().text = m_Profile.QuitDate.Value.Month.ToString();
            m_YearBT.GetComponentInChildren<Text>().text = (m_Profile.QuitDate.Value.Year - 2000).ToString();
        }
        else
        {
            DayBT.GetComponentInChildren<Text>().text = System.DateTime.Now.Day.ToString();
            m_Month.GetComponentInChildren<Text>().text = System.DateTime.Now.Month.ToString();
            m_YearBT.GetComponentInChildren<Text>().text = (System.DateTime.Now.Year - 2000).ToString();
        }
    }

    //Buttons Functions

    public void OKBT()
    {
        this.gameObject.SetActive(false);
        m_PhotoPage.SetActive(false);
        m_Journal.OpenCoverPage();
    }

    public void OnDayBT()
    {
        if (!m_DayScrollbar.activeSelf)
        {
            m_DayScrollbar.SetActive(true);
            for (int i = 1; i < 32; i++)
            {
                Button icon = Instantiate(m_ScrollButtonPefab) as Button;
                icon.onClick.AddListener(() => { onDayClicked(icon); });
                icon.GetComponentInChildren<Text>().text = i.ToString();
                icon.transform.SetParent(m_DayScrollBarContaint.transform, false);
            }
        }
        else
        {
            foreach (Transform child in m_DayScrollBarContaint.transform)
            {
                Destroy(child.gameObject);
            }
            m_DayScrollbar.SetActive(false);
        }
    }

    private void onDayClicked(Button button)
    {
        DayBT.GetComponentInChildren<Text>().text = button.GetComponentInChildren<Text>().text;
        foreach (Transform child in m_DayScrollBarContaint.transform)
        {
            Destroy(child.gameObject);
        }
        m_DayScrollbar.SetActive(false);
    }

    public void OnMonthBT()
    {
        if (!m_MonthScrollbar.activeSelf)
        {
            m_MonthScrollbar.SetActive(true);
            for (int i = 1; i < 13; i++)
            {
                Button icon = Instantiate(m_ScrollButtonPefab) as Button;
                icon.onClick.AddListener(() => { onMothClicked(icon); });
                icon.GetComponentInChildren<Text>().text = i.ToString();
                icon.transform.SetParent(m_MonthScrollBarContaint.transform, false);
            }
        }
        else
        {
            foreach (Transform child in m_MonthScrollBarContaint.transform)
            {
                Destroy(child.gameObject);
            }
            m_MonthScrollbar.SetActive(false);
        }
    }

    private void onMothClicked(Button button)
    {
        m_Month.GetComponentInChildren<Text>().text = button.GetComponentInChildren<Text>().text;
        foreach (Transform child in m_MonthScrollBarContaint.transform)
        {
            Destroy(child.gameObject);
        }
        m_MonthScrollbar.SetActive(false);

    }

    public void OnYearBT()
    {
        if (!m_YearScrollbar.activeSelf)
        {
            m_YearScrollbar.SetActive(true);
            for (int i = 16; i < 18; i++)
            {
                Button icon = Instantiate(m_ScrollButtonPefab) as Button;
                icon.onClick.AddListener(() => { onYearClicked(icon); });
                icon.GetComponentInChildren<Text>().text = i.ToString();
                icon.transform.SetParent(m_YearScrollBarContaint.transform, false);
            }
        }
        else
        {
            foreach (Transform child in m_YearScrollBarContaint.transform)
            {
                Destroy(child.gameObject);
            }
            m_YearScrollbar.SetActive(false);
        }
    }

    private void onYearClicked(Button button)
    {
        m_YearBT.GetComponentInChildren<Text>().text = button.GetComponentInChildren<Text>().text;
        foreach (Transform child in m_YearScrollBarContaint.transform)
        {
            Destroy(child.gameObject);
        }
        m_YearScrollbar.SetActive(false);
    }

    public void DoneButton()
    {
        if (m_NameField.text != string.Empty && m_CigPerDayField.text != string.Empty 
            && m_CostPerPack.text != string.Empty && m_CifInPackField.text != string.Empty)
        {
            PlayerProfile.GetProfile().PlayerName = m_NameField.text;

            PlayerProfile.GetProfile().NumOfCigPerDay = int.Parse(m_CigPerDayField.text);
            PlayerProfile.GetProfile().CostOfPack = int.Parse(m_CostPerPack.text);
            PlayerProfile.GetProfile().NumOfCigPerPack = int.Parse(m_CifInPackField.text);

            string input = DayBT.GetComponentInChildren<Text>().text + "-" + m_Month.GetComponentInChildren<Text>().text + "-" + "20" + m_YearBT.GetComponentInChildren<Text>().text;

            System.DateTime quitDate = new System.DateTime(
                int.Parse("20" + m_YearBT.GetComponentInChildren<Text>().text),
                int.Parse(m_Month.GetComponentInChildren<Text>().text),
                int.Parse(DayBT.GetComponentInChildren<Text>().text),
                System.DateTime.Now.Hour,
                0,
                0,
                0);

            //System.DateTime.ParseExact(input + System.DateTime.Now.Hour.ToString()+ "00:00", "d-M-yyyy hh:mm:s", System.Globalization.CultureInfo.InvariantCulture);

            //quitDate.

            System.DateTime currentTime = System.DateTime.Now;
            System.TimeSpan ts = System.Convert.ToDateTime(quitDate) - currentTime;

            PlayerProfile.GetProfile().QuitDate = quitDate;
            
            //Analitics

            
            this.gameObject.SetActive(false);
            m_PhotoPage.SetActive(true);
        }
    }
}
