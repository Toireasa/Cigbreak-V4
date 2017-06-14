using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;

namespace CigBreakGUI
{
    public class Pause : MonoBehaviour
    {
        [SerializeField]
        private Button pauseButton = null;
        [SerializeField]
        private GameObject pauseMenu = null;
        [SerializeField]
        private Button quitButton = null;
        [SerializeField]
        private Button playButton = null;
        [SerializeField]
        private Button mapButton = null;

        private Gameplay gameplay = null;

        private void Start()
        {
            gameplay = Gameplay.gameplay;
            pauseButton.onClick.AddListener(TogglePause);
            quitButton.onClick.AddListener(Quit);
            playButton.onClick.AddListener(TogglePause);
            mapButton.onClick.AddListener(Map);
        }

        private void TogglePause()
        {
            gameplay.TogglePause(!gameplay.Paused);
            if(pauseMenu != null)
            {
                pauseMenu.SetActive(gameplay.Paused);
                pauseButton.gameObject.SetActive(!gameplay.Paused);
            }
        }

        private void Quit()
        {
            Application.Quit();
        }

        private void Map()
        {
            Time.timeScale = 1f;
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
        }
    } 
}
