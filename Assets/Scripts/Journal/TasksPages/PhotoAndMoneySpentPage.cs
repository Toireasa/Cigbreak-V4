using UnityEngine;
using CigBreak;
using System.Linq;

namespace CigBreakGUI
{
    public class PhotoAndMoneySpentPage : PhotoTaskPage
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            TaskStatus m_Task02Status = PlayerProfile.GetProfile().TaskStatus.FirstOrDefault(r => r.ID == 3);
            Calculate(m_Task02Status.Answer);
        }

        public override void TakePhoto()
        {
            base.TakePhoto();

            m_CaptureButton.SetActive(false);
            m_EditButton.SetActive(true);
        }

        public void Calculate(string _day)
        {
            float year = float.Parse(_day) * 365;
            string _currencySymbol;
            if (PlayerProfile.GetProfile().Contry == Country.UK)
            {
                _currencySymbol = "£";
            }
            else
            {
                _currencySymbol = "$";
            }
            m_Statement.text = "If I quit today I will save:\n"
                + _currencySymbol + year.ToString("0.00") + " in 1 year\n" + _currencySymbol + (year * 3).ToString("0.00")
                + " in 3 years\n" + _currencySymbol + (year * 5).ToString("0.00") + " in 5 years\n"
                + "Take a photo of what you'll buy with the money you save.";
        }
    }
}
