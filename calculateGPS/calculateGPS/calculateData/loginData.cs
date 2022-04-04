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
        public byte[] obdModule = new byte[4];
        public byte[] unitFirm = new byte[4];
        public byte[] unitHard = new byte[4];
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
                    obdModule = eventData[23..27];
                    unitFirm = eventData[27..31];
                    unitHard = eventData[31..35];
                    
                    print_GPS_INFO();
                }
                else
                {
                    Console.WriteLine("Aldaatai Eventdata baina...");
                }
            }
        }

        public void print_GPS_INFO()
        {
            Console.WriteLine("OBD module version: " + BitConverter.ToString(obdModule).Replace("-", ""));
            Console.WriteLine("UNIT firmware version: " + BitConverter.ToString(unitFirm).Replace("-", ""));
            Console.WriteLine("UNIT hardware version: " + BitConverter.ToString(unitHard).Replace("-", ""));
            Console.WriteLine("\nstatus: " + status + "\nlatitude: " + latitude + "\nlongitude: "
                + longitude + "\nspeed: " + speed + "\ncourse: " + course + "\nhigh: " + high);
        }
    }
}
