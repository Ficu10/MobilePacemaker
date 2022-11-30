using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePacemaker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewRunPage : ContentPage
	{

        bool newRun = false;
        int changeLanguage = 0;
        double sumVelocity = 0;
        double prevVelocity = 0.0;
        double timeSeconds = 0;
        double timeMiliSeconds = 0;
        double timeMinutes = 0;
        double timeHours = 0;
        bool firstAvgSpped0 = false;
        bool firstDistance0 = false;
        bool firstPartDistance0 = false;
        double avgSpeedValue = 0;
        double sumOfSpeeds = 0;
        double sumOfSpeedsIter = 0;
        double maxSpeed = 0;
        const double MSTOKMH = 3.6;
        double addDistance = 0;
        double addDistanceSum = 0;
        double addDistanceSumToChange = 0;
        double addDistanceCounter = 0;
        const int THREADVALUE = 100;
        const double SPEEDTODISTANCE = 0.000027777;
        int licznik = 0;
        public static List<double> listOfSpeedsTest;
        public static List<double> listOfTimeTest;
        public NewRunPage ()
		{
			InitializeComponent ();
         
        }
        
        private async void GetLocation()
        {
            while (newRun == true)
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 20;

                var position = await locator.GetPositionAsync();
                addDistance = ((position.Speed * MSTOKMH) * SPEEDTODISTANCE);

               


                Device.BeginInvokeOnMainThread(() =>
                {
                    timeValue.Text = string.Format("{0}:{1}:{2}", timeHours.ToString().PadLeft(2, '0'), timeMinutes.ToString().PadLeft(2, '0'), timeSeconds.ToString().PadLeft(2, '0'));
                    listOfSpeedsTest.Add(addDistance);

                    speedValue.Text = Math.Round((position.Speed) * MSTOKMH, 0).ToString() + " km/h";
                    sumOfSpeeds += Math.Round((position.Speed) * MSTOKMH, 0);
                    sumOfSpeedsIter++;
                    if (Math.Round((position.Speed) * MSTOKMH, 0) > maxSpeed)
                    {
                        maxSpeed = Math.Round((position.Speed) * MSTOKMH, 0);
                    }

                    sumVelocity += (position.Speed * 3.6);
                    var faster = GetStreamFromFile("szybciej.mp3");
                    var slower = GetStreamFromFile("wolniej.mp3");
                    var goodPace = GetStreamFromFile("dobre tempo.mp3");
                    var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;

                    if (changeLanguage == 0)
                    {
                        distanceValue.Text = Math.Round(addDistanceSum, 2).ToString() + " km";

                        if (FrequencyPage.frequencyChanger == 1)
                        {
                            if (addDistanceSumToChange >= 0.1)
                            {
                                informationButton.IsVisible = true;
                                avgSpeedValue = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0);
                                avargeSpeedValue.Text = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0).ToString() + " km/h";
                                sumOfSpeeds = 0;
                                sumOfSpeedsIter = 0;
                                addDistanceSumToChange = addDistanceSumToChange - 0.1;
                                if (PaceChangePage.zadaneTempo == avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Green;
                                    informationButton.Text = "Dobre Tempo";
                                    audio.Load(goodPace);
                                    audio.Play();

                                }
                                if (PaceChangePage.zadaneTempo > avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Red;
                                    informationButton.Text = "Przyspiesz";
                                    audio.Load(faster);
                                    audio.Play();
                                }
                                if (PaceChangePage.zadaneTempo < avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Blue;
                                    informationButton.Text = "Zwolnij";
                                    audio.Load(slower);
                                    audio.Play();

                                }

                            }
                        }
                        if (FrequencyPage.frequencyChanger == 2)
                        {
                            if (addDistanceSumToChange >= 0.2)
                            {
                                informationButton.IsVisible = true;

                                avgSpeedValue = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0);
                                avargeSpeedValue.Text = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0).ToString() + " km/h";
                                sumOfSpeeds = 0;
                                sumOfSpeedsIter = 0;
                                addDistanceSumToChange = addDistanceSumToChange - 0.2;
                                if (PaceChangePage.zadaneTempo == avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Green;
                                    informationButton.Text = "Dobre Tempo";
                                    audio.Load(goodPace);
                                    audio.Play();

                                }
                                if (PaceChangePage.zadaneTempo > avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Red;
                                    informationButton.Text = "Przyspiesz";
                                    audio.Load(faster);
                                    audio.Play();
                                }
                                if (PaceChangePage.zadaneTempo < avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Blue;
                                    informationButton.Text = "Zwolnij";
                                    audio.Load(slower);
                                    audio.Play();

                                }
                            }
                        }
                        if (FrequencyPage.frequencyChanger == 3)
                        {
                            if (addDistanceSumToChange >= 0.5)
                            {
                                informationButton.IsVisible = true;
                                avgSpeedValue = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0);
                                avargeSpeedValue.Text = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0).ToString() + " km/h";
                                sumOfSpeeds = 0;
                                sumOfSpeedsIter = 0;
                                addDistanceSumToChange = addDistanceSumToChange - 0.5;
                                if (PaceChangePage.zadaneTempo == avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Green;
                                    informationButton.Text = "Dobre Tempo";
                                    audio.Load(goodPace);
                                    audio.Play();

                                }
                                if (PaceChangePage.zadaneTempo > avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Red;
                                    informationButton.Text = "Przyspiesz";
                                    audio.Load(faster);
                                    audio.Play();
                                }
                                if (PaceChangePage.zadaneTempo < avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Blue;
                                    informationButton.Text = "Zwolnij";
                                    audio.Load(slower);
                                    audio.Play();

                                }
                            }
                        }
                        if (FrequencyPage.frequencyChanger == 4)
                        {
                            if (addDistanceSumToChange >= 1)
                            {
                                informationButton.IsVisible = true;

                                avgSpeedValue = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0);
                                avargeSpeedValue.Text = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0).ToString() + " km/h";
                                sumOfSpeeds = 0;
                                sumOfSpeedsIter = 0;
                                addDistanceSumToChange = addDistanceSumToChange - 1;
                                if (PaceChangePage.zadaneTempo == avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Green;
                                    informationButton.Text = "Dobre Tempo";
                                    audio.Load(goodPace);
                                    audio.Play();

                                }
                                if (PaceChangePage.zadaneTempo > avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Red;
                                    informationButton.Text = "Przyspiesz";
                                    audio.Load(faster);
                                    audio.Play();
                                }
                                if (PaceChangePage.zadaneTempo < avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Blue;
                                    informationButton.Text = "Zwolnij";
                                    audio.Load(slower);
                                    audio.Play();

                                }
                            }
                        }

                    }

                    if (firstAvgSpped0 == false)
                    {
                        avargeSpeedValue.Text = "0 km/h";
                        firstAvgSpped0 = true;
                    }
                    if (firstDistance0 == false)
                    {
                        distanceValue.Text = "0 km/h";
                        firstDistance0 = true;
                    }
                    if (firstPartDistance0 == false)
                    {
                        firstPartDistance0 = true;
                    }

                });

                Thread.Sleep(THREADVALUE);

            }

        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("MobilePacemaker." + filename);

            return stream;
        }
        private void TimeCounter()
        {
            while (newRun == true)
            {
                timeMiliSeconds++;
                addDistanceSum += addDistance;
                addDistanceSumToChange += addDistance;
                addDistanceCounter++;
                if (timeMiliSeconds > 9)
                {
                    timeMiliSeconds = 0;
                    timeSeconds++;
                }
                if (timeSeconds > 59)
                {
                    timeSeconds = 0;
                    timeMinutes++;
                }
                if (timeMinutes > 59)
                {
                    timeMinutes = 0;
                    timeHours++;
                }
                listOfTimeTest.Add(timeMiliSeconds);
                Thread.Sleep(THREADVALUE);
            }
        }

        private void AllValuesZero()
        {
            changeLanguage = 0;
            sumVelocity = 0;
            prevVelocity = 0.0;
            timeSeconds = 0;
            timeMinutes = 0;
            timeHours = 0;
            addDistance = 0.0;
            addDistanceSum = 0.0;
            addDistanceSumToChange = 0.0;
            addDistanceCounter = 0;
            maxSpeed = 0.0;
            listOfSpeedsTest.Clear();
            listOfTimeTest.Clear();
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            newRun = true;
            Task modifyTaskOne = Task.Run(() => GetLocation());
            Task modifyTaskTwo = Task.Run(() => TimeCounter());
            StartButton.IsVisible = false;
        }

        async void AddNewElementToHistory()
        {
            await App.MyDatabase.CreateHistory(new History
            {
                Date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm", new System.Globalization.CultureInfo("pl-PL")),
                Time = string.Format("{0}:{1}:{2}", timeHours.ToString().PadLeft(2, '0'), timeMinutes.ToString().PadLeft(2, '0'), timeSeconds.ToString().PadLeft(2, '0')),
                Distance = Math.Round(addDistanceSum, 2).ToString() + " km",
                AvgSpeed = Math.Round((addDistanceSum / (addDistanceCounter / 1440)), 2).ToString() + " km/h",
                MaxSpeed = maxSpeed.ToString() + " km/h",
                ImageUrl = "goodRunner.jpg",
                ListOfSpeeds = listOfSpeedsTest,
                ListOfTime = listOfTimeTest

            });
        }

        async void BackFromNewRunToMainPage_Clicked(object sender, EventArgs e)
        {
            if (newRun == true)
            {
                bool result = await DisplayAlert("Koniec ćwiczenia", $"Czy chcesz zakończyć ćwiczenie?", "tak", "nie");
                if (result)
                {
                    StartButton.IsVisible = true;
                    AddNewElementToHistory();
                    AllValuesZero();
                    newRun = false;
                   

                    await Navigation.PushAsync(new MainPage());
                   
                }

            }
            else
            {
                var goodPace = GetStreamFromFile("dobre tempo.mp3");
                var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                audio.Load(goodPace);
                audio.Play();
                await Navigation.PushAsync(new MainPage());
            }

        }
    }
}