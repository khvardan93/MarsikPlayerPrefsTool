using UnityEngine;
using UnityEngine.UIElements;

namespace MarsikTools.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MainWindowItems", menuName = "ScriptableObjects/MainWindowItems")]
    public class MainWindowItems : ScriptableObject
    {
        [SerializeField] private VisualTreeAsset MainWindowAsset;
        [Space] 
        [SerializeField] private VisualTreeAsset DataItemAsset;
        [SerializeField] private VisualTreeAsset GroupHeaderAsset;
        [SerializeField] private VisualTreeAsset EmptyItemAsset;
        [Space] 
        [SerializeField] private bool RefreshStatus = true;
        [SerializeField] private float RefreshRate = 0.5f;

        public VisualElement GetMainWindow() => MainWindowAsset.Instantiate();

        public VisualElement GetDataItem() => DataItemAsset.Instantiate();

        public VisualElement GetGroupHeader() => GroupHeaderAsset.Instantiate();

        public VisualElement GetEmptyItem() => EmptyItemAsset.Instantiate();

        public bool GetRefreshStatus() => RefreshStatus;
        
        public float GetRefreshRate() => RefreshRate;
    }
}