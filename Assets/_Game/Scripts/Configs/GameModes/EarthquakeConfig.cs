using UnityEngine;

namespace _Game.Scripts.Configs.GameModes
{
    [CreateAssetMenu(fileName = "EarthquakeConfig", menuName = "Jenga/Configs/EarthquakeConfig", order = 0)]
    public class EarthquakeConfig : ConfigBase
    {
        public float Duration = 2f;
        public float Strength = 0.25f;
        public int Vibrato = 2;
        public float Randomness = 1f;
    }
}