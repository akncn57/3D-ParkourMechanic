using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    [SerializeField] private EnvironmentScanner environmentScanner;

    private void Update()
    {
        var hitData = environmentScanner.ObstacleCheck();
        
        if (hitData.forwardHitFound) Debug.Log("Obstacle found " + hitData.forwardHit.transform.name);
    }
}
