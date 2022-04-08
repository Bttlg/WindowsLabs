using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateGPS.calculateData
{
    internal class GPS_INFO
    {
        public uint status, latitude, longitude;
        public ushort speed, course;
        public int high;
        public GPS_INFO()
        {

        }
        public GPS_INFO(uint status, uint latitude, uint longitude, ushort speed, ushort course, int high)
        {
            this.status = status;
            this.latitude = latitude;
            this.longitude = longitude;
            this.speed = speed; 
            this.course = course;
            this.high = high;
        }
    }
}
