using UnityEngine;
using UnityEngine.UI;
namespace BaseFramework.Runtime
{
    [RequireComponent(typeof(Image))]
    public sealed class LocalizationSprite : MonoBehaviour
    {
        [SerializeField]
        private string _key;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
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

        private async void Refresh()
        {
            if (string.IsNullOrEmpty(_key))
            {
                return;
            }
            Sprite sprite = await LocalizationMgr.GetInstance().GetSpriteAsync(_key);
            if (sprite != null)
            {
                _image.sprite = sprite;
            }
        }

        private void OnLanguageChanged(E_Language language)
        {
            Refresh();
        }

        public void SetKey(string key)
        {
            _key = key;
            Refresh();
        }
    }
}
