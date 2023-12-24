using GameOfLife.Data.Constants;

namespace GameOfLife.Data.Entities
{
    public class GameFieldDimensions
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public GameFieldDimensions()
        {
            Width = DefaultDimensions.WIDTH;
            Height = DefaultDimensions.HEIGHT;
        }

        public GameFieldDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}