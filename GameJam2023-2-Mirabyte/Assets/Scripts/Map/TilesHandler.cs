using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map
{
    public static class TilesHandler
    {
        public static List<Tile> tiles = new List<Tile>();

        public static void Generate(Vector2 start, Vector2 end)
        {
            for (int i = (int)start.x; i <= (int)end.x; i++)
            {
                for (int j = (int)start.y; j >= (int)end.y; j--)
                {
                    tiles.Add(new Tile(new Vector2Int(i, j), Random.Range(0,100)> 70));
                }
            }
        }

        public static bool IsCurrentTileSnowy(Vector2Int position)
        {
            Tile tile = FindTile(position);
            if (tile == null)
            {
                return false;
            }
            return tile.IsSnowy;
        }

        public static bool IsCurrentTileCrowdy(Vector2Int position)
        {
            Tile tile = FindTile(position);
            if (tile == null)
            {
                return false;
            }
            return tile.HasCrowd;
        }

        public static Tile FindTile(Vector2Int position)
        {
            return tiles.FirstOrDefault(x => x.Coordinates == position);
        }

        public static void SetCrowd(Vector2Int position, bool hasCrowd)
        {
            Tile tile = FindTile(position);
            if (tile != null)
            {
                tile.HasCrowd = hasCrowd;
            }
        }
    }
}

