using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using CigBreakGUI;

namespace CigBreak
{
    public class PowerUp : InteractableObject
    {
        private bool collected = false;


        private PriceTags test;
        public void setpricetag(PriceTags _pt)
        {
            test = _pt;
        }

        public override void Initialise(GameItemData data)
        {
            base.Initialise(data);
            collected = false;
        }

        public override void OnSwipe()
        {
            if (currentHealth > 0)
            {
                // Play sound
                int audioIndex = Mathf.Min(itemData.Health - currentHealth, itemData.SwipeSounds.Length - 1);
                audioSource.PlayOneShot(itemData.SwipeSounds[audioIndex]);

                // Reduce health and update visuals
                currentHealth = Mathf.Max(currentHealth - 1, 0);
                UpdateMaterial();

                // If the object just changed to 0 health
                if (currentHealth == 0 && OnObjectDestroyed != null)
                {
                    OnObjectDestroyed(this);
                }
            }
        }

        public override void OnTap()
        {
            if (!collected)
            {
                collected = true;

                if (itemData.TapSounds.Length > 0)
                {
                    int audioIndex = Mathf.Min(itemData.Health - currentHealth, itemData.TapSounds.Length - 1);
                    audioSource.clip = itemData.TapSounds[audioIndex];
                    audioSource.Play();
                }

                // update visuals
                currentHealth = currentHealth - 1;
                UpdateMaterial();

                PowerupData data = itemData as PowerupData;
                Invoke(data.EffectMethodName, 0f);
            }
        }

        public void Heal()
        {
            Gameplay.gameplay.SetFullHealth();
            if (OnObjectCollected != null)
            {
                OnObjectCollected(this);
            }

            StartCoroutine(SprayCountdown());
        }

        private IEnumerator SprayCountdown()
        {
            yield return new WaitForSeconds(0.8f);
            ObjectFactory.objectFactory.ReturnObject(gameObject);
        }

        public void Gum()
        {
            
            if (OnObjectCollected != null)
            {
                OnObjectCollected(this);
            }

            ObjectLauncher launcher = Gameplay.gameplay.Launcher;
            launcher.Paused = true;

            LevelData levelData = Gameplay.gameplay.CurrentLevel;
            List<LevelData.LevelInteractableObjects> items = levelData.LevelObjects.Where(i => i.ItemData.TypeOfItem == GameItemData.ItemType.Unhealthy).ToList();

            int cigToLaunch;
            if (levelData.CigaretteTarget - Gameplay.gameplay.CigsBroken < 6)
            {
                cigToLaunch = levelData.CigaretteTarget - Gameplay.gameplay.CigsBroken;
            }
            else 
            {
                cigToLaunch = 6;
            }
            

            launcher.LaunchMultiple(cigToLaunch, items);
            List<InteractableObject> unhealthy = launcher.GetActiveObjectsOfType(typeof(UnhealthyObject));

            float money = 0;
            foreach (InteractableObject obj in unhealthy)
            {
                money += obj.ItemData.Price;
            }
            test.test(this, money);
            if (unhealthy.Count > 0)
            { 
                Gameplay.gameplay.GumTrigger.SetActive(true);
            }

            Gameplay.gameplay.StartCoroutine(GumCountDown(unhealthy));
            
            ObjectFactory.objectFactory.ReturnObject(gameObject);
            
        }

        private IEnumerator GumCountDown(List<InteractableObject> objects)
        {
            yield return new WaitForSeconds(1f);

            ObjectLauncher launcher = Gameplay.gameplay.Launcher;
            List<InteractableObject> unhealthy = launcher.GetActiveObjectsOfType(typeof(UnhealthyObject));

            

            foreach (InteractableObject obj in objects)
            {
                obj.TogglePause(false);
            }
           
            for (int i = 0; i < unhealthy.Count; i++)
            {
                (unhealthy[i] as UnhealthyObject).OnSwipe();
            }


            //ObjectLauncher launcher = Gameplay.gameplay.Launcher;
            Gameplay.gameplay.GumTrigger.GetComponent<GameItemTrigger>().powerUpDone();
            Gameplay.gameplay.GumTrigger.SetActive(false);
            launcher.Paused = false;
        }
    }
}
