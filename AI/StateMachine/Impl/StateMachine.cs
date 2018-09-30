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
        //An IState that will be active on the startup of this IStateMachine
        private IState startState;

        //An IList of all IStates this IStateMachine has
        private IList<IState> states;

        //An IDictionary of all IConnections that this IStateMachine has... classified by its input IState
        private IDictionary<IState, IList<IConnection>> connections;

        //A LinkedList containing all IStates that are currently active
        private LinkedList<IState> activeStates;

        //A LinkedList containting all IConnections that are currently transitioning
        private LinkedList<IConnection> transitioningConnections; 
  
        public StateMachine(IState startState, IList<IState> states, IDictionary<IState, IList<IConnection>> connections)
        {
            this.states = states;
            this.connections = connections;
            this.startState = startState;
        }

        public void Start()
        {
            //Clean up
            activeStates.Clear();

            //Set up
            activeStates.AddLast(startState);
            startState.OnActivate();
        }

        public void Update(in double deltaTime)
        {
            //TODO: the core logic of the IStateMachine

            //update all active IStates
            foreach(IState state in activeStates)
            {
                state.OnUpdate(deltaTime);

                //check if the current IState should transition to another IState
                IList<IConnection> connectionsForState = connections[state]; //we only need to consider connections that originate from active states
                foreach(IConnection connection in connectionsForState)
                {
                    if(connection.CanTransition() && !transitioningConnections.Contains(connection))
                    {
                        transitioningConnections.AddLast(connection);
                    }
                }
            }

            //update all transitioning IConnections
            foreach(IConnection connection in transitioningConnections)
            {
                connection.UpdateTransition(deltaTime);
            }


        }

        protected void ActivateState()
        {

        }


        public IState GetStartState()
        {
            return startState;
        }

        public IList<IState> GetStates()
        {
            return states;
        }

    }
}
