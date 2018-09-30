using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.API
{
    /*
     * Represents a connection between two states
     */
    internal interface IConnection
    {
        /// <returns> Get the the IState which this IConnection originates from</returns>
        IState GetState();

        /// <returns> Get the IState which this IConnection leads to </returns>
        IState GetTargetStates();

        /// <summary>
        /// Updates this connection during the transition
        /// </summary>
        /// <param name="deltaTime"> The time that has passed since the last update </param>
        void UpdateTransition(in double deltaTime);

        /// <returns> Can the IStateMachine move from the current IState to the target IState </returns>
        bool CanTransition();

    }
}
