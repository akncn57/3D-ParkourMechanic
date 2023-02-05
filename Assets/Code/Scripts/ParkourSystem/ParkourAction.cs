using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.ParkourSystem
{
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

        [SerializeField] private string obstacleTag;
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
            // If hit object's tag don't equal obstacle tag.
            if (!string.IsNullOrEmpty(obstacleTag) && !hitData.ForwardHit.transform.CompareTag(obstacleTag)) return false;
            
            // Find hit object height..
            var height = hitData.HeightHit.point.y - player.position.y;
            
            // If hit object height don't contain parkour action heights return.
            if (height < minHeight || height > maxHeight) return false;
            
            //  If player didn't look the obstacle, rotate the player to the obstacle.
            if (rotateToObstacle) TargetRotation = Quaternion.LookRotation(-hitData.ForwardHit.normal);
            
            // If target matching is enable, get obstacle point position.
            if (enableTargetMatching) MatchPosition = hitData.HeightHit.point;
            
            return true;
        }
    }
}
