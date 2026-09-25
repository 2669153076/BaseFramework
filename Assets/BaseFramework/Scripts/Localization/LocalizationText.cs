using UnityEngine;
using UnityEngine.UI;
namespace BaseFramework.Runtime
{
    [RequireComponent(typeof(Text))]
    public sealed class LocalizationText : MonoBehaviour
    {
        [SerializeField]
        private string _key;
        [SerializeField]
        private string[] _args;
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            LocalizationMgr.GetInstance().OnLanguageChanged += OnLanguageChanged;
            Refresh();
        }

        private void OnDisable()
        {
            LocalizationMgr.GetInstance().OnLanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged(E_Language language)
        {
            Refresh();
        }

        public void Refresh()
        {
            if (_text == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(_key))
            {
                _text.text = string.Empty;
                return;
            }
            _text.text = LocalizationMgr.GetInstance().Format(_key, _args);
        }

        public void SetKey(string key)
        {
            _key = key;
            Refresh();
        }

        public void SetArgs(params string[] args)
        {
            _args = args;
            Refresh();
        }
    }
}
