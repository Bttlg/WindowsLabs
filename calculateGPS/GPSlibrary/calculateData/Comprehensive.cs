namespace calculateGPS.calculateData
{
    internal class Comprehensive
    {
        public byte[] dataSwitch = new byte[3];
        public byte[] obdData = new byte[0];
        public byte[] current_trip_fuel = new byte[4];
        public byte[] current_trip_mileage = new byte[4];
        public byte[] current_trip_duration = new byte[4];
        public ushort gsensor_length;
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
            } else
            {
                Console.WriteLine("\nHistorical Upload: ");
            }
            dataSwitch = eventData[6..9];

            //DATA_SWITCH нь eventData-д ямар өгөгдөл байгааг илтгэх учир тус бүрд нь шалгаж байгаа...
            if (unchecked((int)dataSwitch[0]) == 128)
            {
                if (objEventData.GPS_INFO_CALC(eventData[9..30]))
                {
                    
                }
                else
                {
                    Console.WriteLine("Aldaatai Eventdata baina...");
                }
            } else
            {
                Console.WriteLine("GPS_DATA null...");
            }

            if (unchecked((int)dataSwitch[1]) == 128)
            {
                //GPS_INFO байх эсэхээс хамаарч OBD_DATA хаанаас нь эхлэнэ тодорхойлогдоно
                if (unchecked((int)dataSwitch[0]) == 128) calculate_obdData(eventData[30..eventData.Length]);
                else calculate_obdData(eventData[9..eventData.Length]);
            }
            else
            {
                Console.WriteLine("OBD_DATA null...");
            }
            if (unchecked((int)dataSwitch[1]) == 128)
            {
                int length = 30 + obdData.Length;
                current_trip_fuel = eventData[length..(length + 4)];
                length += 4;
                current_trip_mileage = eventData[length..(length + 4)];
                length += 4;
                current_trip_duration = eventData[length..(length + 4)];
                length += 4;
                gsensor_length = BitConverter.ToUInt16(eventData[length..(length + 2)]);
            }
            else
            {
                int length = 9 + obdData.Length;
                current_trip_fuel = eventData[length..(length + 4)];
                length += 4;
                current_trip_mileage = eventData[length..(length + 4)];
                length += 4;
                current_trip_duration = eventData[length..(length + 4)];
                length += 4;
                gsensor_length = BitConverter.ToUInt16(eventData[length..(length + 2)]);
            }
            print_COMPGPS_INFO();
        }

        public void calculate_obdData(byte[] data)
        {
            int i;
            Console.WriteLine("\n===========>>> OBD_DATA <<<=============");
            
            if (unchecked((int)data[0]) <= 10)
            {
                int blockCount = unchecked((int)data[0]);
                Console.WriteLine("OBD_DATA_LENGTH: " + blockCount);
                int count;
                int index = 1;
                for (i = 0; i < blockCount; i++)
                {
                    Console.WriteLine("\nblockCount : " + (i + 1));

                    Console.WriteLine("PID[" + index + "] = " + unchecked((int)data[index]));
                    index += 1;
                    Console.WriteLine("PID[" + index + "] = " + unchecked((int)data[index]));
                    index += 1;
                    count = unchecked((int)data[index]);
                    Console.WriteLine("PID[" + index + "] = " + unchecked((int)data[index]));
                    index += 1;
                    for (int j = 0; j < count; j++)
                    {
                        Console.WriteLine("PID[" + index + "] = " + unchecked((int)data[index]));
                        index += 1;
                    }
                }
                obdData = new byte[index];
                obdData = data[0..index];
            }
            else
            {
                Console.WriteLine("obd data length too long");
            }
        }

        public void print_COMPGPS_INFO()
        {
            Console.WriteLine("data_switch: " + BitConverter.ToString(dataSwitch).Replace("-", ""));
            Console.WriteLine("current_trip_fuel: " + BitConverter.ToString(current_trip_fuel).Replace("-", ""));
            Console.WriteLine("current_trip_mileage: " + BitConverter.ToString(current_trip_mileage).Replace("-", ""));
            Console.WriteLine("current_trip_duration: " + BitConverter.ToString(current_trip_duration).Replace("-", ""));
            Console.WriteLine("GSEN_DATA_Len: " + gsensor_length);
            objEventData.print_GPS_INFO();
        }
    }
}
