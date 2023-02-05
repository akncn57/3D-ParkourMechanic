using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.ParkourSystem
{
    public class EnvironmentScanner : MonoBehaviour
    {
        [SerializeField] private Vector3 forwardRayOffset;
        [SerializeField] private float forwardRayLength;
        [SerializeField] private float heightRayLength;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private Color rayFreeColor;
        [SerializeField] private Color rayHitColor;
    
        public ObstacleHitData ObstacleCheck()
        {
            // Get position.
            var position = transform;
            var forwardTransform = position.forward;
            
            // Create new hit data.
            var hitData = new ObstacleHitData();
            
            // Find player's forward origin transform.
            var forwardOrigin = position.position + forwardRayOffset;
        
            // If raycast hitting target layer, fill the data.
            hitData.ForwardHitFound = Physics.Raycast(
                forwardOrigin, 
                forwardTransform,
                out hitData.ForwardHit,
                forwardRayLength,
                obstacleLayer
                );
            
            // Draw raycast.
            Debug.DrawRay(forwardOrigin, forwardTransform * forwardRayLength, (hitData.ForwardHitFound) ? rayHitColor : rayFreeColor);

            // If raycast hitting target layer, check hit height.
            if (hitData.ForwardHitFound)
            {
                // Find hit height origin position.
                var heightOrigin = hitData.ForwardHit.point + Vector3.up * heightRayLength;
                
                // If raycast hitting target layer, fill the data.
                hitData.HeightHitFound = Physics.Raycast(
                    heightOrigin, 
                    Vector3.down,
                    out hitData.HeightHit,
                    heightRayLength,
                    obstacleLayer
                    );
                
                // Draw raycast.
                Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitData.HeightHitFound) ? rayHitColor : rayFreeColor);
            }

            return hitData;
        }
    }

    public struct ObstacleHitData
    {
        public bool ForwardHitFound;
        public bool HeightHitFound;
        public RaycastHit ForwardHit;
        public RaycastHit HeightHit;
    }
}