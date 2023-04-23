using System;
using System.Collections.Generic;
using _Game.Scripts.Core.Interface;
using _Game.Scripts.Jenga.Stack.Controller;

namespace _Game.Scripts.Jenga.Stack.Interface
{
    public interface IStackController : IController
    {
        event Action<List<IStack>> StacksPrepared;
        event Action<IStack> SelectedStackChanged;
        IStack SelectedStack { get; }
        List<IStack> Stacks { get; }
        void CreateJengas(List<JengaInformation> jengaInformations);
    }
}