using System;
namespace Cards.Models
{
    public class Rover
    {
        public int id { get; set; }
        public string name { get; set; }
        public string landing_date { get; set; }
        public string launch_date { get; set; }
        public string status { get; set; }

        /*
         * {"id":5,"name":"Curiosity","
         *  landing_date":"2012-08-06","launch_date":"2011-11-26","status":"active"}
         */
        public Rover()
        {
        }

    }
}
