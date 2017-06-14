using UnityEngine;
using System.Collections;
using CigBreak;
using CigBreakGUI;
using Soomla.Store;

public class UnlockFullGame : MonoBehaviour
{
    [SerializeField]
    GameObject m_MainPage = null;

    [SerializeField]
    GameObject m_PostCodePage = null;

    [SerializeField]
    GameObject m_NotValidPCPage = null;

    [SerializeField]
    ErrorPage m_ErrorPage = null;

    GameObject m_BackEndController = null;

    void Start()
    {
              
    }

    public void MapBT()
    {
        gameObject.SetActive(false);
    }

    public void BuyBT()
    {
        m_BackEndController = GameObject.Find("BackEndController(Clone)");
        m_BackEndController.GetComponent<BackEndController>().SetUnlockFullGame(this);
        m_BackEndController.GetComponent<BackEndController>().UnlockFullGame();        
    }

    public void RegisterBT()
    {
        m_MainPage.SetActive(false);
        m_PostCodePage.SetActive(true);
    }

    public void NoValidPostCodeBackBT()
    {
        m_NotValidPCPage.SetActive(false);
        m_PostCodePage.SetActive(true);
    }

    public void ErrorPage()
    {
        m_ErrorPage.gameObject.SetActive(true);
        m_ErrorPage.PreviousPage = this.gameObject;
        gameObject.SetActive(false);
    }

    public void GameUnlock()
    {
        //m_GameUnlockPage.SetActive(true);
        gameObject.SetActive(false);
    }
	public void Email()
	{
		Application.OpenURL ("mailto:cigbreak.game@gmail.com?subject=EmailSubject&body=EmailBody");
	}
}
