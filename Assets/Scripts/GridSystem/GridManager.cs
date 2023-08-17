using TMPro;
using Ui;
using Ui.ButtonSystem.ConcreteButton;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GridSystem
{
    public class GridManager
    {
        private readonly Grid _grid;
        
        private readonly Button _button;
        private readonly TMP_InputField _inputField;
    
        [Inject]
        public GridManager(GridData gridData, GridUiResources gridUiResources)
        {
            _grid = new Grid(gridData.distance, gridData.cellPrefab, new GameObject("GridCells").transform);

            _button = gridUiResources.rebuildButton;
            _inputField = gridUiResources.inputField;
            
            _button.onClick.AddListener(Rebuild);
            
            _grid.CreateGrid(gridData.initialSize);
        }

        private void Rebuild()
        {
            if (int.TryParse(_inputField.text, out var size))
            {
                _grid.Clear();
                _grid.CreateGrid(size);
            }
        }
    }
}
