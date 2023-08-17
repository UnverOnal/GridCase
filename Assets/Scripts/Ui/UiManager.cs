using Ui.ButtonSystem.ConcreteButton;
using VContainer;

namespace Ui
{
    public class UiManager
    {
        private GridUiResources _gridUiResources;
        
        [Inject]
        public UiManager(GridUiResources gridUiResources)
        {
            _gridUiResources = gridUiResources;
        }
    }
}
