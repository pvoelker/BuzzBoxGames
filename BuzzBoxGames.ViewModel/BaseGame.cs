using BriansUsbQuizBoxApi;
using BuzzBoxGames.ViewModel.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace BuzzBoxGames.ViewModel.Game
{
    /// <summary>
    /// Base class that implements basic connect/disconnect to a quiz box
    /// </summary>
    public abstract class BaseGame : ObservableObject
    {
        protected readonly QuizBoxApi _api = new QuizBoxApi(new QuizBoxCoreApi());

        private IDispatcherTimer? _countdownTimer;

        public BaseGame(bool autoRestart)
        {
            AutoRestart = autoRestart;

            _api.DisconnectionDetected += _api_DisconnectionDetected;

            StartGame = new RelayCommand(() =>
            {
                if (_api.Connect())
                {
                    ResetGame();
                }
                else
                {
                    const string errMsg = "Unable to connect to a quiz box, cannot start game...";

                    if (MessageBoxService != null)
                    {
                        MessageBoxService.ShowError(errMsg);
                    }
                    else
                    {
                        throw new InvalidOperationException(errMsg);
                    }
                }
            });

            EndGame = new RelayCommand(() =>
            {
                Task.Run(async () =>
                {
                    _api.Reset();

                    await Task.Delay(50);

                    _api.Disconnect();

                    if(AutoRestart)
                    {
                        BeginAutoRestart();
                    }
                });
            });

            if (Application.Current != null)
            {
                _countdownTimer = Application.Current.Dispatcher.CreateTimer();
                _countdownTimer.Interval = TimeSpan.FromSeconds(1);
                _countdownTimer.IsRepeating = true;
                _countdownTimer.Tick += (s, e) =>
                {
                    if(RestartCountdown > 0)
                    {
                        RestartCountdown--;
                    }
                    if(RestartCountdown == 0)
                    {
                        if (_countdownTimer != null)
                        {
                            _countdownTimer.Stop();
                        }

                        StartGame.Execute(null);
                    }
                };
            }
        }

        protected void BeginAutoRestart()
        {
            RestartCountdown = 10;

            if (_countdownTimer != null)
            {
                _countdownTimer.Start();
            }
        }

        private void _api_DisconnectionDetected(object? sender, DisconnectionEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                const string errMsg = "Quiz box has been disconnected, cancelling game...";

                if (MessageBoxService != null)
                {
                    MessageBoxService.ShowError(errMsg);
                }
                else
                {
                    throw new InvalidOperationException(errMsg);
                }

                AbortGame();
            });
        }

        /// <summary>
        /// Reset game states
        /// </summary>
        protected abstract void ResetGame();

        /// <summary>
        /// Abort game
        /// </summary>
        protected abstract void AbortGame();

        #region Commands

        /// <summary>
        /// Connect to quiz box and reset the game
        /// </summary>
        public IRelayCommand StartGame { get; protected set; }

        /// <summary>
        /// Disconnect from quiz box
        /// </summary>
        public IRelayCommand EndGame { get; private set; }

        #endregion

        private bool _autoRestart;
        public bool AutoRestart
        {
            get => _autoRestart;
            private set => SetProperty(ref _autoRestart, value);
        }

        private int _restartCountdown = 0;
        public int RestartCountdown
        {
            get => _restartCountdown;
            private set
            {
                SetProperty(ref _restartCountdown, value);
                OnPropertyChanged(nameof(HasRestartCountdown));
            }
        }
        public bool HasRestartCountdown
        {
            get => _restartCountdown > 0;
        }

        private IMessageBoxService? _messageBoxService = null;
        /// <summary>
        /// Message box service
        /// </summary>
        public IMessageBoxService? MessageBoxService
        {
            get => _messageBoxService;
            set => SetProperty(ref _messageBoxService, value);
        }
    }
}
