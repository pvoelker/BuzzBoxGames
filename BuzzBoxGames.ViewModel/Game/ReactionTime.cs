using BriansUsbQuizBoxApi;
using CommunityToolkit.Mvvm.Input;
using System;

namespace BuzzBoxGames.ViewModel.Game
{
    public class ReactionTime : BaseGame
    {
        public enum GameStateEnum { Waiting, Started, Done }

        public List<ReactionTimePaddle> _allPaddles;

        public ReactionTime()
        {
            _api.GameStarted += _api_GameStarted;
            _api.GameDone += _api_GameDone;

            _allPaddles =
            [
                Red1Paddle,
                Red2Paddle,
                Red3Paddle,
                Red4Paddle,
                Green1Paddle,
                Green2Paddle,
                Green3Paddle,
                Green4Paddle
            ];
        }

        protected override void ResetGame()
        {
            _api.Reset();

            _api.StartReactionTimingGame();
        }

        protected override void AbortGame()
        {
            GameState = GameStateEnum.Waiting;
        }

        private void _api_GameDone(object? sender, GameDoneEventArgs e)
        {
            GameState = GameStateEnum.Done;

            foreach (var item in _allPaddles)
            {
                item.IsWinner = false;
            }

            Red1Paddle.Time = (double?)e.Red1Time;
            Red2Paddle.Time = (double?)e.Red2Time;
            Red3Paddle.Time = (double?)e.Red3Time;
            Red4Paddle.Time = (double?)e.Red4Time;

            Green1Paddle.Time = (double?)e.Green1Time;
            Green2Paddle.Time = (double?)e.Green2Time;
            Green3Paddle.Time = (double?)e.Green3Time;
            Green4Paddle.Time = (double?)e.Green4Time;

            var bestTime = _allPaddles.MaxBy(x => x.Time)?.Time;

            var bestTimePlayers = _allPaddles.Where(x => bestTime != null && x.Time == bestTime);

            foreach(var item in bestTimePlayers)
            {
                item.IsWinner = true;
            }

            EndGame.Execute(null);
        }

        private void _api_GameStarted(object? sender, EventArgs e)
        {
            GameState = GameStateEnum.Started;
        }

        #region Commands

        #endregion

        private void _api_BuzzInStats(object? sender, BuzzInStatsEventArgs e)
        {
            Task.Run(_api.Reset);
        }

        private GameStateEnum _gameState = GameStateEnum.Waiting;
        public GameStateEnum GameState
        {
            get => _gameState;
            private set
            {
                SetProperty(ref _gameState, value);
                OnPropertyChanged(nameof(GameIsWaiting));
                OnPropertyChanged(nameof(GameIsStarted));
                OnPropertyChanged(nameof(GameIsDone));
            }
        }
        public bool GameIsWaiting { get => _gameState == GameStateEnum.Waiting; }
        public bool GameIsStarted { get => _gameState == GameStateEnum.Started; }
        public bool GameIsDone { get => _gameState == GameStateEnum.Done; }

        private readonly ReactionTimePaddle _red1Paddle = new ReactionTimePaddle("Red 1");
        public ReactionTimePaddle Red1Paddle { get => _red1Paddle; }

        private readonly ReactionTimePaddle _red2Paddle = new ReactionTimePaddle("Red 2");
        public ReactionTimePaddle Red2Paddle { get => _red2Paddle; }

        private readonly ReactionTimePaddle _red3Paddle = new ReactionTimePaddle("Red 3");
        public ReactionTimePaddle Red3Paddle { get => _red3Paddle; }

        private readonly ReactionTimePaddle _red4Paddle = new ReactionTimePaddle("Red 4");
        public ReactionTimePaddle Red4Paddle { get => _red4Paddle; }

        private readonly ReactionTimePaddle _green1Paddle = new ReactionTimePaddle("Green 1");
        public ReactionTimePaddle Green1Paddle { get => _green1Paddle; }

        private readonly ReactionTimePaddle _green2Paddle = new ReactionTimePaddle("Green 2");
        public ReactionTimePaddle Green2Paddle { get => _green2Paddle; }

        private readonly ReactionTimePaddle _green3Paddle = new ReactionTimePaddle("Green 3");
        public ReactionTimePaddle Green3Paddle { get => _green3Paddle; }

        private readonly ReactionTimePaddle _green4Paddle = new ReactionTimePaddle("Green 4");
        public ReactionTimePaddle Green4Paddle { get => _green4Paddle; }
    }
}
