using UnityEngine;
using UnityEngine.UI;
using CigBreak;
using System.Linq;

namespace CigBreakGUI
{
    public class TaskItem : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Toggle = null;

        [SerializeField]
        private Text m_TaskName = null;

        [SerializeField]
        private Text m_Coins = null;

        [SerializeField]
        private GameObject m_RequirementText = null;

        private TasksData m_Task = null;

       // private ToggleGroup toggleGroup = null;

        private TaskSummary m_GenericPage = null;
        //TaskStatus status = null;

        public void Initialise(TasksData data, ToggleGroup group, TaskSummary _page)
        {
            m_Task = data;
            m_GenericPage = _page; 
           // toggleGroup = group;
           
            if (PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == m_Task.ID) != null)
            {                               
				m_TaskName.text = "Mission "+ m_Task.ID.ToString() + ":\n" + m_Task.Title;
                m_Toggle.SetActive(true);
            }
            else
            {
				m_TaskName.text = "Mission " + m_Task.ID.ToString() + "\n";

                if (m_Task.Requirement != 0)
                {
                    if (PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == m_Task.Requirement) == null)
                    {
                        m_RequirementText.SetActive(true);
                        m_RequirementText.GetComponent<Text>().text = "First Complete " + m_Task.Requirement;
                        GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        m_RequirementText.SetActive(false);
                        GetComponent<Button>().interactable = true;
                    }
                }
            }

            m_Coins.text = "Rewards: "+ m_Task.Coins.ToString();
        }

        public void StartTask()
        {
            m_GenericPage.LoadTask(m_Task);
        }

        public void UpdateMarker()
        {
            if (PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == m_Task.ID) != null)
            {
                m_Toggle.SetActive(true);
				m_TaskName.text = "Mission " + m_Task.ID + ":\n" + m_Task.Title;
            }
            else
            {
                if (m_Task.Requirement != 0)
                {
                    if (PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == m_Task.Requirement) == null)
                    {
                        m_RequirementText.SetActive(true);
                        m_RequirementText.GetComponent<Text>().text = "First Complete " + m_Task.Requirement;
                        GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        m_RequirementText.SetActive(false);
                        GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
    }
}
