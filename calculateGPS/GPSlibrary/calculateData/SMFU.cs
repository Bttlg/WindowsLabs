using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPSlibrary.exception;

namespace calculateGPS.calculateData
{
    public class SMFU
    {
        eventData objEventData;
        public SMFU(eventData param)
        {
            this.objEventData = param;
        }

        public void calculateSMFUdata(ushort checkCode, byte[] eventData)
        {
            Console.WriteLine("Event_DATA_RTC_TIME: " + objEventData.UTC_TIME_CALC(eventData[0..6]));
            if(checkCode == 8196)
            {
                if (objEventData.GPS_INFO_CALC(eventData[6..27]))
                {
                    objEventData.print_GPS_INFO();
                }
            }else
            {
                throw new WrongUnitCodeException("EventCode таарахгүй байна...");
            }
        }
    }
}
