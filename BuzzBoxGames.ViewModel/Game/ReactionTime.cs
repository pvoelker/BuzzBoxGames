using BriansUsbQuizBoxApi;
using CommunityToolkit.Mvvm.Input;
using System;

namespace BuzzBoxGames.ViewModel.Game
{
    public class ReactionTime : BaseGame
    {
        public enum GameStateEnum { Waiting, Started, Done }

        public ReactionTime()
        {
            _api.GameStarted += _api_GameStarted;
            _api.GameDone += _api_GameDone;
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

            Red1Paddle.Time = (double?)e.Red1Time;
            Red2Paddle.Time = (double?)e.Red2Time;
            Red3Paddle.Time = (double?)e.Red3Time;
            Red4Paddle.Time = (double?)e.Red4Time;

            Green1Paddle.Time = (double?)e.Green1Time;
            Green2Paddle.Time = (double?)e.Green2Time;
            Green3Paddle.Time = (double?)e.Green3Time;
            Green4Paddle.Time = (double?)e.Green4Time;

            Red1Paddle.IsWinner = false;
            Red2Paddle.IsWinner = false;
            Red3Paddle.IsWinner = false;
            Red4Paddle.IsWinner = false;

            Green1Paddle.IsWinner = false;
            Green2Paddle.IsWinner = false;
            Green3Paddle.IsWinner = false;
            Green4Paddle.IsWinner = false;

            var timesList = new List<Tuple<Action, double?>>
            {
                new(() => { Red1Paddle.IsWinner = true; }, Red1Paddle.Time),
                new(() => { Red2Paddle.IsWinner = true; }, Red2Paddle.Time),
                new(() => { Red3Paddle.IsWinner = true; }, Red3Paddle.Time),
                new(() => { Red4Paddle.IsWinner = true; }, Red4Paddle.Time),
                new(() => { Green1Paddle.IsWinner = true; }, Green1Paddle.Time),
                new(() => { Green2Paddle.IsWinner = true; }, Green2Paddle.Time),
                new(() => { Green3Paddle.IsWinner = true; }, Green3Paddle.Time),
                new(() => { Green4Paddle.IsWinner = true; }, Green4Paddle.Time)
            };

            var minTime = timesList.MaxBy(x => x.Item2)?.Item2;

            var minTimePlayers = timesList.Where(x => x.Item2 != null && x.Item2 == minTime).ToList();

            foreach(var item in minTimePlayers)
            {
                item.Item1.Invoke();
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

        private readonly ReactionTimePaddle _red1Paddle = new ReactionTimePaddle();
        public ReactionTimePaddle Red1Paddle { get => _red1Paddle; }

        private readonly ReactionTimePaddle _red2Paddle = new ReactionTimePaddle();
        public ReactionTimePaddle Red2Paddle { get => _red2Paddle; }

        private readonly ReactionTimePaddle _red3Paddle = new ReactionTimePaddle();
        public ReactionTimePaddle Red3Paddle { get => _red3Paddle; }

        private readonly ReactionTimePaddle _red4Paddle = new ReactionTimePaddle();
        public ReactionTimePaddle Red4Paddle { get => _red4Paddle; }


        private readonly ReactionTimePaddle _green1Paddle = new ReactionTimePaddle();
        public ReactionTimePaddle Green1Paddle { get => _green1Paddle; }

        private readonly ReactionTimePaddle _green2Paddle = new ReactionTimePaddle();
        public ReactionTimePaddle Green2Paddle { get => _green2Paddle; }

        private readonly ReactionTimePaddle _green3Paddle = new ReactionTimePaddle();
        public ReactionTimePaddle Green3Paddle { get => _green3Paddle; }

        private readonly ReactionTimePaddle _green4Paddle = new ReactionTimePaddle();
        public ReactionTimePaddle Green4Paddle { get => _green4Paddle; }
    }
}
