using _Game.Scripts.Core.Interface;

namespace _Game.Scripts.Core.Base
{
    public abstract class BaseController : IController
    {
        public abstract void Init();
        public abstract void Dispose();
    }
}