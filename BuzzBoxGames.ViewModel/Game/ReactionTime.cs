﻿using BriansUsbQuizBoxApi;
using CommunityToolkit.Mvvm.Input;
using System;

namespace BuzzBoxGames.ViewModel.Game
{
    public class ReactionTime : BaseGame
    {
        public enum GameStateEnum { Waiting, Started, Done }

        private readonly Random _rnd = new Random();

        public ReactionTime()
        {
            _api.GameStarted += _api_GameStarted;
            _api.GameLightOn += _api_GameLightOn;
            _api.GameDone += _api_GameDone;
            _api.ReadThreadDisconnection += _api_ReadThreadDisconnection;
        }

        protected override void ResetGame()
        {
            _api.Reset();

            _api.StartReactionTimingGame();
        }

        private void _api_GameDone(object? sender, GameDoneEventArgs e)
        {
            GameState = GameStateEnum.Done;

            PaddleRed1Time = (double?)e.Red1Time;
            PaddleRed2Time = (double?)e.Red2Time;
            PaddleRed3Time = (double?)e.Red3Time;
            PaddleRed4Time = (double?)e.Red4Time;

            PaddleGreen1Time = (double?)e.Green1Time;
            PaddleGreen2Time = (double?)e.Green2Time;
            PaddleGreen3Time = (double?)e.Green3Time;
            PaddleGreen4Time = (double?)e.Green4Time;

            EndGame.Execute(null);
        }

        private void _api_GameLightOn(object? sender, EventArgs e)
        {
            // Nothing implemented yet
        }

        private void _api_GameStarted(object? sender, EventArgs e)
        {
            GameState = GameStateEnum.Started;
        }

