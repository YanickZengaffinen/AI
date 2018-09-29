using AI.StateMachine.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.Impl
{
    internal class StateMachine : IStateMachine
    {
        private IList<IState> states;


        public StateMachine()
        {

        }

        public IList<IState> GetStates()
        {
            return states;
        }

        public void Update(double deltaTime)
        {

        }
    }
}
