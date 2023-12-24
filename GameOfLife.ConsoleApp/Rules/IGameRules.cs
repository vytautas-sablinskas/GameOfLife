using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Rules
{
    public interface IGameRules
    {
        State DetermineNextState(Cell currentCell, List<Cell> neighbors);
    }
}