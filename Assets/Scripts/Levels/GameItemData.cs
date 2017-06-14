using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CigBreak
{
    public class GameItemData : ScriptableObject
    {
        [SerializeField]
        [HideInInspector]
        private string guid = "";
        public string GUID { get { return guid; } }

        public enum ItemType { Healthy, Unhealthy, Powerup }
        [SerializeField]
        private ItemType itemType = ItemType.Unhealthy;
        public ItemType TypeOfItem { get { return itemType; } }

        [SerializeField]
        private float price = 0;
        public float Price { get { return price; } }

        [SerializeField]
        private int health = 1;
        public int Health { get { return health; } }

        [Header("Collider Settings")]
        [SerializeField]
        private float x = 0.5f;
        public float ColliderX { get { return x; } }
        [SerializeField]
        private float y = 2f;
        public float ColliderY { get { return y; } }

        [Header("Audio")]
        [SerializeField]
        private AudioClip[] swipeSounds = null;
        public AudioClip[] SwipeSounds { get { return swipeSounds; } }
        [SerializeField]
        private AudioClip[] tapSounds = null;
        public AudioClip[] TapSounds { get { return tapSounds; } }

        [Header("Materials")]
        [SerializeField]
        private List<SpriteMaterialData> materials = null;
        public ReadOnlyCollection<SpriteMaterialData> Materials { get { return materials.AsReadOnly(); } }

        [System.Serializable]
        public class SpriteMaterialData
        {
            [SerializeField]
            private Sprite sprite = null;
            public Sprite Sprite { get { return sprite; } }

            [SerializeField]
            private Material material = null;
            public Material Material { get { return material; } }
        }

        private void Reset()
        {
            guid = System.Guid.NewGuid().ToString();
        }
    }
}