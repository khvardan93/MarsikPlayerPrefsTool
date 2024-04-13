using PlayerPrefsUtil;
using UnityEngine;
using UnityEngine.UIElements;

namespace MarsikTools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AddNewWindowItems", menuName = "ScriptableObjects/AddNewWindowItems")]
    public class AddNewWindowItems : ScriptableObject
    {
        [SerializeField] private VisualTreeAsset AddNewWindowAsset;
        [Space] [SerializeField] private VisualTreeAsset IntFieldAsset;
        [SerializeField] private VisualTreeAsset FloatFieldAsset;
        [SerializeField] private VisualTreeAsset StringFieldAsset;

        public VisualElement GetAddNewWindow()
        {
            return AddNewWindowAsset.Instantiate();
        }

        public VisualElement GetField(DataType type)
        {
            switch (type)
            {
                case DataType.Int:
                    return IntFieldAsset.Instantiate();
                case DataType.Float:
                    return FloatFieldAsset.Instantiate();
                case DataType.String:
                    return StringFieldAsset.Instantiate();
            }

            return null;
        }
    }
}