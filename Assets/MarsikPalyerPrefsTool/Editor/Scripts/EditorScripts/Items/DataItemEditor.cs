using PlayerPrefsUtil;
using UnityEngine.UIElements;
using PlayerPrefsUtil.DataBoxes;

namespace MarsikTools.Editor
{
    interface DataItemInterface
    {
        public bool IsSelected();
        public void DeleteItem();
    }
    
    public class DataItemEditor<T, K> : DataItemInterface where T : BaseDataBox<K>
    {
        protected T DataBox;
        protected TextField ValueField;
        protected Toggle SelectToggle;

        protected DataItemEditor(ScrollView root, T dataBox, VisualElement newItem)
        {
            DataBox = dataBox;

            newItem.Query<Label>().Where(element => element.viewDataKey.IsEqual(Configs.Items.ITEM_KEY_FIELD)).First().text = dataBox.Key;
            newItem.Query<Label>().Where(element => element.viewDataKey.IsEqual(Configs.Items.ITEM_TYPE_FIELD)).First().text =
                dataBox.DataType.ToString();

            SelectToggle = newItem.Query<Toggle>().Where(element => element.viewDataKey.IsEqual( Configs.Items.SELECT_TOGGLE)).First();

            ValueField = newItem.Query<TextField>().Where(element => element.viewDataKey.IsEqual(Configs.Items.VALUE_FIELD)).First();
            ValueField.value = dataBox.Value.ToString();
            ValueField.RegisterValueChangedCallback(OnValueChange);

            root.Add(newItem);
        }

        protected virtual void OnValueChange(ChangeEvent<string> evt)
        {

        }

        protected virtual void Save(K value)
        {
            DataBox.Value = value;
        }

        public bool IsSelected()
        {
            return SelectToggle.value;
        }

        public void DeleteItem()
        {
            DataBox.Delete();
        }
    }
}