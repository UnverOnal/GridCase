using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Grid
    {
        private readonly float _distance;

        private readonly ObjectPool<Cell> _cellPool;

        private readonly Dictionary<GameObject, Cell> _girdCells;

        private readonly Vector2 _originPoint;
        private Vector2 _cellBounds;

        private readonly GameObject _cellPrefab;

        private readonly Transform _gridParent;

        public Grid(float distance, GameObject cellPrefab, Transform gridParent)
        {
            _distance = distance;

            _cellPool = new ObjectPool<Cell>();

            _girdCells = new Dictionary<GameObject, Cell>();

            _originPoint = GetOrigin();

            _cellPrefab = cellPrefab;
            _gridParent = gridParent;
        }

        public void CreateGrid(int gridSize)
        {
            var extent = CalculateCellExtent(gridSize);
            for (var i = 0; i < gridSize; i++)
            for (var j = 0; j < gridSize; j++)
            {
                var coordinate = new Coordinate
                {
                    Row = i,
                    Column = j
                };

                var cell = SetCell(coordinate, extent);
                _girdCells.Add(cell.GameObject, cell);
            }
        }

        public void Clear()
        {
            foreach (var value in _girdCells.Values)
            {
                value.Reset();
                _cellPool.ReturnObject(value);
            }

            _girdCells.Clear();
        }

        public Cell GetCell(GameObject cellObject)
        {
            Cell cell = null;

            if (_girdCells.TryGetValue(cellObject, out var value))
                cell = value;

            return cell;
        }

        private Cell SetCell(Coordinate coordinate, float extent)
        {
            var cell = _cellPool.GetObject();
            var cellObject = cell.GameObject ? cell.GameObject : Object.Instantiate(_cellPrefab, _gridParent);

            cell.Set(coordinate, cellObject);
            cell.SetExtent(extent);
            cell.SetPosition(_originPoint, _distance);

            return cell;
        }

        //Returns a starting or origin point for starting grid from.
        private Vector2 GetOrigin()
        {
            var screenOriginPoint = new Vector2(0f, Screen.height);
            var originPoint = Camera.main.ScreenToWorldPoint(screenOriginPoint);

            originPoint = (Vector2)originPoint;

            return originPoint;
        }

        //Calculates, based on screen size and distance that should be in between cells
        private float CalculateCellExtent(int gridSize)
        {
            var cellSprite = _cellPrefab.GetComponent<SpriteRenderer>().sprite;
            var pixelPerUnit = cellSprite.pixelsPerUnit;

            var totalDistance = (gridSize + 1) * _distance;
            var convertedScreenWidth = Screen.width / pixelPerUnit;
            var extent = (convertedScreenWidth - totalDistance) / gridSize;

            return extent;
        }
    }
}