using UnityEngine.UIElements;
using PlayerPrefsUtil.DataBoxes; 

namespace MarsikTools.Editor
{
    public class FloatItemEditor : DataItemEditor<FloatDataBox, float>
    {
        public FloatItemEditor(ScrollView root, FloatDataBox dataBox, VisualElement newItem) : base(root, dataBox, newItem)
        {

        }

        protected override void OnValueChange(ChangeEvent<string> evt)
        {
            if (evt.newValue == evt.previousValue)
                return;

            if (string.IsNullOrEmpty(evt.newValue))
            {
                ValueField.value = "0";
                Save(0f);
            }
            else if (float.TryParse(ValueField.value, out float floatValue))
            {
                Save(floatValue);
            }
            else
            {
                ValueField.value = evt.previousValue;
            }
        }
    }
}