using BriansUsbQuizBoxApi;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using TicTacToe.Game;

namespace BuzzBoxGames.ViewModel.Game
{
    public class DropTacToe : BaseGame
    {
        private const int GRID_SIZE = 4;

        public enum GameStateEnum { Waiting, Started }

        private readonly Random _rnd = new Random();

        private readonly IMultiGame _gameEngine;

        public DropTacToe()
        {
            _api.BuzzIn += _api_BuzzIn;

            _gameEngine = Factory.CreateNewGame(2, 3, [GRID_SIZE, GRID_SIZE]);

            NextRound = new RelayCommand(() =>
            {
                EndGame.Execute(null);

                GameState = GameStateEnum.Waiting;
            });
        }

        protected override void ResetGame()
        {
            Data_0_0 = TicTacToeEnum.None;
            Data_0_1 = TicTacToeEnum.None;
            Data_0_2 = TicTacToeEnum.None;
            Data_0_3 = TicTacToeEnum.None;
            Data_1_0 = TicTacToeEnum.None;
            Data_1_1 = TicTacToeEnum.None;
            Data_1_2 = TicTacToeEnum.None;
            Data_1_3 = TicTacToeEnum.None;
            Data_2_0 = TicTacToeEnum.None;
            Data_2_1 = TicTacToeEnum.None;
            Data_2_2 = TicTacToeEnum.None;
            Data_2_3 = TicTacToeEnum.None;
            Data_3_0 = TicTacToeEnum.None;
            Data_3_1 = TicTacToeEnum.None;
            Data_3_2 = TicTacToeEnum.None;
            Data_3_3 = TicTacToeEnum.None;

            _gameEngine.StartNewGame(_rnd.Next(1) + 1);

            OnGameStateChange();

            _api.Reset();

            GameState = GameStateEnum.Started;
        }

        protected override void AbortGame()
        {
            GameState = GameStateEnum.Waiting;
        }

