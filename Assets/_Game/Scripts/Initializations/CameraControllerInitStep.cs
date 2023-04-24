using _Game.Scripts.Camera.Interface;
using _Game.Scripts.Game.Controller;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "CameraControllerInitStep", menuName = "Jenga/InitModules/CameraControllerInitStep", order = 0)]
    public class CameraControllerInitStep : InitStep
    {
        private ICameraController _cameraController;
        
        protected override void InternalStep()
        {
            _cameraController = GameController.Instance.GetController<ICameraController>(); 
            _cameraController.SubscribeToInitialize(OnInitialized);
            _cameraController.Init();
        }

        private void OnInitialized()
        {
            FinalizeStep();
        }
    }
}