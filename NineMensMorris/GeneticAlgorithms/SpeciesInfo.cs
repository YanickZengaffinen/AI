using AI.NeuralNetworks.FeedForward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GeneticAlgorithms
{
    public class SpeciesInfo
    {
        public GeneticGameAI AI { get; set; }

        public bool Aborted { get; }
        public bool Win { get; }
        public int MenAlive { get; }
        public int MenKilled { get; }
        public int InvalidKills { get; }
        public int Moves { get; }
        public int InvalidMoves { get; }
        public int Flights { get; }
        public int InvalidFlights { get; }
        public int InvalidPlacements { get; }

        public SpeciesInfo(bool aborted, bool win, int menAlive, int menKilled, int invalidKills, int moves, int invalidMoves, int flights, int invalidFlights, int invalidPlacements)
        {
            Aborted = aborted;
            Win = win;
            MenAlive = menAlive;
            MenKilled = menKilled;
            InvalidKills = invalidKills;
            Moves = moves;
            InvalidMoves = invalidMoves;
            Flights = flights;
            InvalidFlights = invalidFlights;
            InvalidPlacements = invalidPlacements;
        }
    }
}
