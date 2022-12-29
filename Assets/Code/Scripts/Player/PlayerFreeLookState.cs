using UnityEngine;

namespace Code.Scripts.Player
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private readonly int _freeLookSpeedHash = Animator.StringToHash("FreeLookMovementSpeed");
        private readonly int _freeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

        public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine){}
    
        public override void OnEnter()
        {
            PlayerStateMachine.inputReader.CrouchEvent += PlayerCrouch;
            PlayerStateMachine.inputReader.RunEvent += PlayerRun;
        
            // Play free look blend tree animation.
            PlayerStateMachine.animator.CrossFadeInFixedTime(_freeLookBlendTreeHash, PlayerStateMachine.animationCrossFadeDuration);
        }

        public override void Tick(float deltaTime)
        {
            // Get movement direction.
            Vector3 movement = PlayerStateMachine.CalculateMovement();

            // Move player.
            Move(movement * (PlayerStateMachine.walkMovementSpeed), deltaTime);

            // If player stop, stop blend animation.
            if (PlayerStateMachine.inputReader.MovementValue == Vector2.zero)
            {
                PlayerStateMachine.animator.SetFloat(_freeLookSpeedHash, 0, PlayerStateMachine.animationDumpTimeDuration, deltaTime);
                return;
            }

            // Set free look speed for animation.
            PlayerStateMachine.animator.SetFloat(_freeLookSpeedHash, 1, PlayerStateMachine.animationDumpTimeDuration, deltaTime);

            // Player camera face movement.
            PlayerStateMachine.FaceMovementDirection(movement, deltaTime);
        }

        public override void OnExit()
        {
            PlayerStateMachine.inputReader.CrouchEvent -= PlayerCrouch;
            PlayerStateMachine.inputReader.RunEvent -= PlayerRun;
        }
        
        // Listening crouch input.
        private void PlayerCrouch()
        {
            PlayerStateMachine.SwitchState(new PlayerCrouchingState(PlayerStateMachine));
        }

        // Listening run input.
        private void PlayerRun()
        {
            PlayerStateMachine.SwitchState(new PlayerRunningState(PlayerStateMachine));
        }
    }
}
