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
	public partial class LanguagePage : ContentPage
	{
        int changeLanguage = 0;
		public LanguagePage ()
		{
			InitializeComponent ();
            if (changeLanguage == 0)
            {
                PolskiButton.BackgroundColor = Color.Green;
                EnglishButton.BackgroundColor = Color.Black;
            }
            if (changeLanguage == 1)
            {
                PolskiButton.BackgroundColor = Color.Black;
                EnglishButton.BackgroundColor = Color.Green;
            }
		}

        private void PolskiButton_Clicked(object sender, EventArgs e)
        {
            ChangeToPolish();
            PolskiButton.BackgroundColor = Color.Green;
            EnglishButton.BackgroundColor = Color.Black;
        }

        private void EnglishButton_Clicked(object sender, EventArgs e)
        {
            ChangeToEnglish();
            PolskiButton.BackgroundColor = Color.Black;
            EnglishButton.BackgroundColor = Color.Green;
        }
        async void BackFromLanguageToSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
        private void ChangeToEnglish()
        {
            changeLanguage = 1;
            //NewRunPage.question.Text = "Do you want to stop exercise?";
            //question2.Text = "Are you sure to remove?";
            //avargeSpeed.Text = "Avg.Speed";
            //distance.Text = "Distance";
            //time.Text = "Time";
            //speed.Text = "Speed";
            //avargeSpeedHistory.Text = "Avg.Speed";
            //distanceHistory.Text = "Distance";
            //timeHistory.Text = "Time";
            //maxSpeedHistory.Text = "Max.Speed";
        }
        private void ChangeToPolish()
        {
            changeLanguage = 0;
            //question.Text = "Czy chcesz zakończyć trening?";
            //question2.Text = "Czy na pewno chcesz usunąć?";
            //avargeSpeed.Text = "Śr.Prędkość";
            //distance.Text = "Dystans";
            //time.Text = "Czas";
            //speed.Text = "Prędkość";
            //avargeSpeedHistory.Text = "Śr.Prędkość";
            //distanceHistory.Text = "Dystans";
            //timeHistory.Text = "Czas";
            //maxSpeedHistory.Text = "Maks.Prędkość";
        }
    }
}