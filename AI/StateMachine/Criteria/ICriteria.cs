using System;
using System.Collections.Generic;
using System.Text;

namespace AI.StateMachine.Criteria
{
    /// <summary>
    /// Interfaces that allows the user to check if a certain criteria is fullfilled or not
    /// </summary>
    public interface ICriteria
    {
        
        /// <returns> Return true if the given <see cref="ICriteria"/> is fullfilled </returns>
        bool IsTrue();

    }
}
