using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateGPS.calculateData
{
    internal class Maintenance: calculateData
    {
        public Maintenance()
        {

        }
        public void calculateMaintenanceData(byte[] eventData)
        {
            if (eventData.Length >= 44)
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
