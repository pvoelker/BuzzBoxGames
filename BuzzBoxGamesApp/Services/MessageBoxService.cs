using System;
using BuzzBoxGames.ViewModel.Services;

namespace BuzzBoxGamesApp.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        private Page _page;

        public MessageBoxService(Page page)
        {
            _page = page;
        }

        public void ShowError(string message)
        {
            _page.DisplayAlert("Error", message, "OK");
        }
    }
}
