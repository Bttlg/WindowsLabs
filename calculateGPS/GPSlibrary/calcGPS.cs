
using calculateGPS.calculateData;
using GPSlibrary.exception;



namespace GPSlibrary
{
    public class calcGPS
    {
        //Ашиглагдах хувьсагчууд
        public eventData objEventData = new eventData();
        public byte[] head = new byte[2] { 0x40, 0x40 };
        public byte[] tail = new byte[2] { 0x0d, 0x0a };
        public ushort length;
        public byte[] UnitCode = new byte[12];
        public ushort EventCode;
        public byte[] EventData;
        public byte[] CRCcode = new byte[2];
        int index;
        
        
        //Байгуулагч функц
        public calcGPS(byte[] rawData)
        {
            //Хэрвээ packet мөн бол утгуудаа хадгалж авна
            if (checkPacket(rawData))
            {
                UnitCode = rawData[(index + 4)..(index + 16)];
                EventCode = BitConverter.ToUInt16(rawData[(index + 16)..(index + 18)]);
                EventData = rawData[(index + 18)..^4];
                CRCcode = rawData[^4..^2];
                printInfo();
                checkPacketType();
                
            }
        }


        //Packet мөн эсэхийг шалгах method. Мөн бол true, үгүй бол false утга буцаана.
        public bool checkPacket(byte[] rawData)
        {
            bool checkHead = false;
            //Орж ирсэн packet дээр давталт гүйнэ.
            for (int i = 0; i + 1 < rawData.Length; i++)
            {
                //Head-ийг хайж байна.
                if (head[0] == rawData[i] && head[1] == rawData[i + 1])
                {
                    checkHead = false;
                    //Head олдчихвол уртыг нь олж аваад өгөгдлийн уртаас болон 1024-өөс бага эсэхийг шалгана. Мөн урт нь үлдсэн датаны хэмжээнээс бага эсэхийг шалгана(Их байвал packet оршин байхгүй.).
                    if (i + 4 < rawData.Length && BitConverter.ToUInt16(rawData[(i + 2)..(i + 4)]) < 1024 && BitConverter.ToUInt16(rawData[(i + 2)..(i + 4)]) <= rawData.Length - i)
                    {
                        //Уртыг нь нэг хувьсагчид хадгалаж авах бөгөөд tail-ийг хайна.
                        length = BitConverter.ToUInt16(rawData[(i + 2)..(i + 4)]);
                        if(tail[0] == rawData[length - 2] && tail[1] == rawData[length - 1]) 
                        {
                            //Хэрвээ tail оршин байвал CRC-аа шалгаснаар packet мөн, биш нь шийдэгдэх болно.
                            byte[] crc = rawData[i..(length - 4)];
                            ushort CRCresult = calculateGPS.calculateCRC.CRC16(crc);
                            if(BitConverter.ToUInt16(rawData[^4..^2]) == CRCresult && length - 8 >= 14){
                                index = i;
                                return true;
                            }
                            throw new WrongCRCException("CRC код буруу байна");
                            
                        }
                        throw new NullTailException("Tail олдсонгүй");
                    }
                    throw new LongLengthException("Length алдаатай байна.");
                }
                checkHead = true;
            }
            if (checkHead)
            {
                throw new NullHeadException("Head олдсонгүй");
            }
            //Packet биш бол мэдээж false утга буцааж байна.
            return false;
        }

        //Ямар төрлийн data вэ? гэдгийг нь шалгаад тохирсон классруу нь eventData дамжуулна...
        public void checkPacketType()
        {   
            Console.WriteLine(this.EventCode);
            switch (this.EventCode) {
                case 4097:
                    Console.WriteLine("\n1001/9001 - Login Data");
                    loginData loginCalc = new loginData(objEventData);
                    loginCalc.calculateLoginData(EventData);
                    break;
                case 4099:
                    Console.WriteLine("\n1003/9003 - Maintenance Data");
                    Console.WriteLine("9003 butsaalaa...");
                    break;
                case 8193:
                    Console.WriteLine("Comprehensive data");
                    Comprehensive comprehensiveCalc = new Comprehensive(objEventData);
                    comprehensiveCalc.calculateCompData(EventCode, EventData);
                    break;
                case 8194:
                    Console.WriteLine("Comprehensive data");
                    Comprehensive comprehensiveCalc1 = new Comprehensive(objEventData);
                    comprehensiveCalc1.calculateCompData(EventCode, EventData);
                    break;
                case 8196:
                    Console.WriteLine("\n2004 - SleepModeFixedUpload Packet");
                    SMFU smfu = new SMFU(objEventData);
                    smfu.calculateSMFUdata(EventCode, EventData);
                    break;
                default:
                    break;
            }
        }

        //Packet мөн бол хадгалж авсан утгуудаа хэвлэх method
        public void printInfo()
        {
            Console.WriteLine("head: " + BitConverter.ToString(head).Replace("-", ""));
            Console.WriteLine("length: " + length);
            Console.WriteLine("UnitCode: " + BitConverter.ToString(UnitCode).Replace("-", ""));
            Console.WriteLine("EventCode: " + this.EventCode.ToString());
            Console.WriteLine("EventData: " + BitConverter.ToString(EventData).Replace("-", ""));
            Console.WriteLine("CRC: " + BitConverter.ToString(CRCcode).Replace("-", ""));
            Console.WriteLine("tail: " + BitConverter.ToString(tail).Replace("-", ""));
            
        }
    }
}

