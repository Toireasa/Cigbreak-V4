using UnityEngine;
using System.Collections;

namespace CigBreakConstants
{
    public static class SceneNames
    {
        public static readonly string IntroScreen = "IntroScreen";
        public static readonly string MapScreen = "CloseupMap01";
        public static readonly string LevelStatus = "GameOverScene";
        public static readonly string Level = "MainScene";
        public static readonly string LoadingScreen = "Loading";
        public static readonly string Journal = "Journal";
        public static readonly string MarketScene = "MarketScene";
        public static readonly string BedRoomScene = "BedRoomSceen";
        public static readonly string LoungeScene = "LoungeScene";
        public static readonly string BusStopScene = "BusStop";
    } 

    public static class Paths
    {
        public static readonly string LevelSet = "Levels/LevelSet";
        public static readonly string BadgeSet = "Profile/BadgeSet";
        public static readonly string PowerupSet = "GameItems/Powerups/_Powerups";
        public static readonly string VegRewards = "Rewards/Veg";
        public static readonly string TreeMessages = "HealthMessages/TreeMessages";
        public static readonly string Photos = Application.persistentDataPath;
    }

    public static class Tags
    {
        public static readonly string MainUICanvas = "UICanvas";
        public static readonly string Popup = "Popup";
        public static readonly string InteractableObject = "GameInteractable";
        public static readonly string Coin = "Coin";
        public static readonly string CoinFallOutZone = "CoinZone";
        public static readonly string VegMound = "VegMound";
    }

    public static class Layers
    {
        public static readonly int InteractableObjects = 12;
    }

    public static class Values
    {
        public static readonly float SceneDistance = 5f;
    }

    public static class PlayerPrefKeys
    {
        public static readonly string ProfileStore = "CigBreak.Profile";
        public static readonly string CurrentLevel = "currentLevel";
        public static readonly string SelectedPowerup = "selectedPowerup";
        public static readonly string LastLevelResult = "LastLevelResult";
    }
}
