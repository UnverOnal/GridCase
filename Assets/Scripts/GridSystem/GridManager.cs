using System;
using TMPro;
using Ui;
using UnityEngine;
using VContainer;

namespace GridSystem
{
    public class GridManager : IDisposable
    {
        private readonly Grid _grid;

        private readonly TMP_InputField _inputField;
        private readonly InputManager _inputManager;
    
        [Inject]
        public GridManager(GridData gridData, GridUiResources gridUiResources, InputManager inputManager)
        {
            _grid = new Grid(gridData.distance, gridData.cellPrefab, new GameObject("GridCells").transform);

            _inputManager = inputManager;
            _inputManager.OnClick += SetCross;

            _inputField = gridUiResources.inputField;
            var button = gridUiResources.rebuildButton;
            button.onClick.AddListener(Rebuild);
            
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

        private void SetCross(GameObject cell)
        {
            if(!cell)
                return;
            
            cell.transform.localScale *= 0.5f;
        }

        public void Dispose()
        {
            _inputManager.OnClick -= SetCross;
        }
    }
}
