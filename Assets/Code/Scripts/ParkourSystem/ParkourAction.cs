using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour System/New Parkour Action")]
public class ParkourAction : ScriptableObject
{
    public Quaternion TargetRotation { get; set; }
    public Vector3 MatchPosition { get; set; }
    public string AnimationName => animationName;
    public bool RotateToObstacle => rotateToObstacle;
    public float PostActionDelay => postActionDelay;
    public bool EnableTargetMatching => enableTargetMatching;
    public AvatarTarget MatchBodyPart => matchBodyPart;
    public float MatchStartTime => matchStartTime;
    public float MatchTargetTime => matchTargetTime;
    public Vector3 MatchPositionWeight => matchPositionWeight;
    
    [SerializeField] private string animationName;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private bool rotateToObstacle;
    [SerializeField] private float postActionDelay;
    
    [Header("Target Matching")]
    [SerializeField] private bool enableTargetMatching = true;
    [SerializeField] private AvatarTarget matchBodyPart;
    [SerializeField] private float matchStartTime;
    [SerializeField] private float matchTargetTime;
    [SerializeField] private Vector3 matchPositionWeight = new Vector3(0, 1, 0);

    public bool CheckIfPossible(ObstacleHitData hitData, Transform player)
    {
        float height = hitData.HeightHit.point.y - player.position.y;
        if (height < minHeight || height > maxHeight) return false;
        if (rotateToObstacle) TargetRotation = Quaternion.LookRotation(-hitData.ForwardHit.normal);
        if (enableTargetMatching) MatchPosition = hitData.HeightHit.point;
        return true;
    }
}
