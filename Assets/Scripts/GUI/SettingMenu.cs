using UnityEngine;
using CigBreak;


namespace CigBreakGUI
{
    public class SettingMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_IntroScene = null;

        [SerializeField]
        private GameObject m_MainPage = null;

        [SerializeField]
        private GameObject m_LanguagePage = null;

        [SerializeField]
        private GameObject m_AboutPage = null;

        [SerializeField]
        private GameObject m_LanguageBackBT = null;

        private GameObject m_CurrentPage = null;



        void OnEnable()
        {
            m_MainPage.SetActive(true);
            m_LanguagePage.SetActive(false);
            m_AboutPage.SetActive(false);
            m_LanguageBackBT.SetActive(true);
            m_CurrentPage = m_MainPage;
            m_InitialSetting = false;
        }

        bool m_InitialSetting = false;
        public void EnableLanguage()
        {
            m_MainPage.SetActive(false);
            m_LanguagePage.SetActive(true);
            m_InitialSetting = true;
            m_LanguageBackBT.SetActive(false);
        }

        public void USButton()
        {
            PlayerProfile.GetProfile().Contry = Country.US;
            m_IntroScene.SetActive(true);
            gameObject.SetActive(false);
        }

        public void UKButton()
        {
            PlayerProfile.GetProfile().Contry = Country.UK;
            m_IntroScene.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OpenPrivacy()
        {
            Application.OpenURL("http://www.healthygames.co.uk/#!privacy-policy/lbm0c");
        }

        public void OpenCredits()
        {
            Application.OpenURL("http://www.healthygames.co.uk/");
        }

        public void CloseSettings()
        {
            this.gameObject.SetActive(false);
            m_IntroScene.SetActive(true);
        }

        public void LangugeBT()
        {
            m_CurrentPage.SetActive(false);
            m_LanguagePage.SetActive(true);
            m_CurrentPage = m_LanguagePage;
        }

        public void AboutBT()
        {
            m_CurrentPage.SetActive(false);
            m_AboutPage.SetActive(true);
            m_CurrentPage = m_AboutPage;
        }

        public void BackBT()
        {
            if (!m_InitialSetting)
            {
                m_CurrentPage.SetActive(false);
                m_MainPage.SetActive(true);
                m_CurrentPage = m_MainPage;
            }
            else
            {
                this.gameObject.SetActive(false);
                m_IntroScene.SetActive(true);
            }
        }
    }
}
