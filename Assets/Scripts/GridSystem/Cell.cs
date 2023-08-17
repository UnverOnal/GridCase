using UnityEngine;

namespace GridSystem
{
    public class Cell
    {
        public GameObject GameObject { get; private set; }

        private Coordinate _coordinate;

        private float _originalExtent;
        private float _extent;

        private Vector3 _originalScale;

        public void Set(Coordinate coordinate, GameObject gameObject)
        {
            _coordinate = coordinate;

            GameObject = gameObject;
            if(!GameObject.activeInHierarchy)GameObject.SetActive(true);

            _originalExtent = GameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
            _extent = _originalExtent;

            _originalScale = GameObject.transform.localScale;
        }

        public void SetPosition(Vector2 offset, float distance)
        {
            var x = _coordinate.Row * _extent 
                    + (_coordinate.Row + 1) * distance
                    + _extent / 2f;
            var y = _coordinate.Column * _extent
                    + (_coordinate.Column + 1) * distance
                    + _extent / 2f;
            var position = new Vector3(x, -y) + (Vector3)offset;
            
            GameObject.transform.position = position;
        }

        //Adjusts scale based on size given
        public void SetExtent(float extent)
        {
            _extent = extent;

            GameObject.transform.localScale = _originalScale * (_extent / _originalExtent);
        }

        public void Reset()
        {
            GameObject.transform.localScale = Vector3.one;
            GameObject.SetActive(false);
        }
    }

    public struct Coordinate
    {
        public int Row;
        public int Column;
    }
}