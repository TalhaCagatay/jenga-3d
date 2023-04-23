using System;

namespace _Game.Scripts.View.Interface
{
    public interface IView
    {
        public event Action<IView> ViewShowed; 
        public event Action<IView> ViewHided; 
        void Init();
        void Show();
        void Hide();
    }
}