using NineMensMorris.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NineMensMorris.GeneticAlgorithms
{
    /// <summary>
    /// Class that simulates a game and calculates a score for a player
    /// </summary>
    public class SimulatedGame : Game
    {
        //Store the players separately to destinguish between enemy and me
        public AIPlayer Me { get; }
        public AIPlayer Enemy { get; }

        #region Multipliers
        //How much should the different actions be weighed
        public double winMultiplier;
        private double abortMultiplier;
        private double killMultiplier;
        private double aliveMultiplier;

        //... those should propably have negative impact (--> be less than zero)
        private double loseMultiplier;
        private double flyMultiplier;
        private double moveMultiplier;

        //punishment for invalid moves
        private double falseKillMultiplier;
        private double falseMoveMultiplier;
        private double falseFlightMultiplier;
        private double falsePlacementMultiplier;

        #endregion

        private int maxMoveCnt; //how many moves until abortion, not including false moves

        //Count illegal moves/kills/placement/flights
        public int FalseMovesCnt { get; private set; }
        public int FalseKillsCnt { get; private set; }
        public int FalsePlacementCnt { get; private set; }
        public int FalseFlightsCnt { get; private set; }


        //Count legal moves/flights
        public int MoveCnt { get; private set; }
        public int FlightCnt { get; private set; }

        /// <summary>
        /// C'tor
        /// </summary>
        public SimulatedGame(AIPlayer enemy, AIPlayer me,
            in double winMultiplier, in double abortMultiplier, in double killMultiplier, in double aliveMultiplier, in double flyMultiplier, in double moveMultiplier,
            in double loseMultiplier, in double falseKillMultiplier, in double falseMoveMultiplier, in double falseFlightMultiplier, in double falsePlacementMultiplier,
            in int maxMoves) : base(enemy, me)
        {
            this.Enemy = enemy;
            this.Me = me;

            this.winMultiplier = winMultiplier;
            this.killMultiplier = killMultiplier;
            this.aliveMultiplier = aliveMultiplier;
            this.flyMultiplier = flyMultiplier;
            this.moveMultiplier = moveMultiplier;

            this.loseMultiplier = loseMultiplier;
            this.falseKillMultiplier = falseKillMultiplier;
            this.falseMoveMultiplier = falseMoveMultiplier;
            this.falseFlightMultiplier = falseFlightMultiplier;
            this.falsePlacementMultiplier = falsePlacementMultiplier;

            this.abortMultiplier = abortMultiplier;

            this.maxMoveCnt = maxMoves;
        }

        /// <summary>
        /// Calculates the score of a player
        /// </summary>
        public double Simulate(out SpeciesInfo info)
        {
            double rVal = 0;

            //reset values
            MoveCnt = 0;



            DoGameLoop();

            var enemyStatus = GetStatus(Enemy);
            var myStatus = GetStatus(Me);

            var menKilled = (enemyStatus.MenPlaced - enemyStatus.MenAlive);

            //calculate the score
            rVal += menKilled * killMultiplier; //kills
            rVal += myStatus.MenAlive * aliveMultiplier; //survivors

            rVal += flyMultiplier * FlightCnt; //flights
            rVal += moveMultiplier * MoveCnt; //moves

            //illegal actions
            rVal += falseKillMultiplier * FalseKillsCnt; //kills
            rVal += falseFlightMultiplier * FalseFlightsCnt; //flights
            rVal += falseMoveMultiplier * FalseMovesCnt; //moves
            rVal += falsePlacementMultiplier * FalsePlacementCnt; //placements

            if(IsAborted)
            {
                rVal += abortMultiplier;
            }
            else
            {
                if (!IsDead(Me)) //winner
                {
                    rVal += winMultiplier;
                }
                else //loser
                {
                    rVal += loseMultiplier;
                }
            }

            info = new SpeciesInfo(IsAborted, !IsDead(Me), myStatus.MenAlive, menKilled, FalseKillsCnt, MoveCnt, FalseMovesCnt, FlightCnt, FalseFlightsCnt, FalsePlacementCnt);

            return rVal;
        }

        //Override to count invalid placements
        public override bool Place(Placement placement)
        {
            if(placement.Player != Me)
                return base.Place(placement);

            if(!base.Place(placement)) //invalid placement
            {
                FalsePlacementCnt++;
                return false;
            }

            //no need to count valid placements as they are stored in the players status

            return true;
        }

        //Override to count moves/flights
        public override bool Move(Move move)
        {
            if (move.Player != Me)
                return base.Move(move);

            if(MoveCnt + FlightCnt >= maxMoveCnt) //check if the game needs to be aborted
            {
                IsAborted = true;
            }

            if(!base.Move(move)) //invalid move
            {
                if(CheckPhase(Me) == Phase.Flying)
                    FalseFlightsCnt++;
                else
                    FalseMovesCnt++;

                return false;
            }
            else
            {
                if (CheckPhase(Me) == Phase.Flying)
                    FlightCnt++;
                else
                    MoveCnt++;

                return true;
            }
        }

        //Override to count illegal kills
        public override bool Kill(Kill kill)
        {
            if(kill.Player != Me)
                return base.Kill(kill);

            if(!base.Kill(kill)) //invalid kill
            {
                FalseKillsCnt++;
                return false;
            }

            //no need to count kills as they can be calculated from the enemys status

            return true;
        }
    }
}
