using AI.Criteria;
using AI.Process;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.Impl
{
    internal class Transition : ITransition
    {
        private IState state; //The original IState
        private IState targetState; //The IState this ITransition leads to
        private IList<ICriteria> transitionCriteria; //An IList of ICriterias that must be true in order for this ITransition to happen
        private IList<IProcess> processes; //An IList of IProcesses that will run once the transition started and must be completed in order for the transition to finish.        


        private bool transitioning = false;

        /// <summary>
        /// C'tor for a Transition
        /// </summary>
        /// <param name="criteria"> The criterias that must be true in order for this <see cref="ITransition"/> to happen. </param>
        /// <param name="processes"> The processes that must be completed in order for this <see cref="ITransition"/> to be finished. </param>
        public Transition(IList<ICriteria> criteria, IList<IProcess> processes)
        {
            this.transitionCriteria = criteria;
        }


        public event EventHandler OnFinish;
        public event EventHandler<double> OnUpdate;
        public event EventHandler OnStart;
        public event EventHandler OnAbort;


        /// <summary>
        /// Check all <see cref="ICriteria"/> that are required to be true in order for the transition to start.
        /// </summary>
        /// <returns> True if the transition can start. </returns>
        public bool CanTransition()
        {
            foreach(var criteria in transitionCriteria)
            {
                if (!criteria.IsTrue())
                    return false;
            }

            return true;
        }


        public void Start()
        {
            transitioning = true;

            //start all processes
            foreach(var process in processes)
            {
                process.Start();
            }

            if(OnStart != null)
            {
                OnStart.Invoke(this, EventArgs.Empty);
            }
        }
        
        public float Update(in double deltaTime)
        {
            //calculate the average progress of the processes and return it
            float rVal = 0;

            foreach(var process in processes)
            {
                rVal += process.Update(deltaTime);
            }

            return rVal / processes.Count;
        }

        public void Stop()
        {
            bool wasTransitioning = transitioning;

            transitioning = false;

            if(wasTransitioning && OnAbort != null) //only invoke the abort event if the transition was active
            {
                OnAbort.Invoke(this, EventArgs.Empty);
            }
        }


        public IState GetState()
        {
            return state;
        }

        public IState GetTargetState()
        {
            return targetState;
        }
        
    }
}
