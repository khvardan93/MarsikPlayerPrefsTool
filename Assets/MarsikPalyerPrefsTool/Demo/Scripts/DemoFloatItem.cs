using UnityEngine;

namespace MarsikTools.Demo
{
    public class DemoFloatItem : DemoBaseItem
    {
        protected override void SetEdit()
        {
            base.SetEdit();
            
            ValueField.SetTextWithoutNotify(PlayerPrefs.GetFloat(PlayerPrefKey).ToString());
        }

        protected override void OnChange(string str)
        {
            if (float.TryParse(str, out float newValue))
            {
                PlayerPrefs.SetFloat(PlayerPrefKey, newValue);
            }
            else
            {
                ValueField.SetTextWithoutNotify(PlayerPrefs.GetFloat(PlayerPrefKey).ToString());
            }
        }
        
        public override void OnCreate()
        {
            PlayerPrefs.SetFloat(PlayerPrefKey, 0f);
            SetEdit();
        }
    }
}