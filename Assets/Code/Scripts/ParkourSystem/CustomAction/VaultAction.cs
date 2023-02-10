using UnityEngine;

namespace Code.Scripts.ParkourSystem.CustomAction
{
    [CreateAssetMenu(menuName = "Parkour System/Custom Actions/New Custom Parkour Action")]
    public class VaultAction : ParkourAction
    {
        public override bool CheckIfPossible(ObstacleHitData hitData, Transform player)
        {
            if (!base.CheckIfPossible(hitData, player)) return false;

            var hitPoint = hitData.ForwardHit.transform.InverseTransformPoint(hitData.ForwardHit.point);

            if (hitPoint.z < 0 && hitPoint.x < 0 || hitPoint.z > 0 && hitPoint.x > 0)
            {
                Mirror = true;
                matchBodyPart = AvatarTarget.RightHand;
            }
            else
            {
                Mirror = false;
                matchBodyPart = AvatarTarget.LeftHand;
            }

            return true;
        }
    }
}
