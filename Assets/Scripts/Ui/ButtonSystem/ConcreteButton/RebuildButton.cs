using UnityEngine;
using UnityEngine.UI;

namespace Ui.ButtonSystem.ConcreteButton
{
    public class RebuildButton : ButtonBehaviour
    {
        public RebuildButton(Button button) : base(button){}
        
        protected override void OnClick()
        {
            Rebuild();
        }

        private void Rebuild()
        {
            Debug.Log("Rebuild");
        }
    }
}
