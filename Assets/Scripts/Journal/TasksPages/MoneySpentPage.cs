using CigBreak;
using UnityEngine;
using UnityEngine.UI;

namespace CigBreakGUI
{
    public class MoneySpentPage : TaskPage
    {
        [SerializeField]
        protected InputField m_Answer;

        [SerializeField]
        private Text m_CurencySymbolTxt = null;

        [SerializeField]
        private Text CalculationField = null;

        string m_currencySymbol;

        void Start()
        {
            if (m_Status != null)
            {
                m_Answer.text = m_Status.Answer;
                Calculate(m_Status.Answer);
            }
           
            if (PlayerProfile.GetProfile().Contry == Country.UK)
            {
                m_currencySymbol = "£";               
            }
            else
            {
                m_currencySymbol = "$";
            }
            m_CurencySymbolTxt.text = m_currencySymbol;
        }

        public void Calculate(string _day)
        {
            float week = float.Parse(_day) * 7;
            float month = float.Parse(_day) * 30;
            float year = float.Parse(_day) * 365;
            CalculationField.text = "Smoking costs me:\n" + m_currencySymbol + week.ToString("0.00") + " per week\n" + m_currencySymbol + month.ToString("0.00") + " per month\n" + m_currencySymbol + year.ToString("0.00") + " per year";
        }

        public void Calculate()
        {
            float day = float.Parse(m_Answer.text);

            float week = day * 7;
            float month = day * 30;
            float year = day * 365;
            CalculationField.text = "Smoking costs me:\n" + m_currencySymbol + week.ToString("0.00") + " per week\n" + m_currencySymbol + month.ToString("0.00") + " per month\n" + m_currencySymbol + year.ToString("0.00") + " per year";
        }

        public override void OnOk()
        {
            if (m_Answer.text != string.Empty)
            {
                base.OnOk();
                PlayerProfile.GetProfile().RecordTaskStatus(m_Task.ID, m_Answer.text);
            }
        }
    }
}

