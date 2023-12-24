using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Core
{
    public interface IGameField
    {
        Cell[,] Cells { get; }

        int LiveCellCount { get; }

        int IterationCount { get; }

        void UpdateField();

        void UpdateIterationCount();
    }
}