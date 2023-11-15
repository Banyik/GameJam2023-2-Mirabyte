using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public static class SnowyTilesHandler
    {
        public static List<Tile> tiles = new List<Tile>();

        public static void Generate()
        {
            for (int i = -50; i < 50; i++)
            {
                for (int j = -50; j < 50; j++)
                {
                    tiles.Add(new Tile(new Vector2Int(i, j), j > 0));
                }
            }
        }

        public static bool IsCurrentTileSnowy(Vector2Int position)
        {
            return tiles.First(x => x.Coordinates == position).IsSnowy;
        }
    }
}

