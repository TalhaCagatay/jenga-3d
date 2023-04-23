﻿using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "GameControllerInitStep", menuName = "Jenga/InitModules/GameControllerInitStep", order = 0)]
    public class GameControllerInitStep : InitStep
    {
        protected override void InternalStep()
        {
            Application.targetFrameRate = 60;
            FinalizeStep();
        }
    }
}