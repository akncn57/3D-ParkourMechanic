using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerCrouchingState : PlayerBaseState
    {
        public PlayerCrouchingState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}
        
        private readonly int _crouchingMovementSpeedHash = Animator.StringToHash("CrouchingMovementSpeed");
        private readonly int _crouchingBlendTreeHash = Animator.StringToHash("CrouchingBlendTree");

        public override void OnEnter()
        {
            PlayerStateMachine.inputReader.CrouchEvent += PlayerStand;

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
        }

        public override void OnExit()
        {
            PlayerStateMachine.inputReader.CrouchEvent -= PlayerStand;
        }

        // Listening crouch input for stand.
        private void PlayerStand()
        {
            PlayerStateMachine.SwitchState(new PlayerFreeLookState(PlayerStateMachine));
        }
    }
}
