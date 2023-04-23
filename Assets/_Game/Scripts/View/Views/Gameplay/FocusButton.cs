using System;
using TMPro;
using UnityEngine.UI;

namespace _Game.Scripts.View.Views.Gameplay
{
    public class FocusButton : Button
    {
        public event Action<int> StackFocusClicked;

        public TMP_Text FocusStackText;

        protected override void Reset()
        {
            FocusStackText = GetComponentInChildren<TMP_Text>();
            base.Reset();
        }

        public void AddListener(int focusID)
        {
            FocusStackText.text = $"Select Stack {focusID}";
            onClick.AddListener(() => StackFocusClicked?.Invoke(focusID));
        }
    }
}