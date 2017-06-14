using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using System.Linq;
using UnityEngine.Events;
using System.Collections.Generic;

namespace CigBreakGUI
{
	public class LevelPopupV4 : MonoBehaviour
	{
		[SerializeField]
		private Text levelNumber = null;
		//[SerializeField]
		//private Text rewardsHeader = null;
		[SerializeField]
		private Text levelTarget = null;
		[SerializeField]
		private Button powerupSelection = null;
		[SerializeField]
		private Button playButton = null;
		[SerializeField]
		private Button backButton = null;
		[SerializeField]
		private Button mapButton = null;

		[SerializeField]
		private PowerupPopup powerupPopup = null;

		[SerializeField]
		private GameObject UnlockGamePanel = null;

		public LevelData LevelData { get; private set; }
		public int LevelIndex { get; private set; }
		//public PowerupData SelectedPowerup { get; set; }

		private PlayerProfile profile = null;

		private List<GameObject> spawnedIcons = new List<GameObject>();

		public void Initialise(LevelData level, int levelIndex)
		{
			LevelData = level;
			LevelIndex = levelIndex;
			this.levelNumber.text = (levelIndex + 1).ToString();
			levelTarget.text = level.CigaretteTarget.ToString();
			profile = PlayerProfile.GetProfile();

			if (profile.GetUnlockedPowerups().Count == 0)
			{
				powerupSelection.interactable = false;
			}
			else
			{
				powerupSelection.onClick.AddListener(() => powerupPopup.gameObject.SetActive(true));
			}

			playButton.onClick.AddListener(LoadLevel);

			if (backButton!=null)
			{
				backButton.onClick.AddListener(() => gameObject.SetActive(false));
			}
			if (mapButton != null)
			{
				mapButton.onClick.AddListener(() => LoadingScreen.LoadScene(CigBreakConstants.SceneNames.MapScreen));
			}

			if (this.GetComponentInParent<AudioSource>() != null)
			{
				this.GetComponentInParent<AudioSource>().Stop();
			}

			PlayerPrefs.SetInt(CigBreakConstants.PlayerPrefKeys.CurrentLevel, LevelIndex);
		}


		public void LoadLevel()
		{
			#if FREE_VERSION
			if (!LevelData.FreeToPlay && !PlayerProfile.GetProfile().UnlockFullGame)
			{
			UnlockGamePanel.SetActive(true);
			}
			else
			{                
			LoadingScreen.LoadLevel();
			}
			#else
			LoadingScreen.LoadLevel();
			#endif
		}

		private void Dismiss()
		{
			gameObject.SetActive(false);
		}

		public void Quit()
		{
			Application.Quit();
		}

		private void OnDisable()
		{
			playButton.onClick.RemoveAllListeners();
			if (backButton != null)
			{
				backButton.onClick.RemoveAllListeners();
			}

			// Remove extra reward icons
			foreach (GameObject go in spawnedIcons.ToList())
			{
				Destroy(go);
			}

			spawnedIcons.Clear();
		}

		#region OldShowRewardCode
		//private void ShowLevelRewards()
		//{
		//    LevelRewards rewardsData = LevelData.LevelRewards;
		//    if (rewardsData == null)
		//    {
		//        return;
		//    }

		//    // Badge reward
		//    RewardRow gold = rewardRows[0];
		//    int badgeCount = 1;
		//    if (LevelIndex != 0)
		//    { 
		//         badgeCount = profile.GetUnlockedBadges().Sum(b => b.Value);
		//    }
		//    gold.Counter.text = badgeCount.ToString();

		//    badgeCount = Mathf.Min(badgeCount, 10);
		//    for (int i = 0; i < badgeCount - 1; i++)
		//    {
		//        GameObject icon = Instantiate(gold.Icon.gameObject) as GameObject;
		//        icon.transform.SetParent(gold.IconLayout.transform, false);

		//        spawnedIcons.Add(icon);
		//    }

		//    float spacingFactor = (float)badgeCount / minSpacingItemCount;
		//    gold.IconLayout.GetComponent<HorizontalLayoutGroup>().spacing = Mathf.Max(minIconSpacing, minIconSpacing * spacingFactor);


