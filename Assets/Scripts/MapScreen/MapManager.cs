using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CigBreak
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField]
        private MapButtons mapButtonController = null;
        [SerializeField]
        private MapCameraController camController = null;

        private GameObject[] vegMounds = null;

        //PlayerProfile profile = null;

       // LevelSet levels = null;

        private void Awake()
        {
          //  levels = Resources.Load<LevelSet>(CigBreakConstants.Paths.LevelSet);
            //vegMounds = GameObject.FindGameObjectsWithTag(CigBreakConstants.Tags.VegMound);
        }

        private void Start()
        {
            //profile = PlayerProfile.GetProfile();
            //mapButtonController.OnButtonInitialised += SpawnVegForLevel;
            mapButtonController.OnButtonsInitialised += SelectFocusTarget;
        }

        private void SelectFocusTarget(GameObject lastUnlockedButton)
        {
            camController.SetViewTarget(lastUnlockedButton);
        }

        private void SpawnVegForLevel(MapButton levelButton)
        {
            //if (levelButton.LevelIndex < levels.LevelData.Length)
            //{
            //    LevelData levelData = levels.LevelData[levelButton.LevelIndex];

            //    LevelStatus result = profile.GetLevelResults().FirstOrDefault(l => l.LevelID == levelData.LevelID);
            //    if (result != null && result.StarsEarned > 0)
            //    {
            //        VegReward veg = levelData.LevelRewards.VegRewards[0].VegReward;
            //        SpawnVeg(veg, levelButton.transform.position);
            //    }
            //}
        }


        private void SpawnVeg(VegReward vegData, Vector3 aroundLocation)
        {
            // Find closest allotments 
            float minDistance = vegMounds.Min(m => Vector3.Distance(m.transform.position, aroundLocation));
            GameObject mound = vegMounds.Where(m => Vector3.Distance(m.transform.position, aroundLocation) == minDistance).First();

            if (mound != null)
            {
                GameObject instance = Instantiate(vegData.Model) as GameObject;

                instance.transform.SetParent(mound.gameObject.transform, false);
                StartCoroutine(GrowVeg(instance));
            }
        }

        private IEnumerator GrowVeg(GameObject vegObject)
        {
            Vector3 targetScale = vegObject.transform.localScale;
            vegObject.transform.localScale = Vector3.zero;

            float t = 0;
            while (t <= 1f)
            {
                vegObject.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
                t += Time.deltaTime * 0.5f;
                yield return null;
            }
        }
    }
}
