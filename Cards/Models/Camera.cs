using System;
namespace Cards.Models
{
    public class Camera
    {
        public int id { get; set; }
        public string name { get; set; }
        public int rover_id { get; set; }
        public string full_name { get; set; }

        // {"id":22,"name":"MAST","rover_id":5,"full_name":"Mast Camera"}
        public Camera()
        {
        }

    }
}
