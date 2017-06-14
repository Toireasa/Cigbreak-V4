using UnityEngine;
using System.Collections;
using System.Linq;
using CigBreakGUI;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CigBreak
{
    public class MapButtons : MonoBehaviour
    {
        [SerializeField]
        private LevelSet levelSet = null;

        private MapCameraController camController = null;

        [SerializeField]
        private LevelPopupV4 levelPopup = null;

        [SerializeField]
        private GameObject levelNumberPrefab = null;

        [SerializeField]
        private Material disabledMaterial = null;

        [SerializeField]
        private MapButton[] buttons = null;
        
        private int lastUnlocked = 0;

        private PlayerProfile playerProfile = null;

        public UnityAction<MapButton> OnButtonInitialised { get; set; }
        public UnityAction<GameObject> OnButtonsInitialised { get; set; }

        private void Awake()
        {
#if UNITY_EDITOR
            if (levelSet.LevelData.Length > buttons.Length)
            {
                Debug.Log("Not enough map buttons for all levels");
            }
#endif

            playerProfile = PlayerProfile.GetProfile();

            camController = Camera.main.GetComponent<MapCameraController>();

            InitialiseButtons();

        }

        private void InitialiseButtons()
        {
            List<LevelStatus> levelResults = playerProfile.LevelResults;

            for (int i = 0; i < buttons.Length; i++)
            {
                bool unlocked = false;
                if (i == 0 || i < levelResults.Count)
                {
                   
                    unlocked = true;
                }
                else if (i == levelResults.Count)
                {
                    unlocked = levelResults[i - 1].StarsEarned > 0;
                }
                buttons[i].Initialise(i, unlocked, camController);

                GameObject levelNumber = Instantiate(levelNumberPrefab) as GameObject;
                levelNumber.transform.SetParent(buttons[i].transform, false);

                LevelButtonOverlay overlay = levelNumber.GetComponent<LevelButtonOverlay>();
                overlay.LevelNumber.text = (i + 1).ToString();

                if (!unlocked)
                {
                    buttons[i].SetMaterial(disabledMaterial);
                }
                else
                {
                    buttons[i].OnButtonClicked += ShowLevelPopup;
                    if (i < levelResults.Count)
                    {
                        overlay.ShowStars(levelResults[i].StarsEarned);
                    }
                }

                if(OnButtonInitialised != null)
                {
                    OnButtonInitialised(buttons[i]);
                }
            }

            if (OnButtonsInitialised != null)
            {
                OnButtonsInitialised(buttons[lastUnlocked].gameObject);
            }
        }

        public MapButton GetLastUnlocked()
        {
            return buttons[lastUnlocked];
        }

        private void ShowLevelPopup(int levelIndex)
        {
            levelPopup.Initialise(levelSet.LevelData[levelIndex], levelIndex);
            levelPopup.gameObject.SetActive(true);
        }
    }
}
