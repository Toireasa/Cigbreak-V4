using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CigBreak;

public class BedRoomScene : MonoBehaviour
{
    [SerializeField]
    private List<RemovableObject> m_CigToRemove = new List<RemovableObject>();

    [SerializeField]
    private GameObject m_HealthMessagePanel = null;

    [SerializeField]
    private GameObject m_InstractionPanel = null;

    private int m_ObjectRenoved = 0;

    private void Awake()
    {


        if (PlayerPrefs.GetString("BedRoom") != string.Empty)
        {
            m_InstractionPanel.SetActive(false);
        }

        for (int i = 0; i < m_CigToRemove.Count; i++)
        {
            m_CigToRemove[i].OnButtonClicked = CheckActiveObjects;
        }
    }

    public void MapBT()
    {
        LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
    }

    public void CheckActiveObjects()
    {
        m_ObjectRenoved++;

        if (m_ObjectRenoved >=m_CigToRemove.Count)
        {
            m_HealthMessagePanel.SetActive(true);

            // Analitics

        }
    }

    public void OnOk()
    {
        if (PlayerPrefs.GetString("BedRoom") == string.Empty)
        {
            PlayerPrefs.SetString("BedRoom", "Played");
            PlayerPrefs.Save();
        }
        m_InstractionPanel.SetActive(false);
    }
	
}
