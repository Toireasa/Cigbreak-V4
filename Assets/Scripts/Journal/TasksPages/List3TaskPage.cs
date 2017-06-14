using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using CigBreak;

namespace CigBreakGUI
{
    public class List3TaskPage : TaskPage
    {
        [SerializeField]
        protected InputField m_AnswerFirstLine;
        [SerializeField]
        protected InputField m_AnswerSecondLine;
        [SerializeField]
        protected InputField m_AnswerThirdLine;

        protected override void OnEnable()
        {
            base.OnEnable();

            if (m_Status != null)
            {
                string[] textSplit = new string[3];
                textSplit = m_Status.Answer.Split("/"[0]);
                m_AnswerFirstLine.text = textSplit[0];
                m_AnswerSecondLine.text = textSplit[1];
                m_AnswerThirdLine.text = textSplit[2];
            }
        }

        private void OnDisable()
        {
            m_AnswerFirstLine.text = "";
            m_AnswerSecondLine.text ="";
            m_AnswerThirdLine.text = "";
        }


        public override void OnOk()
        {
            if (m_AnswerFirstLine.text != string.Empty && m_AnswerSecondLine.text != string.Empty && m_AnswerThirdLine.text != string.Empty)
            {
                base.OnOk();

                string answer = m_AnswerFirstLine.text + "/" + m_AnswerSecondLine.text + "/" + m_AnswerThirdLine.text;
                PlayerProfile.GetProfile().RecordTaskStatus(m_Task.ID, answer);
            }
        }
    }
}

