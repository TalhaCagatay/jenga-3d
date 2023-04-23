using _Game.Scripts.Camera.Interface;
using _Game.Scripts.Game.Controller;
using UnityEngine;

namespace _Game.Scripts.View.Helper
{
    public static class HelperCanvas
    {
        private const string HELPER_CANVAS_PATH = "ViewHelpers/InformationCanvas";

        private static InformationCanvas _informationCanvas;
        private static ICameraController _cameraController;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            var informationCanvas = Resources.Load<InformationCanvas>(HELPER_CANVAS_PATH);
            _informationCanvas = Object.Instantiate(informationCanvas);
            _informationCanvas.gameObject.SetActive(false);
        }
        
        public static void ShowInformationPopup(Vector3 worldPosition, string information)
        {
            if (_cameraController == null)
                _cameraController = GameController.Instance.GetController<ICameraController>();

            var screenPosition = _cameraController.Camera.WorldToScreenPoint(worldPosition);
            _informationCanvas.Init(screenPosition, information);
            _informationCanvas.gameObject.SetActive(true);
        }
    }
}