namespace GameOfLife.ConsoleApp.Conditions
{
    public class AlwaysRunCondition : IRunCondition
    {
        public bool ShouldContinue() => true;
    }
}