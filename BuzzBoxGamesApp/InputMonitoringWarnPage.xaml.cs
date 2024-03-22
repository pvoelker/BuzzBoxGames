namespace BuzzBoxGamesApp
{
	public partial class InputMonitoringWarnPage : ContentPage
	{
		public ContentPage _PageToForwardTo;

		public InputMonitoringWarnPage(ContentPage pageToForwardTo)
		{
			ArgumentNullException.ThrowIfNull(pageToForwardTo);

			_PageToForwardTo = pageToForwardTo;

			InitializeComponent();
		}

		private async void ContinueButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(_PageToForwardTo);
		}
	}
}