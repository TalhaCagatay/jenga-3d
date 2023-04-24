using _Game.Scripts.Core.Interface;
using UnityEngine;

namespace _Game.Scripts.View.Interface
{
    public interface IViewController : IController
    {
        Canvas WorldSpaceCanvas { get; }
        T GetView<T>() where T : class, IView;
        void ChangeView(IView viewToChange);
    }
}