using System;

namespace BuzzBoxGames.ViewModel.Services
{
    /// <summary>
    /// Interface definition for message box service
    /// </summary>
    public interface IMessageBoxService
    {
        /// <summary>
        /// Show an error message
        /// </summary>
        /// <param name="message">Error message to show</param>
        void ShowError(string message);
    }
}
