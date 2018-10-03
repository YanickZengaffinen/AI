using AI.StateMachine;
using AI.StateMachine.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AI.StateMachine.Impl;
using AI.Process;
using AI.Criteria;

namespace AI_Test
{
    internal static class StateMachineUtil
    {

        /// <summary>
        /// Adds logging to a simple IState
        /// </summary>
        public static IState MakeDebugState(IState state)
        {
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
                Console.WriteLine(string.Format("Updating state {0}. DeltaTime: {1}", state, deltaTime));
            };

            return state;
        }


        /// <returns> An object of type ITransition that will log everything it does </returns>
        public static ITransition MakeDebugTransition(ITransition transition)
        {
            transition.Starting += delegate (object o, EventArgs ea)
            {
                Console.WriteLine("Starting transition " + transition);
            };

            transition.Updating += delegate (object o, ProgressiveUpdateEventArgs ea)
            {
                Console.WriteLine(string.Format("Updating transition {0}. Progress: {1}. DeltaTime: {2}", transition, ea.Progress, ea.DeltaTime));
            };

            transition.Finishing += delegate (object o, EventArgs ea)
            {
                Console.WriteLine("Finishing transition " + transition);
            };

            transition.Aborting += delegate (object o, EventArgs ea)
            {
                Console.WriteLine("Aborting transition " + transition);
            };

            return transition;
        }

    }
}
