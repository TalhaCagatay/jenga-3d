using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Stack.Interface;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "StackControllerInitStep", menuName = "Jenga/InitModules/StackControllerInitStep", order = 0)]
    public class StackControllerInitStep : InitStep
    {
        private IStackController _stackController;
        
        protected override void InternalStep()
        {
            _stackController = GameController.Instance.GetController<IStackController>(); 
            _stackController.SubscribeToInitialize(OnInitialized);
            _stackController.Init();
        }

        private void OnInitialized()
        {
            FinalizeStep();
        }
    }
}