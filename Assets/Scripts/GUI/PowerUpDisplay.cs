using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CigBreak;
using UnityEngine.UI;

namespace CigBreakGUI
{
    public class PowerUpDisplay : MonoBehaviour
    {
        [SerializeField]
        private List<Button> m_PowerUpButtons = new List<Button>();

        private Gameplay m_Gameplay = null;
        private PlayerProfile m_Profile = null;
        // Use this for initialization
        void Start()
        {
            m_Gameplay = Gameplay.gameplay;
            m_Profile = PlayerProfile.GetProfile();
            m_Gameplay.OnPowerUpUse += DisplayPowerUp;

            DisplayPowerUp();
        }

        private void DisplayPowerUp()
        {
            for (int i = 0; i < m_PowerUpButtons.Count; ++i)
            {
                if (m_Profile.PowerupInventory.ContainsKey(m_Gameplay.PowerUpList.Items[i].GUID))
                {
                    if (m_Profile.PowerupInventory[m_Gameplay.PowerUpList.Items[i].GUID] > 0)
                    {
                        m_PowerUpButtons[i].gameObject.SetActive(true);
                        m_PowerUpButtons[i].GetComponentInChildren<Text>().text = PlayerProfile.GetProfile().PowerupInventory[m_Gameplay.PowerUpList.Items[i].GUID].ToString();
                    }
                    else
                    {
                        //m_PowerUpButtons[i].gameObject.SetActive(false);
                        m_PowerUpButtons[i].interactable = false;
                        m_PowerUpButtons[i].GetComponentInChildren<Text>().text = "";
                    }
                }
                else
                {
                    //m_PowerUpButtons[i].gameObject.SetActive(false);    
                    m_PowerUpButtons[i].interactable = false;
                    m_PowerUpButtons[i].GetComponentInChildren<Text>().text = "";
                }
            }
        }
    }
}
