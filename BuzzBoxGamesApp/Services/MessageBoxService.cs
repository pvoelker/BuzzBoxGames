using System;
using BuzzBoxGames.ViewModel.Services;

namespace BuzzBoxGamesApp.Services
{
    /// <summary>
    /// Message box service
    /// </summary>
    public class MessageBoxService : IMessageBoxService
    {
        private Page _page;

        public MessageBoxService(Page page)
        {
            _page = page;
        }

        /// <inheritdoc />
        public void ShowError(string message)
        {
            _page.DisplayAlert("Error", message, "OK");
        }
    }
}
