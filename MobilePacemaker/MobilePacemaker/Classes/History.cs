using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobilePacemaker
{
    public class History
    {
        
        public string Date { get; set; }

        public string AvgSpeed { get; set; }

        public string Time { get; set; }

        public string Distance { get; set; }

        public string MaxSpeed { get; set; }

        public string ImageUrl { get; set; }


        public int Id { get; set; }

    }
}
