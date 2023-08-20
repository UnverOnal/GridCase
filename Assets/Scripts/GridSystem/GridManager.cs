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

        private readonly GridData _gridData;

        private readonly MatchChecker _matchChecker;


        [Inject]
        public GridManager(GridData gridData, GridUiResources gridUiResources, InputManager inputManager)
        {
            _gridData = gridData;
            _grid = new Grid(gridData.distance, gridData.cellPrefab, new GameObject("GridCells").transform, gridData.baseResolution);

            _inputManager = inputManager;
            _inputManager.OnClick += SetCross;

            _inputField = gridUiResources.inputField;
            var button = gridUiResources.rebuildButton;
            button.onClick.AddListener(Rebuild);
            
            _grid.CreateGrid(gridData.initialSize);
            _matchChecker = new MatchChecker(gridUiResources.resultText);
        }

        private void Rebuild()
        {
            if (int.TryParse(_inputField.text, out var size))
            {
                _grid.Clear();
                _grid.CreateGrid(size);
                _matchChecker.Reset();
            }
        }

        private void SetCross(GameObject cellObject)
        {
            if (!cellObject)
                return;

            var cell = _grid.GetCell(cellObject);
            cell.SetSprite(_gridData.cross);

            CheckForAMatch(cell);
        }

        private void CheckForAMatch(Cell cell)
        {
            _matchChecker.AddCrossedCell(cell);
            _matchChecker.CheckForAMatch(cell);
        }

        public void Dispose()
        {
            _inputManager.OnClick -= SetCross;
        }
    }
}