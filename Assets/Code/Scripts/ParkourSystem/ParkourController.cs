using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Player;
using UnityEngine;

namespace Code.Scripts.ParkourSystem
{
    public class ParkourController : MonoBehaviour
    {
        #region Inspector Fields
        
        [SerializeField] private List<ParkourAction> parkourActions;
        [SerializeField] private EnvironmentScanner environmentScanner;
        [SerializeField] private PlayerStateMachine playerStateMachine;
        
        #endregion
        
        #region Private Fields
        
        private bool _isJumping;
        private bool _inAction;
        
        #endregion
        
        #region Unity Life Cycles

        private void Awake()
        {
            playerStateMachine.inputReader.JumpEvent += InputReaderOnJumpEvent;
        }

        private void OnDisable()
        {
            playerStateMachine.inputReader.JumpEvent -= InputReaderOnJumpEvent;
        }

        private void Update()
        {
            // If player is jumping and didn't in parkour action.
            if (_isJumping && !_inAction)
            {
                var hitData = environmentScanner.ObstacleCheck();
                
                // If raycast hit obstacle.
                if (hitData.ForwardHitFound)
                {
                    // Check the hit object height among parkour actions.
                    foreach (var action in parkourActions)
                    {
                        // If parkour action possible to start, run parkour action coroutine.
                        if (action.CheckIfPossible(hitData, playerStateMachine.transform))
                        {
                            StartCoroutine(DoParkourAction(action));
                            break;
                        }
                    }
                }   
            }
        }
        
        #endregion
        
        #region Private Methods

        private IEnumerator DoParkourAction(ParkourAction action)
        {
            _inAction = true;
            
            // Close player control.
            playerStateMachine.SetHasControl(false);
            
            // Play parkour action animation.
            playerStateMachine.animator.CrossFade(
                action.AnimationName, 
                0.2f
                );
            
            yield return new WaitForEndOfFrame();
            
            // Get current animation state info.
            var animationState = playerStateMachine.animator.GetNextAnimatorStateInfo(0);
            
            if (!animationState.IsName(action.AnimationName)) Debug.LogError("Parkour animation is wrong!");

            // Start a timer for target rotation and match target.
            var timer = 0f;
            while (timer <= animationState.length)
            {
                timer += Time.deltaTime;
                if (action.RotateToObstacle) transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, 
                    action.TargetRotation, 
                    playerStateMachine.playerRotationSpeed * Time.deltaTime
                );
                
                if (action.EnableTargetMatching) MatchTarget(action);
            
                yield return null;
            }

            // Wait delay.
            yield return new WaitForSeconds(action.PostActionDelay);
        
            // Open player controller.
            playerStateMachine.SetHasControl(true);
            
            _inAction = false;
            _isJumping = false;
        }

        private void MatchTarget(ParkourAction action)
        {
            // If current animation has matching target, return.
            if (playerStateMachine.animator.isMatchingTarget) return;
        
            // Make a match target.
            playerStateMachine.animator.MatchTarget(
                action.MatchPosition,
                transform.rotation,
                action.MatchBodyPart,
                new MatchTargetWeightMask(action.MatchPositionWeight, 0),
                action.MatchStartTime, 
                action.MatchTargetTime
            );
        }
    
        private void InputReaderOnJumpEvent()
        {
            _isJumping = true;
        }
        
        #endregion
    }
}
