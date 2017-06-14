using UnityEngine;
using System.Collections;

namespace CigBreak
{
    public class VegReward : ScriptableObject
    {
        [SerializeField]
        //[HideInInspector]
        private string vegID = "";
        public string VegID { get { return vegID; } }

        [SerializeField]
        private string vegName = "";
        public string VegName { get { return vegName; } }

        [SerializeField]
        private Sprite icon = null;
        public Sprite Icon { get { return icon; } }

        [SerializeField]
        private Sprite title = null;
        public Sprite Title { get { return title; } }

        [SerializeField]
        private Color vegColor = Color.white;
        public Color VegColor { get { return vegColor; } }

        [SerializeField]
        private GameObject model = null;
        public GameObject Model { get { return model; } }

        [SerializeField]
        private string[] descriptions = null;

        private int descriptionIndex = -1;
        public string GetNextDescription()
        {
            descriptionIndex += 1;
            if(descriptionIndex >= descriptions.Length)
            {
                descriptionIndex = 0;
            }

            return descriptions[descriptionIndex];
        }

        private void Reset()
        {
            vegID = System.Guid.NewGuid().ToString();
        }
    } 
}
