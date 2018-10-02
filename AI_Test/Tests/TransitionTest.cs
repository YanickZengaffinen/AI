using AI.StateMachine;
using AI.StateMachine.Impl;
using AI_Test.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Test.Tests
{
    internal class TransitionTest : ATest
    {
        public TransitionTest(string name) : base(name)
        {
        }

        /// <summary>
        /// Creates a simple IStateMachine that should transition from it's start state to another state
        /// </summary>
        protected override void Execute()
        {
            var allStates = new List<IState>();
            var transitions = new List<ITransition>();

            //start state
            var startState = StateMachineUtil.GetDebugState();
            allStates.Add(startState);

            //state 2
            var state2 = StateMachineUtil.GetDebugState();
            allStates.Add(state2);

            //transitions
            var transition = StateMachineUtil.get

            new StateMachine(startState, allStates, null);
        }
    }
}
