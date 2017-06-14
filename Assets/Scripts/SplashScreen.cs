using UnityEngine;
using System.Collections;
using CigBreak;

namespace CigBreakGUI
{
    public class SplashScreen : MonoBehaviour
    {
        //[SerializeField]
        //GameObject[] m_Screnes = null;

        [SerializeField]
        GameObject MainMenu = null;

        [SerializeField]
        SettingMenu m_SettingsPage = null;

        private float m_Timer = 0;
        private bool m_TimeOn = false;

        // Use this for initialization
        void Start()
        {
            m_TimeOn = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (m_TimeOn && m_Timer <= 3)
            {
                m_Timer += Time.deltaTime;
            }
            else if (m_TimeOn)
            {
                m_TimeOn = false;

//#if !FREE_VERSION

                if (PlayerProfile.GetProfile().Contry == Country.NONE)
                {
                    m_SettingsPage.gameObject.SetActive(true);
                    m_SettingsPage.EnableLanguage();
                    gameObject.SetActive(false);
                }
                else
                {
                    MainMenu.SetActive(true);
                    gameObject.SetActive(false);
                }
//#else
                MainMenu.SetActive(true);
                gameObject.SetActive(false);
//#endif
            }                       
        }
    }
}
