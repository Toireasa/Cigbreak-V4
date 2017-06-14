using UnityEngine;
using System.Collections;
using CigBreak;
using CigBreakGUI;

public class PopUpMessage : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PopUpMessage = null;

    [SerializeField]
    private GameObject m_YesScrene = null;

    [SerializeField]
    private GameObject m_NoScrene = null;
     
    private PlayerProfile m_Profile = null;

    void OnEnable()
    {
        m_Profile = PlayerProfile.GetProfile();
        m_Profile.LastPopUpDate = System.DateTime.Now;
    }

    public void YesBT()
    {
        m_Profile.AddCoins(1);
        m_PopUpMessage.SetActive(false);
        m_YesScrene.SetActive(true);


    }

    public void NoBT()
    {
        m_Profile.SpendCoins(1);
        m_NoScrene.SetActive(true);
        m_PopUpMessage.SetActive(false);


    }

    public void OKBT()
    {
        m_YesScrene.SetActive(false);
        m_NoScrene.SetActive(false);
        gameObject.SetActive(false);
    }
}
