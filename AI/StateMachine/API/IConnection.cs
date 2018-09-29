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
        /// Updates this connection. (= check if the statemachine should move to the next connection)
        /// </summary>
        /// <param name="deltaTime"></param>
        void Update(float deltaTime);

    }
}
