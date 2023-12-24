using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Rules
{
    public class SurvivalRule : IRuleStrategy
    {
        public State? Evaluate(Cell currentCell, int liveNeighborCount)
        {
            if (currentCell.IsAlive() &&
               (liveNeighborCount == 2 || liveNeighborCount == 3))
            {
                return State.Alive;
            }

            return null;
        }
    }
}