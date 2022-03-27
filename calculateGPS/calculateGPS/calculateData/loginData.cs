using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using calculateGPS;

namespace calculateGPS.calculateData 
{
    internal class loginData : calculateData
    {

        public loginData()
        {

        }
        public void calculateLoginData(byte[] eventData)
        {
            if(eventData.Length >= 44)
            {
                Console.WriteLine("Event_DATA_UTC_TIME: " + UTC_TIME_CALC(eventData[^6..(eventData.Length)]));
                if (GPS_INFO_CALC(eventData[2..23]))
                {
                    print_GPS_INFO();
                }
                else
                {
                    Console.WriteLine("Aldaatai Eventdata baina...");
                }
            }
        }
    }
}
