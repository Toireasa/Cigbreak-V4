using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using CigBreak;

namespace CigBreakGUI
{
    public class TaskPage : MonoBehaviour
    {
        [SerializeField]
        protected Text m_Title = null;

        [SerializeField]
        protected Text m_Statement = null;

        [SerializeField]
        protected Text m_Comment = null;


        protected TaskSummary m_SummaryPage;
        public TaskSummary SummaryPage
        {
            set { m_SummaryPage = value; }
        }

        protected TasksData m_Task;
        public void SetTaskData(TasksData _data)
        {
            m_Task = _data;
        }
        protected TaskStatus m_Status = null;

        protected virtual void OnEnable()
        {
            m_Status = PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == m_Task.ID);

            m_Title.text = m_Task.Title;
            m_Statement.text = m_Task.Statement;

            if (m_Comment != null)
            {
                m_Comment.text = m_Task.Comment;

            }
        }

        public virtual void OnOk()
        {
            if (PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == m_Task.ID) == null)
            {
                PlayerProfile.GetProfile().AddCoins(m_Task.Coins);
            }

            PlayerProfile.GetProfile().RecordTaskStatus(m_Task.ID, "");
            OnBack();
        }

        public void OnBack()
        {
            m_SummaryPage.UnloadTask();
        }

        public void OpenUrl()
        {
            Application.OpenURL("http://www.nhs.uk/smokefree");
        }

    }
}