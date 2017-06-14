using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using UnityEngine.Events;

namespace CigBreakGUI
{
    public class PowerupItem : MonoBehaviour
    {
        [SerializeField]
        private Button infoButton = null;

        [SerializeField]
        private Image icon = null;

        [SerializeField]
        private Text powerupName = null;

        [SerializeField]
        private Text price = null;

        [SerializeField]
        private Text m_Amount = null;

        [SerializeField]
        private Button m_BuyButton = null;

        [Header("Reverse")]
        [SerializeField]
        private GameObject reverse = null;
        [SerializeField]
        private Text description = null;

        private PowerupData m_PowerupDate = null;
        public PowerupData GetPowerupDate()
        {
            return m_PowerupDate;
        }
        PowerupPopup m_PowerupPopupState = null;

        public void Initialise(PowerupData data, PowerupPopup state, bool canAfford)
        {
            m_PowerupDate = data;
            m_PowerupPopupState = state;

            icon.sprite = data.Sprite;
            powerupName.text = data.Title;
            price.text = data.CoinPrice.ToString();
            description.text = data.Desctiprion;
            m_Amount.text = PlayerProfile.GetProfile().PowerupInventory[m_PowerupDate.GUID].ToString();

            if (!canAfford)
            {
                price.color = Color.red;
                m_BuyButton.interactable = false;
            }
            else
            {
                m_BuyButton.interactable = true;
            }

            infoButton.onClick.AddListener(ToggleReverse);
        }

        public void UpDate(bool canAfford)
        {
            if (!canAfford)
            {
                price.color = Color.red;
                m_BuyButton.interactable = false;
            }
            else
            {
                price.color = Color.green;
                m_BuyButton.interactable = true;
            }
        }

        private void ToggleReverse()
        {
            reverse.SetActive(!reverse.activeSelf);
        }

        public void RemoveToggle()
        {
            //toggle.isOn = false;
        }

        public void OnBuy()
        {
            PlayerProfile.GetProfile().AddPowerUp(m_PowerupDate.GUID);
            PlayerProfile.GetProfile().SpendCoins(m_PowerupDate.CoinPrice);
            m_Amount.text = PlayerProfile.GetProfile().PowerupInventory[m_PowerupDate.GUID].ToString();
            m_PowerupPopupState.OnBuy();

        }
    } 
}
