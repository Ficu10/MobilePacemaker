using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilePacemaker
{
    public partial class App : Application
    {
        public static SQLiteHelper db;

        public static SQLiteHelper MyDatabase
        {
            
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "MyStore.db3"));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