        private void _api_ReadThreadDisconnection(object? sender, DisconnectionEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                const string errMsg = "Quiz box has disconnected, cancelling game...";

                if (MessageBoxService != null)
                {
                    MessageBoxService.ShowError(errMsg);
                }
                else
                {
                    throw new InvalidOperationException(errMsg);
                }

                GameState = GameStateEnum.Waiting;
            });
        }

        #region Commands

        #endregion

        private void _api_BuzzInStats(object? sender, BuzzInStatsEventArgs e)
        {
            Task.Run(() => _api.Reset());
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

        public double MaxTime { get => 1000; }

        private double? _paddleRed1Time = 0;
        public double? PaddleRed1Time
        {
            get => _paddleRed1Time;
            private set
            {
                SetProperty(ref _paddleRed1Time, value);
                OnPropertyChanged(nameof(PaddleRed1Score));
                OnPropertyChanged(nameof(DidPaddleRed1BuzzIn));
                OnPropertyChanged(nameof(DidPaddleRed1NotBuzzIn));
            }
        }
        public double? PaddleRed1Score { get => MaxTime - _paddleRed1Time; }
        public bool DidPaddleRed1BuzzIn { get => _paddleRed1Time != null; }
        public bool DidPaddleRed1NotBuzzIn { get => _paddleRed1Time == null; }

        private double? _paddleRed2Time = 0;
        public double? PaddleRed2Time
        {
            get => _paddleRed2Time;
            private set
            {
                SetProperty(ref _paddleRed2Time, value);
                OnPropertyChanged(nameof(PaddleRed2Score));
                OnPropertyChanged(nameof(DidPaddleRed2BuzzIn));
                OnPropertyChanged(nameof(DidPaddleRed2NotBuzzIn));
            }
        }
        public double? PaddleRed2Score { get => MaxTime - _paddleRed2Time; }
        public bool DidPaddleRed2BuzzIn { get => _paddleRed2Time != null; }
        public bool DidPaddleRed2NotBuzzIn { get => _paddleRed2Time == null; }

        private double? _paddleRed3Time = 0;
        public double? PaddleRed3Time
        {
            get => _paddleRed3Time;
            private set
            {
                SetProperty(ref _paddleRed3Time, value);
                OnPropertyChanged(nameof(PaddleRed3Score));
                OnPropertyChanged(nameof(DidPaddleRed3BuzzIn));
                OnPropertyChanged(nameof(DidPaddleRed3NotBuzzIn));
            }
        }
        public double? PaddleRed3Score { get => MaxTime - _paddleRed3Time; }
        public bool DidPaddleRed3BuzzIn { get => _paddleRed3Time != null; }
        public bool DidPaddleRed3NotBuzzIn { get => _paddleRed3Time == null; }

        private double? _paddleRed4Time = 0;
        public double? PaddleRed4Time
        {
            get => _paddleRed4Time;
            private set
            {
                SetProperty(ref _paddleRed4Time, value);
                OnPropertyChanged(nameof(PaddleRed4Score));
                OnPropertyChanged(nameof(DidPaddleRed4BuzzIn));
                OnPropertyChanged(nameof(DidPaddleRed4NotBuzzIn));
            }
        }
        public double? PaddleRed4Score { get => MaxTime - _paddleRed4Time; }
        public bool DidPaddleRed4BuzzIn { get => _paddleRed4Time != null; }
        public bool DidPaddleRed4NotBuzzIn { get => _paddleRed4Time == null; }

        private double? _paddleGreen1Time = 0;
        public double? PaddleGreen1Time
        {
            get => _paddleGreen1Time;
            private set
            {
                SetProperty(ref _paddleGreen1Time, value);
                OnPropertyChanged(nameof(PaddleGreen1Score));
                OnPropertyChanged(nameof(DidPaddleGreen1BuzzIn));
                OnPropertyChanged(nameof(DidPaddleGreen1NotBuzzIn));
            }
        }
        public double? PaddleGreen1Score { get => MaxTime - _paddleGreen1Time; }
        public bool DidPaddleGreen1BuzzIn { get => _paddleGreen1Time != null; }
        public bool DidPaddleGreen1NotBuzzIn { get => _paddleGreen1Time == null; }

        private double? _paddleGreen2Time = 0;
        public double? PaddleGreen2Time
        {
            get => _paddleGreen2Time;
            private set
            {
                SetProperty(ref _paddleGreen2Time, value);
                OnPropertyChanged(nameof(PaddleGreen2Score));
                OnPropertyChanged(nameof(DidPaddleGreen2BuzzIn));
                OnPropertyChanged(nameof(DidPaddleGreen2NotBuzzIn));
            }
        }
        public double? PaddleGreen2Score { get => MaxTime - _paddleGreen2Time; }
        public bool DidPaddleGreen2BuzzIn { get => _paddleGreen2Time != null; }
        public bool DidPaddleGreen2NotBuzzIn { get => _paddleGreen2Time == null; }

        private double? _paddleGreen3Time = 0;
        public double? PaddleGreen3Time
        {
            get => _paddleGreen3Time;
            private set
            {
                SetProperty(ref _paddleGreen3Time, value);
                OnPropertyChanged(nameof(PaddleGreen3Score));
                OnPropertyChanged(nameof(DidPaddleGreen3BuzzIn));
                OnPropertyChanged(nameof(DidPaddleGreen3NotBuzzIn));
            }
        }
        public double? PaddleGreen3Score { get => MaxTime - _paddleGreen2Time; }
        public bool DidPaddleGreen3BuzzIn { get => _paddleGreen3Time != null; }
        public bool DidPaddleGreen3NotBuzzIn { get => _paddleGreen3Time == null; }

        private double? _paddleGreen4Time = 0;
        public double? PaddleGreen4Time
        {
            get => _paddleGreen4Time;
            private set
            {
                SetProperty(ref _paddleGreen4Time, value);
                OnPropertyChanged(nameof(PaddleGreen4Score));
                OnPropertyChanged(nameof(DidPaddleGreen4BuzzIn));
                OnPropertyChanged(nameof(DidPaddleGreen4NotBuzzIn));
            }
        }
        public double? PaddleGreen4Score { get => MaxTime - _paddleGreen2Time; }
        public bool DidPaddleGreen4BuzzIn { get => _paddleGreen4Time != null; }
        public bool DidPaddleGreen4NotBuzzIn { get => _paddleGreen4Time == null; }
    }
}
