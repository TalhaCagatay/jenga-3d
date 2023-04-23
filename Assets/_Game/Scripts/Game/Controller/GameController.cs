using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Core.Base;
using _Game.Scripts.Core.Interface;
using _Game.Scripts.Game.Interface;
using _Game.Scripts.Jenga.Stack.Controller;
using UnityEngine;

namespace _Game.Scripts.Game.Controller
{
    public class GameController : BaseMonoController, IGameController
    {
        public static GameController Instance;
        
        [SerializeField] private StackController _stackController;

        private List<IController> _controllers;

        private void Awake()
        {
            _controllers = new();
            Init();
            Instance = this;
        }

        public override void Init()
        {
            _stackController.Init();
            _controllers.Add(_stackController);
        }

        public T GetController<T>() where T : class, IController => _controllers.First(controller => controller is T) as T;
        
        private void OnDestroy()
        {
            Dispose();
        }

        public override void Dispose()
        {
            _stackController.Dispose();
        }
    }
}