		//    // Coins rewars
		//    RewardRow silver = rewardRows[1];
		//    int coinCount = 1;
		//    if (LevelIndex != 0)
		//    {
		//        coinCount = profile.Coins;
		//    }
		//    silver.Counter.text = coinCount.ToString();

		//    coinCount = Mathf.Min(coinCount, 10);
		//    for (int i = 0; i < coinCount - 1; i++)
		//    {
		//        GameObject icon = Instantiate(silver.Icon.gameObject) as GameObject;
		//        icon.transform.SetParent(silver.IconLayout.transform, false);

		//        spawnedIcons.Add(icon);
		//    }

		//    spacingFactor = (float)coinCount / minSpacingItemCount;
		//    silver.IconLayout.GetComponent<HorizontalLayoutGroup>().spacing = Mathf.Max(minIconSpacing, minIconSpacing * spacingFactor);


		//    // Veg rewards
		//    RewardRow bronze = rewardRows[2];
		//    float vegSum = rewardsData.VegRewards.Select(v => v.Count).Sum();
		//    if (LevelIndex != 0)
		//    {
		//         vegSum = profile.GetUnlockedVeg().Sum(v => v.Value);
		//    }
		//    bronze.Counter.text = vegSum.ToString();

		//    vegSum = Mathf.Min(vegSum, 10);

		//    // Veg reward data
		//    VegReward[] vegData = Resources.LoadAll<VegReward>(CigBreakConstants.Paths.VegRewards);

		//    // Join profile and reward veg data
		//    Dictionary<VegReward, int> allVeg = new Dictionary<VegReward, int>();
		//    if (LevelIndex != 0)
		//    {
		//        foreach (var vegRecord in profile.GetUnlockedVeg())
		//        {
		//            allVeg.Add(vegData.First(v => v.VegID == vegRecord.Key), vegRecord.Value);
		//        }
		//    }
		//    else
		//    {
		//        foreach (LevelRewards.RewardCount veg in rewardsData.VegRewards)
		//        {
		//            if (allVeg.ContainsKey(veg.VegReward))
		//            {
		//                allVeg[veg.VegReward] += veg.Count;
		//            }
		//            else
		//            {
		//                allVeg.Add(veg.VegReward, veg.Count);
		//            }
		//        }
		//    }

		//    // Display on popup
		//    int count = 0;
		//    int sum = allVeg.Sum(v => v.Value);

		//    bronze.Icon.gameObject.SetActive(false);
		//    while (count < 9 && sum > 0)
		//    {
		//        foreach (var veg in new Dictionary<VegReward, int>(allVeg))
		//        {
		//            if (allVeg[veg.Key] > 0)
		//            {
		//                GameObject icon = Instantiate(bronze.Icon.gameObject) as GameObject;
		//                icon.GetComponent<Image>().sprite = veg.Key.Icon;
		//                icon.transform.SetParent(bronze.IconLayout.transform, false);
		//                icon.SetActive(true);
		//                spawnedIcons.Add(icon);

		//                // control values
		//                allVeg[veg.Key] -= 1;
		//                count++;
		//                sum--;

		//                if (count >= 9 || sum <= 0)
		//                {
		//                    break;
		//                }
		//            }
		//        }
		//    }



		//    spacingFactor = vegSum / minSpacingItemCount;
		//    bronze.IconLayout.GetComponent<HorizontalLayoutGroup>().spacing = Mathf.Max(minIconSpacing, minIconSpacing * spacingFactor);
		//}

		//[System.Serializable]
		//private class RewardRow
		//{
		//    [SerializeField]
		//    private Text counter = null;
		//    public Text Counter { get { return counter; } }

		//    [SerializeField]
		//    private GameObject iconLayout = null;
		//    public GameObject IconLayout { get { return iconLayout; } }

		//    [SerializeField]
		//    private Image icon = null;
		//    public Image Icon { get { return icon; } }

		//}
		#endregion

	}

}