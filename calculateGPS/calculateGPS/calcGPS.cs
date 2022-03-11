
using System.Text;

namespace calculateGPS
{
    internal class calcGPS
    {

        //Жишээ packet
        public byte[] rawData = new byte[]{
            0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06, 
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x0a
        };

        //Ашиглагдах хувьсагчууд
        public byte[] head = new byte[2] { 0x40, 0x40 };
        public byte[] tail = new byte[2] {0x0d, 0x0a};
        public ushort length;
        public byte[] UnitCode = new byte[2];
        public byte[] EventCode = new byte[2];
        public byte[] EventData = new byte[2];
        public byte[] CRCcode = new byte[2];
        public int index;

        //Байгуулагч функц
       public calcGPS()
        {

        }

        //Packet мөн, биш үед хариу үйлдэл үзүүлэх method
        public void calculatePacket()
        {
           
            //Хэрвээ packet мөн бол утгуудаа хадгалж авна
            if (checkPacket())
            {
                UnitCode = rawData[(index + 4)..(index + 16)];
                EventCode = rawData[(index + 16)..(index + 18)];
                EventData = rawData[(index + 18)..^4];
                CRCcode = rawData[^4..^2];
                printInfo();
            }
            else
            {
                //Харин эсрэг тохиолдолд үүнийг хэвлэнэ
                Console.WriteLine("Packet bish");
            }
        }

        //Packet мөн эсэхийг шалгах method. Мөн бол true, үгүй бол false утга буцаана.
        public bool checkPacket()
        {
            //Орж ирсэн packet дээр давталт гүйнэ.
            for (int i = 0; i + 1 < rawData.Length; i++)
            {
                //Head-ийг хайж байна.
                if (head[0] == rawData[i] && head[1] == rawData[i + 1])
                {
                    //Head олдчихвол уртыг нь олж аваад өгөгдлийн уртаас болон 1024-өөс бага эсэхийг шалгана. Мөн урт нь үлдсэн датаны хэмжээнээс бага эсэхийг шалгана(Их байвал packet оршин байхгүй.).
                    if (i + 4 < rawData.Length && BitConverter.ToUInt16(rawData[(i + 2)..(i + 4)]) < 1024 && BitConverter.ToUInt16(rawData[(i + 2)..(i + 4)]) <= rawData.Length - i)
                    {
                        //Уртыг нь нэг хувьсагчид хадгалаж авах бөгөөд tail-ийг хайна.
                        length = BitConverter.ToUInt16(rawData[(i + 2)..(i + 4)]);
                        if(tail[0] == rawData[length - 2] && tail[1] == rawData[length - 1])
                        {
                            //Хэрвээ tail оршин байвал CRC-аа шалгаснаар packet мөн, биш нь шийдэгдэх болно.
                            byte[] crc = rawData[i..^4];
                            ushort CRCresult = calculateCRC.CRC16(crc);
                            if(BitConverter.ToUInt16(rawData[^4..^2]) == CRCresult && length - 8 >= 14){
                                index = i;
                                return true;
                            }
                        }
                    }
                    break;
                }
            }

            //Packet биш бол мэдээж false утга буцааж байна.
            return false;
        }

        //Packet мөн бол хадгалж авсан утгуудаа хэвлэх method
        public void printInfo()
        {
            Console.WriteLine("head: " + BitConverter.ToString(head).Replace("-", ""));
            Console.WriteLine("length: " + length);
            Console.WriteLine("UnitCode: " + BitConverter.ToString(UnitCode).Replace("-", ""));
            Console.WriteLine("EventCode: " + BitConverter.ToString(EventCode).Replace("-", ""));
            Console.WriteLine("EventData: " + BitConverter.ToString(EventData).Replace("-", ""));
            Console.WriteLine("CRC: " + BitConverter.ToString(CRCcode).Replace("-", ""));
            Console.WriteLine("tail: " + BitConverter.ToString(tail).Replace("-", ""));
        }
    }
}
