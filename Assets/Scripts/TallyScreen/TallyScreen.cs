using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using CigBreak;

namespace CigBreakGUI
{
    public class TallyScreen : MonoBehaviour
    {
        // Screens
        [SerializeField]
        private GameObject summaryScreen = null;

        [SerializeField]
        private GameObject m_EndGameScene = null;

        [SerializeField]
        private BadgeScreen m_GoldenStarScreen = null;

        [SerializeField]
        private CoinScreen m_SilverStarScreen = null;

        [SerializeField]
        private VegScreen m_BronzeStarScreen = null;

        private GameObject m_CurrentScreen = null;

        [SerializeField]
        private FailScreen failScreen = null;

        [SerializeField]
        private LevelPopup levelPopup = null;

        [SerializeField]
        private AudioClip m_CheeringSound = null;

        private AudioSource musicSource = null;

        private PlayerProfile profile = null;

        // Level data
        private LevelSet levelSet = null;
        public int CurrentLevelIndex { get; private set; }
        private LevelData currentLevelData = null;

        public LevelResults LevelResult { get; private set; }
        private Badge earnedBadge = null;

        public int StarsEarned { get; private set; }

        private void Awake()
        {
            LoadLevelData();
            profile = PlayerProfile.GetProfile();
            LevelResult = Utility.JsonUtility.ValidateJsonData<LevelResults>(PlayerPrefs.GetString(CigBreakConstants.PlayerPrefKeys.LastLevelResult));

            if (LevelResult.Completed)
            {
                StarsEarned = CalculateStarsEarned();
            }

            UpdateProfile();

            musicSource = GetComponent<AudioSource>();
        }

        private void UpdateProfile()
        {
            // Update player profile
            PlayerProfile profile = PlayerProfile.GetProfile();

            if (LevelResult.Completed)
            {
                profile.RecordLevelResult(currentLevelData.LevelID, StarsEarned);
            }
        }

        private void Start()
        {
            if (LevelResult.Completed)
            {
                musicSource.Play();
                musicSource.PlayOneShot(m_CheeringSound);
                UnlockRewards();
                DisplaySummaryScreen();
            }
            else
            {
                DisplayFailScreen();
            }
        }

        private void LoadLevelData()
        {
            levelSet = Resources.Load<LevelSet>(CigBreakConstants.Paths.LevelSet);
            CurrentLevelIndex = PlayerPrefs.GetInt(CigBreakConstants.PlayerPrefKeys.CurrentLevel);
            currentLevelData = levelSet.LevelData[CurrentLevelIndex];
        }

        private void UnlockRewards()
        {
            if (StarsEarned >= 1)
            {
                foreach (LevelRewards.RewardCount vegReward in currentLevelData.LevelRewards.VegRewards)
                {
                    profile.UnlockVeg(vegReward.VegReward.VegID, vegReward.Count);
                }

                if (currentLevelData.LevelRewards.PowerupReward != null)
                {
                    for (int i = 0; i < currentLevelData.LevelRewards.PowerupReward.Count<PowerupData>(); i++)
                    {
                        profile.UnlockPowerup(currentLevelData.LevelRewards.PowerupReward[i].GUID, 1);
                    }
                }
            }

            if (StarsEarned >= 2)
            {
                profile.AddCoins(currentLevelData.LevelRewards.Coins);
            }

            if (StarsEarned == 3)
            {
                earnedBadge = currentLevelData.LevelRewards.Badge;
                profile.AddBadge(earnedBadge.BadgeID);
            }

            profile.AddMoveySavedInGame(LevelResult.MaxMoney);
        }

        private void DisplaySummaryScreen()
        {
            summaryScreen.GetComponent<SummaryScreen>().Display(currentLevelData, this);
        }

        public void LoadLevelRewardScreen()
        {
            if (StarsEarned == 1)
            {
                m_CurrentScreen = m_BronzeStarScreen.gameObject;
                m_BronzeStarScreen.Display(currentLevelData.LevelRewards.VegRewards[0].VegReward,false);
            }
            else if (StarsEarned == 2)
            {
                m_CurrentScreen = m_SilverStarScreen.gameObject;
                m_SilverStarScreen.Display(false);
            }
            else
            {
                m_CurrentScreen = m_GoldenStarScreen.gameObject;
                m_GoldenStarScreen.Display(earnedBadge, false);
            }
        }

        private void DisplayFailScreen()
        {
            failScreen.Display(CurrentLevelIndex);
        }

        public void LoadMapScreen()
        {
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
        }

        public void LoadNextLevel()
        {
            m_CurrentScreen.SetActive(false);
            if (CurrentLevelIndex < levelSet.LevelData.Length - 1)
            {
                LevelData nextLevelData = levelSet.LevelData[CurrentLevelIndex + 1];
                levelPopup.gameObject.SetActive(true);
                levelPopup.Initialise(nextLevelData, CurrentLevelIndex + 1);
            }
            else
            {
                m_EndGameScene.SetActive(true);
            }
        }

        public void loadSummaryScreen()
        {
            summaryScreen.SetActive(true);
        }

        public void ReplayLevel()
        {
            summaryScreen.SetActive(false);
            levelPopup.gameObject.SetActive(true);
            levelPopup.Initialise(currentLevelData, CurrentLevelIndex);
        }

        private int CalculateStarsEarned()
        {
            float healthLeft = LevelResult.RemainingHealthPercentage;
            int starsEarned = 0;

            if (healthLeft == 1f)
            {
                starsEarned = 3;

            }
            else if (healthLeft >= 0.5f)
            {
                starsEarned = 2;

            }
            else
            {
                starsEarned = 1;
    }

            return starsEarned;
        }
    }
}