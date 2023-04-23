using _Game.Scripts.Game.Controller;
using _Game.Scripts.GameMode.Interface;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "GameModeInitStep", menuName = "Jenga/InitModules/GameModeInitStep", order = 0)]
    public class GameModeInitStep : InitStep
    {
        private IGameModeController _gameModeController; 
        
        protected override void InternalStep()
        {
            _gameModeController = GameController.Instance.GetController<IGameModeController>(); 
            _gameModeController.SubscribeToInitialize(OnInitialized);
            _gameModeController.Init();
        }

        private void OnInitialized()
        {
            FinalizeStep();
        }
    }
}