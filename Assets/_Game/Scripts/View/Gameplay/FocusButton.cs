using System;
using TMPro;
using UnityEngine.UI;

namespace _Game.Scripts.View.Gameplay
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

        public void AddListener(int focusID, string title)
        {
            FocusStackText.text = $"Select {title}";
            onClick.AddListener(() => StackFocusClicked?.Invoke(focusID));
        }
    }
}