using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CigBreak
{
    /// <summary>
    /// Stores the details of the player profile that need to be saved permamently
    /// </summary>
    /// 
    public enum Country
    {
        NONE,
        UK,
        US
    }

    public class PlayerProfile
    {
        /* User details
           Data collected by asking user about their habits and quit plan throuought the game.
           Where IDs are stored, this is a reference to data contained in scriptable objects,
           up to date details can be then retrieved at runtime.
        */

        // Dates
        private DateTime? m_StartDate = null;
        public DateTime? StartDate
        {
            get { return m_StartDate; }
            private set { m_StartDate = value; SaveProfile(); }
        }

        private DateTime? quitDate = null;
        public DateTime? QuitDate
        {
            get { return quitDate; }
            set { quitDate = value; SaveProfile(); }
        }

        private DateTime? m_LastPopUpDate = null;
        public DateTime? LastPopUpDate
        {
            get { return m_LastPopUpDate; }
            set { m_LastPopUpDate = value; SaveProfile(); }
        }

        // Name
        private string playerName = "";
        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; SaveProfile(); }
        }


        // Postcode
        private string m_Postcode = null;
        public string Postcode
        {
            get { return m_Postcode; }
            set { m_Postcode = value; SaveProfile(); }
        }

        // Num Of Cig
        private int m_NumOfCigPerDay = 0;
        public int NumOfCigPerDay
        {
            get { return m_NumOfCigPerDay; }
            set { m_NumOfCigPerDay = value; SaveProfile(); }
        }

        // Num Of Cig
        private int m_NumOfCigPerPack = 0;
        public int NumOfCigPerPack
        {
            get { return m_NumOfCigPerPack; }
            set { m_NumOfCigPerPack = value; SaveProfile(); }
        }

        // Num Of Cig
        private int m_CostOfPack = 0;
        public int CostOfPack
        {
            get { return m_CostOfPack; }
            set { m_CostOfPack = value; SaveProfile(); }
        }

        private Country m_Contry = Country.NONE;
        public Country Contry
        {
            get { return m_Contry; }
            set { m_Contry = value; SaveProfile(); }
        }

        // Token
        private string m_Token = null;
        public string Token
        {
            get { return m_Token; }
            set { m_Token = value; SaveProfile(); }
        }

        // Token
        private string m_Email = null;
        public string Email
        {
            get { return m_Email; }
            set { m_Email = value; SaveProfile(); }
        }

        private bool m_UnlockFullGame = false;

        public bool UnlockFullGame
        {
            get { return m_UnlockFullGame; }
            set { m_UnlockFullGame = value; SaveProfile(); }
        }



        // keep????
        private string m_Signature = "";
        public string Signature
        {
            get { return m_Signature; }
            set { m_Signature = value; SaveProfile(); }
        }


        /*
         Game related stats and storage
        */

        // Badges
        private Dictionary<string, int> m_EarnedBadges = null;
        public Dictionary<string, int> EarnedBadges
        {
            get { return m_EarnedBadges; }
            private set { m_EarnedBadges = value; SaveProfile(); }
        }

        // Veg
        private Dictionary<string, int> m_UnlockedVeg = null;
        public Dictionary<string, int> UnlockedVeg
        {
            get { return m_UnlockedVeg; }
            private set { m_UnlockedVeg = value; SaveProfile(); }
        }


        // Game Stats

        // Total count of broken unhealthy objects
        private int totalCigsBroken = 0;
        public int TotalCigsBroken
        {
            get { return totalCigsBroken; }
            set { totalCigsBroken = value; SaveProfile(); }
        }

        // Coins curently in inventory
        private float coins = 0;
        public float Coins
        {
            get { return coins; }
            set { coins = value; SaveProfile(); }
        }

        private float m_TotalMoneySavedInGame = 0;
        public float TotalMoneySavedInGame
        {
            get { return m_TotalMoneySavedInGame; }
            set { m_TotalMoneySavedInGame = value; SaveProfile(); }
        }

        // Level results
        private List<LevelStatus> m_LevelResults = null;
        public List<LevelStatus> LevelResults
        {
            get { return m_LevelResults; }
            private set { m_LevelResults = value; SaveProfile(); }
        }

        //Task Status
        private List<TaskStatus> m_TaskStatus = null;
        public List<TaskStatus> TaskStatus
        {
            get { return m_TaskStatus; }
            private set { m_TaskStatus = value; SaveProfile(); }
        }

        // Powerups
        private List<string> m_UnlockedPowerups = null;
        public List<string> UnlockedPowerups
        {
            get { return m_UnlockedPowerups; }
            private set { m_UnlockedPowerups = value; SaveProfile(); }
        }

        private Dictionary<string, int> m_PowerupInventory = null;
        public Dictionary<string, int> PowerupInventory
        {
            get { return m_PowerupInventory; }
            private set { m_PowerupInventory = value; SaveProfile(); }
        }


        // FailTutorials
        private List<string> m_SeenFailTutorials = null;
        public List<string> SeenFailTutorials
        {
            get { return m_SeenFailTutorials; }
            private set { m_SeenFailTutorials = value; SaveProfile(); }
        }

        // Profile instance
        private static PlayerProfile m_CurrentProfile = null;

        public PlayerProfile()
        {
            StartDate = DateTime.Today;
            m_EarnedBadges = new Dictionary<string, int>();
            m_UnlockedVeg = new Dictionary<string, int>();
            m_PowerupInventory = new Dictionary<string, int>();
            m_LevelResults = new List<LevelStatus>();
            m_TaskStatus = new List<TaskStatus>();
            m_UnlockedPowerups = new List<string>();
            m_SeenFailTutorials = new List<string>();
            m_CurrentProfile = this;
        }

        public void AddBadge(string id)
        {
            if (EarnedBadges.ContainsKey(id))
            {
                EarnedBadges[id] += 1;
            }
            else
            {
                EarnedBadges.Add(id, 1);
            }

            SaveProfile();
        }

        /// <summary>
        /// Returns a copy of the earned badges dictionery
        /// Use AddBadge to make permament profile changes
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetUnlockedBadges()
        {
            return new Dictionary<string, int>(EarnedBadges);
        }

        public void UnlockVeg(string id, int amount = 1)
        {
            if (UnlockedVeg.ContainsKey(id))
            {
                UnlockedVeg[id] += amount;
            }
            else
            {
                UnlockedVeg.Add(id, amount);
            }

            SaveProfile();
        }

        public void RemoveVeg(int _vegToRemove)
        {
            int elementAt = 0;

            for (int i = 0; i < _vegToRemove; i++)
            {
                var item = UnlockedVeg.ElementAt(elementAt);
                var itemKey = item.Key;
                var itemValue = item.Value;

                if (itemValue > 0)
                {
                    m_UnlockedVeg[itemKey]--;
                }
                else
                {
                    elementAt++;
                    item = UnlockedVeg.ElementAt(elementAt);
                    itemKey = item.Key;
                    itemValue = item.Value;
                    while (itemValue <= 0)
                    {
                        elementAt++;
                        item = UnlockedVeg.ElementAt(elementAt);
                        itemKey = item.Key;
                        itemValue = item.Value;
                    }

                    m_UnlockedVeg[itemKey]--;
                }
            }
                SaveProfile();
        }            

        /// <summary>
        /// Returns a copy of the unlocked veg dictionary. 
        /// Use UnlockVeg() to modify the stored contents
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetUnlockedVeg()
        {
            return new Dictionary<string, int>(UnlockedVeg);
        }

        public Dictionary<string, int> GetPowerupInvetory()
        {
            return new Dictionary<string, int>(PowerupInventory);
        }

        public void AddMoveySavedInGame(float _amount)
        {
            m_TotalMoneySavedInGame += _amount;
            SaveProfile();
        }
        public void AddCoins(float amount)
        {
            if (amount > 0)
            {
                Coins += amount;
                SaveProfile();
            }
        }

        public bool SpendCoins(float amount)
        {
            if (Coins >= amount)
            {
                Coins -= amount;
                SaveProfile();
                return true;
            }

            return false;
        }

        public void UnlockPowerup(string guid, int initialStock)
        {
            if (!UnlockedPowerups.Contains(guid))
            {
                UnlockedPowerups.Add(guid);
                AddPowerUp(guid, initialStock);
                SaveProfile();
            }
            else
            {
                AddPowerUp(guid, initialStock);
                SaveProfile();
            }
        }

        /// <summary>
        /// Returns a copy of the unlocked powerups list
        /// Use UnlockPowerup() to make presistent profile changes
        /// </summary>
        /// <returns></returns>
        public List<string> GetUnlockedPowerups()
        {
            return new List<string>(UnlockedPowerups);
        }

        public void AddPowerUp(string id, int amount = 1)
        {
            if (PowerupInventory.ContainsKey(id))
            {
                PowerupInventory[id] += amount;
            }
            else
            {
                PowerupInventory.Add(id, amount);
            }

            SaveProfile();
        }
        public void RemovePowerUp(string id, int amount = 1)
        {
            if (PowerupInventory.ContainsKey(id))
            {
                PowerupInventory[id] -= amount;
            }

            SaveProfile();
        }
        //        public void AddPowerup(string guid, int amount)
        //        {
        //            // Only add if this powerup is unlocked
        //            if (PowerupInventory.ContainsKey(guid))
        //            {
        //                PowerupInventory[guid] += amount;
        //                SaveProfile();
        //            }
        //        }

        //        public bool UsePowerup(string guid)
        //        {
        //            if (PowerupInventory.ContainsKey(guid) && PowerupInventory[guid] > 0)
        //            {
        //#if UNITY_EDITOR
        //                Debug.Log("using powerup");
        //#endif
        //                PowerupInventory[guid] -= 1;
        //                return true;
        //            }

        //            return false;
        //        }

        //        public void ChangeAllPowerupInventoryValues(int change)
        //        {
        //            foreach(string key in PowerupInventory.Keys.ToList())
        //            {
        //                PowerupInventory[key] += change;
        //            }
        //        }


        public void FailTutorialSeen(string guid)
        {
            SeenFailTutorials.Add(guid);
            SaveProfile();
        }

        public void RecordLevelResult(string levelID, int stars)
        {
            // If a record for this level already exists
            LevelStatus status = LevelResults.FirstOrDefault(r => r.LevelID == levelID);
            if (status != null)
            {
                if (status.StarsEarned < stars)
                {
                    status.StarsEarned = stars;
                }
            }
            // if there was no record, create a new one
            else
            {
                status = new LevelStatus() { LevelID = levelID, StarsEarned = stars };
                LevelResults.Add(status);
            }

            SaveProfile();
        }

        public void RecordTaskStatus(int taskID, string answer = null)
        {
            // If a record for this level already exists
            TaskStatus status = m_TaskStatus.FirstOrDefault(r => r.ID == taskID);
            if (status != null)
            {
                status.Answer = answer;
            }
            // if there was no record, create a new one
            else
            {
                status = new TaskStatus() { ID = taskID, Answer = answer, Completed=true };
                m_TaskStatus.Add(status);

            }

            SaveProfile();
        }

        /// <summary>
        /// Returns a copy of the level results list
        /// Use RecordLevelResults() to record results
        /// </summary>
        /// <returns></returns>
        public List<LevelStatus> GetLevelResults()
        {
            return new List<LevelStatus>(LevelResults);
        }

        public static PlayerProfile GetProfile()
        {
            // If there is no profile already and one couldn't be loaded create a new one
            if (m_CurrentProfile == null && LoadProfile() == null)
            {
                m_CurrentProfile = new PlayerProfile();
                m_CurrentProfile.SaveProfile();
            }

            return m_CurrentProfile;
        }

        public static void DeleteProfile()
        {
            PlayerPrefs.SetString(CigBreakConstants.PlayerPrefKeys.ProfileStore, string.Empty);
            m_CurrentProfile = null;           
        }

        private static PlayerProfile LoadProfile()
        {
            // Try to get a saved profile from player prefs
            string serialised = PlayerPrefs.GetString(CigBreakConstants.PlayerPrefKeys.ProfileStore);
            if (serialised != string.Empty)
            {
                // If a record was found attempt to deserialise, record and return if the object was valid
                PlayerProfile profile = Utility.JsonUtility.ValidateJsonData<PlayerProfile>(serialised);
                if (profile != default(PlayerProfile))
                {
                    m_CurrentProfile = profile;
                    return m_CurrentProfile;
                }
            }

            // Profile not stored or the stored data was invalid - return null
            return null;
        }

        public void SaveProfile()
        {
            string serialised = Utility.JsonUtility.SerializeToJson(this);

#if UNITY_EDITOR
            Debug.Log(serialised);
#endif

            if (serialised != string.Empty)
            {
                PlayerPrefs.SetString(CigBreakConstants.PlayerPrefKeys.ProfileStore, serialised);
                PlayerPrefs.Save();
            }

#if UNITY_EDITOR
            // If the app is runing in the editor display serialisation error
            else
            {
                Debug.LogError("Failed to serialise profile");
            }
#endif
        }
    }
}

