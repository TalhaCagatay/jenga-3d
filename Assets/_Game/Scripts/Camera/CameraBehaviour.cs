using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Stack;
using _Game.Scripts.Jenga.Stack.Interface;
using UnityEngine;

namespace _Game.Scripts.Camera
{
    public class CameraBehaviour : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 1.0f;
        
        private int _defaultTargetIndex = 0;
        private Transform[] _targets;
        private int _currentTargetIndex;
        private Transform _currentTarget;
        private float _xRot;
        private float _yRot;

        private void Start()
        {
            GameController.Instance.GetController<IStackController>().StacksPrepared += OnStacksPrepared;
        }

        private void OnDestroy()
        {
            GameController.Instance.GetController<IStackController>().StacksPrepared -= OnStacksPrepared;
        }

        private void OnStacksPrepared(List<StackBehaviour> stacks)
        {
            _targets = stacks.Select(stack => stack.transform).ToArray();
            
            _xRot = transform.rotation.eulerAngles.y;
            _yRot = transform.rotation.eulerAngles.x;
            _currentTargetIndex = _defaultTargetIndex;
            SetCurrentTarget();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _xRot += Input.GetAxis("Mouse X") * _rotateSpeed;
                _yRot -= Input.GetAxis("Mouse Y") * _rotateSpeed;
                transform.rotation = Quaternion.Euler(_yRot, _xRot, 0.0f);
                transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -10.0f) + _currentTarget.position;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _currentTargetIndex = 0;
                SetCurrentTarget();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _currentTargetIndex = 1;
                SetCurrentTarget();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _currentTargetIndex = 2;
                SetCurrentTarget();
            }
        }

        private void SetCurrentTarget()
        {
            _currentTarget = _targets[_currentTargetIndex];
            transform.rotation = Quaternion.Euler(_yRot, _xRot, 0.0f);
            transform.position = transform.rotation * new Vector3(0.0f, 0.0f, -10.0f) + _currentTarget.position;
            transform.LookAt(_currentTarget);
        }
    }
}