using System;
using MarsikTools.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using PlayerPrefsUtil;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

namespace MarsikTools.Editor
{
    public class AddNewEditorWindow : EditorWindow
    {
        [SerializeField] private AddNewWindowItems AddNewWindowItems;
        
        private VisualElement Root;
        private static ItemsContainerEditor ItemsContainer;

        private TextField KeyLabel;
        private EnumField TypeField;
        private VisualElement CurrentValueField;
        private DataType CurrentDataType;
        
        public static void ShowWindow(ItemsContainerEditor itemsContainer)
        {
            ItemsContainer = itemsContainer;
            
            AddNewEditorWindow wnd = GetWindow<AddNewEditorWindow>();

            wnd.minSize = new Vector2(400f, 160f);
            wnd.maxSize = new Vector2(400f, 160f);
            
            wnd.titleContent = new GUIContent(Configs.NewItemWindow.TITLE);
        }

        public void CreateGUI()
        {
            Root = rootVisualElement;
            Root.Add(AddNewWindowItems.GetAddNewWindow());


            Root.Query<Button>()
                .Where(element => element.viewDataKey.IsEqual(Configs.NewItemWindow.ADD_NEW_ITEM_BUTTON)).First()
                .RegisterCallback<ClickEvent>(AddNewItemButton);

            Root.Query<Button>()
                .Where(element => element.viewDataKey.IsEqual(Configs.NewItemWindow.CLOSE_NEW_ITEM_BUTTON)).First()
                .RegisterCallback<ClickEvent>((e) => CloseWindow());
            
            ResetAddContainer();
        }

        private void OnDisable()
        {
            RemoveValueField();
        }

        private void ResetAddContainer()
        {
            KeyLabel = Root.Query<TextField>()
                .Where(element => element.viewDataKey.IsEqual(Configs.NewItemWindow.NEW_ITEM_KEY)).First();
            TypeField = Root.Query<EnumField>()
                .Where(element => element.viewDataKey.IsEqual(Configs.NewItemWindow.NEW_ITEM_TYPE)).First();

            KeyLabel.value = string.Empty;
            TypeField.value = DataType.Int;

            TypeField.RegisterValueChangedCallback((evt) => { SetValueField((DataType)evt.newValue); });

            SetValueField(DataType.Int);
        }
        
        private void SetValueField(DataType type)
        {
            if (CurrentValueField != null)
            {
                RemoveValueField();
            }

            CurrentValueField = AddNewWindowItems.GetField(type);
            CurrentDataType = type;
            Root.Query<VisualElement>().Where(element => element.name.IsEqual(Configs.NewItemWindow.CONTAINER_NAME)).First()
                .Insert(3, CurrentValueField);
        }

        private void RemoveValueField()
        {
            switch (CurrentDataType)
            {
                case DataType.Int:
                {
                    CurrentValueField.Query<IntegerField>().First().value = 0;
                    break;
                }
                case DataType.Float:
                {
                    CurrentValueField.Query<FloatField>().First().value = 0f;
                    break;
                }
                case DataType.String:
                {
                    CurrentValueField.Query<TextField>().First().value = String.Empty;
                    break;
                }
            }
                
            CurrentValueField.RemoveFromHierarchy();
        }
        
        private void AddNewItemButton(ClickEvent evt)
        {
            if(string.IsNullOrEmpty(KeyLabel.value))
                return;
            
            var tmpType = TypeField.value;
            DataType type = tmpType is DataType dataType ? dataType : DataType.Int;

            switch (type)
            {
                case DataType.String:
                {
                    AddString();
                    break;
                }
                case DataType.Int:
                {
                    AddInt();
                    break;
                }
                case DataType.Float:
                {
                    AddFloat();
                    break;
                }
            }

            CloseWindow();
        }

        private void CloseWindow()
        {
            ResetAddContainer();
            GetWindow<AddNewEditorWindow>()?.Close();
        }
        
        private void AddInt()
        {
            int value = Root.Query<IntegerField>()
                .Where(element => element.viewDataKey.IsEqual(Configs.NewItemWindow.INPUT_FIELD_DATA_NAME)).First().value;

            ItemsContainer.AddNewItem(KeyLabel.value, value);
        }

        private void AddString()
        {
            string value = Root.Query<TextField>()
                .Where(element => element.viewDataKey.IsEqual(Configs.NewItemWindow.INPUT_FIELD_DATA_NAME)).First().value;

            ItemsContainer.AddNewItem(KeyLabel.value, value);
        }

        private void AddFloat()
        {
            float value = Root.Query<FloatField>()
                .Where(element => element.viewDataKey.IsEqual(Configs.NewItemWindow.INPUT_FIELD_DATA_NAME)).First().value;

            ItemsContainer.AddNewItem(KeyLabel.value, value);
        }
    }
}