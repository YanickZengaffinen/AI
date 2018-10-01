using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.Impl
{
    internal class State : IState
    {
        // Keep track of whether this State is currently considered active or not
        private bool isActive = false;

        public event EventHandler OnActivate;
        public event EventHandler OnDeactivate;
        public event EventHandler<double> OnUpdate;

        public void Activate()
        {
            isActive = true;

            if(OnActivate != null) //call the OnStateActivated event to allow the user to add more logic to the activate function
            {
                OnActivate.Invoke(this, EventArgs.Empty);
            }
        }

        public void Deactivate()
        {
            isActive = false;

            if(OnDeactivate != null) //call the OnStateDeactivated event to allow the user to add more logic to the deactivate function
            {
                OnDeactivate.Invoke(this, EventArgs.Empty);
            }
        }

        public bool IsActive()
        {
            return isActive;
        }

        public void Update(in double deltaTime)
        {
            if (!isActive) //exit the update method early if the State isn't active
                return;

            if(OnUpdate != null) //call the OnStateUpdated event to allow the user to add more logic to the update function
            {
                OnUpdate.Invoke(this, deltaTime);
            }
        }
    }
}
