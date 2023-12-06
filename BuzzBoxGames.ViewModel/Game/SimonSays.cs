using BriansUsbQuizBoxApi;
using CommunityToolkit.Mvvm.Input;
using System;

namespace BuzzBoxGames.ViewModel.Game
{
    public class SimonSays : BaseGame
    {
        public enum GameStateEnum { Waiting, Started }

        private readonly Random _rnd = new();

        private readonly List<Paddle> _sequence = new();

        private int? _currentSequenceIndex = null;

        private int? _repeatCurrentSequenceIndex = null;

        private readonly List<Paddle> _allPaddles = [Paddle.RED_1, Paddle.RED_2, Paddle.RED_3, Paddle.RED_4, Paddle.GREEN_1, Paddle.GREEN_2, Paddle.GREEN_3, Paddle.GREEN_4];

        public SimonSays()
        {
            _api.BuzzIn += _api_BuzzIn;
        }

        protected override void ResetGame()
        {
            _sequence.Clear();
            _sequence.Add(GetRandomPaddle());

            PlaySequence();

            GameState = GameStateEnum.Started;

            _repeatCurrentSequenceIndex = null;

            Score = 0;
        }

        protected override void AbortGame()
        {
            GameState = GameStateEnum.Waiting;
        }

        private void _api_BuzzIn(object? sender, BuzzInEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (_repeatCurrentSequenceIndex.HasValue)
                {
                    var expectedPaddle = _sequence[_repeatCurrentSequenceIndex.Value];

                    if (e.Paddle == expectedPaddle)
                    {
                        var _ = Task.Run(async () =>
                        {
                            _api.Reset();

                            if (_repeatCurrentSequenceIndex.Value < _sequence.Count - 1)
                            {
                                _repeatCurrentSequenceIndex++;
                            }
                            else
                            {
                                _repeatCurrentSequenceIndex = null;

                                _sequence.Add(GetRandomPaddle());

                                if (Score != null)
                                {
                                    Score = Score.Value + 1;
                                }

                                PlaySequence();
                            }

                            _api.Reset();

                            LitePaddle(expectedPaddle, true);

                            await Task.Delay(1000);

                            LitePaddle(expectedPaddle, false);
                        });
                    }
                    else
                    {
                        var _ = Task.Run(() =>
                        {
                            WrongPaddlePress?.Execute(null);

                            EndGame.Execute(null);

                            GameState = GameStateEnum.Waiting;
                        });
                    }
                }
            });
        }

        #region Commands

        public IRelayCommand? WrongPaddlePress { get; set; }

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

        private int? _score = null;
        public int? Score
        {
            get => _score;
            private set
            {
                SetProperty(ref _score, value);
                OnPropertyChanged(nameof(HasScore));
            }
        }
        public bool HasScore { get => _score != null; }

        private bool _paddleRed1Lit = false;
        public bool PaddleRed1Lit
        {
            get => _paddleRed1Lit;
            private set => SetProperty(ref _paddleRed1Lit, value);
        }

        private bool _paddleRed2Lit = false;
        public bool PaddleRed2Lit
        {
            get => _paddleRed2Lit;
            private set => SetProperty(ref _paddleRed2Lit, value);
        }

        private bool _paddleRed3Lit = false;
        public bool PaddleRed3Lit
        {
            get => _paddleRed3Lit;
            private set => SetProperty(ref _paddleRed3Lit, value);
        }

        private bool _paddleRed4Lit = false;
        public bool PaddleRed4Lit
        {
            get => _paddleRed4Lit;
            private set => SetProperty(ref _paddleRed4Lit, value);
        }

        private bool _paddleGreen1Lit = false;
        public bool PaddleGreen1Lit
        {
            get => _paddleGreen1Lit;
            private set => SetProperty(ref _paddleGreen1Lit, value);
        }

        private bool _paddleGreen2Lit = false;
        public bool PaddleGreen2Lit
        {
            get => _paddleGreen2Lit;
            private set => SetProperty(ref _paddleGreen2Lit, value);
        }

        private bool _paddleGreen3Lit = false;
        public bool PaddleGreen3Lit
        {
            get => _paddleGreen3Lit;
            private set => SetProperty(ref _paddleGreen3Lit, value);
        }

        private bool _paddleGreen4Lit = false;
        public bool PaddleGreen4Lit
        {
            get => _paddleGreen4Lit;
            private set => SetProperty(ref _paddleGreen4Lit, value);
        }

        private void PlaySequence()
        {
            var _ = Task.Run(async () =>
            {
                InGameInstructions = "Listen for the pattern...";

                _api.StartPaddleLockout();

                await Task.Delay(2000);

                PlayNextInSequence();
            });
        }

        private void PlayNextInSequence()
        {
            if(_currentSequenceIndex.HasValue)
            {
                LitePaddle(_sequence[_currentSequenceIndex.Value], false);

                _currentSequenceIndex++;
                if (_currentSequenceIndex < _sequence.Count)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(200);

                        LitePaddle(_sequence[_currentSequenceIndex.Value], true);

                        await Task.Delay(1000);

                        PlayNextInSequence();
                    });
                }
                else
                {
                    Task.Run(async () => // Need to look into why this is needed
                    {
                        _api.Reset(); // Not using 'StopPaddleLockout' as is causes a beep

                        InGameInstructions = "Repeat the pattern!";

                        _currentSequenceIndex = null;

                        _repeatCurrentSequenceIndex = 0;
                    });
                }
            }
            else
            {
                _currentSequenceIndex = 0;

                if (_sequence.Count > 0)
                {
                    LitePaddle(_sequence[_currentSequenceIndex.Value], true);

                    Task.Run(async () =>
                    {
                        await Task.Delay(1000);

                        PlayNextInSequence();
                    });
                }
                else
                {
                    throw new InvalidOperationException("A least one element is expected in the paddle sequence");
                }
            }
        }

        private Paddle GetRandomPaddle()
        {
            return _allPaddles[_rnd.Next(_allPaddles.Count)];
        }

        private void LitePaddle(Paddle paddle, bool isLit)
        {
            if(paddle == Paddle.RED_1)
            {
                PaddleRed1Lit = isLit;
            }
            else if (paddle == Paddle.RED_2)
            {
                PaddleRed2Lit = isLit;
            }
            else if (paddle == Paddle.RED_3)
            {
                PaddleRed3Lit = isLit;
            }
            else if (paddle == Paddle.RED_4)
            {
                PaddleRed4Lit = isLit;
            }
            else if (paddle == Paddle.GREEN_1)
            {
                PaddleGreen1Lit = isLit;
            }
            else if (paddle == Paddle.GREEN_2)
            {
                PaddleGreen2Lit = isLit;
            }
            else if (paddle == Paddle.GREEN_3)
            {
                PaddleGreen3Lit = isLit;
            }
            else if (paddle == Paddle.GREEN_4)
            {
                PaddleGreen4Lit = isLit;
            }
            else
            {
                throw new InvalidOperationException("Invalid paddle");
            }
        }
    }
}
