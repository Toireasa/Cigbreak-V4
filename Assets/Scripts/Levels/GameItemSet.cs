using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace CigBreak
{
    public class GameItemSet : ScriptableObject
    {
        [SerializeField]
        private GameItemData.ItemType itemsType = GameItemData.ItemType.Unhealthy;
        public GameItemData.ItemType ItemsType { get { return itemsType; } }

        [SerializeField]
        private List<GameItemData> items = null;
        public ReadOnlyCollection<GameItemData> Items { get { return items.AsReadOnly(); } }
    }
}
