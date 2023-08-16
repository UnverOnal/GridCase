using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GridSystem
{
    public class Grid
    {
        private readonly int _initialSize;

        private readonly float _distance;

        private readonly ObjectPool<Node> _nodePool;
        
        private readonly Dictionary<Coordinate, Node> _gridNodes;

        private readonly Vector2 _originPoint;
        private Vector2 _cellBounds;

        private readonly GameObject _cellPrefab;

        private readonly Transform _gridParent;

        public Grid(GridData gridData, Transform gridParent)
        {
            _initialSize = gridData.initialSize;
            _distance = gridData.distance;
            
            _nodePool = new ObjectPool<Node>();

            _gridNodes = new Dictionary<Coordinate, Node>();

            _originPoint = GetOrigin();

            _cellPrefab = gridData.cellPrefab;

            _gridParent = gridParent;
        }

        public void CreateGrid()
        {
            var size = CalculateSize();
            for (int i = 0; i < _initialSize; i++)
            {
                for (int j = 0; j < _initialSize; j++)
                { 
                    var coordinate = new Coordinate
                    {
                        Row = i,
                        Column = j
                    };
                    
                    var node = SetNode(coordinate, size);
                    _gridNodes.Add(coordinate, node);
                }
            }
        }

        private Node SetNode(Coordinate coordinate, float size)
        {
            var node = _nodePool.GetObject();
            var cellObject = node.GameObject ? node.GameObject : Object.Instantiate(_cellPrefab, parent:_gridParent);
            
            node.Set(coordinate, cellObject);
            node.SetSize(size);
            node.SetPosition(_originPoint, _distance);

            return node;
        }
        
        private Node GetNode(Coordinate coordinate)
        {
            if (_gridNodes.TryGetValue(coordinate, out Node value))
                return value;

            throw new ArgumentNullException(nameof(coordinate), "Node not found for the given coordinate.");
        }

        //Returns a starting or origin point 
        private Vector2 GetOrigin()
        {
            var screenOriginPoint = new Vector2(0f, Screen.height);
            var originPoint = Camera.main.ScreenToWorldPoint(screenOriginPoint);

            originPoint = (Vector2)originPoint;

            return originPoint;
        }

        //Calculates, based on screen size and distance that should be in between cells
        private float CalculateSize()
        {
            var cellSprite = _cellPrefab.GetComponent<SpriteRenderer>().sprite;
            var pixelPerUnit = cellSprite.pixelsPerUnit;
            
            var totalDistance = (_initialSize + 1) * _distance;
            var convertedScreenWidth = Screen.width / pixelPerUnit;
            var width = (convertedScreenWidth - totalDistance) / _initialSize;

            return width;
        }
    }
}