using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace CigBreak
{
    /// <summary>
    /// Main gameplay control, in effect only during the play sequences
    /// Includes state machine, game state variables, events
    /// </summary>
    public class Gameplay : MonoBehaviour
    {
        // Player health
        [SerializeField]
        private int playerMaxHealth = 6;
        public int PlayerMaxHealth { get { return playerMaxHealth; } }

        private int currentPlayerHealth = 0;
        public int CurrentPlayerHealth { get { return currentPlayerHealth; } }

        // Values controlling behaviour of in game objects
        [SerializeField]
        private float minObjectForce = 550f;
        [SerializeField]
        private float maxObjectForce = 650f;
        [SerializeField]
        private float forceScale = 1f;

        // Return a random value from the set range
        public float ObjectForce { get { return Random.Range(minObjectForce, maxObjectForce) * forceScale; } }
        public float ObjectTorque { get { return Random.Range(currentLevelData.MinTorque, currentLevelData.MaxTorque); } }

        // Audio
        private AudioSource musicSource = null;
        [SerializeField]
        private List<AudioClip> m_CoughSoundsList = null;
        private AudioSource soundEffectSource = null;

        // Game state
        private enum GameState { None, Tutorial, PreLevel, InLevel, PostLevel }
        private GameState gameState = GameState.None;

        public bool Paused { get; private set; }

        public bool perfectRun = true;

        // Level data
        private LevelSet levelSet = null;
        public int CurrentLevelIndex { get; private set; }
        private LevelData currentLevelData = null;
        public LevelData CurrentLevel { get { return currentLevelData; } }

        private GameItemSet powerupList = null;
        public GameItemSet PowerUpList
        {
            get { return powerupList; }
        }
        //private GameItemData selectedPowerup = null;

        //[SerializeField]
        //private Button m_SprayPowerUpBT = null;
        //[SerializeField]
        //private Button m_GumPowerUpBT = null;

        [SerializeField]
        private GameObject m_SprayFingerTutorial = null;

        [SerializeField]
        private GameObject m_GumFingerTutorial = null;


        // Fail tutorials
        [SerializeField]
        private FailTutorial[] failTutorials = null;

        // Scene references 
        private ObjectFactory factory = null;
        [SerializeField]
        private ObjectLauncher launcher = null;
        public ObjectLauncher Launcher 
        {
            get { return launcher; } 
        }

        [SerializeField]
        private GameObject gumTrigger = null;
        public GameObject GumTrigger { get { return gumTrigger; } }

        // Singleton variable
        public static Gameplay gameplay = null;

        // Counters
        public int CigsBroken { get; private set; }
        private int vegBroken = 0;
        //private float brokenVegValue = 0f;
        private float moneySaved = 0f;
        private float maxMoney = 0f;

        // Powerup
        //private bool powerupCooldown = false;
        //private float powerupCooldownTime = 30f;

        // Tutorial and level prep
        private int completedPreLevelActions = 0;
        private int completedTutorialSteps = 0;
        private TutorialData tutorialData = null;
        private TutorialData.TutorialInstruction currentTutorialInstruction = null;
        [SerializeField]
        private GameItemTrigger tutorialTrigger = null;

        // Events
        public UnityAction<Sprite> OnTutorialInstruction { get; set; } // Called when a tutorial instruction is executed, passes instruction sprite
        public UnityAction<UnityAction> OnEndTutorialStep { get; set; } // Called when tutorial was completed
        public UnityAction<UnityAction> OnEndStep { get; set; } // Called when tutorial was completed
        public UnityAction<GameObject, UnityAction> OnFailTutorialInstruction { get; set; } // Called when a fail tutorial is displayed, passes instruction object and dismiss callback
        public UnityAction OnHideFailTutorial { get; set; } // Called when fail tutorial is dismissed
        public UnityAction<int, UnityAction> OnPrepareLevel { get; set; } // Called when level is about to start, passes level number and completion callback
        public UnityAction<InteractableObject> OnInteractableObjectDestroyed { get; set; } // Called when an interactable object is destroyed, passes object data
        public UnityAction<InteractableObject> OnPowerupCollected { get; set; } // Called when a powerup is collected
        public UnityAction OnPointEarned { get; set; } // Called when a point is earned - unhealthy objects is destroyed
        public UnityAction<float> OnMoneySavedUpdate { get; set; } // Called when the money saved value changes
        public UnityAction<int> OnHealthChanged { get; set; }   // Called when player health changes
        public UnityAction<UnityAction> OnFinishLevel { get; set; } // Called when level is finished

        public UnityAction OnPowerUpUse { get; set; } //Calle when power up is used
        private void Awake()
        {
            // Singleton
            if (gameplay == null)
            {
                gameplay = this;
            }
            else
            {
                Destroy(gameObject);
            }

            TogglePause(false);
            SetFullHealth();

            // Set up factory and objects
            factory = ObjectFactory.objectFactory;
            factory.UpdateForceScale(forceScale - 0.2f);

            // Interactable objects should not collide with eachother
            Physics2D.IgnoreLayerCollision(CigBreakConstants.Layers.InteractableObjects, CigBreakConstants.Layers.InteractableObjects);

            // Load level data
            levelSet = Resources.Load<LevelSet>(CigBreakConstants.Paths.LevelSet);
            CurrentLevelIndex = PlayerPrefs.GetInt(CigBreakConstants.PlayerPrefKeys.CurrentLevel);
            currentLevelData = levelSet.LevelData[CurrentLevelIndex];

            // Powerups
            powerupList = Resources.Load<GameItemSet>(CigBreakConstants.Paths.PowerupSet);

            if (currentLevelData.Tutorial != null)
            {
                if (currentLevelData.Tutorial.PowerupItem != null)
                {
                    //selectedPowerup = currentLevelData.LevelRewards.PowerupReward;
                    PlayerProfile.GetProfile().AddPowerUp(currentLevelData.Tutorial.PowerupItem.GUID);
                }
            }

            // Audio
            musicSource = GetComponent<AudioSource>();
            musicSource.clip = currentLevelData.Music;

            soundEffectSource = gameObject.AddComponent<AudioSource>();

            // Tutorial
            tutorialTrigger.Deactivate();

            // Keep only the untriggered fail tutorials
            FilterFailTutorials();


        }

        private void Start()
        {
            ChangeState(GameState.Tutorial);
        }

        // State machine
        private void ChangeState(GameState newState)
        {
            gameState = newState;

            switch (newState)
            {
                case GameState.Tutorial:
                    OnTutorialState();
                    break;

                case GameState.PreLevel:
                    OnPreLevelState();
                    break;

                case GameState.InLevel:
                    OnLevelStart();
                    break;

                case GameState.PostLevel:
                    OnLevelFinished();
                    break;
            }
        }


        #region Tutorial

        private void OnTutorialState()
        {
            tutorialData = currentLevelData.Tutorial;
            if (tutorialData != null)
            {
                if (tutorialData.name.Contains("Gum") )
                {
                    m_GumFingerTutorial.SetActive(true);
                }
                else if(tutorialData.name.Contains("Spray"))
                {
                    m_SprayFingerTutorial.SetActive(true);
                }

                if (tutorialData.ShowSmoke && OnHealthChanged != null)
                {
                    OnHealthChanged(playerMaxHealth / 2);
                }

                // Play music
                musicSource.clip = tutorialData.TutorialMusic;
                musicSource.loop = true;
                StartCoroutine(FadeInMusic(1f));

                // Execute instructions
                StartCoroutine(ExecuteTutorial());
            }
            else
            {
                EndTutorial();
            }
        }

        private IEnumerator ExecuteTutorial()
        {
            if (completedTutorialSteps < tutorialData.Instructions.Count())
            {
               ;

                // Send instruction to be displayed
                currentTutorialInstruction = tutorialData.Instructions.ElementAt(completedTutorialSteps);

                if (currentTutorialInstruction.ExpectedAction == TutorialData.TutorialInstruction.Action.Swipe)
                {
                    tutorialTrigger.gameObject.SetActive(true);
                }
                if (OnTutorialInstruction != null)
                {
                    OnTutorialInstruction(currentTutorialInstruction.InstructionBackground);
                }

                // Launch the object
                if (currentTutorialInstruction.GameItem != null)
                {
                    LaunchTutorialObject(currentTutorialInstruction);
                }
            }
            else
            {
                //Tutorial complete
                if (OnEndTutorialStep != null)
                {
                    OnEndTutorialStep(EndTutorial);
                }
                else
                {
                    EndTutorial();
                }
            }
            yield return 0;
        }

        private void TutorialReset()
        {
            tutorialTrigger.Deactivate();
            completedTutorialSteps = 0;
            StartCoroutine(ExecuteTutorial());
        }

        private void TutorialMoveNext()
        {
            completedTutorialSteps++;
            StartCoroutine(ExecuteTutorial());
        }

        private void LaunchTutorialObject(TutorialData.TutorialInstruction instruction)
        {
            launcher.LaunchSingleObject(instruction.GameItem);
        }

        private void EndTutorial()
        {
            StartCoroutine(FadeOutMusic(1f));
            tutorialTrigger.Deactivate();
            ChangeState(GameState.PreLevel);
        }

        #endregion

        #region preLevel

        private void OnPreLevelState()
        {
            if (OnPrepareLevel != null)
            {
                OnPrepareLevel(CurrentLevelIndex + 1, PreparationActionComplete);
            }
        }

        private void PreparationActionComplete()
        {
            // Keep track and proceed once all subscribers to OnPrepareLever have calledback
            completedPreLevelActions++;
            if (completedPreLevelActions <= OnPrepareLevel.GetInvocationList().Length)
            {
                ChangeState(GameState.InLevel);
            }
        }

        #endregion

        #region inLevel

        private void OnLevelStart()
        {
            // Set health to max health
            SetFullHealth();
            perfectRun = true;

            //Start level music
            musicSource.clip = currentLevelData.Music;
            musicSource.loop = true;
            StartCoroutine(FadeInMusic(1f));

            launcher.StartLevel();
        }


        private void OnLevelFinished()
        {
            TogglePause(true);
            launcher.EndLevel();

            musicSource.Stop();
            if (OnFinishLevel != null)
            {
                OnFinishLevel(EndLevel);
            }
        }

        private void EndLevel()
        {
            bool completed = CigsBroken == currentLevelData.CigaretteTarget;
            float healthPercentage = (float)currentPlayerHealth / (float)playerMaxHealth;

            // Create a results objects and save it
            LevelResults levelResult = new LevelResults()
            {
                Completed = completed,
                MoneySaved = moneySaved,
                MaxMoney = maxMoney,
                VegBroken = vegBroken,
                //BrokenVegValue = brokenVegValue,
                RemainingHealthPercentage = healthPercentage
            };

            levelResult.Save();

            TogglePause(false);

            // Load tally screen
            LoadingScreen.LoadScene(CigBreakConstants.SceneNames.LevelStatus);
        }

        #endregion

        #region Listeners

        public void ObjectSpawned(InteractableObject obj)
        {
            // When object is spawned subscribe to all its events
            obj.OnObjectDestroyed += ObjectDestroyed;
            obj.OnObjectDestroyed += OnInteractableObjectDestroyed;
            obj.OnObjectCollected += ObjectCollected;
            obj.OnObjectOutOfScreen += ObjectOutOfScreen;
        }


        private void ObjectDestroyed(InteractableObject obj)
        {
            if (gameState == GameState.InLevel)
            {
                switch (obj.ItemData.TypeOfItem)
                {
                    // For unhealth objects add points and advance towards level goal
                    case GameItemData.ItemType.Unhealthy:
                        if (OnPointEarned != null)
                        {
                            OnPointEarned();
                        }
                        CigsBroken++;
                        maxMoney += obj.ItemData.Price;
                        UpdateMoneySaved(obj.ItemData.Price);
                        if (CigsBroken == currentLevelData.CigaretteTarget)
                        {
                            PlayerProfile.GetProfile().TotalCigsBroken += CigsBroken;
                            ChangeState(GameState.PostLevel);
                        }

                        break;

                    // For healthy objects remove health and value from saved money
                    case GameItemData.ItemType.Healthy:
                        vegBroken++;
                        //brokenVegValue += obj.ItemData.Price;
                        UpdateHealth(-1);
                        //UpdateMoneySaved(-obj.ItemData.Price);
                        PlayFailSound();

                        break;
                }

                // Fail tutorial
                TutorialData.TutorialInstruction.Action action = TutorialData.TutorialInstruction.Action.Swipe;
                CheckAndExecuteFailTutorial(obj, action);
            }
            else if (gameState == GameState.Tutorial)
            {
                // During tutorial check if the action was correct then advance or repeat tutorial
                if (currentTutorialInstruction.ExpectedAction == TutorialData.TutorialInstruction.Action.Swipe)
                {
                    // Disreagard all additional objects
                    foreach (InteractableObject item in factory.GetActiveObjectsOfType(obj.GetType()))
                    {
                        item.TogglePause(false);
                        RemoveListeners(item);
                    }

                    TutorialMoveNext();
                }
                else
                {
                    TutorialReset();
                }
            }

            // Remove all event subscriptions from object
            RemoveListeners(obj);
        }


        private void ObjectCollected(InteractableObject obj)
        {
            if (gameState == GameState.InLevel)
            {
                // Only powerups can be collected
                switch (obj.ItemData.TypeOfItem)
                {
                    case GameItemData.ItemType.Powerup:
                        Paused = false;
                        PowerupCollected(obj);
                        break;
                }

                // Fail tutorial
                TutorialData.TutorialInstruction.Action action = TutorialData.TutorialInstruction.Action.Tap;
                CheckAndExecuteFailTutorial(obj, action);
            }
            else if (gameState == GameState.Tutorial)
            {
                // During tutorial check if the action was correct then advance or repeat tutorial
                if (currentTutorialInstruction.ExpectedAction == TutorialData.TutorialInstruction.Action.Tap)
                {
                    PowerupCollected(obj);
                    TutorialMoveNext();
                }
                else
                {
                    TutorialReset();
                }
            }

            RemoveListeners(obj);
        }

        private void PowerupCollected(InteractableObject obj)
        {
            if (OnPowerupCollected != null)
            {
                OnPowerupCollected(obj);
            }
        }

        private void ObjectOutOfScreen(InteractableObject obj)
        {
            if (gameState == GameState.InLevel)
            {
                // If unhealthy object falls unbroken then remove health
                switch (obj.ItemData.TypeOfItem)
                {
                    case GameItemData.ItemType.Unhealthy:
                        UpdateHealth(-1);
                        PlayFailSound();
                        break;
                }

                //Fail tutorial
                TutorialData.TutorialInstruction.Action action = TutorialData.TutorialInstruction.Action.None;
                CheckAndExecuteFailTutorial(obj, action);
            }
            else if (gameState == GameState.Tutorial)
            {
                // During tutorial check if the action was correct then advance or repeat tutorial
                if (currentTutorialInstruction.ExpectedAction == TutorialData.TutorialInstruction.Action.None)
                {
                    TutorialMoveNext();
                }
                else
                {
                    // Disreagard all additional objects
                    foreach (InteractableObject item in factory.GetActiveObjectsOfType(obj.GetType()))
                    {
                        RemoveListeners(item);
                    }

                    TutorialReset();
                }
            }

            RemoveListeners(obj);
        }

        private void RemoveListeners(InteractableObject obj)
        {
            // Remove all event subscruptions from object
            obj.OnObjectDestroyed -= ObjectDestroyed;
            obj.OnObjectDestroyed -= OnInteractableObjectDestroyed;
            obj.OnObjectCollected -= ObjectCollected;
            obj.OnObjectOutOfScreen -= ObjectOutOfScreen;
        }

        private void CheckAndExecuteFailTutorial(InteractableObject obj, TutorialData.TutorialInstruction.Action action)
        {
            if (failTutorials.Length > 0)
            {
                string guid = DisplayFailTutorial(obj, action);
                if (guid != null)
                {
                    PlayerProfile.GetProfile().FailTutorialSeen(guid);
                    FilterFailTutorials();
                }
            }
        }

        private string DisplayFailTutorial(InteractableObject obj, TutorialData.TutorialInstruction.Action failAction)
        {
            FailTutorial tutorial = failTutorials.FirstOrDefault(t => t.FailAction == failAction && t.ItemType == obj.ItemData.TypeOfItem);
            if (tutorial != null && OnFailTutorialInstruction != null)
            {
                TogglePause(true);
                OnFailTutorialInstruction(tutorial.UIPrefab, () => TogglePause(false));
                return tutorial.GUID;
            }

            return null;
        }

        #endregion

        // Utility and cleanup

        public void UpdateHealth(int change)
        {
            int prevHealth = currentPlayerHealth;
            currentPlayerHealth = Mathf.Clamp(currentPlayerHealth + change, 0, playerMaxHealth);
            if (prevHealth != currentPlayerHealth)
            {
                if (currentPlayerHealth == 0)
                {
                    ChangeState(GameState.PostLevel);
                }

                OnHealthEvent();

            }

            if (change < 0)
            {
                perfectRun = false;
            }
        }

        public void SetFullHealth()
        {
            currentPlayerHealth = playerMaxHealth;
            OnHealthEvent();
        }

        private void OnHealthEvent()
        {
            if (OnHealthChanged != null)
            {
                OnHealthChanged(currentPlayerHealth);
            }
        }

        public void LauncheSprayPowerUp()
        {
            if (gameState == GameState.Tutorial)
            {
                m_SprayFingerTutorial.SetActive(false);
                PlayerProfile.GetProfile().RemovePowerUp(powerupList.Items[0].GUID);
                if (OnPowerUpUse != null)
                {
                    OnPowerUpUse();
                }
                TutorialMoveNext();

            }
            else
            {
                Paused = true;
                if (PlayerProfile.GetProfile().PowerupInventory.ContainsKey(powerupList.Items[0].GUID))
                {
                    if (launcher.GetActiveObjectsOfType(typeof(PowerUp)).Count == 0 && PlayerProfile.GetProfile().PowerupInventory[powerupList.Items[0].GUID] > 0)
                    {
                        launcher.LaunchSingleObject(powerupList.Items[0]);
                        PlayerProfile.GetProfile().RemovePowerUp(powerupList.Items[0].GUID);
                        if (OnPowerUpUse != null)
                        {
                            OnPowerUpUse();
                        }
                    }

                }
                if (gameState == GameState.Tutorial)
                {
                    TutorialMoveNext();
                }
            }
        }

        public void LauncheGumPowerUp()
        {
            if (gameState == GameState.Tutorial)
            {
                m_GumFingerTutorial.SetActive(false);
                PlayerProfile.GetProfile().RemovePowerUp(powerupList.Items[1].GUID);
                if (OnPowerUpUse != null)
                {
                    OnPowerUpUse();
                }
                OnEndStep(null);
                TutorialMoveNext();
            }
            else
            {
                Paused = true;
                if (PlayerProfile.GetProfile().PowerupInventory.ContainsKey(powerupList.Items[1].GUID))
                {
                    if (launcher.GetActiveObjectsOfType(typeof(PowerUp)).Count == 0 && PlayerProfile.GetProfile().PowerupInventory[powerupList.Items[1].GUID] > 0)
                    {
                        launcher.LaunchSingleObject(powerupList.Items[1]);
                        PlayerProfile.GetProfile().RemovePowerUp(powerupList.Items[1].GUID);
                    }
                    if (OnPowerUpUse != null)
                    {
                        OnPowerUpUse();
                    }
                }
            }

        }

        private void UpdateMoneySaved(float change)
        {
            moneySaved = moneySaved + change;
            //moneySaved = Mathf.Max(0f, moneySaved + change);
            if (OnMoneySavedUpdate != null)
            {
                OnMoneySavedUpdate(moneySaved);
            }
        }

        private void FilterFailTutorials()
        {
            PlayerProfile profile = PlayerProfile.GetProfile();
            failTutorials = failTutorials.Where(t => !profile.SeenFailTutorials.Contains(t.GUID)).ToArray();
        }

        public void TogglePause(bool pause)
        {
            Paused = pause;
            Time.timeScale = Paused ? 0f : 1f;

            if (musicSource != null)
            {
                if (pause)
                {
                    musicSource.Pause();
                }
                else
                {
                    musicSource.UnPause();
                }
            }
        }

        public void SetForceScale(float newScale)
        {
            forceScale = Mathf.Clamp01(newScale);
            factory.UpdateForceScale(forceScale - 0.2f);
        }

        private void PlayFailSound()
        {
            float healthPercentage = ((float)currentPlayerHealth / (float)playerMaxHealth)*100;
        
            if (healthPercentage > 80)
            {
                soundEffectSource.PlayOneShot(m_CoughSoundsList[0]);
            }
            else if (healthPercentage > 50)
            {
                soundEffectSource.PlayOneShot(m_CoughSoundsList[1]);
            }
            else
            {
                soundEffectSource.PlayOneShot(m_CoughSoundsList[2]);
            }            
        }

        private IEnumerator FadeInMusic(float time)
        {
            musicSource.volume = 0;
            musicSource.Play();

            float speed = 1f / time;
            while (musicSource.volume < 0.5f)
            {
                musicSource.volume += speed * Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator FadeOutMusic(float time)
        {
            float speed = 1f / time;
            while (musicSource.volume > 0f)
            {
                musicSource.volume -= speed * Time.deltaTime;
                yield return null;
            }

            musicSource.Stop();
        }

        //private IEnumerator CoolDownPowerup()
        //{
        //    powerupCooldown = true;
        //    yield return new WaitForSeconds(powerupCooldownTime);
        //    powerupCooldown = false;
        //}

        private void OnDisable()
        {
            gameplay = null;
        }
    }
}
