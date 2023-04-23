using System;
using _Game.Scripts.Core.Interface;
using UnityEngine;

namespace _Game.Scripts.Core.Base
{
    public abstract class BaseMonoController : MonoBehaviour, IController, IMono
    {
        private event Action Initialized;

        public bool IsInitialized { get; private set; }

        public void SubscribeToInitialize(Action callback)
        {
            if (IsInitialized)
                callback?.Invoke();
            else
                Initialized += callback;
        }

        public virtual void Init()
        {
            IsInitialized = true;
            Initialized?.Invoke();
        }
        public abstract void Dispose();
    }
}