using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CigBreak;
using System.Linq;
using UnityEngine.Events;
using System.Collections.Generic;


namespace CigBreakGUI
{
    public class SummaryScreen : MonoBehaviour
    {
        [SerializeField]
        private float countTime = 2f;

        [SerializeField]
        private Image[] stars = null;

        [SerializeField]
        private Image barFill = null;

        [SerializeField]
        private Text m_CurrencySign = null;

        [SerializeField]
        private Text m_CounterText = null;

        [SerializeField]
        private Text m_PriceText = null;

        [SerializeField]
        private GameObject m_EndGameScreen = null;

        [SerializeField]
        private SocialShare m_SharePanel = null;

        private TallyScreen tallyScreen = null;

        private LevelData currentLevelData = null;

        private List<CounterCoroutine> runningCounters = null;

        private IEnumerator barCoroutine = null;

        public void Awake()
        {
            m_CounterText.text = "0";
            m_PriceText.text = "0";

            if (PlayerProfile.GetProfile().Contry == Country.UK)
                m_CurrencySign.text = "£";
            else
                m_CurrencySign.text = "$";            

            barFill.fillAmount = 0f;
        }

        public void Display(LevelData levelData, TallyScreen tallyScreen)
        {
            this.gameObject.SetActive(true);
            currentLevelData = levelData;
            this.tallyScreen = tallyScreen;

            runningCounters = new List<CounterCoroutine>();

            ShowSummary();


        }

        public void Share(bool _gold)
        {
            m_SharePanel.gameObject.SetActive(true);

            if (_gold)
            {                
                m_SharePanel.MessageText = "Share your story with your friends";
                m_SharePanel.SetNameMessage = currentLevelData.LevelID.ToUpper() + " COMPLETED";
                m_SharePanel.SetDescriptionMessage = "I’m sticking to my goal to quit smoking forever! Playing Cigbreak on my phone really helps me beat the cravings. I just won a gold star for for level " + currentLevelData.LevelID;
                m_SharePanel.SetImageURL = "http://static.wixstatic.com/media/ac9214_3c6e05d3738f4f81be3933e884cd84d9.png/v1/fill/w_125,h_125,al_c/ac9214_3c6e05d3738f4f81be3933e884cd84d9.png";
            }
            else
            {
                m_SharePanel.MessageText = "Share your story with your friends";
                m_SharePanel.SetNameMessage = "YAY! I’VE FINISHED THE GAME";
                m_SharePanel.SetDescriptionMessage = "I’ve played through all the levels of Cigbreak Free and I am staying smokefree";
                m_SharePanel.SetImageURL = "http://static.wixstatic.com/media/ac9214_bfb2811d43254e66a7977151fc535779.jpeg/v1/fill/w_250,h_250,al_c,q_90/ac9214_bfb2811d43254e66a7977151fc535779.jpeg";
            }
        }


        private void ShowSummary()
        {
            StartAndAddCounterCoroutine(m_CounterText, currentLevelData.CigaretteTarget, 0f);
            StartAndAddCounterCoroutine(m_PriceText, tallyScreen.LevelResult.MaxMoney, 0f);
            barCoroutine = FillBar(countTime);
            StartCoroutine(barCoroutine);
        }

        private void StartAndAddCounterCoroutine(Text field, int value, float delay = 0f)
        {
            IEnumerator coroutine = CountUp(field, value, delay);
            StartCoroutine(coroutine);

            AddNewCounterCoroutine(coroutine, field, value.ToString());
        }

        private void StartAndAddCounterCoroutine(Text field, float value, float delay = 0f)
        {
            IEnumerator coroutine = CountUp(field, value, delay);
            StartCoroutine(coroutine);

            AddNewCounterCoroutine(coroutine, field, value.ToString("0.00"));
        }

        private IEnumerator CountUp(Text text, int value, float delay = 0f)
        {
            int currentVal = 0;
            float interval = countTime / value;

            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            while (currentVal <= value)
            {
                text.text = currentVal.ToString();
                currentVal += 1;
                yield return new WaitForSeconds(interval);
            }

            text.text = value.ToString();
            RemoveCoroutineCounter(text);
        }

