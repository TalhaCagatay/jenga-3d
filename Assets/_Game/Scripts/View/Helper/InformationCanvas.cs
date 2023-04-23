using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.View.Helper
{
    public class InformationCanvas : MonoBehaviour
    {
        [SerializeField] private RectTransform _informationPopupRectTransform;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _informationText;

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Init(Vector3 position, string text)
        {
            _informationPopupRectTransform.position = position;
            _informationText.text = text;
        }
    }
}