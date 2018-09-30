using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.API
{
    /*
     * Represents a transition between two states
     */
    internal interface ITransition
    {
        /// <returns> Get the the IState which this ITransition originates from</returns>
        IState GetState();

        /// <returns> Get the IState which this ITransition leads to </returns>
        IState GetTargetState();

        /// <summary>
        /// Updates this connection during the transition
        /// </summary>
        /// <param name="deltaTime"> The time that has passed since the last update </param>
        /// <returns> The current progress of the transition. 1.0 meaning the transition is completed. </returns>
        float UpdateTransition(in double deltaTime);

        /// <returns> Can the IStateMachine move from the current IState to the target IState </returns>
        bool CanTransition();

    }
}
