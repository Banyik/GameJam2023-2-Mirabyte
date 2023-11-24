using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace Map
{
    public class CrowdHandler : MonoBehaviour
    {
        [SerializeField]
        public List<Zone> zones = new List<Zone>();
        public Vector2 start, end;
        public int zoneAmount;
        public Camera camera;
        float width;
        float height;

        public Tilemap tilemap;
        public UnityEngine.Tilemaps.Tile tile;
        private void Start()
        {
            height = 2f * camera.orthographicSize;
            width = height * camera.aspect;
            TilesHandler.Generate(start, end);
            for (int i = 0; i < zoneAmount; i++)
            {
                zones.Add(new Zone(new Vector2(start.x + (end.x / zoneAmount) * i, start.y), new Vector2((end.x / zoneAmount) * (i + 1), end.y)));
            }
            foreach (var zone in zones)
            {
                RegenerateCrowd(zone);
            }
        }
        private void Update()
        {
            float playerX = camera.GetComponentInParent<Transform>().position.x;
            int negativeX = (int)(playerX - (width / 2));
            int positiveX = (int)(playerX + (width / 2));
            foreach (var zone in zones)
            {
                if(!((negativeX <= zone.Max.x && negativeX > zone.Min.x) || 
                    (positiveX >= zone.Min.x && positiveX < zone.Max.x) || 
                    (negativeX <= zone.Min.x && positiveX >= zone.Max.x)))
                {
                    RegenerateCrowd(zone);
                }
            }
        }

        void RegenerateCrowd(Zone zone)
        {
            foreach (var crowd in zone.Crowds)
            {
                tilemap.SetTile(new Vector3Int(crowd.x, crowd.y, 0), null);
                TilesHandler.SetCrowd(new Vector2Int(crowd.x, crowd.y), false);
            }
            zone.Crowds.Clear();
            for (int i = (int)zone.Min.x; i <= (int)zone.Max.x; i++)
            {
                for (int j = (int)zone.Min.y; j >= (int)zone.Max.y; j--)
                {
                    if(Random.Range(0, 100) > 75)
                    {
                        zone.Crowds.Add(new Vector2Int(i, j));
                        TilesHandler.SetCrowd(new Vector2Int(i, j), true);
                        tilemap.SetTile(new Vector3Int(i, j, 0), tile);
                    }
                }
            }
        }
    }
}

