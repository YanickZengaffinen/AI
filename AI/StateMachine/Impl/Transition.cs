﻿using AI.Criteria;
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


        public event EventHandler Finishing;
        public event EventHandler<double> Updating;
        public event EventHandler Starting;
        public event EventHandler Aborting;


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

            Starting?.Invoke(this, EventArgs.Empty);
        }
        
        public float Update(in double deltaTime)
        {
            if (!transitioning) //leave early if the transition isn't currently happening
                return 0;

            //calculate the average progress of the processes and return it
            float rVal = 0;

            foreach(var process in processes)
            {
                rVal += Math.Min(process.Update(deltaTime), 1.0f);
            }

            rVal /= processes.Count;

            //OnUpdate event
            Updating?.Invoke(this, deltaTime);

            //check if the transition has been finished
            if(rVal >= 1.0f)
            {
                Finishing?.Invoke(this, EventArgs.Empty);
            }

            return rVal;
        }

        public void Stop()
        {
            bool wasTransitioning = transitioning;

            transitioning = false;

            if(wasTransitioning) //only invoke the abort event if the transition was active
            {
                Aborting?.Invoke(this, EventArgs.Empty);
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
