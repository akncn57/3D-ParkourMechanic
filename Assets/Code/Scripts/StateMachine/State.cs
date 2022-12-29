namespace Code.Scripts.StateMachine
{
    public abstract class State
    {
        /// <summary>
        /// The first method that runs when the relevant state is entered.
        /// </summary>
        public abstract void OnEnter();
    
        /// <summary>
        /// The method that works continuously in the relevant case.
        /// <param name="deltaTime"></param>
        /// </summary>
        public abstract void Tick(float deltaTime);
    
        /// <summary>
        /// The first method that runs when the relevant state is exit.
        /// </summary>
        public abstract void OnExit();
    }
}