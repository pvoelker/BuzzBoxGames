using System;
using BuzzBoxGames.ViewModel.Services;

namespace BuzzBoxGamesApp.Services
{
    /// <summary>
    /// Message box service
    /// </summary>
    public class MessageBoxService(Page page) : IMessageBoxService
    {
        private readonly Page _page = page;

        /// <inheritdoc />
        public void ShowError(string message)
        {
            _page.DisplayAlertAsync("Error", message, "OK");
        }
    }
}
