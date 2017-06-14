using UnityEngine;
using System.Collections;

namespace CigBreak
{

    public class FailTutorial : ScriptableObject
    {
        [SerializeField]
        [HideInInspector]
        private string guid = "";
        public string GUID { get { return guid; } }

        [SerializeField]
        private GameItemData.ItemType itemType = GameItemData.ItemType.Unhealthy;
        public GameItemData.ItemType ItemType { get { return itemType; } }

        [SerializeField]
        private TutorialData.TutorialInstruction.Action failAction = TutorialData.TutorialInstruction.Action.None;
        public TutorialData.TutorialInstruction.Action FailAction { get { return failAction; } }

        [SerializeField]
        private GameObject uiPrefab = null;
        public GameObject UIPrefab { get { return uiPrefab; } }

        private void Reset()
        {
            guid = System.Guid.NewGuid().ToString();
        }
    }

}