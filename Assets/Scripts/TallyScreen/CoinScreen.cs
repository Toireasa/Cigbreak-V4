using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;

namespace CigBreakGUI
{
    public class CoinScreen : MonoBehaviour
    {
        [SerializeField]
        private Text m_MessageTXT = null;

        [Header("Messages")]
        [Multiline]
        [SerializeField]
        private string[] messages = null;

        [Multiline]
        [SerializeField]
        private string[] m_USMessages = null;

        private string[] m_MessagesToUse = null;

        private static int messageIndex = 0;

        public void Display(bool forceMapExit)
        {
            gameObject.SetActive(true);

            if (PlayerProfile.GetProfile().Contry == Country.UK)
                m_MessagesToUse = messages;
            else
                m_MessagesToUse = m_USMessages;            

            m_MessageTXT.text = GetNextMessage();
        }

        private string GetNextMessage()
        {
            messageIndex++;
            messageIndex = messageIndex >= m_MessagesToUse.Length ? 0 : messageIndex;

            return m_MessagesToUse[messageIndex];
        }

    }
}
