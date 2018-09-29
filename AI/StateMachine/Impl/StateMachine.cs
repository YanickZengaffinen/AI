using AI.StateMachine.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.StateMachine.Impl
{
    /// <summary>
    /// Immutable State Machine which doesn't allow for any changes in structure
    /// </summary>
    internal class StateMachine : IStateMachine
    {
        //An IList of all IStates that will be active on the startup of this IStateMachine
        private IList<IState> startStates;

        //An IList of all IStates this IStateMachine has
        private IList<IState> states;

        //An IDictionary of all IConnections that this IStateMachine has... classified by its input IState
        private IDictionary<IState, IConnection> connections;

        //A LinkedList containing all IStates that are currently active
        private LinkedList<IState> activeStates;

        //A LinkedList containting all IConnections that are currently transitioning
        private LinkedList<IConnection> activeTransitions; 
  
        public StateMachine(IList<IState> startStates, IList<IState> states, IDictionary<IState, IConnection> connections)
        {
            this.states = states;
            this.connections = connections;
            this.startStates = startStates;
        }

        public void Start()
        {
            //Clean up
            activeStates.Clear();

            //Set up
            foreach(IState state in startStates)
            {
                activeStates.AddLast(state);

                state.OnActivate();
            }
        }

        public void Update(double deltaTime)
        {
            //TODO: the core logic of the IStateMachine
        }


        public IList<IState> GetStartStates()
        {
            return startStates;
        }

        public IList<IState> GetStates()
        {
            return states;
        }

    }
}
