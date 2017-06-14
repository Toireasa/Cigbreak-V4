using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CigBreakGUI
{
    public class LevelButtonOverlay : MonoBehaviour
    {
        [SerializeField]
        private Text levelNumber = null;
        public Text LevelNumber { get { return levelNumber; } }

        [SerializeField]
        private GameObject[] stars = null;

        public void ShowStars(int count)
        {
            for(int i = 0; i < count; i++)
            {
                stars[i].SetActive(true);
            }
        }

    } 
}
