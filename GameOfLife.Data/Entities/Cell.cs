namespace GameOfLife.Data.Entities
{
    public class Cell
    {
        public State CurrentState { get; set; }
        public Position Position { get; }

        public Cell(Position position, State initialState)
        {
            Position = position;
            CurrentState = initialState;
        }

        public bool IsAlive() => CurrentState == State.Alive;

        private IEnumerable<Position> NeighborPositionsToCheck()
        {
            return new List<Position>
            {
                new Position(Position.x - 1, Position.y + 1),
                new Position(Position.x - 1, Position.y - 1),
                new Position(Position.x + 1, Position.y + 1),
                new Position(Position.x + 1, Position.y - 1),
                new Position(Position.x + 1, Position.y),
                new Position(Position.x - 1, Position.y),
                new Position(Position.x, Position.y + 1),
                new Position(Position.x, Position.y - 1),
            };
        }

        public List<Cell> GetPossibleNeighbors(Cell[,] currentCells, int boardHeight, int boardWidth)
        {
            var neighbors = new List<Cell>();

            foreach (var neighborPosition in NeighborPositionsToCheck())
            {
                if (neighborPosition.IsWithinBounds(boardHeight, boardWidth))
                {
                    neighbors.Add(currentCells[neighborPosition.x, neighborPosition.y]);
                }
            }

            return neighbors;
        }
    }
}