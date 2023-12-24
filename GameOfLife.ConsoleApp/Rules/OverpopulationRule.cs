using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Rules
{
    public class OverpopulationRule : IRuleStrategy
    {
        public State? Evaluate(Cell currentCell, int liveNeighborCount)
        {
            if (currentCell.IsAlive() && liveNeighborCount > 3)
                return State.Dead;

            return null;
        }
    }
}