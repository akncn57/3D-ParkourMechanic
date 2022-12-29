using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Player
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine PlayerStateMachine;

        protected PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            this.PlayerStateMachine = playerStateMachine;
        }

        /// <summary>
        /// Move player method.
        /// </summary>
        /// <param name="motion">Get player motion for movement.</param>
        /// <param name="deltaTime">Get delta time.</param>
        protected void Move(Vector3 motion, float deltaTime)
        {
            PlayerStateMachine.characterController.Move((motion + PlayerStateMachine.forceReceiver.Movement) * deltaTime);
        }

        /// <summary>
        /// Move player method.
        /// </summary>
        /// <param name="deltaTime">Get delta time.</param>
        private void Move(float deltaTime)
        {
            Move(Vector3.zero, deltaTime);
        }
    }
}
