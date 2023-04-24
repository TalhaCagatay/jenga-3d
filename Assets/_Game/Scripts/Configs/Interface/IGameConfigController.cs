using _Game.Scripts.Configs.GameModes;
using _Game.Scripts.Core.Interface;

namespace _Game.Scripts.Configs.Interface
{
    public interface IGameConfigController : IController
    {
        T GetConfig<T>() where T : ConfigBase;
    }
}