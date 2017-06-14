using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;

namespace CigBreakGUI
{
    public class GoalCounter : MonoBehaviour
    {
        [SerializeField]
        private Text levelNo = null;
        [SerializeField]
        private Text counter = null;
        [SerializeField]
        private Text moneySaved = null;

        private int currentCount = 0;

        private Gameplay gameplay = null;

        private void Start()
        {
            gameplay = Gameplay.gameplay;
            levelNo.text = string.Format("Level {0}", gameplay.CurrentLevelIndex + 1);
            UpdateCounter(gameplay.CurrentLevel.CigaretteTarget);
            UpdateMoneyEarned(0f);
            gameplay.OnPointEarned += () => UpdateCounter(-1);
            gameplay.OnMoneySavedUpdate += UpdateMoneyEarned;
        }

        private void UpdateCounter(int change)
        {
            currentCount += change;
            counter.text = currentCount.ToString();
        }

        private void UpdateMoneyEarned(float value)
        {
            if (PlayerProfile.GetProfile().Contry == Country.UK)
                moneySaved.text = string.Format("£{0}", value.ToString("0.00"));
            else
                moneySaved.text = string.Format("${0}", value.ToString("0.00"));
        }
    }

}