﻿using System;
using System.Collections.Generic;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Stack.Interface;
using _Game.Scripts.View.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.View.Gameplay
{
    public class GameplayView : MonoBehaviour, IView
    {
        public event Action<IView> ViewShowed;
        public event Action<IView> ViewHided;
        public event Action TestMyStackClicked;
        public event Action EarthQuakeClicked;
        public event Action ResetClicked;

        [SerializeField] private Button _testMyStackButton;
        [SerializeField] private Button _earthQuakeButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private FocusButton _focusButton;
        [SerializeField] private RectTransform _focusButtonParent;

        public List<FocusButton> FocusButtons { get; private set; }

        public void Init()
        {
            FocusButtons = new();
            _testMyStackButton.onClick.AddListener(() => TestMyStackClicked?.Invoke());
            _earthQuakeButton.onClick.AddListener(() => EarthQuakeClicked?.Invoke());
            _resetButton.onClick.AddListener(() => ResetClicked?.Invoke());
            GameController.Instance.GetController<IStackController>().SubscribeToInitialize(OnInitialized);
        }

        private void OnInitialized()
        {
            var stacks = GameController.Instance.GetController<IStackController>().Stacks;
            for (var i = 0; i < stacks.Count; i++)
            {
                var stack = stacks[i];
                var focusButton = Instantiate(_focusButton, _focusButtonParent);
                focusButton.AddListener(i, stack.ID);
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