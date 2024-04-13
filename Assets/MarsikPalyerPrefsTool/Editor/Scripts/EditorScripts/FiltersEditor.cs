using System;
using UnityEngine.UIElements;
using PlayerPrefsUtil;
using UnityEditor;
using FiltersConfigs = MarsikTools.Configs.FiltersEditor;

namespace MarsikTools.Editor
{
    public class FiltersEditor
    {
        private TextField SearchField;
        private EnumField TypeField;
        private EnumField OrderField;
        private EnumField SortField;
        private Toggle GroupField;
        private Toggle AutorefreshField;

        private Action OnChangeFilter;
        
        public FiltersEditor(VisualElement root, Action callback)
        {
            OnChangeFilter = callback;

            SearchField = root.Query<TextField>().Where(element => element.viewDataKey.IsEqual(Configs.FiltersEditor.SEARCH_FIELD) ).First();
            TypeField = root.Query<EnumField>().Where(element => element.viewDataKey.IsEqual(Configs.FiltersEditor.TYPE_FIELD)).First();
            OrderField = root.Query<EnumField>().Where(element => element.viewDataKey.IsEqual(Configs.FiltersEditor.ORDER_FIELD)).First();
            SortField = root.Query<EnumField>().Where(element => element.viewDataKey.IsEqual(Configs.FiltersEditor.SORT_FIELD)).First();
            GroupField = root.Query<Toggle>().Where(element => element.viewDataKey.IsEqual(Configs.FiltersEditor.GROUP_FIELD)).First();
            AutorefreshField = root.Query<Toggle>().Where(element => element.viewDataKey.IsEqual(Configs.FiltersEditor.AUTOREFRESH_FIELD)).First();

            Reset();
            
            SearchField.RegisterValueChangedCallback(OnSearch);
            TypeField.RegisterValueChangedCallback(OnChangeType);
            OrderField.RegisterValueChangedCallback(OnChangeOrder);
            SortField.RegisterValueChangedCallback(OnChangeSort);
            GroupField.RegisterValueChangedCallback(OnChangeGroup);
            AutorefreshField.RegisterValueChangedCallback(OnChangeAutoRefresh);
        }

        public void Reset()
        {
            Configs.FiltersEditor.Filters = new ()
            {
                SearchText = String.Empty,
                ShowType = ShowType.All,
                SortBy = SortBy.Key,
                OrderBy = 0,
                GroupByType = false
            } ;
            
            SearchField.SetValueWithoutNotify(Configs.FiltersEditor.Filters.SearchText);
            TypeField.SetValueWithoutNotify(Configs.FiltersEditor.Filters.ShowType);
            OrderField.SetValueWithoutNotify(Configs.FiltersEditor.Filters.OrderBy);
            SortField.SetValueWithoutNotify(Configs.FiltersEditor.Filters.SortBy);
            GroupField.SetValueWithoutNotify(Configs.FiltersEditor.Filters.GroupByType);
        }
        
        private void OnSearch(ChangeEvent<string> evt)
        {
            if(evt.newValue == evt.previousValue)
                return;

            Configs.FiltersEditor.Filters.SearchText = evt.newValue;
            OnChangeFilter?.Invoke();
        }

        private void OnChangeType(ChangeEvent<Enum> evt)
        {
            if(evt.newValue == evt.previousValue)
                return;
            
            ShowType value = (ShowType)evt.newValue;

            if(Configs.FiltersEditor.Filters.GroupByType)
            {
                Configs.FiltersEditor.Filters.GroupByType = false;
                GroupField.SetValueWithoutNotify(false);
            }
            
            Configs.FiltersEditor.Filters.ShowType = value;
            OnChangeFilter?.Invoke();
        }
        
        private void OnChangeOrder(ChangeEvent<Enum> evt)
        {
            if(evt.newValue == evt.previousValue)
                return;
            
            OrderBy value = (OrderBy)evt.newValue;
            Configs.FiltersEditor.Filters.OrderBy = value;
            OnChangeFilter?.Invoke();
        }
        
        private void OnChangeSort(ChangeEvent<Enum> evt)
        {
            SearchField.value = "";
            if(evt.newValue == evt.previousValue)
                return;
            
            SortBy value = (SortBy)evt.newValue;
            Configs.FiltersEditor.Filters.SortBy = value;
            OnChangeFilter?.Invoke();
        }
        
        private void OnChangeGroup(ChangeEvent<bool> evt)
        {
            SearchField.value = "";
            
            if(evt.newValue == evt.previousValue)
                return;
            
            Configs.FiltersEditor.Filters.GroupByType = evt.newValue;

            if (Configs.FiltersEditor.Filters.GroupByType)
            {
                Configs.FiltersEditor.Filters.ShowType = ShowType.All;
                TypeField.SetValueWithoutNotify(ShowType.All);
            }

            OnChangeFilter?.Invoke();
        }

        private void OnChangeAutoRefresh(ChangeEvent<bool> evt)
        {
            if(evt.newValue == evt.previousValue)
                return;

            if (evt.newValue)
            {
                if (EditorUtility.DisplayDialog("Enable auto refresh.", "Are you sure you want to enable auto refresh?",
                        "Enable", "Cancel"))
                {
                    DataStorageEditorWindow.AutoRefresh = true;
                }
                else
                {
                    AutorefreshField.SetValueWithoutNotify(false);
                }
            }
            else
            {
                DataStorageEditorWindow.AutoRefresh = false;
            }
        }
    }
}