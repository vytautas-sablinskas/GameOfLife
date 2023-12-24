namespace GameOfLife.Data.Entities
{
    public class Position
    {
        public int x { get; }
        public int y { get; }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool IsWithinBounds(int width, int height) =>
            x >= 0 && x < width && y >= 0 && y < height;
    }
}