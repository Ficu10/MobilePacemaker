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
        bool newRun = false;
        int changeLanguage = 0;
        int backButtonChanger = 0;
        double sumVelocity = 0;
        double prevVelocity = 0.0;
        double avgVelocity = 0.0;
        int timeSeconds = 0;
        int timeMiliSeconds = 0;
        int timeMinutes = 0;
        int timeHours = 0;
        bool firstAvgSpped0 = false;
        bool firstDistance0 = false;
        bool firstPartDistance0 = false;
        int frequencyChanger = 1;
        double zadaneTempo = 15.0;
        double avgSpeedValue = 0;
        double sumOfSpeeds = 0;
        double sumOfSpeedsIter = 0;
        double maxSpeed = 0;
        const double MSTOKMH = 3.6;
        bool acceptDeclineButtons = false;
        double addDistance = 0;
        double addDistanceSum = 0;
        double addDistanceSumToChange = 0;
        double addDistanceCounter = 0;
        const int THREADVALUE = 100;
        const double SPEEDTODISTANCE = 0.000027777;
        int licznik = 0;


        public MainPage()
        {
            InitializeComponent();
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

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("App3." + filename);

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

                Thread.Sleep(THREADVALUE);
            }
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
                    //timeValue.Text = timeHours.ToString() + timeMinutes.ToString() + timeSeconds.ToString();

                    speedValue.Text = Math.Round((position.Speed) * MSTOKMH, 0).ToString() + "km/h";
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
                        distanceValue.Text = Math.Round(addDistanceSum, 2).ToString() + "km";
                        avgVelocity = sumVelocity - prevVelocity;

                        if (frequencyChanger == 1)
                        {
                            if (addDistanceSumToChange >= 0.1)
                            {
                                informationButton.IsVisible = true;
                                avgSpeedValue = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0);
                                avargeSpeedValue.Text = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0).ToString() + " km/h";
                                sumOfSpeeds = 0;
                                sumOfSpeedsIter = 0;
                                addDistanceSumToChange = addDistanceSumToChange - 0.1;
                                if (zadaneTempo == avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Green;
                                    informationButton.Text = "Dobre Tempo";
                                    audio.Load(goodPace);
                                    audio.Play();

                                }
                                if (zadaneTempo > avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Red;
                                    informationButton.Text = "Przyspiesz";
                                    audio.Load(faster);
                                    audio.Play();
                                }
                                if (zadaneTempo < avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Blue;
                                    informationButton.Text = "Zwolnij";
                                    audio.Load(slower);
                                    audio.Play();

                                }

                            }
                        }
                        if (frequencyChanger == 2)
                        {
                            if (addDistanceSumToChange >= 0.2)
                            {
                                informationButton.IsVisible = true;

                                avgSpeedValue = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0);
                                avargeSpeedValue.Text = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0).ToString() + " km/h";
                                sumOfSpeeds = 0;
                                sumOfSpeedsIter = 0;
                                addDistanceSumToChange = addDistanceSumToChange - 0.2;
                                if (zadaneTempo == avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Green;
                                    informationButton.Text = "Dobre Tempo";
                                    audio.Load(goodPace);
                                    audio.Play();

                                }
                                if (zadaneTempo > avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Red;
                                    informationButton.Text = "Przyspiesz";
                                    audio.Load(faster);
                                    audio.Play();
                                }
                                if (zadaneTempo < avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Blue;
                                    informationButton.Text = "Zwolnij";
                                    audio.Load(slower);
                                    audio.Play();

                                }
                            }
                        }
                        if (frequencyChanger == 3)
                        {
                            if (addDistanceSumToChange >= 0.5)
                            {
                                informationButton.IsVisible = true;
                                avgSpeedValue = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0);
                                avargeSpeedValue.Text = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0).ToString() + " km/h";
                                sumOfSpeeds = 0;
                                sumOfSpeedsIter = 0;
                                addDistanceSumToChange = addDistanceSumToChange - 0.5;
                                if (zadaneTempo == avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Green;
                                    informationButton.Text = "Dobre Tempo";
                                    audio.Load(goodPace);
                                    audio.Play();

                                }
                                if (zadaneTempo > avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Red;
                                    informationButton.Text = "Przyspiesz";
                                    audio.Load(faster);
                                    audio.Play();
                                }
                                if (zadaneTempo < avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Blue;
                                    informationButton.Text = "Zwolnij";
                                    audio.Load(slower);
                                    audio.Play();

                                }
                            }
                        }
                        if (frequencyChanger == 4)
                        {
                            if (addDistanceSumToChange >= 1)
                            {
                                informationButton.IsVisible = true;

                                avgSpeedValue = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0);
                                avargeSpeedValue.Text = Math.Round((sumOfSpeeds / sumOfSpeedsIter), 0).ToString() + " km/h";
                                sumOfSpeeds = 0;
                                sumOfSpeedsIter = 0;
                                addDistanceSumToChange = addDistanceSumToChange - 1;
                                if (zadaneTempo == avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Green;
                                    informationButton.Text = "Dobre Tempo";
                                    audio.Load(goodPace);
                                    audio.Play();

                                }
                                if (zadaneTempo > avgSpeedValue)
                                {
                                    informationButton.BackgroundColor = Color.Red;
                                    informationButton.Text = "Przyspiesz";
                                    audio.Load(faster);
                                    audio.Play();
                                }
                                if (zadaneTempo < avgSpeedValue)
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

        private double CalcVelocity(double meters, double seconds)
        {
            return meters / seconds;
        }

        private void MenuDisappear()
        {
            NewRunButton.IsVisible = false;
            SettingsButton.IsVisible = false;
            ExitButton.IsVisible = false;
            HistoryButton.IsVisible = false;
            NewRunButton.IsEnabled = false;
            SettingsButton.IsEnabled = false;
            ExitButton.IsEnabled = false;
            HistoryButton.IsEnabled = false;
            Mobile.IsVisible = false;
            Pacemaker.IsVisible = false;

        }
        private void MenuAppear()
        {
            NewRunButton.IsVisible = true;
            SettingsButton.IsVisible = true;
            ExitButton.IsVisible = true;
            HistoryButton.IsVisible = true;
            NewRunButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;
            ExitButton.IsEnabled = true;
            HistoryButton.IsEnabled = true;
            Mobile.IsVisible = true;
            Pacemaker.IsVisible = true;
        }
        private void NewRunAppear()
        {
            informationButton.IsEnabled = true;
            informationButton.IsVisible = true;
            StartButton.IsVisible = true;
            StartButton.IsEnabled = true;
            ValuesOfRunAppear();
        }
        private void NewRunDisappear()
        {
            informationButton.IsEnabled = false;
            informationButton.IsVisible = false;
            StartButton.IsEnabled = false;
            StartButton.IsVisible = false;
            newRun = false;
            ValuesOfRunDisappear();

        }

        private void ValuesOfRunAppear()
        {
            avargeSpeed.IsVisible = true;
            speed.IsVisible = true;
            time.IsVisible = true;
            BackButton.IsEnabled = true;
            BackButton.IsVisible = true;
            distance.IsVisible = true;
            distanceValue.IsVisible = true;
            avargeSpeedValue.IsVisible = true;
            speedValue.IsVisible = true;
            timeValue.IsVisible = true;
        }
        private void ValuesOfRunDisappear()
        {
            avargeSpeedValue.IsVisible = false;
            speed.IsVisible = false;
            time.IsVisible = false;
            BackButton.IsEnabled = false;
            BackButton.IsVisible = false;
            distance.IsVisible = false;
            distanceValue.IsVisible = false;
            avargeSpeedValue.IsVisible = false;
            speedValue.IsVisible = false;
            timeValue.IsVisible = false;

        }
        private void SettingsAppear()
        {
            MenuDisappear();
            LanguageButton.IsEnabled = true;
            LanguageButton.IsVisible = true;
            SignalsFrequencyButton.IsEnabled = true;
            SignalsFrequencyButton.IsVisible = true;
            BackButton.IsEnabled = true;
            BackButton.IsVisible = true;
        }
        private void SettingsDisappear()
        {

            MenuAppear();
            LanguageButton.IsEnabled = false;
            LanguageButton.IsVisible = false;
            SignalsFrequencyButton.IsEnabled = false;
            SignalsFrequencyButton.IsVisible = false;
            BackButton.IsEnabled = false;
            BackButton.IsVisible = false;

        }
        private void LanguageAppear()
        {
            SettingsDisappear();
            MenuDisappear();
            PolskiButton.IsEnabled = true;
            PolskiButton.IsVisible = true;
            EnglishButton.IsEnabled = true;
            EnglishButton.IsVisible = true;
            BackButton.IsEnabled = true;
            BackButton.IsVisible = true;
        }
        private void LanguageDisappear()
        {
            SettingsAppear();
            PolskiButton.IsEnabled = false;
            PolskiButton.IsVisible = false;
            EnglishButton.IsEnabled = false;
            EnglishButton.IsVisible = false;
            BackButton.IsEnabled = false;
            BackButton.IsVisible = false;
        }
        private void SignalsFrequencyAppear()
        {
            SettingsDisappear();
            MenuDisappear();
            OneHundredMButton.IsEnabled = true;
            OneHundredMButton.IsVisible = true;
            TwoHundredMButton.IsEnabled = true;
            TwoHundredMButton.IsVisible = true;
            FiveHundredMButton.IsEnabled = true;
            FiveHundredMButton.IsVisible = true;
            ThousandMButton.IsEnabled = true;
            ThousandMButton.IsVisible = true;
            BackButton.IsEnabled = true;
            BackButton.IsVisible = true;
        }
        private void SignalsFrequencyDisappear()
        {
            SettingsAppear();
            OneHundredMButton.IsEnabled = false;
            OneHundredMButton.IsVisible = false;
            TwoHundredMButton.IsEnabled = false;
            TwoHundredMButton.IsVisible = false;
            FiveHundredMButton.IsEnabled = false;
            FiveHundredMButton.IsVisible = false;
            ThousandMButton.IsEnabled = false;
            ThousandMButton.IsVisible = false;
        }
        private void AllValuesZero()
        {
            changeLanguage = 0;
            backButtonChanger = 0;
            sumVelocity = 0;
            prevVelocity = 0.0;
            avgVelocity = 0.0;
            timeSeconds = 0;
            timeMinutes = 0;
            timeHours = 0;
            addDistance = 0.0;
            addDistanceSum = 0.0;
            addDistanceSumToChange = 0.0;
            addDistanceCounter = 0;
            maxSpeed = 0.0;
        }

        private void NewRunButton_Clicked(object sender, EventArgs e)
        {
            MenuDisappear();
            NewRunAppear();
        }

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            MenuAppear();
            NewRunDisappear();
            SettingsDisappear();
            if (backButtonChanger == 4)
            {
                MenuAppear();
                BackButton.IsVisible = false;
                BackButton.IsEnabled = false;
                listViewHistory.IsVisible = false;
                DeleteHistoryButton.IsVisible = false;
            }
            if (backButtonChanger == 3)
            {
                MenuDisappear();
                question.IsVisible = true;
                AcceptButton.IsVisible = true;
                DeclineButton.IsVisible = true;
                avargeSpeed.IsVisible = false;
            }
            if (backButtonChanger == 1)
            {
                LanguageDisappear();
                SettingsAppear();
                backButtonChanger = 0;
            }
            if (backButtonChanger == 2)
            {
                SignalsFrequencyDisappear();
                SettingsAppear();
                backButtonChanger = 0;
            }
            if (backButtonChanger == 5)
            {
                MenuAppear();
                DaneTempo.IsVisible = false;
                UpButton.IsVisible = false;
                UpButton.IsEnabled = false;
                DownButton.IsVisible = false;
                DownButton.IsEnabled = false;
                UpButton2.IsVisible = false;
                UpButton2.IsEnabled = false;
                DownButton2.IsVisible = false;
                DownButton2.IsEnabled = false;
            }
            if (backButtonChanger == 6)
            {
                MenuDisappear();
                BackButton.IsVisible = true;
                BackButton.IsEnabled = true;
                listViewHistory.IsVisible = true;
                listViewHistory.IsEnabled = true;
                DeleteHistoryButton.IsVisible = false;
                listViewHistory.IsEnabled = true;
                backButtonChanger = 4;
            }
        }
        private void SettingsButton_Clicked(object sender, EventArgs e)
        {
            MenuDisappear();
            SettingsAppear();
        }
        private void SignalsFrequencyButton_Clicked(object sender, EventArgs e)
        {
            MenuDisappear();
            SettingsDisappear();
            SignalsFrequencyAppear();
            backButtonChanger = 2;
        }
        private void LanguageButton_Clicked(object sender, EventArgs e)
        {
            MenuDisappear();
            SettingsDisappear();
            LanguageAppear();
            backButtonChanger = 1;
        }

        private void OneHundredMButton_Clicked(object sender, EventArgs e)
        {
            frequencyChanger = 1;
            OneHundredMButton.BackgroundColor = Color.Green;
            TwoHundredMButton.BackgroundColor = Color.Black;
            FiveHundredMButton.BackgroundColor = Color.Black;
            ThousandMButton.BackgroundColor = Color.Black;
        }

        private void TwoHundredMButton_Clicked(object sender, EventArgs e)
        {
            frequencyChanger = 2;
            OneHundredMButton.BackgroundColor = Color.Black;
            TwoHundredMButton.BackgroundColor = Color.Green;
            FiveHundredMButton.BackgroundColor = Color.Black;
            ThousandMButton.BackgroundColor = Color.Black;
        }

        private void FiveHundredMButton_Clicked(object sender, EventArgs e)
        {
            frequencyChanger = 3;
            OneHundredMButton.BackgroundColor = Color.Black;
            TwoHundredMButton.BackgroundColor = Color.Black;
            FiveHundredMButton.BackgroundColor = Color.Green;
            ThousandMButton.BackgroundColor = Color.Black;
        }

        private void ThousandMButton_Clicked(object sender, EventArgs e)
        {
            frequencyChanger = 4;
            OneHundredMButton.BackgroundColor = Color.Black;
            TwoHundredMButton.BackgroundColor = Color.Black;
            FiveHundredMButton.BackgroundColor = Color.Black;
            ThousandMButton.BackgroundColor = Color.Green;
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
        private void StartButton_Clicked(object sender, EventArgs e)
        {
            newRun = true;
            Task modifyTaskOne = Task.Run(() => GetLocation());
            Task modifyTaskTwo = Task.Run(() => TimeCounter());

            StartButton.Text = "Stop";
            backButtonChanger = 3;
            StartButton.IsVisible = false;
            acceptDeclineButtons = false;
            distanceValue.IsVisible = true;
            timeValue.IsVisible = true;
            speedValue.IsVisible = true;
            avargeSpeedValue.IsVisible = true;

        }
        private void ChangeToEnglish()
        {
            changeLanguage = 1;
            question.Text = "Do you want to stop exercise?";
            question2.Text = "Are you sure to remove?";
            avargeSpeed.Text = "Avg.Speed";
            distance.Text = "Distance";
            time.Text = "Time";
            speed.Text = "speed";
        }
        private void ChangeToPolish()
        {
            changeLanguage = 0;
            question.Text = "Czy chcesz zakończyć trening?";
            question2.Text = "Czy na pewno chcesz usunąć?";
            avargeSpeed.Text = "Śr.Prędkość";
            distance.Text = "Dystans";
            time.Text = "Czas";
            speed.Text = "Prędkość";
        }
        async void AcceptButton_Clicked(object sender, EventArgs e)
        {
            if (acceptDeclineButtons == false)
            {
                NewRunDisappear();
                MenuAppear();
                question.IsVisible = false;
                AcceptButton.IsVisible = false;
                DeclineButton.IsVisible = false;
                StartButton.Text = "Start";
                listViewHistory.IsVisible = false;
                firstDistance0 = false;
                firstAvgSpped0 = false;
                firstPartDistance0 = false;
                informationButton.IsVisible = false;
                licznik++;

                //AddNewElementToHistory();

                History runsHistory = new History()
                {
                    Date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm", new System.Globalization.CultureInfo("pl-PL")),
                    Time = timeHours.ToString() + ":" + timeMinutes.ToString() + ":" + timeSeconds.ToString(),
                    Distance = Math.Round(addDistanceSum, 2).ToString(),
                    AvgSpeed = Math.Round((addDistanceSum / (addDistanceCounter / 7200)), 2).ToString(),
                    MaxSpeed = maxSpeed.ToString(),
                    ImageUrl = "goodRunner.jpg",
                };
                using (SQLiteConnection conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyStore.db3")))
                {
                    conn.CreateTable<History>();
                    int rowsAdded = conn.Insert(runsHistory);
                }

                listViewHistory.ItemsSource = await App.MyDatabase.ReadHistory();


            }
            else
            {
                question.IsVisible = false;
                AcceptButton.IsVisible = false;
                DeclineButton.IsVisible = false;
                MenuDisappear();
                BackButton.IsVisible = true;
                BackButton.IsEnabled = true;
                listViewHistory.IsVisible = true;
                listViewHistory.IsEnabled = true;
                backButtonChanger = 4;
                acceptDeclineButtons = true;
            }
        }

        //async void AddNewElementToHistory()
        //{
        //    await App.MyDatabase.CreateHistory(new History
        //    {
        //        Date = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm", new System.Globalization.CultureInfo("pl-PL")),
        //        Time = timeHours.ToString() + ":" + timeMinutes.ToString() + ":" + timeSeconds.ToString(),
        //        Distance = Math.Round(addDistanceSum, 2).ToString(),
        //        AvgSpeed = Math.Round((addDistanceSum / (addDistanceCounter / 7200)), 2).ToString(),
        //        MaxSpeed = maxSpeed.ToString(),
        //        ImageUrl = "goodRunner.jpg",
        //        Id = licznik

        //    });
        //    await Navigation.PopAsync();
        //}
        private void DeclineButton_Clicked(object sender, EventArgs e)
        {
            if (acceptDeclineButtons == false)
            {
                NewRunAppear();
                question.IsVisible = false;
                AcceptButton.IsVisible = false;
                DeclineButton.IsVisible = false;
                StartButton.IsVisible = false;
                StartButton.IsEnabled = false;
            }
            else
            {
                question.IsVisible = false;
                AcceptButton.IsVisible = false;
                DeclineButton.IsVisible = false;
                BackButton.IsVisible = true;
                DeleteHistoryButton.IsVisible = true;
                ValuesOfRunAppear();
            }

        }
        private void HistoryButton_Clicked(object sender, EventArgs e)
        {
            MenuDisappear();
            BackButton.IsVisible = true;
            BackButton.IsEnabled = true;
            listViewHistory.IsVisible = true;
            listViewHistory.IsEnabled = true;
            backButtonChanger = 4;
            acceptDeclineButtons = true;

        }

        private void ExitButton_Clicked(object sender, EventArgs e)
        {
            MenuDisappear();
            DaneTempo.IsVisible = true;
            UpButton.IsVisible = true;
            UpButton.IsEnabled = true;
            DownButton.IsVisible = true;
            DownButton.IsEnabled = true;
            UpButton2.IsVisible = true;
            UpButton2.IsEnabled = true;
            DownButton2.IsVisible = true;
            DownButton2.IsEnabled = true;
            backButtonChanger = 5;
            BackButton.IsVisible = true;
            BackButton.IsEnabled = true;
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

        private void DeleteHistoryButton_Clicked(object sender, EventArgs e)
        {
            BackButton.IsVisible = false;
            question.IsVisible = true;
            AcceptButton.IsVisible = true;
            DeclineButton.IsVisible = true;
            DeleteHistoryButton.IsVisible = false;
            ValuesOfRunDisappear();
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
            }
        }

        private void ListViewHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValuesOfRunAppear();

            var listahistorii = e.CurrentSelection[0] as History;
            distanceValue.Text = listahistorii.Distance;
            timeValue.Text = listahistorii.Time;
            avargeSpeedValue.Text = listahistorii.AvgSpeed;
            speedValue.Text = listahistorii.MaxSpeed;
            DeleteHistoryButton.IsVisible = true;
            listViewHistory.IsVisible = false;
            backButtonChanger = 6;
        }
    }
}

