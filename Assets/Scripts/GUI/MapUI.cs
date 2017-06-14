using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using CigBreak;

namespace CigBreakGUI
{
    public class MapUI : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField]
        private Button unlockButton = null;
        [SerializeField]
        private Button clearButton = null;

        [Header("Player Rewards")]
        [SerializeField]
        private Text m_GameBadgeText = null;

        [SerializeField]
        private Text m_CoinsText = null;

        [SerializeField]
        private Text m_VegText = null;

        [Header("MapBT")]

        [SerializeField]
        private MapClickable m_JournalBT = null;

        [SerializeField]
        private MapClickable m_JournalBT02 = null;

        [SerializeField]
        private MapClickable m_MarketBT = null;

        [SerializeField]
        private MapClickable m_BedRoomtBT = null;

        [SerializeField]
        private MapClickable m_LoungeBT = null;

        [SerializeField]
        private MapClickable m_BusStopBT = null;

        [Header("Tutorial")]
        [SerializeField]
        GameObject m_TutorialPanel = null;

        private PlayerProfile m_Profile;

        private void Awake()
        {
            m_Profile = PlayerProfile.GetProfile();

            m_GameBadgeText.text = m_Profile.GetUnlockedBadges().Sum(b => b.Value).ToString();
            m_CoinsText.text = m_Profile.Coins.ToString();
            m_VegText.text = m_Profile.GetUnlockedVeg().Sum(v => v.Value).ToString();


            m_JournalBT.OnButtonClicked = LoadJournalScene;
            m_JournalBT02.OnButtonClicked = LoadJournalScene;
            m_MarketBT.OnButtonClicked = LoadMarketScene;
            m_BedRoomtBT.OnButtonClicked = LoadBedRoomScene;
            m_LoungeBT.OnButtonClicked = LoadLoungeScene;
            m_BusStopBT.OnButtonClicked = LoadBusStopScene;

#if DEBUG
            unlockButton.gameObject.SetActive(true);
            clearButton.gameObject.SetActive(true);
            unlockButton.onClick.AddListener(UnlockAll);
            clearButton.onClick.AddListener(ClearProfile);
#endif

            if (PlayerPrefs.GetString("Tutorial") == string.Empty)
            {
                PlayerPrefs.SetString("Tutorial", "ON");
                m_TutorialPanel.SetActive(true);
                PlayerPrefs.Save();
            }
            else if (PlayerPrefs.GetString("Tutorial") == "ON")
            {
                m_TutorialPanel.SetActive(true);
            }
        }

        private void UnlockAll()
        {
            LevelSet levels = Resources.Load<LevelSet>(CigBreakConstants.Paths.LevelSet);
            
            foreach (LevelData level in levels.LevelData)
            {
                m_Profile.RecordLevelResult(level.LevelID, Random.Range(1, 4));
            }
            m_Profile.UnlockPowerup("f626c4cd-3fdb-4bc9-b69b-804bbf84bd32", 0);
            m_Profile.UnlockPowerup("e56fb75f-aa95-4b60-8938-3daaf85c22f6", 0);
            m_Profile.AddCoins(10);
            //PlayerProfile.GetProfile().UnlockFullGame = true;
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
        }

        private void ClearProfile()
        {
            PlayerProfile.DeleteProfile();
            PlayerProfile.GetProfile().SaveProfile();
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen);
            PlayerPrefs.DeleteAll();
        }

        public void Menu()
        {
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.IntroScreen);
        }

        public void LoadJournalScene()
        {
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.Journal);
        }

        public void LoadMarketScene()
        {
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MarketScene);
        }

        public void LoadBedRoomScene()
        {         
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.BedRoomScene);
        }

        public void LoadLoungeScene()
        {
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.LoungeScene);
        }
        
        public void LoadBusStopScene()
        {
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.BusStopScene);
        }
    } 
}
