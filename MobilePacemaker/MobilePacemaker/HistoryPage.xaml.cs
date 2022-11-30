using Microcharts;
using SkiaSharp;
using Microcharts.Forms;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePacemaker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistoryPage : ContentPage
	{
        bool isOpenHistory = false;

        List<ChartEntry> entries = new List<ChartEntry>();
        public HistoryPage ()
		{
			InitializeComponent();
            //LineChartXML.Chart = new LineChart() { Entries = (IEnumerable<ChartEntry>)entries };
		}
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                listViewHistory.ItemsSource = await App.MyDatabase.ReadHistory();
            }
            catch { }
        }
        async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as History;
            bool result = await DisplayAlert("Usuń", $"Czy chcesz usunąć bieg, który odbył się w {emp.Date}?", "tak", "nie");
            if (result)
            {


                await App.MyDatabase.DeleteHistory(emp);
                listViewHistory.ItemsSource = await App.MyDatabase.ReadHistory();

                //using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyStore.db3")))
                //{
                //    conn.Delete(emp);
                //}

                //listViewHistory.ItemsSource = await App.MyDatabase.ReadHistory();
            }
        }
        History lastSelection;
        private void ListViewHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as History;
            var listahistorii = e.CurrentSelection[0] as History;
            distanceHistoryValue.Text = listahistorii.Distance;
            timeHistoryValue.Text = listahistorii.Time;
            avargeSpeedHistoryValue.Text = listahistorii.AvgSpeed;
            speedHistoryValue.Text = listahistorii.MaxSpeed;
            //foreach (var item in NewRunPage.listOfSpeedsTest)
            //{

            //    entries.Add(new ChartEntry(Convert.ToSingle(item)));
            //}
            listViewHistory.IsVisible = false;
            isOpenHistory = true;
        }

        async void BackFromHistoryToMainPage_Clicked(object sender, EventArgs e)
        {
            if (isOpenHistory == false)
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                isOpenHistory = false;
                await Navigation.PushAsync(new HistoryPage());
            }
        }
    }
}