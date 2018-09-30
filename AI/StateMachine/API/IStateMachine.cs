﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.API
{
    /// <summary>
    /// This class contains all the <see cref="IState"/>s and their <see cref="ITransition"/>s.
    /// </summary>
    internal interface IStateMachine
    {
        /// <returns> An IList of all states the machine has </returns>
        IList<IState> GetStates();

        /// <returns> An IState that will be active once the IStateMachine is started </returns>
        IState GetStartState();

        /// <summary>
        /// Goes through all the states of the machine and updates them.
        /// </summary>
        /// <param name="timeDelta"> The time that has passed since the last update </param>
        void Update(in double deltaTime);

        /// <summary>
        /// Starts this <see cref="IStateMachine"/>
        /// </summary>
        void Start();
    }
}
