using System;
using System.Collections.Generic;
using System.Text;

namespace BuzzBoxGames.ViewModel.Services
{
    public interface IMessageBoxService
    {
        void ShowError(string message);

        //bool PromptToContinue(string messsage);
    }
}
