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
	public partial class PaceChangePage : ContentPage
	{
        public static double zadaneTempo = 15.0;
		public PaceChangePage ()
		{
			InitializeComponent ();
            DaneTempo.Text = Math.Round(zadaneTempo, 1).ToString() + " KM/H";
        }
        private void UpButton_Clicked(object sender, EventArgs e)
        {
            zadaneTempo += 0.1;
            DaneTempo.Text = Math.Round(zadaneTempo, 1).ToString() + " KM/H";
        }
        private void DownButton_Clicked(object sender, EventArgs e)
        {
            if (zadaneTempo >= 0.1)
            {
                zadaneTempo -= 0.1;
                DaneTempo.Text = Math.Round(zadaneTempo, 1).ToString() + " KM/H";
            }
        }
        private void UpButton2_Clicked(object sender, EventArgs e)
        {
            zadaneTempo += 1;
            DaneTempo.Text = Math.Round(zadaneTempo, 1).ToString() + " KM/H";
        }
        private void DownButton2_Clicked(object sender, EventArgs e)
        {
            if (zadaneTempo >= 1)
            {
                zadaneTempo -= 1;
                DaneTempo.Text = Math.Round(zadaneTempo, 1).ToString() + " KM/H";
            }
        }

        //async void BackFromPaceToMainPage_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new MainPage());
        //}

    }
}