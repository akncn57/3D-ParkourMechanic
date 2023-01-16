using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerRunningState : PlayerBaseState
    {
        private readonly int _runningSpeedHash = Animator.StringToHash("RunningMovementSpeed");
        private readonly int _runningBlendTreeHash = Animator.StringToHash("RunningBlendTree");

        public PlayerRunningState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}

        public override void OnEnter()
        {
            PlayerStateMachine.inputReader.CrouchEvent += PlayerCrouch;
        
            // Play running blend tree animation.
            PlayerStateMachine.animator.CrossFadeInFixedTime(_runningBlendTreeHash, PlayerStateMachine.animationCrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            // Get movement direction.
            Vector3 movement = PlayerStateMachine.CalculateMovement();

            // Move player.
            Move(movement * PlayerStateMachine.runMovementSpeed, deltaTime);

            // If player stop, stop blend animation.
            if (PlayerStateMachine.inputReader.MovementValue == Vector2.zero)
            {
                PlayerStateMachine.animator.SetFloat(_runningSpeedHash, 0, PlayerStateMachine.animationDumpTimeDuration, deltaTime);
                return;
            }

            // Set crouching speed for animation.
            PlayerStateMachine.animator.SetFloat(_runningSpeedHash, 1, PlayerStateMachine.animationDumpTimeDuration, deltaTime);
        
            // Player camera face movement.
            PlayerStateMachine.FaceMovementDirection(movement, deltaTime);
        }

        public override void OnExit()
        {
            PlayerStateMachine.inputReader.CrouchEvent -= PlayerCrouch;
        }
    
        // Listening run input for walk.
        private void PlayerWalk()
        {
            PlayerStateMachine.SwitchState(new PlayerFreeLookState(PlayerStateMachine));
        }
    
        // Listening crouch input.
        private void PlayerCrouch()
        {
            PlayerStateMachine.SwitchState(new PlayerCrouchingState(PlayerStateMachine));
        }
    }
}
