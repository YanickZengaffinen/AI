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

        //An IDictionary of all ITransitions that this IStateMachine has... classified by its input IState
        private IDictionary<IState, IList<ITransition>> transitions;

        //A LinkedList containing all IStates that are currently active
        private LinkedList<IState> activeStates;

        //A LinkedList containting all ITransitions that are currently transitioning
        private LinkedList<ITransition> activeTransitions; 
  
        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="startState"> The state that will be active once the <see cref="IStateMachine"/> is being started. </param>
        /// <param name="states"> All the states that the <see cref="IStateMachine"/> has. This should also include the <paramref name="startState"/></param>
        /// <param name="transitions"> All the <see cref="ITransition"/>s that exist between the <see cref="IState"/>s on this <see cref="IStateMachine"/></param>
        public StateMachine(IState startState, IList<IState> states, IDictionary<IState, IList<ITransition>> transitions)
        {
            this.states = states;
            this.transitions = transitions;
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
                IList<ITransition> transitionsForState = transitions[state]; //we only need to consider ITransitions that originate from active IStates
                foreach(ITransition transition in transitionsForState)
                {
                    if(transition.CanTransition() && !activeTransitions.Contains(transition))
                    {
                        activeTransitions.AddLast(transition);
                    }
                }
            }

            //update all active ITransitions
            foreach(ITransition transition in activeTransitions)
            {
                if(transition.UpdateTransition(deltaTime) >= 1.0f) //check if the transition has been completed
                {
                    OnTransitionCompleted(transition);
                }
            }
        }

        /// <summary>
        /// Call this method in order to finalize an <see cref="ITransition"/>
        /// This includes:
        /// Activating the target IState
        /// </summary>
        /// <param name="transition"> </param>
        protected void OnTransitionCompleted(ITransition transition)
        {
            activeStates.AddLast(transition.GetTargetState());
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
