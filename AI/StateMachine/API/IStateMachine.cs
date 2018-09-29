using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.API
{
    /*
     * This class contains all the states and their connections
     */
    internal interface IStateMachine
    {
        /// <returns> A list of all states the machine has </returns>
        IList<IState> GetStates();

        /// <summary>
        /// Goes through all the states of the machine and updates them.
        /// </summary>
        /// <param name="timeDelta"> The time that has passed since the last update </param>
        void Update(double deltaTime);

    }
}
