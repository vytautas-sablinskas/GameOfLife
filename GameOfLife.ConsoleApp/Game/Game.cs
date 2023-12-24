using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.Data.Constants;
using GameOfLife.ConsoleApp.Files;

namespace GameOfLife.ConsoleApp.Core
{
    public class Game : IGame
    {
        private IGameField _gameField;
        private readonly GameDisplay _gameDisplay;
        private readonly IGameInputListener _gameInputListener;
        private readonly IGameFileSaver _gameSaver;

        private bool _isRunning = true;
        private bool _isPaused = false;

        private readonly object _runningLock = new object();
        private readonly object _pausedLock = new object();

        private bool IsRunning
        {
            get
            {
                lock (_runningLock)
                {
                    return _isRunning;
                }
            }
            set
            {
                lock (_runningLock)
                {
                    _isRunning = value;
                }
            }
        }

        private bool IsPaused
        {
            get
            {
                lock (_pausedLock)
                {
                    return _isPaused;
                }
            }
            set
            {
                lock (_pausedLock)
                {
                    _isPaused = value;
                }
            }
        }

        public Game(IGameField gameField, IGameInputListener gameInputListener, GameDisplay gameDisplay, IGameFileSaver gameSaver)
        {
            _gameField = gameField ?? throw new ArgumentNullException(nameof(gameField));
            _gameDisplay = gameDisplay ?? throw new ArgumentNullException(nameof(gameDisplay));
            _gameInputListener = gameInputListener ?? throw new ArgumentNullException(nameof(gameInputListener));
            _gameSaver = gameSaver ?? throw new ArgumentNullException(nameof(gameSaver));

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _gameInputListener.StopGameRequested += OnStopGameRequested;
            _gameInputListener.InformationSaveRequested += OnInformationSaveRequested;
            _gameInputListener.PauseRequested += OnPauseRequested;
        }

        public void Start()
        {
            _gameDisplay.SetupConsole(
                boardHeight: _gameField.Cells.GetLength(0),
                boardWidth: _gameField.Cells.GetLength(1)
            );

            new Thread(_gameInputListener.StartListening).Start();

            while (IsRunning)
            {
                while (IsPaused)
                {
                }

                _gameDisplay.DisplayField(_gameField);
                _gameField.UpdateField();

                Thread.Sleep(Times.GAME_ITERATION_DELAY_MILLISECONDS);
            }

            _gameDisplay.InformAboutGameStopping();
            UnsubscribeFromEvents();
        }

        private void UnsubscribeFromEvents()
        {
            _gameInputListener.StopGameRequested -= OnStopGameRequested;
            _gameInputListener.InformationSaveRequested -= OnInformationSaveRequested;
            _gameInputListener.PauseRequested -= OnPauseRequested;
        }

        private void OnStopGameRequested() => IsRunning = false;

        private void OnInformationSaveRequested()
        {
            IsPaused = true;

            var pathSavedTo = _gameSaver.SaveGame(_gameField.ToString());
            _gameDisplay.InformAboutGameSaved(pathSavedTo);

            IsPaused = false;
        }

        private void OnPauseRequested()
        {
            IsPaused = true;

            _gameDisplay.InformAboutGamePaused();

            IsPaused = false;
        }

        public void SetGameField(IGameField gameField)
        {
            _gameField = gameField;
        }
    }
}