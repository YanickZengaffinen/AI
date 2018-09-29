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
        /// <returns> An IList of all states the machine has </returns>
        IList<IState> GetStates();

        /// <returns> An IList of all IStates that will be active once the IStateMachine is started </returns>
        IList<IState> GetStartStates();

        /// <summary>
        /// Goes through all the states of the machine and updates them.
        /// </summary>
        /// <param name="timeDelta"> The time that has passed since the last update </param>
        void Update(double deltaTime);

        /// <summary>
        /// Starts this IStateMachine
        /// </summary>
        void Start();
    }
}
