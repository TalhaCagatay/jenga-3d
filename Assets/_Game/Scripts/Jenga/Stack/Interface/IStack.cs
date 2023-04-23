using System.Collections.Generic;
using _Game.Scripts.Jenga.Interface;
using UnityEngine;

namespace _Game.Scripts.Jenga.Stack.Interface
{
    public interface IStack
    {
        string ID { get; }
        Transform Transform { get; }
        List<IJenga> Jengas { get; }
        void Init(string id);
        void PlaceJenga(IJenga jengaToPlace);
    }
}