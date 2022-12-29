using UnityEngine;

namespace Code.Scripts.StateMachine
{
    public abstract class StateMachines : MonoBehaviour
    {
        private State _currentState;

        private void Update()
        {
            // Start current state tick method.
            _currentState?.Tick(Time.deltaTime);
        }
    
        /// <summary>
        /// This method switch the state.
        /// </summary>
        /// <param name="newState">New state.</param>
        public void SwitchState(State newState)
        {
            // First, start exit method in current state.
            _currentState?.OnExit();
        
            _currentState = newState;
        
            // Start enter method in new state.
            _currentState?.OnEnter();
        }
    }   
}
