using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.ConsoleApp.Core;
using GameOfLife.ConsoleApp.Files;
using GameOfLife.Data.Entities;
using Moq;

namespace CoreTests
{
    public class GameTests
    {
        private const int WIDTH = 5;
        private const int HEIGHT = 5;
        private const int SLEEP_TIME_MILLISECONDS = 50;

        private readonly Mock<GameDisplay> _mockGameDisplay;
        private readonly Mock<IGameField> _mockGameField;
        private readonly Mock<IConsole> _consoleMock;
        private readonly Mock<IGameInputListener> _mockGameInputListener;
        private readonly Mock<IGameFileSaver> _mockGameSaver;
        private readonly Game _game;

        public GameTests()
        {
            _consoleMock = new Mock<IConsole>();
            _mockGameDisplay = new Mock<GameDisplay>(_consoleMock.Object);
            _mockGameField = new Mock<IGameField>();
            _mockGameInputListener = new Mock<IGameInputListener>();
            _mockGameSaver = new Mock<IGameFileSaver>();

            _game = new Game(_mockGameField.Object, _mockGameInputListener.Object, _mockGameDisplay.Object, _mockGameSaver.Object);
        }

        [Fact]
        public void Start_InitialRun_SetsGameFieldAndThenCallsAllMethodsRelatedToStoppingGameOnce()
        {
            var mockGrid = InitializeCells(State.Alive);
            _mockGameField.Setup(m => m.Cells).Returns(mockGrid);
            SetupStopGameAfterOneLoop(_mockGameInputListener);

            _game.SetGameField(_mockGameField.Object);
            _game.Start();

            _mockGameDisplay.Verify(d => d.SetupConsole(HEIGHT, WIDTH), Times.Once());
            _mockGameField.Verify(f => f.UpdateField(), Times.Once());
            _mockGameDisplay.Verify(d => d.DisplayField(_mockGameField.Object), Times.Once());
            _mockGameDisplay.Verify(d => d.InformAboutGameStopping(), Times.Once());
        }

        [Fact]
        public void Start_InformationSaveRequestedEventIsTriggered_PausesAndSaves()
        {
            var saveEvent = new AutoResetEvent(false);
            const string mockPath = "mockPath";
            _mockGameInputListener.Setup(listener => listener.StartListening()).Callback(() =>
            {
                saveEvent.WaitOne();
                _mockGameInputListener.Raise(l => l.InformationSaveRequested += null);
            });
            _mockGameSaver.Setup(s => s.SaveGame(It.IsAny<string>())).Returns(mockPath);

            var gameTask = Task.Run(_game.Start);
            saveEvent.Set();
            gameTask.Wait(SLEEP_TIME_MILLISECONDS);

            _mockGameDisplay.Verify(d => d.InformAboutGameSaved(mockPath), Times.Once());
        }

        [Fact]
        public void Start_PauseRequestedEventIsTriggered_PausesAndInforms()
        {
            var pauseEvent = new AutoResetEvent(false);

            _mockGameInputListener.Setup(listener => listener.StartListening()).Callback(() =>
            {
                pauseEvent.WaitOne();
                _mockGameInputListener.Raise(l => l.PauseRequested += null);
            });

            var gameTask = Task.Run(_game.Start);
            pauseEvent.Set();
            gameTask.Wait(SLEEP_TIME_MILLISECONDS);

            _mockGameDisplay.Verify(d => d.InformAboutGamePaused(), Times.Once());
        }

        private Cell[,] InitializeCells(State state)
        {
            var mockGrid = new Cell[HEIGHT, WIDTH];
            for (int i = 0; i < mockGrid.GetLength(0); i++)
            {
                for (int j = 0; j < mockGrid.GetLength(1); j++)
                {
                    mockGrid[i, j] = new Cell(new Position(i, j), state);
                }
            }

            return mockGrid;
        }

        private void SetupStopGameAfterOneLoop(Mock<IGameInputListener> _mockGameInputListener)
        {
            _mockGameInputListener.Setup(listener => listener.StartListening()).Callback(() =>
            {
                _mockGameInputListener.Raise(l => l.StopGameRequested += null);
            });
        }
    }
}