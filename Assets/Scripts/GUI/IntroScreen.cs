using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using CigBreak;
using Soomla.Profile;

namespace CigBreakGUI
{
    /// <summary>
    /// App intro screens
    /// </summary>
    public class IntroScreen : MonoBehaviour
    {
        private static bool m_SplashScreenPlayed = false;

        [SerializeField]
        private GameObject m_IntroScene = null;

        [SerializeField]
        private GameObject m_SplashScene_Logo = null;

		[SerializeField]
		private GameObject m_SplashScene_PersonSmoking = null;

        [SerializeField]
        private GameObject m_SettingPage = null;

        [SerializeField]
        private AudioClip m_ClickSound = null;        

        [SerializeField]
        BackEndController m_BackEndController = null;


        private AudioSource m_SoundEffectSource = null;

        private void OnEnable()
        {
            m_SoundEffectSource = this.GetComponentInParent<AudioSource>();

            if (!m_SplashScreenPlayed)
            {
                m_SplashScreenPlayed = true;
#if FREE_VERSION
                Instantiate(m_BackEndController);
#endif
                SoomlaProfile.Initialize();
                m_IntroScene.SetActive(false);
				m_SplashScene_Logo.SetActive(true);
            }
            else
            {
				m_SplashScene_Logo.SetActive(false);
                m_IntroScene.SetActive(true);
            }
        }

        public void Map()
        {
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
        }

        public void Setting()
        {
            m_SettingPage.SetActive(true);
        }

        public void PlaySound()
        {
            m_SoundEffectSource.PlayOneShot(m_ClickSound);
        }
    }
    
}