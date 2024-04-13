using UnityEngine.UIElements;
using PlayerPrefsUtil.DataBoxes;

namespace MarsikTools.Editor
{
    public class StringItemEditor : DataItemEditor<StringDataBox, string>
    {
        public StringItemEditor(ScrollView root, StringDataBox dataBox, VisualElement newItem) : base(root, dataBox, newItem)
        {

        }

        protected override void OnValueChange(ChangeEvent<string> evt)
        {
            if (evt.newValue == evt.previousValue)
                return;

            if (string.IsNullOrEmpty(evt.newValue))
            {
                ValueField.value = string.Empty;
                Save(string.Empty);
            }
            else
            {
                Save(ValueField.value);
            }
        }
    }
}