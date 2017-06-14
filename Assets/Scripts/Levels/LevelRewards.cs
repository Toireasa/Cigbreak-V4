using UnityEngine;
using System.Collections;

namespace CigBreak
{
    public class LevelRewards : ScriptableObject
    {
        [SerializeField]
        private Badge badge = null;
        public Badge Badge { get { return badge; } }

        [SerializeField]
        private int coins = 0;
        public int Coins { get { return coins; } }

        [SerializeField]
        private PowerupData[] powerupReward = null;
        public PowerupData[] PowerupReward { get { return powerupReward; } }
        
        [SerializeField]
        private RewardCount[] vegRewards = null;
        public RewardCount[] VegRewards { get { return vegRewards; } }

        [System.Serializable]
        public class RewardCount
        {
            [SerializeField]
            private VegReward vegReward = null;
            public VegReward VegReward { get { return vegReward; } }

            [SerializeField]
            private int count = 1;
            public int Count { get { return count; } }
        }
    } 
}
