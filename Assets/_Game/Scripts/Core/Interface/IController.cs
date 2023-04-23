using System;

namespace _Game.Scripts.Core.Interface
{
    public interface IController
    {
        bool IsInitialized { get; }
        void SubscribeToInitialize(Action callback);
        void Init();
        void Dispose();
    }
}