using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Configs.GameModes;
using _Game.Scripts.Configs.Interface;
using _Game.Scripts.Core.Base;
using UnityEngine;

namespace _Game.Scripts.Configs.Controller
{
    public class GameConfigController : BaseController, IGameConfigController
    {
        private List<ConfigBase> _configs;

        public override void Init()
        {
            _configs = new();
            _configs = Resources.LoadAll<ConfigBase>("Configs/").ToList();
            base.Init();
        }

        public override void Dispose()
        {
            
        }

        public T GetConfig<T>() where T : ConfigBase => _configs.First(config => config is T) as T;
    }
}