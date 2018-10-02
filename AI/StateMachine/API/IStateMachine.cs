using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine
{
    /// <summary>
    /// This interfaces contains all the <see cref="IState"/>s and their <see cref="ITransition"/>s.
    /// </summary>
    public interface IStateMachine
    {
        /// <summary>
        /// Event that gets called whenever the <see cref="IStateMachine"/> gets started.
        /// </summary>
        event EventHandler Starting;

        /// <summary>
        /// Starts this <see cref="IStateMachine"/>
        /// </summary>
        void Start();

        /// <summary>
        /// Goes through all the states of the machine and updates them.
        /// </summary>
        /// <param name="timeDelta"> The time that has passed since the last update </param>
        void Update(in double deltaTime);


        /// <returns> An IList of all states the machine has </returns>
        IList<IState> GetStates();

        /// <returns> An IState that will be active once the IStateMachine is started </returns>
        IState GetStartState();

    }
}
