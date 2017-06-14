using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using CigBreakGUI;
using System.Linq;
using UnityEngine.Events;

namespace CigBreak
{
    public class MapButton : MonoBehaviour
    {
        private int m_LevelIndex = 0;
        public int LevelIndex
        {
            get { return m_LevelIndex; }
            private set { m_LevelIndex = value; }
        }

        private bool m_Unlocked = false;
        public bool Unlocked
        {
            get { return m_Unlocked; }
            private set { m_Unlocked = value; }
        }

        private MapCameraController camControl = null;
        //private MeshRenderer meshRenderer = null;

        public UnityAction<int> OnButtonClicked { get; set; }

        //private void Awake()
        //{
        //    //LevelIndex = -1;
        //    meshRenderer = GetComponent<MeshRenderer>();
        //}

        public void Initialise(int levelIndex, bool unlocked, MapCameraController camController)
        {
            m_LevelIndex = levelIndex;
            m_Unlocked = unlocked;
            camControl = camController;
        }

        private void OnMouseUpAsButton()
        {
            // Only accept event if they are not being captured by the gui
            if (EventSystem.current.currentSelectedGameObject == null && 
                !camControl.Scrolling &&
                Unlocked &&
                OnButtonClicked != null)
            {
                OnButtonClicked(LevelIndex);
            }
        }

        public void SetMaterial(Material mat)
        {
            if (GetComponent<MeshRenderer>() != null)
            {
                Material[] mats = GetComponent<MeshRenderer>().materials;
                mats[0] = mat;
                GetComponent<MeshRenderer>().materials = mats;
            }
        }
    } 
}
