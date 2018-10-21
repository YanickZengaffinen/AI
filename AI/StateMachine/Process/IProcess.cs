namespace AI.StateMachine.Process
{
    /// <summary>
    /// Interface 
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Start this <see cref="IProcess"/>
        /// Also does reset the <see cref="IProcess"/> first
        /// </summary>
        void Start();

        /// <summary>
        /// Updates this connection during the transition phase
        /// </summary>
        /// <param name="deltaTime"> The time that has passed since the last update </param>
        /// <returns> The current progress of the transition. 1.0 meaning the transition is completed. </returns>
        float Update(in double deltaTime);

        /// <summary>
        /// Stops this <see cref="IProcess"/> if it has started already.
        /// </summary>
        void Stop();

        /// <returns> True if the <see cref="IProcess"/> is currently considering itself active/started </returns>
        bool IsActive();

    }
}
