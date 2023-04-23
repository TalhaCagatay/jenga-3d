using _Game.Scripts.Game.Controller;
using _Game.Scripts.View.Interface;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "ViewControllerInitStep", menuName = "Jenga/InitModules/ViewControllerInitStep", order = 0)]
    public class ViewControllerInitStep : InitStep
    {
        private IViewController _viewController;
        
        protected override void InternalStep()
        {
            _viewController = GameController.Instance.GetController<IViewController>();
            _viewController.SubscribeToInitialize(OnInitialized);
            _viewController.Init();
        }

        private void OnInitialized()
        {
            FinalizeStep();
        }
    }
}