using UnityEngine;

namespace MarsikTools.Demo
{
    public class DemoStringItem : DemoBaseItem
    {
        protected override void SetEdit()
        {
            base.SetEdit();
            
            ValueField.SetTextWithoutNotify(PlayerPrefs.GetString(PlayerPrefKey));
        }

        protected override void OnChange(string str)
        {
            PlayerPrefs.SetString(PlayerPrefKey, str);
        }

        public override void OnCreate()
        {
            PlayerPrefs.SetString(PlayerPrefKey, string.Empty);
            SetEdit();
        }
    }
}