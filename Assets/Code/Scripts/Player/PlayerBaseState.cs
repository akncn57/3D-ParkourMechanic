using Code.Scripts.StateMachine;
using UnityEngine;

namespace Code.Scripts.Player
{
    public abstract class PlayerBaseState : State
    {
        protected readonly PlayerStateMachine PlayerStateMachine;

        protected PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            PlayerStateMachine = playerStateMachine;
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
    }
}
