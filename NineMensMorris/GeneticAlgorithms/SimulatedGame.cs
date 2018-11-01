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
        private AIPlayer me;
        private AIPlayer enemy;

        #region Multipliers
        //How much should the different actions be weighed
        private double winMultiplier;
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
        private int falseMovesCnt;
        private int falseKillsCnt;
        private int falsePlacementCnt;
        private int falseFlightsCnt;


        //Count legal moves/flights
        private int moveCnt;
        private int flightCnt;

        /// <summary>
        /// C'tor
        /// </summary>
        public SimulatedGame(AIPlayer enemy, AIPlayer me,
            in double winMultiplier, in double abortMultiplier, in double killMultiplier, in double aliveMultiplier, in double flyMultiplier, in double moveMultiplier,
            in double loseMultiplier, in double falseKillMultiplier, in double falseMoveMultiplier, in double falseFlightMultiplier, in double falsePlacementMultiplier,
            in int maxMoves) : base(enemy, me)
        {
            this.enemy = enemy;
            this.me = me;

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
        public double Simulate()
        {
            double rVal = 0;

            DoGameLoop();

            var enemyStatus = GetStatus(enemy);
            var myStatus = GetStatus(me);

            //calculate the score
            rVal += (enemyStatus.MenPlaced - enemyStatus.MenAlive) * killMultiplier; //kills
            rVal += myStatus.MenAlive * aliveMultiplier; //survivors

            rVal += flyMultiplier * flightCnt; //flights
            rVal += moveMultiplier * moveCnt; //moves

            //illegal actions
            rVal += falseKillMultiplier * falseKillsCnt; //kills
            rVal += falseFlightMultiplier * falseFlightsCnt; //flights
            rVal += falseMoveMultiplier * falseMovesCnt; //moves
            rVal += falsePlacementMultiplier * falsePlacementCnt; //placements

            if(IsAborted)
            {
                rVal += abortMultiplier;
            }
            else
            {
                if (!IsDead(me)) //winner
                {
                    rVal += winMultiplier;
                }
                else //loser
                {
                    rVal += loseMultiplier;
                }
            }

            return rVal;
        }

        //Override to count invalid placements
        public override bool Place(Placement placement)
        {
            if(placement.Player != me)
                return base.Place(placement);

            if(!base.Place(placement)) //invalid placement
            {
                falsePlacementCnt++;
                return false;
            }

            //no need to count valid placements as they are stored in the players status

            return true;
        }

        //Override to count moves/flights
        public override bool Move(Move move)
        {
            if (move.Player != me)
                return base.Move(move);

            if(moveCnt >= maxMoveCnt) //check if the game needs to be aborted
            {
                IsAborted = true;
            }

            if(!base.Move(move)) //invalid move
            {
                if(CheckPhase(me) == Phase.Flying)
                    falseFlightsCnt++;
                else
                    falseMovesCnt++;

                return false;
            }
            else
            {
                if (CheckPhase(me) == Phase.Flying)
                    flightCnt++;
                else
                    moveCnt++;

                return true;
            }
        }

        //Override to count illegal kills
        public override bool Kill(Kill kill)
        {
            if(kill.Player != me)
                return base.Kill(kill);

            if(!base.Kill(kill)) //invalid kill
            {
                falseKillsCnt++;
                return false;
            }

            //no need to count kills as they can be calculated from the enemys status

            return true;
        }
    }
}
