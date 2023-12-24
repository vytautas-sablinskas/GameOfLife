using Moq;
using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.Tests;

namespace CoreTests
{
    public class GameInputListenerTests
    {
        [Theory]
        [InlineData(ConsoleKey.S, "Stop")]
        [InlineData(ConsoleKey.U, "Save")]
        [InlineData(ConsoleKey.P, "Pause")]
        public void StartListening_ShouldTriggerCorrectEvent(ConsoleKey key, string action)
        {
            var mockConsole = new Mock<IConsole>();
            mockConsole.Setup(c => c.ReadKey(true)).Returns(new ConsoleKeyInfo('\0', key, false, false, false));
            var runCondition = new TestRunCondition(1);
            var listener = new GameInputListener(mockConsole.Object, runCondition);
            bool eventTriggered = false;

            if (action == "Stop")
            {
                listener.StopGameRequested += () => eventTriggered = true;
            }
            else if (action == "Save")
            {
                listener.InformationSaveRequested += () => eventTriggered = true;
            }
            else if (action == "Pause")
            {
                listener.PauseRequested += () => eventTriggered = true;
            }

            listener.StartListening();

            Assert.True(eventTriggered);
        }
    }
}