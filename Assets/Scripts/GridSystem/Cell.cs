using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Cell
    {
        public GameObject GameObject { get; private set; }
        
        public Coordinate Coordinate { get; private set; }
        
        public Coordinate[] NeighbourCoordinates { get; private set; }

        private SpriteRenderer _spriteRenderer;

        private float _originalExtent;
        private float _extent;

        private Vector3 _originalScale;

        private Sprite _originalSprite;

        private Connection _connection;

        public void Set(Coordinate coordinate, GameObject gameObject)
        {
            Coordinate = coordinate;

            GameObject = gameObject;
            if(!GameObject.activeInHierarchy)GameObject.SetActive(true);

            _spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
            _originalSprite = _spriteRenderer.sprite;
            _originalExtent = _originalSprite.bounds.size.x;
            _extent = _originalExtent;

            _originalScale = GameObject.transform.localScale;

            _connection ??= new Connection(this);

            NeighbourCoordinates = _connection.GetNeighbourCellCoordinates();
        }

        public void SetPosition(Vector2 offset, float distance)
        {
            var x = Coordinate.Row * _extent 
                    + (Coordinate.Row + 1) * distance
                    + _extent / 2f;
            var y = Coordinate.Column * _extent
                    + (Coordinate.Column + 1) * distance
                    + _extent / 2f;
            var position = new Vector3(x, -y) + (Vector3)offset;
            
            GameObject.transform.position = position;
        }

        //Adjusts scale based on extent given
        public void SetExtent(float extent)
        {
            _extent = extent;

            GameObject.transform.localScale = _originalScale * (_extent / _originalExtent);
        }

        public void Reset()
        {
            GameObject.transform.localScale = Vector3.one;
            SetSprite(_originalSprite);
            GameObject.SetActive(false);
            _connection.Reset();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void AddNeighbour(Cell neighbour)
        {
            _connection.AddNeighbour(neighbour);
        }

        //Removes all the connections of the cluster, that is, removes the cluster.
        public void RemoveConnections(ref Dictionary<Coordinate, Cell> cellsWithCross, Cell exception = null)
        {
            SetSprite(_originalSprite);
            _connection.Remove(ref cellsWithCross, exception);
        }

        public int GetConnectedCellCount(Cell exception = null)
        {
            return _connection.GetConnectedCellCount(exception);
        }
    }

    public struct Coordinate
    {
        public int Row;
        public int Column;
    }
}