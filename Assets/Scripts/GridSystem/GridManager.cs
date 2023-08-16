using UnityEngine;
using VContainer;

namespace GridSystem
{
    public class GridManager
    {
        private readonly Grid _grid;
    
        [Inject]
        public GridManager(GridData gridData)
        {
            _grid = new Grid(gridData, new GameObject("GridCells").transform);
            
            _grid.CreateGrid();
        }
    }
}
