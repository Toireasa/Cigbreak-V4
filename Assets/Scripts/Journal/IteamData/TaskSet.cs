using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CigBreak
{
    public class TaskSet : ScriptableObject
    {

        [SerializeField]
        private TasksData[] tasksData = null;
        public TasksData[] TasksData { get { return tasksData; } }

        // Supply a separate IndexOf function, since it's not accessible via IEnumerable<>
        public int IndexOf(TasksData data)
        {
            return Array.IndexOf(tasksData, data);
        }
    }
}
