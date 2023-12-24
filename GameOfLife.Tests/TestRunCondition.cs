using GameOfLife.ConsoleApp.Conditions;

namespace GameOfLife.Tests
{
    public class TestRunCondition : IRunCondition
    {
        private readonly int _maxRuns;
        private int _currentRun = 0;

        public TestRunCondition(int maxRuns)
        {
            _maxRuns = maxRuns;
        }

        public bool ShouldContinue()
        {
            _currentRun++;
            return _currentRun <= _maxRuns;
        }
    }
}