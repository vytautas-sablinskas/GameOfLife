using GameOfLife.ConsoleApp.Rules;
using GameOfLife.Data.Entities;

namespace RuleTests
{
    public class UnderpopulationRuleTests
    {
        private readonly UnderpopulationRule _rule;
        private readonly Cell _cell;

        public UnderpopulationRuleTests()
        {
            _rule = new UnderpopulationRule();
            _cell = new Cell(new Position(1, 1), State.Alive);
        }

        [Fact]
        public void Evaluate_AliveCellWithLessThanTwoNeighbors_ReturnsDead()
        {
            var result = _rule.Evaluate(_cell, 1);
            Assert.Equal(State.Dead, result);
        }

        [Fact]
        public void Evaluate_AliveCellWithTwoOrMoreNeighbors_ReturnsNull()
        {
            var result = _rule.Evaluate(_cell, 2);
            Assert.Null(result);
        }
    }
}