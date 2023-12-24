using Moq;
using GameOfLife.ConsoleApp.Navigation.Actions;
using GameOfLife.Data.Entities;
using GameOfLife.Data.Constants;

namespace NavigationTests
{
    public class MenuActionFactoryTests
    {
        private readonly MenuActionFactory _menuActionFactory;
        private readonly GameFieldDimensions _mockGameFieldDimensions;

        public MenuActionFactoryTests()
        {
            _menuActionFactory = new MenuActionFactory();

            var mockGameFieldDimensions = new Mock<GameFieldDimensions>();
            mockGameFieldDimensions.SetupGet(m => m.Width).Returns(10);
            mockGameFieldDimensions.SetupGet(m => m.Height).Returns(10);
            _mockGameFieldDimensions = mockGameFieldDimensions.Object;
        }

        [Theory]
        [InlineData(NavigationActions.START_NEW_GAME, typeof(StartNewGameAction))]
        [InlineData(NavigationActions.CHANGE_BOARD_SIZE, typeof(ChangeGameFieldDimensionsAction))]
        [InlineData(NavigationActions.UPLOAD_SAVED_FILE, typeof(UploadSavedGameAction))]
        [InlineData(NavigationActions.EXIT_APP, typeof(ExitApplicationAction))]
        public void CreateAction_ValidInput_ReturnsExpectedActionType(string input, Type expectedType)
        {
            var action = _menuActionFactory.CreateAction(input, _mockGameFieldDimensions);
            Assert.IsType(expectedType, action);
        }

        [Fact]
        public void CreateAction_InvalidInput_ReturnsNull()
        {
            string actionType = "invalid action";

            var action = _menuActionFactory.CreateAction(actionType, _mockGameFieldDimensions);
            Assert.Null(action);
        }
    }
}