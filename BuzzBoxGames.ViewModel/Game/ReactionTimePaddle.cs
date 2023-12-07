using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzBoxGames.ViewModel.Game
{
    public class ReactionTimePaddle : ObservableObject
    {
        public double MaxTime { get => 1000; }

        private double? _time = 0;
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
