using AI.Criteria;
using AI.Process;
using AI.StateMachine;
using AI.StateMachine.Impl;
using AI_Test.API;
using System;
using System.Collections.Generic;

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
            var startState = new State("startState");
            allStates.Add(StateMachineUtil.MakeDebugState(startState));

            //state 2
            var state2 = new State("state2");
            allStates.Add(StateMachineUtil.MakeDebugState(state2));

            //transitions
            var transition = new Transition(startState, state2, new List<ICriteria>() { new ConsoleCriteria() }, new List<IProcess>() { new TimerProcess(2) });
            transitions.Add(StateMachineUtil.MakeDebugTransition(transition));

            var stateMachine = new StateMachine(startState, allStates, transitions);
            stateMachine.Start();

            //simulate the update loop
            DateTime lastUpdate = DateTime.Now;

            int loopCnt = 0;

            do
            {
                ConsoleUtil.WriteLine("Update Loop #" + loopCnt, ConsoleColor.Green);
                stateMachine.Update((DateTime.Now - lastUpdate).TotalSeconds);

                lastUpdate = DateTime.Now;
                loopCnt++;

                Console.WriteLine("Enter x to abort update loop");
            } while (!Console.ReadLine().Equals("x"));


        }
    }
}
