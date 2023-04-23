using _Game.Scripts.Core.Interface;
using Cinemachine;

namespace _Game.Scripts.Camera.Interface
{
    public interface ICameraController : IController
    {
        UnityEngine.Camera Camera { get; }
    }
}