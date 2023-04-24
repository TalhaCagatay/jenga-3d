using _Game.Scripts.Configs.GameModes;
using _Game.Scripts.Configs.Interface;
using _Game.Scripts.Game.Controller;
using _Game.Scripts.Jenga.Glass;
using _Game.Scripts.Jenga.Stack.Interface;
using _Game.Scripts.View.Gameplay;
using _Game.Scripts.View.Interface;
using UnityEngine;

namespace _Game.Scripts.GameMode.Modes
{
    public class TestMyStackMode : GameModeBase
    {
        private Rigidbody _explosionRigidbody;
        private IStackController _stackController;
        
        public override void Init()
        {
            GameController.Instance.SubscribeToInitialize(OnGameControllerInitialized);
        }

        private void OnGameControllerInitialized()
        {
            GameController.Instance.GetController<IViewController>().GetView<GameplayView>().TestMyStackClicked += OnTestMyStackClicked;
        }

        private void OnTestMyStackClicked() => ApplyEffect();

        public override void ApplyEffect()
        {
            GameController.Instance.GetController<IStackController>().SelectedStack.Jengas.ForEach(jenga =>
            {
                if (jenga is JengaGlassBehaviour)
                    jenga.Transform.gameObject.SetActive(false);
                jenga.Rigidbody.isKinematic = false;
            });
        }
    }
}