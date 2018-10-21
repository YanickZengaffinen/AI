using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.StateMachine
{
    /// <summary>
    /// Immutable State Machine which doesn't allow for any changes in structure
    /// </summary>
    public class StateMachine : IStateMachine
    {
        //An IState that will be active on the startup of this IStateMachine
        private IState startState;

        //An IList of all IStates this IStateMachine has
        private IList<IState> states;

        //An IDictionary of all ITransitions that this IStateMachine has... classified by its input IState
        private IDictionary<IState, IList<ITransition>> transitions;

        //A LinkedList containing all IStates that are currently active
        private LinkedList<IState> activeStates = new LinkedList<IState>();

        //A LinkedList containting all ITransitions that are currently transitioning
        private LinkedList<ITransition> activeTransitions = new LinkedList<ITransition>();


        /// <summary>
        /// C'tor that already takes in a dictionary of transitions ordered by their origin
        /// </summary>
        /// <param name="startState"> The state that will be active once the <see cref="IStateMachine"/> is being started. </param>
        /// <param name="states"> All the states that the <see cref="IStateMachine"/> has. This should also include the <paramref name="startState"/></param>
        /// <param name="transitions"> All the <see cref="ITransition"/>s that exist between the <see cref="IState"/>s on this <see cref="IStateMachine"/></param>
        public StateMachine(IState startState, IList<IState> states, IDictionary<IState, IList<ITransition>> transitions) : this(startState, states)
        {
            this.transitions = transitions;
        }

        /// <summary>
        /// Advanced c'tor which automatically splits up a list of transitions into groups with the same origin
        /// </summary>
        public StateMachine(IState startState, IList<IState> states, IList<ITransition> transitions) : this(startState, states)
        {
            this.transitions = new Dictionary<IState, IList<ITransition>>();

            foreach(var state in states) //loop over states instead of transitions so states with no transitions will have an empty list assigned
            {
                this.transitions.Add(state, transitions.ToList().Where(x => x.GetOrigin() == state).ToList()); //collect all transitions that have this IState as their origin
            }
        }

        /// <summary>
        /// Helper c'tor as a base for more complex c'tors
        /// </summary>
        private StateMachine(IState startState, IList<IState> states)
        {
            this.startState = startState;
            this.states = states;
        }


        public event EventHandler Starting;


        public void Start()
        {
            //Clean up
            activeStates.Clear();

            //Set up
            activeStates.AddLast(startState);
            startState.Activate();

            //invoke the OnStart event which allows the user to add more functionality to the Start method
            Starting?.Invoke(this, EventArgs.Empty);
        }

        public void Update(in double deltaTime)
        {
            //TODO: the core logic of the IStateMachine

            //update all active IStates
            foreach(var state in activeStates)
            {
                state.Update(deltaTime);

                //check if the current IState should transition to another IState
                var transitionsForState = transitions[state]; //we only need to consider ITransitions that originate from active IStates
                foreach(var transition in transitionsForState)
                {
                    if(!activeTransitions.Contains(transition) && transition.CanTransition())
                    {
                        transition.Start();
                        activeTransitions.AddLast(transition);
                    }
                }
            }

            //update all active ITransitions
            ITransition currentTransition;
            for(int i = activeTransitions.Count - 1; i >= 0; --i)
            {
                currentTransition = activeTransitions.ElementAt(i);
                if(currentTransition.Update(deltaTime) >= 1.0f) //check if the transition has been completed
                {
                    CompleteTransition(currentTransition);
                }
            }
        }

        /// <summary>
        /// Call this method in order to finalize an <see cref="ITransition"/>
        /// This includes:
        /// Activating the target IState
        /// </summary>
        /// <param name="transition"> </param>
        protected void CompleteTransition(ITransition transition)
        {
            //remove this transition from the list of active transitions. No need to call abort as it will set itself to inactive
            activeTransitions.Remove(transition);

            //remove the old state from the list of active states and deactivate it
            var originState = transition.GetOrigin();
            originState.Deactivate();
            activeStates.Remove(originState);
            
            //add the target state to the list of active states and activate it
            var newlyActiveState = transition.GetTarget();
            activeStates.AddLast(newlyActiveState);
            newlyActiveState.Activate();
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
