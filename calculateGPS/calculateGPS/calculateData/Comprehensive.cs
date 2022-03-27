﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculateGPS.calculateData
{
    internal class Comprehensive: calculateData
    {
        public Comprehensive()
        {

        }
        public void calculateCompData(byte[] eventData)
        {
            uint checkCode = BitConverter.ToUInt16(eventData[0..2]);
            Console.WriteLine("Event_DATA_UTC_TIME: " + UTC_TIME_CALC(eventData[2..7]));
            if(checkCode == 8193)
            {
                Console.WriteLine("Realtime Upload: ");
                if (GPS_INFO_CALC(eventData[11..32]))
                {
                    print_GPS_INFO();
                }
                else
                {
                    Console.WriteLine("Aldaatai Eventdata baina...");
                }
            }
            else
            {
                Console.WriteLine("Historical Supplement: ");
                if (GPS_INFO_CALC(eventData[11..32]))
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