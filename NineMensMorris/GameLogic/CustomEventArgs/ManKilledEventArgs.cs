using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    public class ManKilledEventArgs : EventArgs
    {
        /// <summary>
        /// The player that killed the man
        /// </summary>
        public IPlayer Killer { get; }

        /// <summary>
        /// That position at which the man has been killed
        /// </summary>
        public Position Target { get; }

        /// <summary>
        /// C'tor
        /// </summary>
        public ManKilledEventArgs(IPlayer killer, Position target)
        {
            Killer = killer;
            Target = target;
        }
    }
}
