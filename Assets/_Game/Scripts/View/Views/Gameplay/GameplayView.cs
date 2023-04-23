using System;
using System.Collections.Generic;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Stack.Interface;
using _Game.Scripts.View.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.View.Views.Gameplay
{
    public class GameplayView : MonoBehaviour, IView
    {
        public event Action<IView> ViewShowed;
        public event Action<IView> ViewHided;
        public event Action TestMyStackClicked;

        [SerializeField] private Button _testMyStackButton;
        [SerializeField] private FocusButton _focusButton;
        [SerializeField] private RectTransform _focusButtonParent;

        public List<FocusButton> FocusButtons { get; private set; }

        public void Init()
        {
            FocusButtons = new();
            _testMyStackButton.onClick.AddListener(() => TestMyStackClicked?.Invoke());
            GameController.Instance.GetController<IStackController>().SubscribeToInitialize(OnInitialized);
        }

        private void OnInitialized()
        {
            for (var i = 0; i < GameController.Instance.GetController<IStackController>().Stacks.Count; i++)
            {
                var focusButton = Instantiate(_focusButton, _focusButtonParent);
                focusButton.AddListener(i);
                FocusButtons.Add(focusButton);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            ViewShowed?.Invoke(this);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            ViewHided?.Invoke(this);
        }
    }
}