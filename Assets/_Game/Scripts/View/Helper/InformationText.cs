using TMPro;
using UnityEngine;

namespace _Game.Scripts.View.Helper
{
    public class InformationText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _informationText;
        public Vector3 LocalOffset;

        private Transform _target;

        public void SetParent(Transform parent) => transform.SetParent(parent);
        
        public void SetInformationText(string text) => _informationText.text = text;

        public void SetTarget(Transform transform) => _target = transform;

        private void Update()
        {
            if (_target == null) return;

            transform.position = _target.position + LocalOffset;
            transform.localPosition += LocalOffset;
            transform.rotation = _target.rotation;
        }
    }
}