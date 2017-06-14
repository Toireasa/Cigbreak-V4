using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CigBreakGUI
{
    public class FailScreen : MonoBehaviour
    {
        [SerializeField]
        private Text levelNumber = null;

        public void Display(int _CurrentLevelIndex)
        {
            gameObject.SetActive(true);

            levelNumber.text = (_CurrentLevelIndex + 1).ToString();

        }

        public void CloseScreen()
        {
            gameObject.SetActive(false);
        }

        public void ReplayBT()
        {
            gameObject.SetActive(false);
        }

    } 
}
