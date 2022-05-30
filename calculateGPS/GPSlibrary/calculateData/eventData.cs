
using GPSlibrary.exception;

namespace calculateGPS.calculateData
{
    public class eventData
    {
        //Хэрэгтэй хувьсагчид...
        public string monthText = "Jan";
        public string UTC_TIME_TEXT = "";
        public uint status;
        public double latitude, longitude;
        public ushort speed, course;
        public int high;
        public int day, month, year, hour, minute, second;

        public eventData() { }


        public string UTC_TIME_CALC(byte[] time)
        {
            //Орж ирсэн датанаас зохих2 байрлалын утгыг хөрвүүлэн авч байна...
            //Console.WriteLine("check: " + BitConverter.ToString(time).Replace("-", "") + " , urt : " + time.Length);
            if(time.Length != 6)
            {
                throw new LongLengthException("UTC_TIME заавал 6 урттай байх шаардлагатай...");
            }
            second = unchecked((int)time[5]);
            minute = unchecked((int)time[4]);
            hour = unchecked((int)time[3]); ;
            year = 2000 + unchecked((int)time[2]);
            month = unchecked((int)time[1]);
            day = unchecked((int)time[0]);

            //Жишээ нь: 25 цаг гээд ирчихвэл энэ нь асуудал болох учир бүгдэнгийнх нь интервалыг нэг бүрчлэн шалгаж байгаа юм...
            if (month > 12 || month < 1 || day > 31 || day < 1 || hour > 24 || hour < 0 || minute > 60 || minute < 0 || second > 60 || second < 0)
            {
                throw new NullMonthException("UTC_TIME алдаатай байна.");
            }
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
            //Ямар ч асуудалгүй байвал иймэрхүү маягаар форматлаж string болгоод түүнийг буцаана...
            UTC_TIME_TEXT = year + " " + monthText + "." + day + " " + hour + ":" + minute + ":" + second;
            return UTC_TIME_TEXT;
        }

        public bool GPS_INFO_CALC(byte[] GPS_DATA)
        {
            Console.WriteLine("GPS_INFO_UTC_TIME: " + UTC_TIME_CALC(GPS_DATA[0..6]));
            status = unchecked((uint)GPS_DATA[6]);
            //Бүгд тодорхой интервалтай байх учир тус бүрд нь шалгаад асуудалгүй байвал датагаа заагдсан хувьсагчид хадгалан
            //true үр дүн буцаана...
            //Хэрвээ интервалаас хальсан асуудал гарах юм бол false утга буцаан цааш пакетыг задлах үйл явцыг үүгээр дуусгавар болгоно...
            if (status >= 0)
            {
                latitude = (double)BitConverter.ToUInt32(GPS_DATA[7..11]) / 3600000;
                longitude = (double)BitConverter.ToUInt32(GPS_DATA[11..15]) / 3600000;
                
                if (latitude >= 0 && latitude <= 90 * 3600000 && longitude >= 0 && longitude <= 180 * 3600000)
                {
                    speed = BitConverter.ToUInt16(GPS_DATA[15..17]);
                    if (speed >= 0 && speed <= 65535)
                    {
                        course = BitConverter.ToUInt16(GPS_DATA[17..19]);
                        if (course >= 0 && course <= 3599)
                        {

                            high = BitConverter.ToInt16(GPS_DATA[19..21]);
                            if (high >= -32768 && high <= 32767)
                            {
                                return true;
                            }
                        }
                        throw new TooMuchHighException("course hetersen baina...");
                    }
                    throw new TooMuchHighException("speed hetersen baina...");
                }
            }
            throw new TooMuchHighException("status 0-ees baga baina...") ;
        }

        public void print_GPS_INFO()
        {
            Console.WriteLine("\nstatus: " + status + "\nlatitude: " + latitude + "\nlongitude: "
                + longitude + "\nspeed: " + speed + "\ncourse: " + course + "\nhigh: " + high);   
        }
    }
}
