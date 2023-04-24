using UnityEngine;

namespace _Game.Scripts.Configs.GameModes
{
    [CreateAssetMenu(fileName = "ResetConfig", menuName = "Jenga/Configs/ResetConfig", order = 0)]
    public class ResetConfig : ConfigBase
    {
        public float ResetDuration = 2f;
    }
}