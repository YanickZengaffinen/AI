using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine
{
    /// <summary>
    /// Represents a connection between two <see cref="IState"/>s
    /// </summary>
    public interface ITransition
    {
        /// <summary>
        /// Event that gets called whenever the <see cref="ITransition"/> is being updated.
        /// </summary>
        event EventHandler<double> Updating;

        /// <summary>
        /// Event that gets called whenever the <see cref="ITransition"/> is being started.
        /// </summary>
        event EventHandler Starting;

        /// <summary>
        /// Event that gets called whenever the <see cref="ITransition"/> is being completed.
        /// </summary>
        event EventHandler Finishing;

        /// <summary>
        /// Event that gets called whenever the <see cref="ITransition"/> was running and is now being stopped.
        /// </summary>
        event EventHandler Aborting;

        /// <summary>
        /// Start this <see cref="ITransition"/>
        /// </summary>
        void Start();

        /// <summary>
        /// Updates this connection during the transition phase
        /// </summary>
        /// <param name="deltaTime"> The time that has passed since the last update </param>
        /// <returns> The current progress of the transition. 1.0 meaning the transition is completed. </returns>
        float Update(in double deltaTime);

        /// <summary>
        /// Stops this <see cref="ITransition"/> if it has started already.
        /// </summary>
        void Stop();

        /// <returns> Can the <see cref="IStateMachine"/> move from the current <see cref="IState"/> to the target <see cref="IState"/> </returns>
        bool CanTransition();


        /// <returns> Get the the <see cref="IState"/> which this <see cref="ITransition"/> originates from</returns>
        IState GetState();

        /// <returns> Get the <see cref="IState"/> which this <see cref="ITransition"/> leads to </returns>
        IState GetTargetState();

    }
}
