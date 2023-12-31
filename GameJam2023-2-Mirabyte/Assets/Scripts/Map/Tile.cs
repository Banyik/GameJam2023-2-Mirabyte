using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class Tile
    {
        Vector2Int coordinates;
        bool isSnowy;
        bool hasCrowd;
        bool hasNeighbourCrowd;
        public Tile(Vector2Int coordinates, bool isSnowy)
        {
            this.coordinates = coordinates;
            this.isSnowy = isSnowy;
        }

        public Vector2Int Coordinates { get => coordinates; set => coordinates = value; }
        public bool IsSnowy { get => isSnowy; set => isSnowy = value; }
        public bool HasCrowd { get => hasCrowd; set => hasCrowd = value; }
        public bool HasNeighbourCrowd { get => hasNeighbourCrowd; set => hasNeighbourCrowd = value; }
    }
}

