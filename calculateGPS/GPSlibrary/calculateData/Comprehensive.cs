namespace calculateGPS.calculateData
{
    internal class Comprehensive : eventData
    {
        public byte[] dataSwitch = new byte[3];
        public byte[] obdData = new byte[55];
        GPS_INFO gpsInfo;
        public Comprehensive()
        {

        }
        public void calculateCompData(ushort checkCode ,byte[] eventData)
        {
            Console.WriteLine("Event_DATA_UTC_TIME: " + UTC_TIME_CALC(eventData[0..6]));
           
            if (checkCode == 8193)
            {
                Console.WriteLine("Realtime Upload: ");
                dataSwitch = eventData[6..9];
                if (GPS_INFO_CALC(eventData[9..30]))
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
                dataSwitch = eventData[6..9];
                if (GPS_INFO_CALC(eventData[9..30]))
                {
                    //calculate_obdData(eventData[30..eventData.Length]);
                    print_GPS_INFO();
                }
                else
                {
                    Console.WriteLine("Aldaatai Eventdata baina...");
                }
            }
        }

        public byte[] calculate_obdData(byte[] eventData)
        {
            if (eventData.Length > 1 && eventData.Length <= 55 && unchecked((int)eventData[0]) <= 10)
            {
                
                int blockCount = unchecked((int)eventData[0]);
                for(int i = 0; i != blockCount; i++)
                {
                    if(i + 2 <= eventData.Length && unchecked((int)eventData[i + 2]) <= 4 && unchecked((int)eventData[i + 2]) >= 1)
                    {

                    }else
                    {
                        break;
                    }
                }
                return eventData;
            }
            else
            {
                return eventData;
            }

        }
    }
}
