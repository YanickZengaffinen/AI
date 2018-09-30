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
        /// Called whenenver the IState is being activated
        /// </summary>
        void OnActivate();

        /// <summary>
        /// Called whenever the IState is being deactivated
        /// </summary>
        void OnDeactivate();

        /// <summary>
        /// Called whenever this IState is active and being updated
        /// </summary>
        /// <param name="deltaTime"> The time that has passed since the last update </param>
        void OnUpdate(in double deltaTime);
        
        /// <returns> Is this IState currently considering itself active? </returns>
        bool IsActive();

    }
}
