using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CigBreak;

public class BusStopScene : MonoBehaviour
{
    [SerializeField]
    private List<RemovableObject> m_CigToRemove = new List<RemovableObject>();

    [SerializeField]
    private GameObject m_Panel = null;

    [SerializeField]
    private GameObject m_InstractionPanel = null;

    private int m_ObjectRenoved = 0;

    private void Awake()
    {
        // Analitics


        //if (PlayerProfile.GetProfile().ScenePlayed.Contains("BusStop"))
        //{
        //    m_InstractionPanel.SetActive(false);
        //}
        if (PlayerPrefs.GetString("BusStop") != string.Empty)
        {
            m_InstractionPanel.SetActive(false);
        }

        for (int i = 0; i < m_CigToRemove.Count; i++)
        {
            m_CigToRemove[i].OnButtonClicked = CigRemoved;
        }
    }

    public void CigRemoved()
    {
        m_ObjectRenoved++;

        if (m_ObjectRenoved >= m_CigToRemove.Count)
        {
            m_Panel.SetActive(true);

            // Analitics

        }
    }

    public void OnOk()
    {
        m_InstractionPanel.SetActive(false);
    }

    public void MapBT()
    {
        //if (!PlayerProfile.GetProfile().ScenePlayed.Contains("BusStop"))
        //{
        //    PlayerProfile.GetProfile().ScenePlayed.Add("BusStop");
        //    PlayerProfile.GetProfile().SaveProfile();
        //}
        if (PlayerPrefs.GetString("BusStop") == string.Empty)
        {
            PlayerPrefs.SetString("BusStop", "Played");
            PlayerPrefs.Save();
        }
        LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
    }
}
