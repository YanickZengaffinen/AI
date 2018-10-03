using System;

namespace AI.StateMachine
{
    /// <summary>
    /// Class that represents a certain behaviour
    /// </summary>
    public interface IState
    {
        #region Events
        
        /// <summary>
        /// Event that gets called whenever this <see cref="IState"/> is being activated
        /// </summary>
        event EventHandler Activating;

        /// <summary>
        /// Event that gets called whenever this <see cref="IState"/> is being deactivated
        /// </summary>
        event EventHandler Deactivating;

        /// <summary>
        /// Event that gets called whenever this <see cref="IState"/> is being updated
        /// </summary>
        event EventHandler<double> Updating;

        #endregion


        /// <summary>
        /// Called whenenver the <see cref="IState"/> is being activated
        /// </summary>
        void Activate();

        /// <summary>
        /// Called whenever the <see cref="IState"/> is being deactivated
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Called whenever this <see cref="IState"/> is active and being updated
        /// </summary>
        /// <param name="deltaTime"> The time that has passed since the last update </param>
        void Update(in double deltaTime);

        /// <returns> Is this <see cref="IState"/> currently considering itself active? </returns>
        bool IsActive();

    }
}
