using UnityEngine;

namespace GridSystem
{
    [CreateAssetMenu(fileName = "GridData", menuName = "ScriptableObjects/GridData", order = 1)]
    public class GridData : ScriptableObject
    {
        public GameObject cellPrefab;
        public int initialSize;
        public float distance;
    }
}
