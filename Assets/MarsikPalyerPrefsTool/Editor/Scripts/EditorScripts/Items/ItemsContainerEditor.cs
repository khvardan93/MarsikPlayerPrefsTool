using System.Collections.Generic;
using MarsikTools.ScriptableObjects;
using UnityEngine.UIElements;
using PlayerPrefsUtil;
using PlayerPrefsUtil.DataBoxes;
using Storage = PlayerPrefsUtil.PlayerPrefsUtil;

namespace MarsikTools.Editor
{
    public class ItemsContainerEditor
    {
        private ScrollView ItemsContainer;

        private Dictionary<string, DataItemInterface> Items = new(); 

        private MainWindowItems MainWindowItems;
        
        public ItemsContainerEditor(ScrollView itemsContainer, MainWindowItems mainWindowItems)
        {
            MainWindowItems = mainWindowItems;
            ItemsContainer = itemsContainer;
        }

        public void RefreshItemList()
        {
            ItemsContainer.contentContainer.Clear(); //
            Items.Clear();

            Storage.RefreshAllPlayerPrefs();

            if (Configs.FiltersEditor.Filters.GroupByType)
                AddItemsGrouped();
            else
                AddItems();
        }

        private void AddItems()
        {
            foreach (var item in Storage.GetItemList(Configs.FiltersEditor.Filters))
            {
                AddItem(item.Key, item.Value);
            }
            
            AddEmpty();
        }
        
        private void AddItemsGrouped()
        {
            foreach (var group in Storage.GetGroupedItemList(Configs.FiltersEditor.Filters))
            {
                if(group.Value.Count == 0)
                    continue;
                
                AddHeader(group.Key);

                foreach (var item in group.Value)
                {
                    AddItem(item.Key, item.Value);
                }
            }
            
            AddEmpty();
        }

        public void DeleteSelected()
        {
            foreach (var item in Items)
            {
                if (item.Value.IsSelected())
                {
                    item.Value.DeleteItem();
                }
            }
            
            RefreshItemList();
        }
        
        public void AddNewItem(string key, int value)
        {
            if (Storage.TryAddNewItem(key, value, out var newItem))
                RefreshItemList();
        }

        public void AddNewItem(string key, float value)
        {
            if (Storage.TryAddNewItem(key, value, out var newItem))
                RefreshItemList();
        }

        public void AddNewItem(string key, string value)
        {
            if (Storage.TryAddNewItem(key, value, out var newItem))
                RefreshItemList();
        }

        private void AddItem(IntDataBox dataBox)
        {
            Items.TryAdd(dataBox.Key, new IntItemEditor(ItemsContainer, dataBox, MainWindowItems.GetDataItem()));
        }

        private void AddItem(FloatDataBox dataBox)
        {
            Items.TryAdd(dataBox.Key, new FloatItemEditor(ItemsContainer, dataBox, MainWindowItems.GetDataItem()));
        }

        private void AddItem(StringDataBox dataBox)
        {
            Items.TryAdd(dataBox.Key, new StringItemEditor(ItemsContainer, dataBox, MainWindowItems.GetDataItem()));
        }

        private void AddHeader(DataType dataType)
        {
            var newItem = MainWindowItems.GetGroupHeader();

            newItem.Query<Label>().First().text = dataType.ToString();
            
            ItemsContainer.Add(newItem);
        }

        private void AddEmpty()
        {
            ItemsContainer.Add(MainWindowItems.GetEmptyItem());
        }
        
        private void AddItem(DataBoxInterface dataBox, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Int:
                {
                    AddItem(dataBox as IntDataBox);
                    break;
                }
                case DataType.Float:
                {
                    AddItem(dataBox as FloatDataBox);
                    break;
                }
                case DataType.String:
                {
                    AddItem(dataBox as StringDataBox);
                    break;
                }
            }
        }
    }
}