using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace BuzzBoxGames.ViewModel.Game
{
    /// <summary>
    /// Represents a paddle on the reaction time game
    /// </summary>
    public class ReactionTimePaddle : ObservableObject
    {
        static public double MaxTime { get => 1000; }

        private double? _time = null;
        public double? Time
        {
            get => _time;
            set
            {
                SetProperty(ref _time, value);
                OnPropertyChanged(nameof(Score));
                OnPropertyChanged(nameof(BuzzedIn));
                OnPropertyChanged(nameof(NotBuzzedIn));
            }
        }
        public double? Score { get => MaxTime - _time; }
        public bool BuzzedIn { get => _time != null; }
        public bool NotBuzzedIn { get => _time == null; }

        private bool _isWinner = false;
        public bool IsWinner
        {
            get => _isWinner;
            set => SetProperty(ref _isWinner, value);
        }
    }
}
