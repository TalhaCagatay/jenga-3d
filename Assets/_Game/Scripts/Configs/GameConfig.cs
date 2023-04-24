using _Game.Scripts.Configs.GameModes;
using UnityEngine;

namespace _Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Jenga/Configs/GameConfig", order = 0)]
    public class GameConfig : ConfigBase
    {
        public float CameraMovementSpeed = 1f;
    }
}