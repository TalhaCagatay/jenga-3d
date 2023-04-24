using _Game.Scripts.Camera.Interface;
using _Game.Scripts.Configs;
using _Game.Scripts.Configs.Interface;
using _Game.Scripts.Core.Base;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Stack.Interface;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Camera.Controller
{
    public class CameraController : BaseMonoController, ICameraController
    {
        [SerializeField] private CinemachineVirtualCamera _camera;

        public UnityEngine.Camera Camera { get; private set; }
        
        private float _xRot;
        private float _yRot;
        private bool _canRotate;
        private IStackController _stackController;
        private GameConfig _gameConfig;

        public override void Init()
        {
            Camera = UnityEngine.Camera.main;
            _canRotate = true;
            _stackController = GameController.Instance.GetController<IStackController>(); 
            _stackController.SelectedStackChanged += OnSelectedStackChanged;
            _gameConfig = GameController.Instance.GetController<IGameConfigController>().GetConfig<GameConfig>();
            base.Init();
        }

        private void OnDestroy()
        {
            _stackController.SelectedStackChanged -= OnSelectedStackChanged;
        }

        private void OnSelectedStackChanged(IStack selectedStack)
        {
            _xRot = _camera.transform.rotation.eulerAngles.y;
            _yRot = _camera.transform.rotation.eulerAngles.x;
            SetCurrentTarget(selectedStack.Transform);
        }

        private void Update()
        {
            if (!_canRotate) return;
            
            if (Input.GetMouseButton(0))
            {
                _xRot += Input.GetAxis("Mouse X") * _gameConfig.CameraMovementSpeed;
                _yRot -= Input.GetAxis("Mouse Y") * _gameConfig.CameraMovementSpeed;
                _camera.transform.rotation = Quaternion.Euler(_yRot, _xRot, 0.0f);
                _camera.transform.position = _camera.transform.rotation * new Vector3(0.0f, 2.0f, -10.0f) + _stackController.SelectedStack.Transform.position;
            }
        }

        private void SetCurrentTarget(Transform target)
        {
            _canRotate = false;
            _camera.transform.DOKill(false);
            
            // transform.rotation = Quaternion.Euler(_yRot, _xRot, 0.0f);
            var targetPosition = _camera.transform.rotation * new Vector3(0.0f, 2.0f, -10.0f) + target.position;
            _camera.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.Linear).OnComplete(() => _canRotate = true);
        }

        public override void Dispose()
        {
            
        }
    }
}