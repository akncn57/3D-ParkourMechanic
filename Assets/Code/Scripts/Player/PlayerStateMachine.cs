using Code.Scripts.StateMachine;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts.Player
{
    public class PlayerStateMachine : StateMachines
    {
        [Header("Player Properties")][Space]
        [Range(1, 10)]
        public float walkMovementSpeed;
        [Range(1, 10)]
        public float runMovementSpeed;
        [Range(1, 10)]
        public float crouchingMovementSpeed;
        [Range(0, 500)]
        public float playerRotationSpeed;
        [Range(1, 15)]
        public float rotationDumping;
        [Range(0, 1)]
        public float animationCrossFadeDuration;
        [Range(0, 1)]
        public float animationDumpTimeDuration;

        [Space][Space][Space]
    
        [Header("Other Components")][Space]
        public CharacterController characterController;
        public Animator animator;
        public InputReader inputReader;
        public ForceReceiver forceReceiver;
        public Camera mainCamera;

        [HideInInspector]
        public bool playerWalking;
        [HideInInspector]
        public bool playerRunning;
        [HideInInspector]
        public bool playerCrouching;
        [HideInInspector]
        public bool hasControl = true;

        private void Start()
        {
            hasControl = true;
            
            // Set default state.
            SwitchState(new PlayerFreeLookState(this));
        }
        
        /// <summary>
        /// Calculate move with camera's move and player input. 
        /// </summary>
        /// <returns></returns>
        public Vector3 CalculateMovement()
        {
            // Get z axis and right values and make normalize.
            var mainCameraTransform = mainCamera.transform;
            Vector3 forward = mainCameraTransform.forward;
            Vector3 right = mainCameraTransform.right;
            
            forward.y = 0f;
            right.y = 0f;
            
            forward.Normalize();
            right.Normalize();
            
            return forward * inputReader.MovementValue.y +
                   right * inputReader.MovementValue.x;
        }

        /// <summary>
        /// Set face to movement direction.
        /// </summary>
        /// <param name="movement">Get movement direction.</param>
        /// <param name="deltaTime">Get deltaTime because we need this for optimization.</param>
        public void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation, 
                Quaternion.LookRotation(movement), 
                deltaTime * rotationDumping
            );
        }

        public void SetHasControl(bool hasControl)
        {
            this.hasControl = hasControl;
            characterController.enabled = hasControl;
            if (!hasControl) animator.SetFloat("FreeLookMovementSpeed", 0);
        }
    }
}
