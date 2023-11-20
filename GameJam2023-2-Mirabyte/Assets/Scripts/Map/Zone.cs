using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Map
{
    [System.Serializable]
    public class Zone
    {
        [SerializeField]
        Vector2 min, max;
        List<Vector2Int> crowds = new List<Vector2Int>();

        public Zone(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        public Vector2 Min { get => min; set => min = value; }
        public Vector2 Max { get => max; set => max = value; }
        public List<Vector2Int> Crowds { get => crowds; set => crowds = value; }
    }
}

