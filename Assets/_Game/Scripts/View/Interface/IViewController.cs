using _Game.Scripts.Core.Interface;

namespace _Game.Scripts.View.Interface
{
    public interface IViewController : IController
    {
        T GetView<T>() where T : class, IView;
    }
}