        private void _api_BuzzIn(object? sender, BuzzInEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var player = MapGameEngineIntToEnum(_gameEngine.NextPlayer);

                if (MapPaddleColorToEnum(e.Paddle.Color) == player)
                {
                    var col = (int)e.Paddle.Number - 1;
                    var row = FindNextRow(col);

                    if (row == null)
                    {
                        InvalidMove?.Execute(null);
                    }
                    else
                    {
                        var success = _gameEngine.MakeMoveByPosition([row.Value, col]);
                        Debug.Assert(success == true, "The tic-tac-toe move should never fail");

                        SetBoardByPosition(row.Value, col, player);

                        OnGameStateChange();

                        if (MapGameEngineIntToEnum(_gameEngine.Winner) != TicTacToeEnum.None)
                        {
                            NextRound.Execute(null);
                        }
                        else
                        {
                            // Waiting for NextRound commands to continue
                        }
                    }
                }
                else
                {
                    InvalidMove?.Execute(null);
                }

                _api.Reset();
            });
        }

        #region Commands

        public IRelayCommand? InvalidMove { get; set; }

        public IRelayCommand NextRound { get; set; }

        #endregion

        private GameStateEnum _gameState = GameStateEnum.Waiting;
        public GameStateEnum GameState
        {
            get => _gameState;
            private set
            {
                SetProperty(ref _gameState, value);
                OnPropertyChanged(nameof(GameIsWaiting));
                OnPropertyChanged(nameof(GameIsStarted));
            }
        }
        public bool GameIsWaiting { get => _gameState == GameStateEnum.Waiting; }
        public bool GameIsStarted { get => _gameState == GameStateEnum.Started; }

        private string _inGameInstructions = string.Empty;
        public string InGameInstructions
        {
            get => _inGameInstructions;
            private set => SetProperty(ref _inGameInstructions, value);
        }

        public TicTacToeEnum Turn
        {
            get { return MapGameEngineIntToEnum(_gameEngine.NextPlayer); }
        }
        public bool GreenPaddlesTurn
        {
            get { return MapGameEngineIntToEnum(_gameEngine.NextPlayer) == MapPaddleColorToEnum(PaddleColorEnum.Green); }
        }
        public bool RedPaddlesTurn
        {
            get { return MapGameEngineIntToEnum(_gameEngine.NextPlayer) == MapPaddleColorToEnum(PaddleColorEnum.Red); }
        }

        public bool HasWinner
        {
            get { return _gameEngine.IsGameFinished && MapGameEngineIntToEnum(_gameEngine.Winner) != TicTacToeEnum.None ; }
        }

        public TicTacToeEnum Winner
        {
            get { return MapGameEngineIntToEnum(_gameEngine.Winner); }
        }
        public bool IsGameDraw
        {
            get { return HasWinner && MapGameEngineIntToEnum(_gameEngine.Winner) == TicTacToeEnum.None; }
        }
        public bool AreGreenPaddlesWinner
        {
            get { return MapGameEngineIntToEnum(_gameEngine.Winner) == MapPaddleColorToEnum(PaddleColorEnum.Green); }
        }
        public bool AreRedPaddlesWinner
        {
            get { return MapGameEngineIntToEnum(_gameEngine.Winner) == MapPaddleColorToEnum(PaddleColorEnum.Red); }
        }

        /// <summary>
        /// First dimension are rows, the second dimension are columns
        /// </summary>
        private TicTacToeEnum[,] _data = new TicTacToeEnum[GRID_SIZE, GRID_SIZE];
        public TicTacToeEnum Data_0_0
        {
            get => _data[0, 0];
            private set => SetProperty(ref _data[0, 0], value);
        }
        public TicTacToeEnum Data_0_1
        {
            get => _data[0, 1];
            private set => SetProperty(ref _data[0, 1], value);
        }
        public TicTacToeEnum Data_0_2
        {
            get => _data[0, 2];
            private set => SetProperty(ref _data[0, 2], value);
        }
        public TicTacToeEnum Data_0_3
        {
            get => _data[0, 3];
            private set => SetProperty(ref _data[0, 3], value);
        }
        public TicTacToeEnum Data_1_0
        {
            get => _data[1, 0];
            private set => SetProperty(ref _data[1, 0], value);
        }
        public TicTacToeEnum Data_1_1
        {
            get => _data[1, 1];
            private set => SetProperty(ref _data[1, 1], value);
        }
        public TicTacToeEnum Data_1_2
        {
            get => _data[1, 2];
            private set => SetProperty(ref _data[1, 2], value);
        }
        public TicTacToeEnum Data_1_3
        {
            get => _data[1, 3];
            private set => SetProperty(ref _data[1, 3], value);
        }
        public TicTacToeEnum Data_2_0
        {
            get => _data[2, 0];
            private set => SetProperty(ref _data[2, 0], value);
        }
        public TicTacToeEnum Data_2_1
        {
            get => _data[2, 1];
            private set => SetProperty(ref _data[2, 1], value);
        }
        public TicTacToeEnum Data_2_2
        {
            get => _data[2, 2];
            private set => SetProperty(ref _data[2, 2], value);
        }
        public TicTacToeEnum Data_2_3
        {
            get => _data[2, 3];
            private set => SetProperty(ref _data[2, 3], value);
        }
        public TicTacToeEnum Data_3_0
        {
            get => _data[3, 0];
            private set => SetProperty(ref _data[3, 0], value);
        }
        public TicTacToeEnum Data_3_1
        {
            get => _data[3, 1];
            private set => SetProperty(ref _data[3, 1], value);
        }
        public TicTacToeEnum Data_3_2
        {
            get => _data[3, 2];
            private set => SetProperty(ref _data[3, 2], value);
        }
        public TicTacToeEnum Data_3_3
        {
            get => _data[3, 3];
            private set => SetProperty(ref _data[3, 3], value);
        }

        private void SetBoardByPosition(int row, int col, TicTacToeEnum val)
        {
            if(row == 0 && col == 0)
            {
                Data_0_0 = val;
            }
            else if(row == 0 && col == 1)
            {
                Data_0_1 = val;
            }
            else if (row == 0 && col == 2)
            {
                Data_0_2 = val;
            }
            else if (row == 0 && col == 3)
            {
                Data_0_3 = val;
            }
            else if (row == 1 && col == 0)
            {
                Data_1_0 = val;
            }
            else if (row == 1 && col == 1)
            {
                Data_1_1 = val;
            }
            else if (row == 1 && col == 2)
            {
                Data_1_2 = val;
            }
            else if (row == 1 && col == 3)
            {
                Data_1_3 = val;
            }
            else if (row == 2 && col == 0)
            {
                Data_2_0 = val;
            }
            else if (row == 2 && col == 1)
            {
                Data_2_1 = val;
            }
            else if (row == 2 && col == 2)
            {
                Data_2_2 = val;
            }
            else if (row == 2 && col == 3)
            {
                Data_2_3 = val;
            }
            else if (row == 3 && col == 0)
            {
                Data_3_0 = val;
            }
            else if (row == 3 && col == 1)
            {
                Data_3_1 = val;
            }
            else if (row == 3 && col == 2)
            {
                Data_3_2 = val;
            }
            else if (row == 3 && col == 3)
            {
                Data_3_3 = val;
            }
            else
            {
                throw new ArgumentOutOfRangeException(null, "Row or column values are out of range");
            }
        }

        private int? FindNextRow(int column)
        {
            int? retVal = null;

            for(int i = GRID_SIZE - 1; i >= 0; i--)
            {
                if (_data[i, column] == TicTacToeEnum.None)
                {
                    retVal = i;
                    break;
                }
            }

            return retVal;
        }

        private void OnGameStateChange()
        {
            OnPropertyChanged(nameof(Turn));
            OnPropertyChanged(nameof(GreenPaddlesTurn));
            OnPropertyChanged(nameof(RedPaddlesTurn));

            OnPropertyChanged(nameof(HasWinner));
            OnPropertyChanged(nameof(Winner));
            OnPropertyChanged(nameof(IsGameDraw));
            OnPropertyChanged(nameof(AreGreenPaddlesWinner));
            OnPropertyChanged(nameof(AreRedPaddlesWinner));
        }

        static private TicTacToeEnum MapGameEngineIntToEnum(int val)
        {
            switch(val)
            {
                case 0:
                    return TicTacToeEnum.None;
                case 1:
                    return TicTacToeEnum.X;
                case 2:
                    return TicTacToeEnum.O;
                default:
                    throw new ArgumentOutOfRangeException(nameof(val));
            }
        }

        static private TicTacToeEnum MapPaddleColorToEnum(PaddleColorEnum val)
        {
            switch (val)
            {
                case PaddleColorEnum.Red:
                    return TicTacToeEnum.X;
                case PaddleColorEnum.Green:
                    return TicTacToeEnum.O;
                default:
                    throw new ArgumentOutOfRangeException(nameof(val));
            }
        }
    }
}
