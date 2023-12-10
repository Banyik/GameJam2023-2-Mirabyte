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

        public GameObject NPC;

        public Tilemap tilemap;
        public Sprite[] sprites;
        List<GameObject> npcs = new List<GameObject>();
        private void Start()
        {
            height = 2f * camera.orthographicSize;
            width = height * camera.aspect;
            TilesHandler.Generate(start, end);
            for (int i = 0; i < zoneAmount; i++)
            {
                zones.Add(new Zone(new Vector2(start.x + (Mathf.Abs(start.x - end.x) / zoneAmount) * i, start.y), new Vector2(start.x + (Mathf.Abs(start.x - end.x) / zoneAmount) * (i + 1), end.y)));
            }
            foreach (var zone in zones)
            {
                RegenerateCrowd(zone);
            }
        }
        private void FixedUpdate()
        {
            float playerX = camera.GetComponentInParent<Transform>().position.x;
            int negativeX = (int)(playerX - (width / 2));
            int positiveX = (int)(playerX + (width / 2));
            foreach (var zone in zones)
            {
                if(positiveX + 3 < zone.Min.x || negativeX - 3 > zone.Max.x)
                {
                    RegenerateCrowd(zone);
                }
                else if(zone.IsRegenerated)
                {
                    zone.IsRegenerated = false;
                }
            }
        }

        void RegenerateCrowd(Zone zone)
        {
            if (zone.IsRegenerated)
            {
                return;
            }

            zone.IsRegenerated = true;
            foreach (var crowd in zone.Crowds)
            {
                foreach (var npc in new List<GameObject>(npcs))
                {
                    if(npc.transform.position == new Vector3(crowd.x, crowd.y, 0))
                    {
                        npcs.Remove(npc);
                        Destroy(npc);
                    }
                }
                //tilemap.SetTile(new Vector3Int(crowd.x, crowd.y, 0), null);
                TilesHandler.SetCrowd(new Vector2Int(crowd.x, crowd.y), false);
            }
            zone.Crowds.Clear();
            for (int i = (int)zone.Min.x; i <= (int)zone.Max.x; i++)
            {
                for (int j = (int)zone.Min.y; j >= (int)zone.Max.y; j--)
                {
                    if(Random.Range(0, 100) > 80 && !zone.Crowds.Contains(new Vector2Int(i,j)))
                    {
                        zone.Crowds.Add(new Vector2Int(i, j));
                        TilesHandler.SetCrowd(new Vector2Int(i, j), true);
                        var obj = Instantiate(NPC);
                        obj.transform.position = new Vector3(i - 0.5f, j, 0);
                        obj.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
                        obj.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 100) > 50;
                        npcs.Add(obj);
                    }
                }
            }
        }
    }
}

