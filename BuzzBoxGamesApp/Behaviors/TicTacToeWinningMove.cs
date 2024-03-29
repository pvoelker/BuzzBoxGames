﻿using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System;

namespace BuzzBoxGamesApp.Behaviors
{
    public class TicTacToeWinningMove : Behavior<ContentPage>
    {
        private IAudioPlayer? _audioPlayer = null;

        public static readonly BindableProperty SoundNameProperty =
            BindableProperty.Create(nameof(SoundName), typeof(string), typeof(TicTacToeWinningMove), propertyChanged: async (b, o, n) =>
            {
                if (b is TicTacToeWinningMove bb)
                {
                    if (bb._audioPlayer != null)
                    {
                        bb._audioPlayer.Dispose();
                        bb._audioPlayer = null;
                    }

                    if (n != null)
                    {
                        bb._audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync((string)n));
                    }
                }
            });

        public string SoundName
        {
            get => (string)GetValue(SoundNameProperty);
            set => SetValue(SoundNameProperty, value);
        }

        protected override void OnAttachedTo(ContentPage control)
        {
            var context = control.BindingContext as BuzzBoxGames.ViewModel.Game.DropTacToe;

            if (context != null)
            {
                context.WinningMove = new RelayCommand(() =>
                {
                    if (_audioPlayer != null)
                    {
                        if (_audioPlayer.IsPlaying)
                        {
                            _audioPlayer.Stop();
                        }

                        _audioPlayer.Play();
                    }
                });
            }

            base.OnAttachedTo(control);
        }

        protected override void OnDetachingFrom(ContentPage control)
        {
            var context = control.BindingContext as BuzzBoxGames.ViewModel.Game.DropTacToe;

            if (context != null)
            {
                context.WinningMove = null;
            }

            base.OnDetachingFrom(control);
        }
    }
}
