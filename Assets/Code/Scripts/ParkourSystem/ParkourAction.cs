using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour System/New Parkour Action")]
public class ParkourAction : ScriptableObject
{
    public string AnimationName => animationName;
    
    [SerializeField] private string animationName;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    public bool CheckIfPossible(ObstacleHitData hitData, Transform player)
    {
        float height = hitData.HeightHit.point.y - player.position.y;
        if (height < minHeight || height > maxHeight) return false;
        return true;
    }
}
