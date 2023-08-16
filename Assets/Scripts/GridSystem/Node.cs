using UnityEngine;

namespace GridSystem
{
    public class Node
    {
        public GameObject GameObject { get; private set; }

        private Coordinate _coordinate;

        private float _originalWidth;
        private float _size;

        private Vector3 _originalScale;

        public void Set(Coordinate coordinate, GameObject gameObject)
        {
            _coordinate = coordinate;

            GameObject = gameObject;

            _originalWidth = GameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            _size = _originalWidth;

            _originalScale = GameObject.transform.localScale;
        }

        public void SetPosition(Vector2 offset, float distance)
        {
            var x = _coordinate.Row * _size 
                    + (_coordinate.Row + 1) * distance
                    + _size / 2f;
            var y = _coordinate.Column * _size
                    + (_coordinate.Column + 1) * distance
                    + _size / 2f;
            var position = new Vector3(x, -y) + (Vector3)offset;
            
            GameObject.transform.position = position;
        }

        //Adjusts scale based on size given
        public void SetSize(float size)
        {
            _size = size;

            GameObject.transform.localScale = _originalScale * (_size / _originalWidth);
        }
    }

    public struct Coordinate
    {
        public int Row;
        public int Column;
    }
}