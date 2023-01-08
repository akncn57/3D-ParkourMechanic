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
        
        if (hitData.ForwardHitFound) Debug.Log("Obstacle found " + hitData.ForwardHit.transform.name);
    }
}
