using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;

public class EmailPage : MonoBehaviour
{
    [SerializeField]
    private InputField m_Email;

    [SerializeField]
    private GameObject m_ConfirnEmailPage = null;

    [SerializeField]
    private GameObject m_PreviousPage = null;
    
    public void OnOk()
    {
        PlayerProfile.GetProfile().Email = m_Email.text;
        m_ConfirnEmailPage.SetActive(true);
        gameObject.SetActive(false);
    }

    public void BackBT()
    {
        m_PreviousPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
