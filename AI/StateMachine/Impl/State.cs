using System;

namespace AI.StateMachine
{
    public class State : IState
    {
        // Keep track of whether this State is currently considered active or not
        private bool isActive = false;

        public event EventHandler Activating;
        public event EventHandler Deactivating;
        public event EventHandler<double> Updating;

        private string name;

        public State(string name)
        {
            this.name = name;
        }

        public void Activate()
        {
            isActive = true;

            //call the OnStateActivated event to allow the user to add more logic to the activate function
            Activating?.Invoke(this, EventArgs.Empty);
        }

        public void Deactivate()
        {
            isActive = false;

            //call the OnStateDeactivated event to allow the user to add more logic to the deactivate function
            Deactivating?.Invoke(this, EventArgs.Empty);
        }

        public bool IsActive()
        {
            return isActive;
        }

        public void Update(in double deltaTime)
        {
            if (!isActive) //exit the update method early if the State isn't active
                return;

            //call the OnStateUpdated event to allow the user to add more logic to the update function
            Updating?.Invoke(this, deltaTime);
        }
    }
}
