using System;

namespace AI.StateMachine.Events
{
    public class ProgressiveUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// The time that has passed since the last update
        /// </summary>
        public double DeltaTime { get; }

        /// <summary>
        /// The progess of whatever is being updated
        /// </summary>
        public float Progress { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public ProgressiveUpdateEventArgs(double deltaTime, float progress)
        {
            this.DeltaTime = deltaTime;
            this.Progress = progress;
        }

    }
}
