using GameOfLife.ConsoleApp.Rules;
using GameOfLife.Data.Entities;
using Moq;

namespace RuleTests
{
    public class GameRulesTests
    {
        [Theory]
        [InlineData(State.Alive, State.Alive)]
        [InlineData(null, State.Dead)]
        public void DetermineNextState_WhenRuleReturnsState_ReturnsThatState(State? cellNewState, State actualNewState)
        {
            var mockRule = new Mock<IRuleStrategy>();
            var gameRules = new GameRules(new List<IRuleStrategy> { mockRule.Object });
            var cell = new Cell(new Position(1, 1), State.Alive);
            mockRule.Setup(r => r.Evaluate(It.IsAny<Cell>(), It.IsAny<int>())).Returns(cellNewState);

            var result = gameRules.DetermineNextState(cell, new List<Cell>());

            Assert.Equal(actualNewState, result);
        }
    }
}