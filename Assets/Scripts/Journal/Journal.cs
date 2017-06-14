using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

using CigBreak;

namespace CigBreakGUI
{
    public class Journal : MonoBehaviour
    {
        [Header("Index Pages")]
        [SerializeField]
        private GameObject m_PersonalisePage = null;

        [SerializeField]
        private GameObject m_CoverPage = null;

        [SerializeField]
        private GameObject m_ProgressPage = null;

        [SerializeField]
        private GameObject m_MissionsPage = null;

        [SerializeField]
        private GameObject m_BadgessPage = null;

        [SerializeField]
        private GameObject m_CravingsPage = null;

        [SerializeField]
        private GameObject m_PopUpPanel = null;

        [Header("Index Buttons")]
        [SerializeField]
        private GameObject m_IndexButtons = null;

        [Header("Fields")]
        [SerializeField]
        private Text m_NameField = null;

        private GameObject m_CurrentPage = null;

        private PlayerProfile m_Profile = null;

        private void Awake()
        {
            m_Profile = PlayerProfile.GetProfile();

            if (m_Profile.PlayerName == string.Empty)
            {
                m_PersonalisePage.SetActive(true);
                m_CurrentPage = m_PersonalisePage;
                m_IndexButtons.SetActive(false);

            }
            else
            {
                if (m_Profile.LastPopUpDate == null)
                {
                    m_Profile.LastPopUpDate = System.DateTime.Now;
                }

                System.TimeSpan message = System.DateTime.Now - m_Profile.LastPopUpDate.Value;

                if (message.Days > 0 || message.Hours >= 12)
                {
                    m_PopUpPanel.SetActive(true);
                }

                m_CurrentPage = m_CoverPage;
                m_CurrentPage.SetActive(true);

                m_NameField.text = m_Profile.PlayerName + "'s";
            }
        }


        public void OpenCoverPage()
        {
            m_IndexButtons.SetActive(true);
            m_CurrentPage.SetActive(false);
            m_CurrentPage = m_CoverPage;
            m_CurrentPage.SetActive(true);

            m_NameField.text = m_Profile.PlayerName + "'s";
        }

        //Index Buttons Functions

        public void ProgressButton()
        {
            m_CurrentPage.SetActive(false);
            m_ProgressPage.SetActive(true);
            m_CurrentPage = m_ProgressPage;
        }

        public void PersonaliseButton()
        {
            m_CurrentPage.SetActive(false);
            m_PersonalisePage.SetActive(true);
            m_CurrentPage = m_PersonalisePage;
        }

        
        public void MissionsButton()
        {
            m_CurrentPage.SetActive(false);
            m_MissionsPage.SetActive(true);
            m_CurrentPage = m_MissionsPage;
        }


        public void CravingsButton()
        {
            m_CurrentPage.SetActive(false);
            m_CravingsPage.SetActive(true);
            m_CurrentPage = m_CravingsPage;
        }


        public void BadgesButton()
        {
            m_CurrentPage.SetActive(false);
            m_BadgessPage.SetActive(true);
            m_CurrentPage = m_BadgessPage;
        }

        public void Close()
        {
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
        } 

    } 
}
