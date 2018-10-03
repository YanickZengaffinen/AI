namespace AI.Process
{
    /// <summary>
    /// Class that provides a simple version of an <see cref="IProcess"/> which takes a certain amount of time to complete
    /// </summary>
    public class TimerProcess : IProcess
    {
        /// <summary>
        /// The time this timer must reach in order for the process to be completed
        /// </summary>
        public double Time { get; private set; }

        //The current internal time of the timer
        private double currentTime = 0;

        //The current active state of the timer
        private bool isActive = false;

        /// <summary>
        /// C'tor
        /// </summary>
        /// <param name="time">The time the timer must reach in order for the process to be completed </param>
        public TimerProcess(double time)
        {
            this.Time = time;
        }

        public void Start()
        {
            isActive = true;

            //reset the timer
            this.currentTime = 0;
        }


        public void Stop()
        {
            isActive = false;
        }

        public float Update(in double deltaTime)
        {
            currentTime += deltaTime;

            return (float)(currentTime / Time);
        }

        public bool IsActive()
        {
            return isActive;
        }
    }
}
