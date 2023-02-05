using UnityEngine;

namespace Code.Scripts.StateMachine
{
    public abstract class StateMachines : MonoBehaviour
    {
        public static State CurrentState { get; private set; }

        private void Update()
        {
            // Start current state tick method.
            CurrentState?.Tick(Time.deltaTime);
        }
    
        /// <summary>
        /// This method switch the state.
        /// </summary>
        /// <param name="newState">New state.</param>
        public void SwitchState(State newState)
        {
            // First, start exit method in current state.
            CurrentState?.OnExit();
        
            CurrentState = newState;
        
            // Start enter method in new state.
            CurrentState?.OnEnter();
        }
    }   
}
