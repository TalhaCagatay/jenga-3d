using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Game.Scripts.Jenga.Glass;
using _Game.Scripts.Jenga.Interface;
using _Game.Scripts.Jenga.Stone;
using _Game.Scripts.Jenga.Wood;
using UnityEngine;

namespace _Game.Scripts.Jenga.Factory
{
    public static class JengaFactory
    {
        private static Dictionary<int, JengaBehaviour> _jengas;

        static JengaFactory()
        {
            _jengas = new();

            var allJengas = Resources.LoadAll<JengaBehaviour>("Jengas/");
            foreach (var jenga in allJengas)
            {
                if(jenga is JengaGlassBehaviour)
                    _jengas.Add(0, jenga);
                if(jenga is JengaWoodBehaviour)
                    _jengas.Add(1, jenga);
                if(jenga is JengaStoneBehaviour)
                    _jengas.Add(2, jenga);
            }
        }

        public static JengaBehaviour GetJengaByMastery(int mastery) => _jengas[mastery];
    }
}