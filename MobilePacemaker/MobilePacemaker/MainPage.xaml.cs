using Plugin.Geolocator;
using SQLite;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace MobilePacemaker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
       


        public MainPage()
        {
            InitializeComponent();
        }

      

       


      

    
        async void NewRunButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewRunPage());
        }

        async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
      
        async void HistoryButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());

        }
        async void PaceButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaceChangePage());
        }





        //void AcceptButton_Clicked(object sender, EventArgs e)
        //{
        //    if (acceptDeclineButtons == false)
        //    {

        //        licznik++;

        //        AddNewElementToHistory();

        //        //History runsHistory = new History()
        //        //{
        //        //    Date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm", new System.Globalization.CultureInfo("pl-PL")),
        //        //    Time = timeHours.ToString() + ":" + timeMinutes.ToString() + ":" + timeSeconds.ToString(),
        //        //    Distance = Math.Round(addDistanceSum, 2).ToString(),
        //        //    AvgSpeed = Math.Round((addDistanceSum / (addDistanceCounter / 1440)), 2).ToString(),
        //        //    MaxSpeed = maxSpeed.ToString(),
        //        //    ImageUrl = "goodRunner.jpg",
        //        //};

        //        //using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyStore.db3")))
        //        //{
        //        //    conn.CreateTable<History>();
        //        //    rowsAdded = conn.Insert(runsHistory);
        //        //    listViewHistory.ItemsSource = conn.Table<History>().ToList();
        //        //}






        //    }
        //    else
        //    {

        //        backButtonChanger = 4;

        //    }
        //}




    }
}

