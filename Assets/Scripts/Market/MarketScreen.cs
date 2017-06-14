using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using CigBreak;
public class MarketScreen : MonoBehaviour
{
    //[SerializeField]
    //private Text m_PlayersName = null;

    [SerializeField]
    private Text m_PanelText = null;

    [SerializeField]
    private Button m_SellBT = null;

    [SerializeField]
    private List<GameObject> m_VegetablesGameObjects = new List<GameObject>();

    [SerializeField]
    private GameObject m_Chest = null;

    private PlayerProfile m_Profile = null;

    int m_VegSum = 0;
    // Use this for initialization
    void Start()
    {
        m_Profile = PlayerProfile.GetProfile();

        //if (m_Profile.PlayerName != "")
        //{
        //    m_PlayersName.text = m_Profile.PlayerName + "'s Market";
        //}

        m_VegSum = m_Profile.GetUnlockedVeg().Sum(v => v.Value);

        if (m_VegSum>10)
        {
            m_PanelText.text = m_VegSum.ToString();
        }
        else
        {
            m_PanelText.text ="0" + m_VegSum.ToString();
        }

        int countToActivete = 0;
        if (m_VegSum >= 4)
        {
            if (m_VegSum>m_VegetablesGameObjects.Count)
            {
                countToActivete = m_VegetablesGameObjects.Count;
            }
            else
            {
                countToActivete = m_VegSum;
            }
        }
        else
        {
            countToActivete = m_VegSum;
            m_SellBT.interactable = false;
        }

        for (int i = 0; i < countToActivete; i++)
        {
            m_VegetablesGameObjects[i].SetActive(true);
        }
    }

    private bool m_AnimationStarted =false;

    void Update()
    {
        if (m_AnimationStarted && !m_Chest.GetComponent<Animation>().isPlaying)
        {
            m_AnimationStarted = false;
            m_Chest.SetActive(false);
            m_VegSum = m_Profile.GetUnlockedVeg().Sum(v => v.Value);

            if (m_VegSum > 10)
            {
                m_PanelText.text = m_VegSum.ToString();
            }
            else
            {
                m_PanelText.text = "0" + m_VegSum.ToString();
            }

            m_SellBT.interactable = false;

            for (int i = 0; i < m_VegetablesGameObjects.Count; i++)
            {
                m_VegetablesGameObjects[i].SetActive(false);
            }
        }
    }
    // ** Menu Buttons ** //

    public void MapBT()
    {
        LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
    }

    public void SellBT()
    {
        int vegToSell = m_VegSum / 4;
        m_Profile.AddCoins (vegToSell);
        m_Profile.RemoveVeg(vegToSell*4);

        m_Chest.SetActive(true);
        m_AnimationStarted = true;
        m_Chest.GetComponent<Animation>().Play();


    }

    // ** End Menu Buttons ** //
}
