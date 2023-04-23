using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Core.Base;
using _Game.Scripts.Core.Interface;
using _Game.Scripts.Game.Interface;
using _Game.Scripts.Helpers;
using _Game.Scripts.Initializations;
using _Game.Scripts.Jenga.Stack.Controller;
using UnityEngine;

namespace _Game.Scripts.Game.Controller
{
    public class GameController : BaseMonoController, IGameController
    {
        public static GameController Instance;
        
        [SerializeField] private StackController _stackController;
        [SerializeField] private InitStep _mainInitStep;

        private List<IController> _controllers;

        private void Awake()
        {
            Instance = this;
            _controllers = new();
            Init();
        }

        public override void Init()
        {
            var nonMonoControllers = AssemblyHelper.CreateInstancesOfNonMonoTypes<IController>().ToList();
            var monoControllers = transform.GetInstancesOfMonoTypes<IController>().ToList();

            _controllers.AddRange(nonMonoControllers);
            _controllers.AddRange(monoControllers);

            _mainInitStep.Initialized += OnInitialized;
            _mainInitStep.Run();
        }

        private void OnInitialized()
        {
            base.Init();
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