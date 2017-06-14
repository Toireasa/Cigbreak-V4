using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using CigBreak;

public class LoungeScene : MonoBehaviour
{
    [SerializeField]
    private List<RemovableObject> m_CigToRemover = new List<RemovableObject>();

	[SerializeField]
	private Text m_HealthMessageField = null;

    [SerializeField]
    private GameObject m_HealthMessagePanel = null;

    [SerializeField]
	private string[] m_HealthMessages;

    [SerializeField]
    private ParticleSystem m_Smoke = null;

    [SerializeField]
    private GameObject m_OkPanel = null;

    private int m_ObjectRenoved = 0;

    private void Awake()
    {


        if (PlayerPrefs.GetString("Lounge") != string.Empty)
        {
            m_OkPanel.SetActive(false);
        }

        for (int i=0; i<m_CigToRemover.Count;i++)
        {
            m_CigToRemover[i].OnButtonClicked = RemoveParticles;
        }        
    }

    private void RemoveParticles()
    {
        m_ObjectRenoved++;

        if (m_ObjectRenoved >= m_CigToRemover.Count)
        {

            int MessageNum = PlayerPrefs.GetInt("LoungMessagePlayed");


            m_HealthMessagePanel.SetActive(true);
            m_HealthMessageField.text = m_HealthMessages[MessageNum];

            MessageNum++;
            if (MessageNum == 2)
            {
                PlayerPrefs.SetInt("LoungMessagePlayed", 0);

            }
            else
            {
                PlayerPrefs.SetInt("LoungMessagePlayed", MessageNum);
            }
            PlayerPrefs.Save();

            m_Smoke.Stop();


        }
    }

    public void OnOK()
    {
        m_OkPanel.SetActive(false);
    }

    // ** Menu Buttons ** //
    public void MapBT()
    {
        //if (!PlayerProfile.GetProfile().ScenePlayed.Contains("Lounge"))
        //{
        //    PlayerProfile.GetProfile().ScenePlayed.Add("Lounge");
        //    PlayerProfile.GetProfile().SaveProfile();
        //}

        if (PlayerPrefs.GetString("Lounge") == string.Empty)
        {
            PlayerPrefs.SetString("Lounge", "Played");
            PlayerPrefs.Save();
        }
        LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
    }
    // ** End Menu Buttons ** //
}
