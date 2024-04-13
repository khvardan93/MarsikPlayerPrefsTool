using UnityEngine;

namespace MarsikTools.Demo
{
    public class DemoIntItem : DemoBaseItem
    {
        protected override void SetEdit()
        {
            base.SetEdit();
            
            ValueField.SetTextWithoutNotify(PlayerPrefs.GetInt(PlayerPrefKey).ToString());
        }

        protected override void OnChange(string str)
        {
            if (int.TryParse(str, out int newValue))
            {
                PlayerPrefs.SetInt(PlayerPrefKey, newValue);
            }
            else
            {
                ValueField.SetTextWithoutNotify(PlayerPrefs.GetInt(PlayerPrefKey).ToString());
            }
        }
        
        public override void OnCreate()
        {
            PlayerPrefs.SetInt(PlayerPrefKey, 0);
            SetEdit();
        }
    }
}