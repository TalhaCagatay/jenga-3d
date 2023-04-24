using System.Collections.Generic;
using _Game.Scripts.Extensions.Button;

namespace _Game.Scripts.Helpers
{
    public static class UIHelper
    {
        private static List<RegisterableButton> _buttons = new();

        public static void RegisterButton(RegisterableButton button)
        {
            if (_buttons.Contains(button))
            {
                Logger.Logger.LogWarning($"button:{button.name} already registered");
                return;
            }
            _buttons.Add(button);
        }

        public static void DisableButtons() => _buttons.ForEach(button => button.interactable = false);
        public static void EnableButtons() => _buttons.ForEach(button => button.interactable = true);
    }
}