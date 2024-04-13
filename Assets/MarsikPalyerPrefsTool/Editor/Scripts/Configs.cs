using PlayerPrefsUtil;

namespace MarsikTools
{
    public class Configs
    {
        public const bool LOGS_STATUS = false;
        
        public class Items
        {
            public const string ITEM_KEY_FIELD = "item_key_field";
            public const string ITEM_TYPE_FIELD = "item_type_field";
            public const string SELECT_TOGGLE = "data_item_select_toggle";
            public const string VALUE_FIELD = "item_value_field";
        }
        
        public class MainWindow
        {
            public const string MENU_PATH = "Window/MarsikTools/PlayerPrefsTool";

            public const string MENU_NAME = "Marsik PlayerPrefs Tool";
            public const string LIST_VIEW = "ItemsListView";

            public const string TOP_BUTTONS_OPEN_NEW_BUTTON = "top_buttons_add_new";
            public const string TOP_BUTTONS_DELETE_SELECTED_BUTTON = "top_buttons_delete_selected";
            public const string TOP_BUTTONS_RESET_BUTTON = "top_buttons_reset";
            public const string TOP_BUTTONS_REFRESH_BUTTON = "top_buttons_refresh";
        }
        
        public class NewItemWindow
        {
            public const string TITLE = "Add New Item";
            public const string CONTAINER_NAME = "AddNewContainer";
            public const string ADD_NEW_ITEM_BUTTON = "add_new_item_button";
            public const string CLOSE_NEW_ITEM_BUTTON = "close_add_container_button";
            public const string INPUT_FIELD_DATA_NAME = "new_item_value";
            public const string NEW_ITEM_KEY = "new_item_key";
            public const string NEW_ITEM_TYPE = "new_item_type";
        }
        
        public class FiltersEditor
        {
            public static Filters Filters;

            public const string SEARCH_FIELD = "filters_search_field";
            public const string TYPE_FIELD = "filters_type_field";
            public const string ORDER_FIELD = "filters_order_field";
            public const string SORT_FIELD = "filters_sort_field";
            public const string GROUP_FIELD = "filters_group_field";
            public const string AUTOREFRESH_FIELD = "filters_autorefresh_field";
        }
    }
}