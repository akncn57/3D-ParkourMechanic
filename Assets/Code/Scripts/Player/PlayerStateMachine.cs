using Code.Scripts.ParkourSystem;
using Code.Scripts.StateMachine;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Scripts.Player
{
    public class PlayerStateMachine : StateMachines
    {
        #region Inspector Fields

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
        public EnvironmentScanner environmentScanner;
        
        #endregion

        #region Public Fields

        public bool IsOnLedge { get; set; }

        #endregion

        #region Private Fields

        private bool _hasControl = true;
        
        #endregion
        
        #region Unity Life Cycles
        
        private void Start()
        {
            _hasControl = true;
            
            // Set default state.
            SwitchState(new PlayerFreeLookState(this));
        }
        
        #endregion
        
        #region Public Methods
        
        /// <summary>
        /// Calculate move with camera's move and player input. 
        /// </summary>
        /// <returns></returns>
        public Vector3 CalculateMovement()
        {
            // Get z axis and right values and make normalize.
            var mainCameraTransform = mainCamera.transform;
            var forward = mainCameraTransform.forward;
            var right = mainCameraTransform.right;
            
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
        
        /// <summary>
        /// Set player controller.
        /// </summary>
        /// <param name="hasControl">Get control value.</param>
        public void SetHasControl(bool hasControl)
        {
            _hasControl = hasControl;
            characterController.enabled = hasControl;
            if (!hasControl) animator.SetFloat("FreeLookMovementSpeed", 0);
        }
        
        #endregion
    }
}
