using _Game.Scripts.Configs.GameModes;
using _Game.Scripts.Configs.Interface;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Helpers;
using _Game.Scripts.Jenga.Stack.Interface;
using _Game.Scripts.View.Gameplay;
using _Game.Scripts.View.Interface;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.GameMode.Modes
{
    public class EarthquakeMode : GameModeBase
    {
        private EarthquakeConfig _earthquakeConfig;
        private Transform _ground;
        
        public override void Init()
        {
            _ground = GameObject.FindWithTag("Ground").transform;
            GameController.Instance.SubscribeToInitialize(OnGameControllerInitialized);
        }

        private void OnGameControllerInitialized()
        {
            GameController.Instance.GetController<IViewController>().GetView<GameplayView>().EarthQuakeClicked += OnEarthQuakeClicked;
            _earthquakeConfig = GameController.Instance.GetController<IGameConfigController>().GetConfig<EarthquakeConfig>();
        }

        private void OnEarthQuakeClicked()
        {
            ApplyEffect();
        }

        public override void ApplyEffect()
        {
            UIHelper.DisableButtons();
            
            GameController.Instance.GetController<IStackController>().Stacks.ForEach(stack =>
            {
                stack.Jengas.ForEach(jenga => jenga.Rigidbody.isKinematic = false);
            });

            _ground.DOShakePosition(_earthquakeConfig.Duration, _earthquakeConfig.Strength, _earthquakeConfig.Vibrato,
                _earthquakeConfig.Randomness).SetLoops(2, LoopType.Yoyo).OnComplete(UIHelper.EnableButtons);
        }
    }
}