        private IEnumerator CountUp(Text text, float value, float delay = 0f)
        {
            float currentVal = 0f;
            float interval = countTime / (value * 4f); // Multiply the value by 4 as we increment by 0.25

            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            while (currentVal <= value)
            {
                text.text = currentVal.ToString("0.00");
                currentVal += 0.25f;
                yield return new WaitForSeconds(interval);
            }

            text.text = value.ToString("0.00");
        }

        private IEnumerator FillBar(float delay = 0f)
        {
            float fillLevel = tallyScreen.LevelResult.RemainingHealthPercentage;

            int starCount = 0;

            float currentFill = 0f;
            float fillSpeed = 1f / countTime;

            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            while (currentFill < fillLevel)
            {
                barFill.fillAmount = currentFill;
                starCount += UpdateStarDisplay(starCount, currentFill);

                currentFill += fillSpeed * Time.deltaTime;

                yield return null;
            }

            UpdateStarDisplay(starCount, currentFill);
        }

        private int UpdateStarDisplay(int displayedStarCount, float fill)
        {
            switch (displayedStarCount)
            {
                case 0:
                    if (fill == 0f)
                    {
                        ShowNextStar();
                        return 1;
                    }
                    break;

                case 1:
                    if (fill >= 0.5f)
                    {
                        ShowNextStar();
                        return 1;
                    }
                    break;

                case 2:
                    if (fill >= 1f)
                    {
                        ShowNextStar();
                        return 1;
                    }
                    break;
            }

            return 0;
        }

        private void ShowNextStar()
        {
            Image next = stars.FirstOrDefault(s => s.color.a < 1f);
            if (next != null)
            {
                next.color = Color.white;
            }
        }


        public void LevelRewardBT()
        {
            SkipAllCounters();
            SkipFillBar();
            this.gameObject.SetActive(false);

            if (currentLevelData.LevelID != "Lvl30")
            {
                tallyScreen.LoadLevelRewardScreen();

            }
            else
            {
                m_EndGameScreen.SetActive(true);

            }
        }

        private void SkipAllCounters()
        {
            foreach (CounterCoroutine cc in runningCounters)
            {
                StopCoroutine(cc.Coroutine);
                cc.Field.text = cc.Value;
            }
        }

        private void SkipFillBar()
        {
            StopCoroutine(barCoroutine);
            barFill.fillAmount = tallyScreen.LevelResult.RemainingHealthPercentage;
            for (int i = 0; i < tallyScreen.StarsEarned; i++)
            {
                stars[i].color = Color.white;
            }
        }

        private void AddNewCounterCoroutine(IEnumerator coroutine, Text field, string value)
        {
            runningCounters.Add(new CounterCoroutine() { Coroutine = coroutine, Field = field, Value = value });
        }

        private void RemoveCoroutineCounter(Text field)
        {
            CounterCoroutine cc = runningCounters.FirstOrDefault(c => c.Field == field);
            runningCounters.Remove(cc);
        }


        private struct CounterCoroutine
        {
            public IEnumerator Coroutine { get; set; }
            public Text Field { get; set; }
            public string Value { get; set; }
        }

        //[System.Serializable]
        //private class ObjectValueCounter
        //{
        //    public enum ObjectType { None, Cigarette, Veg }

        //    [SerializeField]
        //    private ObjectType objectType = ObjectType.None;
        //    public ObjectType TypeOfObject { get { return objectType; } }

        //    [SerializeField]
        //    private Text count = null;
        //    public Text Count { get { return count; } }

        //    [SerializeField]
        //    private Text currencyValue = null;
        //    public Text CurrencyValue { get { return currencyValue; } }
        //}

        //private IEnumerator ExitScreen(UnityAction callback)
        //{
        //    SkipAllCounters();
        //    SkipFillBar();

        //    yield return new WaitForSeconds(0.5f);
        //    callback();
        //}

    }
}
