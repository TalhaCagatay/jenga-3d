using _Game.Scripts.Configs.Interface;
using _Game.Scripts.Game.Controller;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "GameConfigControllerInitStep", menuName = "Jenga/InitModules/GameConfigControllerInitStep", order = 0)]
    public class GameConfigControllerInitStep : InitStep
    {
        private IGameConfigController _gameConfigController;
        
        protected override void InternalStep()
        {
            _gameConfigController = GameController.Instance.GetController<IGameConfigController>();
            _gameConfigController.SubscribeToInitialize(OnInitialized);
            _gameConfigController.Init();
        }

        private void OnInitialized()
        {
            FinalizeStep();
        }
    }
}