using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CigBreak
{
    public class PowerupData : GameItemData
    {
        [Header("UI data")]
        [SerializeField]
        private string title = "";
        public string Title { get { return title; } }

        [SerializeField]
        [Multiline]
        private string descrition = "";
        public string Desctiprion { get { return descrition; } }

        [SerializeField]
        private Sprite sprite = null;
        public Sprite Sprite { get { return sprite; } }

        [SerializeField]
        private int coinPrice = 1;
        public int CoinPrice { get { return coinPrice; } }

        [SerializeField]
        [HideInInspector]
        private string effectMethod = "";
        public string EffectMethodName { get { return effectMethod; } }
    }
}