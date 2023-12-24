using GameOfLife.ConsoleApp.Rules;
using GameOfLife.Data.Constants;
using GameOfLife.Data.Entities;

using System.Text;

namespace GameOfLife.ConsoleApp.Core
{
    public class GameField : IGameField
    {
        public Cell[,] Cells { get; }

        public int LiveCellCount
        {
            get
            {
                return Cells.Cast<Cell>().Count(cell => cell.IsAlive());
            }
        }

        public int IterationCount { get; private set; }

        private readonly IGameRules _gameRules;

        public GameField(GameFieldDimensions gameFieldDimensions, IGameRules gameRules, int iterationCount = 0)
        {
            if (gameFieldDimensions.Width <= 0)
                throw new ArgumentException(nameof(gameFieldDimensions.Width), Exceptions.WIDTH_MESSAGE);

            if (gameFieldDimensions.Height <= 0)
                throw new ArgumentException(nameof(gameFieldDimensions.Height), Exceptions.HEIGHT_MESSAGE);

            Cells = InitializeCells(gameFieldDimensions);
            _gameRules = gameRules;
            IterationCount = iterationCount;
        }

        private Cell[,] InitializeCells(GameFieldDimensions gameFieldDimensions)
        {
            Cell[,] cells = new Cell[gameFieldDimensions.Height, gameFieldDimensions.Width];
            var random = new Random();

            for (int row = 0; row < gameFieldDimensions.Height; row++)
            {
                for (int column = 0; column < gameFieldDimensions.Width; column++)
                {
                    var state = random.Next(100) < Chances.BIRTH_CHANCE_PERCENTAGE ? State.Alive : State.Dead;
                    cells[row, column] = new Cell(new Position(row, column), state);
                }
            }

            return cells;
        }

        public void UpdateField()
        {
            State[,] nextStates = CalculateNextStates();
            ApplyNextStates(nextStates);
            UpdateIterationCount();
        }

        public void UpdateIterationCount() => IterationCount++;

        private State[,] CalculateNextStates()
        {
            State[,] nextStates = new State[Cells.GetLength(0), Cells.GetLength(1)];

            for (int row = 0; row < Cells.GetLength(0); row++)
            {
                for (int column = 0; column < Cells.GetLength(1); column++)
                {
                    var cell = Cells[row, column];
                    nextStates[row, column] = _gameRules.DetermineNextState(
                        currentCell: cell,
                        neighbors: cell.GetPossibleNeighbors(Cells, Cells.GetLength(0), Cells.GetLength(1))
                    );
                }
            }

            return nextStates;
        }

        private void ApplyNextStates(State[,] nextStates)
        {
            for (int row = 0; row < Cells.GetLength(0); row++)
            {
                for (int column = 0; column < Cells.GetLength(1); column++)
                {
                    Cells[row, column].CurrentState = nextStates[row, column];
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(IterationCount.ToString());
            builder.AppendLine(LiveCellCount.ToString());

            for (int row = 0; row < Cells.GetLength(0); row++)
            {
                for (int column = 0; column < Cells.GetLength(1); column++)
                {
                    builder.Append(Cells[row, column].IsAlive() ? "#" : ".");
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}