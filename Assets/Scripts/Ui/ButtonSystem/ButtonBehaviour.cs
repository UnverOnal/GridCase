using UnityEngine.UI;

namespace Ui.ButtonSystem
{
    public abstract class ButtonBehaviour
    {
        private readonly Button _button;

        protected ButtonBehaviour(Button button)
        {
            _button = button;
        }

        public void LinkButton()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}