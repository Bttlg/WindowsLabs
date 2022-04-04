

namespace calculateGPS.calculateData
{
    public abstract class calculateData
    {
        public string monthText;
        public string UTC_TIME_TEXT;
        public uint status, latitude, longitude;
        public ushort speed, course;
        public int high;
        
        
        public string UTC_TIME_CALC(byte[] time)
        {
            int day, month, year, hour, minute, second;
            second = unchecked((int)time[5]);
            minute = unchecked((int)time[4]);
            hour = unchecked((int)time[3]); ;
            year = 2000 + unchecked((int)time[2]);
            month = unchecked((int)time[1]);
            day = unchecked((int)time[0]);
            switch (month) {
                case 1:
                    monthText = "Jan";
                    break;
                case 2:
                    monthText = "Feb";
                    break;
                case 3:
                    monthText = "Mar";
                    break;
                case 4:
                    monthText = "Apr";
                    break;
                case 5:
                    monthText = "May";
                    break;
                case 6:
                    monthText = "Jun";
                    break;
                case 7:
                    monthText = "Jul";
                    break;
                case 8:
                    monthText = "Aug";
                    break;
                case 9:
                    monthText = "Sep";
                    break;
                case 10:
                    monthText = "Oct";
                    break;
                case 11:
                    monthText = "Nov";
                    break;
                case 12:
                    monthText = "Dec";
                    break;
                default:
                    break;
            }
            UTC_TIME_TEXT = year + " " + monthText + "." + day + " " + hour + ":" + minute + ":" + second;
            return UTC_TIME_TEXT;
        }

        public bool GPS_INFO_CALC(byte[] GPS_DATA)
        {
            Console.WriteLine("GPS_INFO_UTC_TIME: " + UTC_TIME_CALC(GPS_DATA[0..6]));
            if (unchecked((int)GPS_DATA[6]) >= 0 && unchecked((int)GPS_DATA[6]) <= 7)
            {
                status = unchecked((uint)GPS_DATA[6]);
                latitude = BitConverter.ToUInt32(GPS_DATA[7..11]);
                longitude = BitConverter.ToUInt32(GPS_DATA[11..15]);
                if (latitude >= 0 && latitude <= 90 * 3600000 && longitude >= 0 && longitude <= 90 * 3600000)
                {
                    speed = BitConverter.ToUInt16(GPS_DATA[15..17]);
                    if (speed >= 0 && speed <= 65535)
                    {
                        course = BitConverter.ToUInt16(GPS_DATA[17..19]);
                        if (course >= 0 && course <= 3599)
                        {
                            high = BitConverter.ToInt16(GPS_DATA[19..22]);
                            if (high >= -32768 && high <= 32767)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
