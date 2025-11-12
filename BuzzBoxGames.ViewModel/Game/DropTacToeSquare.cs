using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace BuzzBoxGames.ViewModel.Game
{
    /// <summary>
    /// Represents a square in Drop-Tac-Toe
    /// </summary>
    public partial class DropTacToeSquare : ObservableObject
    {
        private TicTacToeEnum _value = TicTacToeEnum.None;
        public TicTacToeEnum Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private bool _isWinner = false;
        public bool IsWinner
        {
            get => _isWinner;
            set => SetProperty(ref _isWinner, value);
        }
    }
}
