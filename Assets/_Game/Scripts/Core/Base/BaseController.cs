using System;
using _Game.Scripts.Core.Interface;

namespace _Game.Scripts.Core.Base
{
    public abstract class BaseController : IController
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
            Initialized?.Invoke();
        }
        public abstract void Dispose();
    }
}