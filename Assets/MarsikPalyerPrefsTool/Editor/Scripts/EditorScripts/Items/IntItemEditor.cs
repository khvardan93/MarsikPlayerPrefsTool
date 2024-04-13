using UnityEngine.UIElements;
using PlayerPrefsUtil.DataBoxes;

namespace MarsikTools.Editor
{
    public class IntItemEditor : DataItemEditor<IntDataBox, int>
    {
        public IntItemEditor(ScrollView root, IntDataBox dataBox, VisualElement newItem) : base(root, dataBox, newItem)
        {

        }

        protected override void OnValueChange(ChangeEvent<string> evt)
        {
            if (evt.newValue == evt.previousValue)
                return;

            if (string.IsNullOrEmpty(evt.newValue))
            {
                ValueField.value = "0";
                Save(0);
            }
            else if (int.TryParse(ValueField.value, out int intValue))
            {
                Save(intValue);
            }
            else
            {
                ValueField.value = evt.previousValue;
            }
        }
    }
}