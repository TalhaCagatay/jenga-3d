using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Core.Base;
using _Game.Scripts.GameMode.Interface;
using _Game.Scripts.Helpers;

namespace _Game.Scripts.GameMode.Controller
{
    public class GameModeController : BaseController, IGameModeController
    {
        private List<IGameMode> _gameModes;

        public override void Init()
        {
            _gameModes = AssemblyHelper.CreateInstancesOfNonMonoTypes<IGameMode>().ToList();
            _gameModes.ForEach(mode => mode.Init());
            base.Init();
        }

        public override void Dispose()
        {
            
        }
    }
}