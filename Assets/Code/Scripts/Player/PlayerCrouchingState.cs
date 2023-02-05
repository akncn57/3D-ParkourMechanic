using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerCrouchingState : PlayerBaseState
    {

        #region Constructor
        
        public PlayerCrouchingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}
        
        #endregion

        #region Private Fields

        private readonly int _crouchingMovementSpeedHash = Animator.StringToHash("CrouchingMovementSpeed");
        private readonly int _crouchingBlendTreeHash = Animator.StringToHash("CrouchingBlendTree");
        
        #endregion

        #region StateMachine Events

        public override void OnEnter()
        {
            // Play crouch blend tree animation.
            PlayerStateMachine.animator.CrossFadeInFixedTime(_crouchingBlendTreeHash, PlayerStateMachine.animationCrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            // Get movement direction.
            Vector3 movement = PlayerStateMachine.CalculateMovement();

            // Move player.
            Move(movement * PlayerStateMachine.crouchingMovementSpeed, deltaTime);

            // If player stop, stop blend animation.
            if (PlayerStateMachine.inputReader.MovementValue == Vector2.zero)
            {
                PlayerStateMachine.animator.SetFloat(_crouchingMovementSpeedHash, 0, PlayerStateMachine.animationDumpTimeDuration, deltaTime);
                return;
            }

            // Set crouching speed for animation.
            PlayerStateMachine.animator.SetFloat(_crouchingMovementSpeedHash, 1, PlayerStateMachine.animationDumpTimeDuration, deltaTime);
        
            // Player camera face movement.
            PlayerStateMachine.FaceMovementDirection(movement, deltaTime);
            
            if (!PlayerStateMachine.inputReader.IsCrouching) PlayerStateMachine.SwitchState(new PlayerFreeLookState(PlayerStateMachine));
        }

        public override void OnExit(){}
        
        #endregion
    }
}
