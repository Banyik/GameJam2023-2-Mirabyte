using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        private void Start()
        {
            height = 2f * camera.orthographicSize;
            width = height * camera.aspect;
            for (int i = 0; i < zoneAmount; i++)
            {
                zones.Add(new Zone(new Vector2(start.x + (end.x / zoneAmount) * i, start.y), new Vector2((end.x / zoneAmount) * (i + 1), end.y)));
            }
        }
        private void Update()
        {
            float playerX = camera.GetComponentInParent<Transform>().position.x;
            float negativeX = playerX - (width / 2);
            float positiveX = playerX + (width / 2);
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

        }
    }
}

