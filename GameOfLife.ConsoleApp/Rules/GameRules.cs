using GameOfLife.Data.Entities;

namespace GameOfLife.ConsoleApp.Rules
{
    public class GameRules : IGameRules
    {
        private readonly List<IRuleStrategy> _rules;

        public GameRules(List<IRuleStrategy> rules)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
        }

        public State DetermineNextState(Cell currentCell, List<Cell> neighbors)
        {
            int liveNeighborCount = neighbors.Count(cell => cell.IsAlive());

            foreach (var rule in _rules)
            {
                var result = rule.Evaluate(currentCell, liveNeighborCount);
                if (result.HasValue)
                    return result.Value;
            }

            return State.Dead;
        }
    }
}