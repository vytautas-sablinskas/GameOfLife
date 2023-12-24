using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Rules
{
    public class UnderpopulationRule : IRuleStrategy
    {
        public State? Evaluate(Cell currentCell, int liveNeighborCount)
        {
            if (currentCell.IsAlive() && liveNeighborCount < 2)
                return State.Dead;

            return null;
        }
    }
}