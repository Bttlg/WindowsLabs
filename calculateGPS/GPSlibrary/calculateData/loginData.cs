using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using calculateGPS;
using GPSlibrary.exception;

namespace calculateGPS.calculateData 
{
    public class loginData 
    {
        public byte[] obdModule = new byte[4];
        public byte[] unitFirm = new byte[4];
        public byte[] unitHard = new byte[4];
        eventData objEventData;
        public loginData(eventData param)
        {
            this.objEventData = param;
        }
        public void calculateLoginData(ushort checkCode, byte[] eventData)
        {
            if(checkCode != 4097)
            {
                throw new WrongUnitCodeException("EventCode таарахгүй байна...");
            }
                Console.WriteLine("Event_DATA_UTC_TIME: " + objEventData.UTC_TIME_CALC(eventData[^6..(eventData.Length)]));
                //GPS_INFO нь амжилттай задарчих юм бол true утга буцаах бөгөөд үүний дараа үлдсэн хэсгээ хадгалж авна...
                if (objEventData.GPS_INFO_CALC(eventData[0..21]))
                {
                    obdModule = eventData[21..25];
                    unitFirm = eventData[25..29];
                    unitHard = eventData[29..33];
                    print_LOGINGPS_INFO();
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
