using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using CigBreak;

public class BadgePage : MonoBehaviour
{
    [SerializeField]
    JournalSharePage m_SharePage = null;

    [SerializeField]
    GameObject m_BadgePanel = null;

    [SerializeField]
    BadgeDataSet m_BadgeSet;

    [SerializeField]
    Button m_BadgeButtonObject = null;

    [Header("ParentElements")]
    [SerializeField]
    private GameObject m_QuitDisplayParent = null;

    [SerializeField]
    private GameObject m_HourstDisplayParent = null;

    [SerializeField]
    private GameObject m_LastTrophy = null;
    

    void OnEnable()
    {

        //Load data
        for (int i = 0; i < m_BadgeSet.BadgeData.Count<BadgeData>(); i++)
        {
            Button _bt = Instantiate(m_BadgeButtonObject) as Button;
            _bt.GetComponent<Image>().sprite = m_BadgeSet.BadgeData[i].Icon;
            _bt.GetComponentInChildren<Text>().text = m_BadgeSet.BadgeData[i].Title;

            switch (m_BadgeSet.BadgeData[i].GetBadgeType)
            {
                case BadgeData.BadgeType.QuitDate:
                    int x = i;
                    _bt.transform.SetParent(m_QuitDisplayParent.transform, false);
                    _bt.onClick.AddListener(() => { Onclick(x); });
                    break;

                case BadgeData.BadgeType.Time:
                    int y = i;
                    _bt.transform.SetParent(m_HourstDisplayParent.transform, false);
                    _bt.interactable = CheckHours(m_BadgeSet.BadgeData[i]);
                    _bt.onClick.AddListener(() => { Onclick(y); });

                    _bt.GetComponentInChildren<Text>().transform.rotation = Quaternion.Euler(0, 0, 11.73f);
                    break;

                case BadgeData.BadgeType.LastTrophy:
                    int z = i;
                    _bt.transform.SetParent(m_LastTrophy.transform, false);
                    _bt.interactable = CheckHours(m_BadgeSet.BadgeData[i]);
                    _bt.onClick.AddListener(() => { Onclick(z); });
                    break;
            }
        }
    }
    
    private bool CheckHours(BadgeData _bg)
    {
        System.TimeSpan ts = System.DateTime.Now - PlayerProfile.GetProfile().QuitDate.Value;

        if (_bg.Hours < (ts.Hours + ts.Days*24))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //private bool CheckCig(BadgeData _bg)
    //{
    //    System.TimeSpan ts = System.DateTime.Now - PlayerProfile.GetProfile().QuitDate.Value;

    //    if (_bg.NumberOfCigarettes < (ts.Days* PlayerProfile.GetProfile().NumOfCigPerDay))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    void OnDisable()
    {
        foreach (Transform child in m_QuitDisplayParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in m_HourstDisplayParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in m_LastTrophy.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Onclick(int _badgeID)
    {
        m_SharePage.SetBadgeIcon = m_BadgeSet.BadgeData[_badgeID].Icon;
        m_SharePage.SetDescriptionMessage = m_BadgeSet.BadgeData[_badgeID].Statement;
        m_SharePage.SetNameMessage = m_BadgeSet.BadgeData[_badgeID].Title;

        if (m_BadgeSet.BadgeData[_badgeID].GetBadgeType == BadgeData.BadgeType.Time)
        {
            m_SharePage.SetMessageText();
            m_SharePage.SetImageURL = "http://static.wixstatic.com/media/ac9214_2c55d3593a974625b5df1f29b1079502.jpg/v1/fill/w_125,h_124,al_c,q_90/ac9214_2c55d3593a974625b5df1f29b1079502.jpg";
            m_SharePage.ShareTitleText = "I AM STICKING TO IT";
        }
        else if(m_BadgeSet.BadgeData[_badgeID].GetBadgeType == BadgeData.BadgeType.LastTrophy)
        {
            m_SharePage.SetImageURL = "http://static.wixstatic.com/media/ac9214_9d23b6ee7d2b4e61b118374adb42c6b5.jpg/v1/fill/w_125,h_126,al_c,q_90/ac9214_9d23b6ee7d2b4e61b118374adb42c6b5.jpg";
            m_SharePage.ShareTitleText = m_BadgeSet.BadgeData[_badgeID].Title;
        }
        else
        {
            m_SharePage.SetImageURL = "http://static.wixstatic.com/media/ac9214_2cb0307bc6cd42f59c44a7a1d88d3321.png/v1/fill/w_125,h_125,al_c/ac9214_2cb0307bc6cd42f59c44a7a1d88d3321.png";
            m_SharePage.ShareTitleText = "MY QUIT DATE IS " + PlayerProfile.GetProfile().QuitDate.Value.Day + "/" + PlayerProfile.GetProfile().QuitDate.Value.Month + "/" + PlayerProfile.GetProfile().QuitDate.Value.Year;
        }

        m_SharePage.gameObject.SetActive(true);
        m_BadgePanel.SetActive(false);
    }

    public void BackBT()
    {
        m_SharePage.gameObject.SetActive(false);
        m_BadgePanel.SetActive(true);
    }

}

