using AI.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AI.StateMachine.Impl;

namespace AI_Test
{
    internal static class StateMachineUtil
    {

        /// <returns> An object of type IState which will log everything it does </returns>
        public static IState GetDebugState()
        {
            var state = new State();

            state.Activating += delegate (object o, EventArgs ea)
            {
                Console.WriteLine("Activating state " + state);
            };

            state.Deactivating += delegate (object o, EventArgs ea)
            {
                Console.WriteLine("Deactivating state " + state);
            };

            state.Updating += delegate (object o, double deltaTime)
            {
                Console.WriteLine("Updating state " + state);
            };

            return state;
        }

        public static ITransition GetDebugTransition(IState state, IState targetState)
        {
            var transition = new Transition();

            return transition;
        }

    }
}
