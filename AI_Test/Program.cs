using AI.StateMachine;
using AI.StateMachine.Impl;
using AI_Test.Tests;
using Autofac;
using System;

namespace AI_Test
{
    public class Program
    {
        private static IContainer DIScope { get; set; }  //used for dependency injection

        static void Main(string[] args)
        {
            SetupDependencyInjection();

            new TransitionTest("Transition Test").Run();

            //ask the user for permission to finish the program...
            Console.WriteLine("Press Enter to exit window");
            Console.ReadLine();

            DIScope.Dispose();
        }

        /// <summary>
        /// Sets up dependency injection for the AI_Test project
        /// </summary>
        private static void SetupDependencyInjection()
        {
            var builder = new ContainerBuilder();

            //subscribe for StateMachine
            builder.RegisterType<State>().As<IState>();
            builder.RegisterType<Transition>().As<ITransition>();
            builder.RegisterType<StateMachine>().As<IStateMachine>();

            DIScope = builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"> The type of the registered type </typeparam>
        /// <returns> An instance of the registerd type </returns>
        internal static T ResolveDIType<T>()
        {
            T rVal = default(T);

            using (var scope = DIScope.BeginLifetimeScope())
            {
                rVal = scope.Resolve<T>();
            }

            return rVal;
        }
    }
}
