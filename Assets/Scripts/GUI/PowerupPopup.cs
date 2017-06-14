using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using CigBreak;
using System.Linq;
using System;

namespace CigBreakGUI
{
    public class PowerupPopup : MonoBehaviour
    {
        //[SerializeField]
        //private LevelPopup levelPopup = null;

        [Header("Elements")]
        [SerializeField]
        private Text coinInventory = null;
        [SerializeField]
        private GameObject displayParent = null;
        [SerializeField]
        private ScrollRect scrollRect = null;
        [SerializeField]
        private Scrollbar scrollbar = null;

        [Header("Data")]
        [SerializeField]
        private PowerupItem itemPrefab = null;
        [SerializeField]
        private GameItemSet powerupsSet = null;

        [Header("Buttons")]
        [SerializeField]
        private Button back = null;

        //[SerializeField]
        //private GameObject m_WarningPanel = null; 

        private PlayerProfile profile = null;

        private List<PowerupData> m_SelectedPowerups = new List<PowerupData>();
        private List<PowerupItem> m_PowerUpItems = new List<PowerupItem>();

        private void OnEnable()
        {
            m_SelectedPowerups.Clear();
            if (m_PowerUpItems.Count!=0)
            {
                foreach (PowerupItem PI in m_PowerUpItems)
                {
                    PI.RemoveToggle();
                    PI.UpDate(profile.Coins >= PI.GetPowerupDate().CoinPrice);
                }
            }
            coinInventory.text = profile.Coins.ToString();
            //if (profile.Coins < 2)
            //{
            //    m_WarningPanel.SetActive(true);
            //}
        }
        
        private void OnDisable()
        {
            m_SelectedPowerups.Clear();
            if (m_PowerUpItems.Count != 0)
            {
                foreach (PowerupItem PI in m_PowerUpItems)
                {
                    PI.RemoveToggle();
                }
            }
        }
        private void Awake()
        {
            // Connect scrollrect and scrolbar - not doing this in editor as we don't want the default scalling scrollbar handle
            scrollbar.onValueChanged.AddListener((val) => scrollRect.verticalNormalizedPosition = val);

            //Load data
            profile = PlayerProfile.GetProfile();
            coinInventory.text = profile.Coins.ToString();
            foreach (string id in profile.GetUnlockedPowerups())
            {
                GameItemData item = powerupsSet.Items.FirstOrDefault(p => p.GUID == id);
                PowerupData powerup = (PowerupData)item;
                PowerupItem icon = Instantiate(itemPrefab) as PowerupItem;
                icon.transform.SetParent(displayParent.transform, false);
                icon.Initialise(powerup, this, profile.Coins >= powerup.CoinPrice);
                m_PowerUpItems.Add(icon);
            }

            // Buttons
            back.onClick.AddListener(() => gameObject.SetActive(false));

            //if (profile.Coins<2)
            //{
            //    m_WarningPanel.SetActive(true);
            //}
        }

        public void OnBuy()
        {
            coinInventory.text = profile.Coins.ToString();
            foreach (PowerupItem PI in m_PowerUpItems)
            {
                if (!m_SelectedPowerups.Contains(PI.GetPowerupDate()))
                {
                    PI.UpDate(profile.Coins >= PI.GetPowerupDate().CoinPrice);
                }
            }
        }

        private void Start()
        {
            scrollbar.value = 0.99f;
        }

        //int moneytoSend = 0;
        //private void ToggleChanged(bool state, PowerupData powerup)
        //{
        //    if (!test)
        //    {
        //        if (state)
        //        {
        //            m_SelectedPowerups.Add(powerup);
        //            moneytoSend += powerup.CoinPrice;
        //            //PlayerProfile.GetProfile().SpendCoins(powerup.CoinPrice);
        //            coinInventory.text = (profile.Coins - moneytoSend).ToString();
        //            foreach (PowerupItem PI in m_PowerUpItems)
        //            {
        //                if (!m_SelectedPowerups.Contains(PI.GetPowerupDate()))
        //                {
        //                    PI.UpDate(profile.Coins - moneytoSend >= PI.GetPowerupDate().CoinPrice);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            m_SelectedPowerups.Remove(powerup);
        //            moneytoSend -= powerup.CoinPrice;
        //            coinInventory.text = (profile.Coins - moneytoSend).ToString();
        //            if (m_PowerUpItems.Count != 0)
        //            {
        //                foreach (PowerupItem PI in m_PowerUpItems)
        //                {
        //                    PI.UpDate(profile.Coins - moneytoSend >= PI.GetPowerupDate().CoinPrice);
        //                }
        //            }
        //        }
        //    }
        //}


    }
}
