using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using UnityEngine.Events;

namespace CigBreakGUI
{
    public class VegScreen : MonoBehaviour
    {
        [SerializeField]
        private Image title = null;
        [SerializeField]
        private Image vegIcon = null;
        [SerializeField]
        private Text vegName = null;
        [SerializeField]
        private Text vegDescription = null;


        public void Display(VegReward vegReward, bool forceMapExit)
        {
            gameObject.SetActive(true);
            title.sprite = vegReward.Title;
            vegIcon.sprite = vegReward.Icon;
            vegName.text = vegReward.VegName.ToUpper();
            vegName.color = vegReward.VegColor;
            vegDescription.text = vegReward.GetNextDescription();
        }
    } 
}
