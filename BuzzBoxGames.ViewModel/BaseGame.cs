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

        public BaseGame()
        {
            _api.DisconnectionDetected += _api_DisconnectionDetected;

            StartGame = new RelayCommand(() =>
            {
                if (_api.Connect())
                {
                    ResetGame();
                }
                else
                {
                    const string errMsg = "Unable to connect to a quiz box, unable to start game...";

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
                });
            });
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
