using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine
{
    /// <summary>
    /// Class that represents a certain behaviour
    /// </summary>
    internal interface IState
    {
        #region Events
        
        /// <summary>
        /// Event that gets called whenever this <see cref="IState"/> is being activated
        /// </summary>
        event EventHandler OnActivate;

        /// <summary>
        /// Event that gets called whenever this <see cref="IState"/> is being deactivated
        /// </summary>
        event EventHandler OnDeactivate;

        /// <summary>
        /// Event that gets called whenever this <see cref="IState"/> is being updated
        /// </summary>
        event EventHandler<double> OnUpdate;

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
