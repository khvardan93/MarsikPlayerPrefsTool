using System;
using MarsikTools.ScriptableObjects;
using PlayerPrefsUtil;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MarsikTools.Editor
{
    public class DataStorageEditorWindow : EditorWindow
    {
        private ItemsContainerEditor ItemsContainer;
        private FiltersEditor Filters;
        private VisualElement Root;

        [SerializeField] private MainWindowItems MainWindowItems;
        
        private static DataStorageEditorWindow wnd;
        public static bool AutoRefresh = false;
        
        private double RefreshTimer;
        
        [MenuItem(Configs.MainWindow.MENU_PATH)]
        public static void ShowWindow()
        {
            wnd = GetWindow<DataStorageEditorWindow>();
            
            wnd.minSize = Vector2.one * 100f;
            wnd.titleContent = new GUIContent(Configs.MainWindow.MENU_NAME);
        }

        public void CreateGUI()
        {
            Root = rootVisualElement;
            Root.Add(MainWindowItems.GetMainWindow());

            ScrollView itemsListView = rootVisualElement.Query<ScrollView>()
                .Where(element => element.name.IsEqual(Configs.MainWindow.LIST_VIEW)).First();
            EditorUtility.SetDirty(this);
            ItemsContainer = new ItemsContainerEditor(itemsListView, MainWindowItems);
            
            GetWindow<DataStorageEditorWindow>().Focus();
            Filters = new FiltersEditor(Root, ItemsContainer.RefreshItemList);
            GetWindow<DataStorageEditorWindow>().Repaint();

            
            Root.Query<Button>().Where(element => element.viewDataKey.IsEqual(Configs.MainWindow.TOP_BUTTONS_OPEN_NEW_BUTTON)).First()
                .RegisterCallback<ClickEvent>(OpenAddContainer);
            
            Root.Query<Button>().Where(element => element.viewDataKey.IsEqual(Configs.MainWindow.TOP_BUTTONS_DELETE_SELECTED_BUTTON)).First()
                .RegisterCallback<ClickEvent>(DeleteSelectedItems);
            
            Root.Query<Button>().Where(element => element.viewDataKey.IsEqual(Configs.MainWindow.TOP_BUTTONS_RESET_BUTTON)).First()
                .RegisterCallback<ClickEvent>((e) => Filters.Reset());
            
            Root.Query<Button>().Where(element => element.viewDataKey.IsEqual(Configs.MainWindow.TOP_BUTTONS_REFRESH_BUTTON)).First()
                .RegisterCallback<ClickEvent>((e) => ItemsContainer.RefreshItemList());
            
            ItemsContainer.RefreshItemList();
        }
        
        private void OnEnable()
        {
            EditorApplication.update += RefreshState;
            RefreshTimer = Time.time;
        }

        private void OnDisable()
        {
            EditorApplication.update -= RefreshState;
        }

        private void RefreshState()
        {
            if (AutoRefresh && DateTime.Now.TimeOfDay.TotalSeconds - RefreshTimer > MainWindowItems.GetRefreshRate())
            {
                RefreshTimer = DateTime.Now.TimeOfDay.TotalSeconds;
                ItemsContainer?.RefreshItemList();
            }
        }

        private void OpenAddContainer(ClickEvent evt)
        {
            AddNewEditorWindow.ShowWindow(ItemsContainer);
        }

        private void DeleteSelectedItems(ClickEvent evt)
        {
            if (EditorUtility.DisplayDialog("Delete Selected Items",
                    "Are you sure you want to delete the selected items?",
                    "Delete", "Cancel"))
            {
                ItemsContainer.DeleteSelected();
            }
        }
    }
}