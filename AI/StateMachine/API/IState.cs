using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.API
{
    /*
     * Class that represents a behaviour
     */
    interface IState
    {
        /// <summary>
        /// Called whenenver the <see cref="IState"/> is being activated
        /// </summary>
        void OnActivate();

        /// <summary>
        /// Called whenever the <see cref="IState"/> is being deactivated
        /// </summary>
        void OnDeactivate();

        /// <summary>
        /// Called whenever this <see cref="IState"/> is active and being updated
        /// </summary>
        /// <param name="deltaTime"> The time that has passed since the last update </param>
        void OnUpdate(in double deltaTime);

        /// <returns> Is this <see cref="IState"/> currently considering itself active? </returns>
        bool IsActive();

    }
}
