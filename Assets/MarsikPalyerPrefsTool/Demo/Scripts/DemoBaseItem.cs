using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MarsikTools.Demo
{
    public class DemoBaseItem : MonoBehaviour
    {
        [SerializeField] protected GameObject CreateButton;
        [SerializeField] protected GameObject EditObject;
        [SerializeField] protected InputField ValueField;

        [FormerlySerializedAs("Key")] [SerializeField] protected string PlayerPrefKey;
        
        protected virtual void Start()
        {
            if (PlayerPrefs.HasKey(PlayerPrefKey))
            {
                SetEdit();
            }
            else
            {
                SetCreate();
            }
            
            ValueField.onValueChanged.AddListener(OnChange);
        }

        protected virtual void OnDestroy()
        {
            ValueField.onValueChanged.RemoveAllListeners();
        }

        protected virtual void SetCreate()
        {
            CreateButton.SetActive(true);
            EditObject.SetActive(false);
        }
        
        protected virtual void SetEdit()
        {
            CreateButton.SetActive(false);
            EditObject.SetActive(true);
        }
        
        protected virtual void OnChange(string str)
        {
            
        }
        
        public virtual void OnCreate()
        {
            
        }

        public virtual void OnDelete()
        {
            PlayerPrefs.DeleteKey(PlayerPrefKey);
            SetCreate();
        }
    }
}