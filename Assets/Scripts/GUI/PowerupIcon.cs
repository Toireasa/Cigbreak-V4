using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using UnityEngine.Events;

namespace CigBreakGUI
{
    public class PowerupIcon : MonoBehaviour
    {
        [SerializeField]
        private Image border = null;
        [SerializeField]
        private Image iconImage = null;
        [SerializeField]
        private Text count = null;
        [SerializeField]
        private Button button = null;

        public GameItemData AssignedPowerup { get; private set; }

        public UnityAction<PowerupIcon> OnClicked { get; set; }

        public void Initialise(Sprite icon, int count, GameItemData powerup)
        {
            border.color = Color.gray;
            iconImage.sprite = icon;
            this.count.text = count.ToString();
            AssignedPowerup = powerup;

            button.onClick.AddListener(() => { if (OnClicked != null) { OnClicked(this); } });
        }

        public void Toggle(bool selected)
        {
            border.color = selected ? Color.yellow : Color.gray;
        }


    }
}
