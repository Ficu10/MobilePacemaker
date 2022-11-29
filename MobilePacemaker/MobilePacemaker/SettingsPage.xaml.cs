using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePacemaker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
		}

        async void BackFromSettingsToMainPage_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync (new MainPage ());
        }

        async void SignalsFrequencyButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FrequencyPage());
        }
        async void LanguageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LanguagePage());
        }
    }
}