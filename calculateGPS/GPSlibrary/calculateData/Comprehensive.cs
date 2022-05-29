namespace calculateGPS.calculateData
{
    internal class Comprehensive
    {
        public byte[] dataSwitch = new byte[3];
        public byte[] obdData = new byte[55];
        eventData objEventData;
        public Comprehensive(eventData param)
        {
            this.objEventData = param;
        }
        public void calculateCompData(ushort checkCode, byte[] eventData)
        {
            Console.WriteLine("Event_DATA_UTC_TIME: " + objEventData.UTC_TIME_CALC(eventData[0..6]));

            //Realtime, Historical гэсэн 2 төрөл байх учир тус тусад нь задална...
            //8193 - Realtime, 8194 - Historical
            if (checkCode == 8193)
            {
                Console.WriteLine("\nRealtime Upload: ");
            }else
            {
                Console.WriteLine("\nHistorical Upload: ");
            }
                dataSwitch = eventData[6..9];

                //DATA_SWITCH нь eventData-д ямар өгөгдөл байгааг илтгэх учир тус бүрд нь шалгаж байгаа...
                if (unchecked((int)dataSwitch[0]) == 128)
                {
                    if (objEventData.GPS_INFO_CALC(eventData[9..30]))
                    {
                        print_COMPGPS_INFO();
                    }
                    else
                    {
                        Console.WriteLine("Aldaatai Eventdata baina...");
                    }
                }else
                {
                    Console.WriteLine("GPS_DATA null...");
                }

                if(unchecked((int)dataSwitch[1]) == 128)
                {

                }else
                {
                    Console.WriteLine("OBD_DATA null...");
                }


                if (unchecked((int)dataSwitch[2]) == 128)
                {
                    
                }
                else
                {
                    Console.WriteLine("GSENSOR null...");
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

        public void print_COMPGPS_INFO()
        {
            Console.WriteLine("data_switch: " + BitConverter.ToString(dataSwitch).Replace("-", ""));
            objEventData.print_GPS_INFO();
        }
    }
}
