using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScanner : MonoBehaviour
{
    [SerializeField] private Vector3 forwardRayOffset;
    [SerializeField] private float forwardRayLength;
    [SerializeField] private float heightRayLength;
    [SerializeField] private LayerMask obstacleLayer;
    
    public ObstacleHitData ObstacleCheck()
    {
        var hitData = new ObstacleHitData();
        var forwardOrigin = transform.position + forwardRayOffset;
        
        hitData.ForwardHitFound = Physics.Raycast(forwardOrigin, transform.forward, out hitData.ForwardHit, forwardRayLength,
            obstacleLayer);
        Debug.DrawRay(forwardOrigin, transform.forward * forwardRayLength, (hitData.ForwardHitFound) ? Color.red : Color.white);

        // Height control.
        if (hitData.ForwardHitFound)
        {
            var heightOrigin = hitData.ForwardHit.point + Vector3.up * heightRayLength;
            hitData.HeightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.HeightHit, heightRayLength,
                obstacleLayer);
            Debug.DrawRay(heightOrigin, Vector3.down * heightRayLength, (hitData.HeightHitFound) ? Color.red : Color.white);
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
