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
	public partial class FrequencyPage : ContentPage
	{
        public static int frequencyChanger = 1;
        public FrequencyPage ()
		{
			InitializeComponent ();
            if (frequencyChanger == 1)
            {
                OneHundredMButton.BackgroundColor = Color.Green;
                TwoHundredMButton.BackgroundColor = Color.Black;
                FiveHundredMButton.BackgroundColor = Color.Black;
                ThousandMButton.BackgroundColor = Color.Black;
            }
            if (frequencyChanger == 2)
            {
                OneHundredMButton.BackgroundColor = Color.Black;
                TwoHundredMButton.BackgroundColor = Color.Green;
                FiveHundredMButton.BackgroundColor = Color.Black;
                ThousandMButton.BackgroundColor = Color.Black;
            }
            if (frequencyChanger == 3)
            {
                OneHundredMButton.BackgroundColor = Color.Black;
                TwoHundredMButton.BackgroundColor = Color.Black;
                FiveHundredMButton.BackgroundColor = Color.Green;
                ThousandMButton.BackgroundColor = Color.Black;
            }
            if (frequencyChanger == 4)
            {
                OneHundredMButton.BackgroundColor = Color.Black;
                TwoHundredMButton.BackgroundColor = Color.Black;
                FiveHundredMButton.BackgroundColor = Color.Black;
                ThousandMButton.BackgroundColor = Color.Green;
            }

        }

            private void OneHundredMButton_Clicked(object sender, EventArgs e)
        {
            frequencyChanger = 1;
           
        }

        private void TwoHundredMButton_Clicked(object sender, EventArgs e)
        {
            frequencyChanger = 2;
          
        }

        private void FiveHundredMButton_Clicked(object sender, EventArgs e)
        {
            frequencyChanger = 3;
         
        }

        private void ThousandMButton_Clicked(object sender, EventArgs e)
        {
            frequencyChanger = 4;
          
        }
        async void BackFromFrequencyToSettings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}