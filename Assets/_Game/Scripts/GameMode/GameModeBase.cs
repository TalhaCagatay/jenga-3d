using _Game.Scripts.GameMode.Interface;

namespace _Game.Scripts.GameMode
{
    public abstract class GameModeBase : IGameMode
    {
        public abstract void Init();
        public abstract void ApplyEffect();
    }
}