using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using CigBreak;
using System.Linq;
using System;

namespace CigBreakGUI
{
    public class TaskSummary : MonoBehaviour
    {
        [SerializeField]
        private UnlockFullGame m_UnlockGamePage = null;

        [Header("Data")]
        [SerializeField]
        private TaskItem itemPrefab = null;

        [SerializeField]
        private TaskSet m_TaskSet;

        [SerializeField]
        private GameObject m_MainPage = null;

        [Header("TaskType")]

        [SerializeField]
        private TaskPage m_ListThreeTask = null;

        [SerializeField]
        private TaskPage m_ListOneTask = null;

        [SerializeField]
        private TaskPage m_PhotoTask = null;

        [SerializeField]
        private TaskPage m_SimpleTask = null;

        [SerializeField]
        private TaskPage m_CalculateMoneyTask = null;

        [Header("Elements")]
        [SerializeField]
        private GameObject displayParent = null;

        [SerializeField]
        private ToggleGroup toggleGroup = null;

        private List<TaskItem> m_ItemList = new List<TaskItem>();
        private TaskPage m_CurrentPage;

        void OnEnable()
        {            
            m_ItemList.Clear();
            foreach (Transform child in displayParent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            //Load data
            for (int i = 0; i < m_TaskSet.TasksData.Count<TasksData>(); i++)
            {
                TaskItem icon = Instantiate(itemPrefab) as TaskItem;
                icon.transform.SetParent(displayParent.transform, false);
                icon.Initialise(m_TaskSet.TasksData[i], toggleGroup, this);
                m_ItemList.Add(icon);
            }
        }

        public void UpdateMarkers()
        {
            for (int i = 0; i < m_ItemList.Count; i++)
            {
                m_ItemList[i].UpdateMarker();
            }
        }

        // ** Load Task Functions ** //

        public void LoadTask(TasksData _taskData)
        {
            switch (_taskData.Type)
            {
                case TasksData.TaskType.ListThree:
                    m_CurrentPage = m_ListThreeTask;
                    break;

                case TasksData.TaskType.ListOne:
                    m_CurrentPage = m_ListOneTask;
                    break;

                case TasksData.TaskType.CalculateMoney:
                    m_CurrentPage = m_CalculateMoneyTask;
                    break;

                case TasksData.TaskType.Photo:
                    m_CurrentPage = m_PhotoTask;
                    break;

                default:
                    m_CurrentPage = m_SimpleTask;
                    break;
            }
#if FREE_VERSION
            if (!PlayerProfile.GetProfile().UnlockFullGame && !_taskData.FreeToPlay)
            {
                m_UnlockGamePage.gameObject.SetActive(true);
            }
            else {
                m_CurrentPage.SetTaskData(_taskData);
                m_CurrentPage.SummaryPage = this;
                m_CurrentPage.gameObject.SetActive(true);
                m_MainPage.SetActive(false);
            }
# else
            m_CurrentPage.SetTaskData(_taskData);
            m_CurrentPage.SummaryPage = this;
            m_CurrentPage.gameObject.SetActive(true);
            m_MainPage.SetActive(false);
#endif
        }

        public void UnloadTask()
        {
            UpdateMarkers();
            m_CurrentPage.gameObject.SetActive(false);
            m_MainPage.SetActive(true);
        }


    }
}
