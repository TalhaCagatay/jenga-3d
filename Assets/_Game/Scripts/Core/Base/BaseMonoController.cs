using _Game.Scripts.Core.Interface;
using UnityEngine;

namespace _Game.Scripts.Core.Base
{
    public abstract class BaseMonoController : MonoBehaviour, IController, IMono
    {
        public abstract void Init();
        public abstract void Dispose();
    }
}