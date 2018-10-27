using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GameLogic
{
    /// <summary>
    /// The 3 phases of a nine men's morris game
    /// </summary>
    public enum Phase
    {
        Placing, //players place men
        Moving, //players try to form mills
        Flying //one player is down to 3 men
    }
}
