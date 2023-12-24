using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Rules
{
    public interface IRuleStrategy
    {
        State? Evaluate(Cell currentCell, int liveNeighborCount);
    }
}