using _Game.Scripts.Helpers;

namespace _Game.Scripts.Extensions.Button
{
    public class RegisterableButton : UnityEngine.UI.Button
    {
        protected override void Awake()
        {
            UIHelper.RegisterButton(this);
            base.Awake();
        }
    }
}