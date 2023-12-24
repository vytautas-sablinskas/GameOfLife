using GameOfLife.ConsoleApp.Rules;
using GameOfLife.Data.Entities;

namespace RuleTests
{
    public class ReproductionRuleTests
    {
        private readonly ReproductionRule _rule;
        private readonly Cell _cell;

        public ReproductionRuleTests()
        {
            _rule = new ReproductionRule();
            _cell = new Cell(new Position(1, 1), State.Dead);
        }

        [Fact]
        public void Evaluate_DeadCellWithThreeNeighbors_ReturnsAlive()
        {
            var result = _rule.Evaluate(_cell, 3);

            Assert.Equal(State.Alive, result);
        }

        [Fact]
        public void Evaluate_DeadCellWithNonThreeNeighbors_ReturnsNull()
        {
            var result = _rule.Evaluate(_cell, 2);

            Assert.Null(result);
        }
    }
}