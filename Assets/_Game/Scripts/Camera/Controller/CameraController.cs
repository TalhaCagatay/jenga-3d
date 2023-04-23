using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Stack.Interface;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 1.0f;
        
        private float _xRot;
        private float _yRot;
        private bool _canRotate;
        private IStackController _stackController;

        private void Start()
        {
            _canRotate = true;
            _stackController = GameController.Instance.GetController<IStackController>(); 
            _stackController.SelectedStackChanged += OnSelectedStackChanged;
        }

        private void OnDestroy()
        {
            _stackController.SelectedStackChanged -= OnSelectedStackChanged;
        }

        private void OnSelectedStackChanged(IStack selectedStack)
        {
            _xRot = transform.rotation.eulerAngles.y;
            _yRot = transform.rotation.eulerAngles.x;
            SetCurrentTarget(selectedStack.Transform);
        }

        private void Update()
        {
            if (!_canRotate) return;
            
            if (Input.GetMouseButton(0))
            {
                _xRot += Input.GetAxis("Mouse X") * _rotateSpeed;
                _yRot -= Input.GetAxis("Mouse Y") * _rotateSpeed;
                transform.rotation = Quaternion.Euler(_yRot, _xRot, 0.0f);
                transform.position = transform.rotation * new Vector3(0.0f, 2.0f, -10.0f) + _stackController.SelectedStack.Transform.position;
            }
        }

        private void SetCurrentTarget(Transform target)
        {
            _canRotate = false;
            transform.DOKill(false);
            
            // transform.rotation = Quaternion.Euler(_yRot, _xRot, 0.0f);
            var targetPosition = transform.rotation * new Vector3(0.0f, 2.0f, -10.0f) + target.position;
            transform.DOMove(targetPosition, 0.5f).SetEase(Ease.Linear).OnComplete(() => _canRotate = true);
        }
    }
}