using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using calculateGPS;
using GPSlibrary.exception;

namespace calculateGPS.calculateData 
{
    internal class loginData 
    {
        public byte[] obdModule = new byte[4];
        public byte[] unitFirm = new byte[4];
        public byte[] unitHard = new byte[4];
        eventData objEventData;
        public loginData(eventData param)
        {
            this.objEventData = param;
        }
        public void calculateLoginData(byte[] eventData)
        {
            if(eventData.Length >= 44)
            {
                Console.WriteLine("Event_DATA_UTC_TIME: " + objEventData.UTC_TIME_CALC(eventData[^6..(eventData.Length)]));
                if (objEventData.GPS_INFO_CALC(eventData[2..23]))
                {
                    obdModule = eventData[23..27];
                    unitFirm = eventData[27..31];
                    unitHard = eventData[31..35];
                    print_LOGINGPS_INFO();
                }
                else
                {
                    Console.WriteLine("Aldaatai Eventdata baina...");
                }
            }
        }

        public void print_LOGINGPS_INFO()
        {
            Console.WriteLine("OBD module version: " + BitConverter.ToString(obdModule).Replace("-", ""));
            Console.WriteLine("UNIT firmware version: " + BitConverter.ToString(unitFirm).Replace("-", ""));
            Console.WriteLine("UNIT hardware version: " + BitConverter.ToString(unitHard).Replace("-", ""));
            objEventData.print_GPS_INFO();
        }
    }
}
