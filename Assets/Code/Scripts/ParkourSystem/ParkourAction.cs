using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour System/New Parkour Action")]
public class ParkourAction : ScriptableObject
{
    public Quaternion TargetRotation { get; set; }
    public string AnimationName => animationName;
    public bool RotateToObstacle => rotateToObstacle;
    
    [SerializeField] private string animationName;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private bool rotateToObstacle;

    public bool CheckIfPossible(ObstacleHitData hitData, Transform player)
    {
        float height = hitData.HeightHit.point.y - player.position.y;
        if (height < minHeight || height > maxHeight) return false;
        if (rotateToObstacle) TargetRotation = Quaternion.LookRotation(-hitData.ForwardHit.normal);
        return true;
    }
}
