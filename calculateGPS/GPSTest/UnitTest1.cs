using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GPSlibrary;
using GPSlibrary.exception;
using calculateGPS.calculateData;

namespace GPSTest
{
    [TestClass]
    public class TestMethods
    {
        public byte[] rawData = new byte[]{
            0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x0a
        };

        public byte[] rawData6th = new byte[]{
            0x40, 0x40,0x67,0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31,0x30,0x36, 0x37,0x36,0x02, 0x20,0x12,0x03,0x16
            ,0x02,0x0d,0x38,0x80,0x80,0x00, 0x12,0x03,0x16,0x02,0x0d,0x3a,0x0f, 0x02,0x55,0x47,0x0a,0xe8,0xfb,0xef,0x16,
            0x02,0x00,0x00,0x00,0x23,0x32,0x05,0x05,0x20,0x01,0x62,0x0c,0x20,0x02,0x0e,0x14,0x0d,0x20,0x01,0x00, 0x0f,0x20,0x01,0x35,
            0x10,0x20,0x02,0xc6,0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xe7,0x33,0x00,0x00,0x8b,0x00,0x02,0x01,0x01,0x01,0x00,0x00,
            0xe0,0x97, 0x9b, 0x01, 0xcb,0x37, 0x03,0x00,0x61,0xaa,0x0d,0x0a
        };

        [DataTestMethod]
        [DataRow(new byte[]{
            0x40, 0x41,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x0a
        }, typeof(NullHeadException))]
        public void TestPacketHead(byte[] rawData, Type type)
        {
            TestPacketTail(new byte[]{
            0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x00
        }, typeof(NullTailException));
            try
            {
               var gpsObj = new calcGPS(rawData);
               Assert.AreEqual(gpsObj.EventCode, 0x1001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        [DataTestMethod]
        [DataRow(new byte[]{
            0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x00
        }, typeof(NullTailException))]
        public void TestPacketTail(byte[] rawData, Type type)
        {
            try
            {
                var gpsObj = new calcGPS(rawData);
                Assert.AreEqual(gpsObj.EventCode, 0x1001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        

        [DataTestMethod]
        [DataRow(new byte[]{
            0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x0a
        }, typeof(WrongUnitCodeException))]
        public void TestPacketUnitCode(byte[] rawData, Type type)
        {
            try
            {
                var gpsObj = new calcGPS(rawData);
                gpsObj.EventCode = 4000;
                gpsObj.checkPacketType();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        [DataTestMethod]
        [DataRow(new byte[]{
            0x40, 0x40,0x04, 0x01, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x0a
        }, typeof(LongLengthException))]
        public void TestPacketLength(byte[] rawData, Type type)
        {
            try
            {
                var gpsObj = new calcGPS(rawData);
                Assert.AreEqual(gpsObj.EventCode, 0x1001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }


        [DataTestMethod]
        [DataRow(new byte[]{
            0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x61,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x0a
        }, typeof(WrongCRCException))]
        public void TestPacketCRC(byte[] rawData, Type type)
        {
            try
            {
                var gpsObj = new calcGPS(rawData);
                Assert.AreEqual(gpsObj.EventCode, 0x1001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }


        [DataTestMethod]
        [DataRow(new byte[]{
             0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x0a
        }, typeof(NullMonthException))]
        public void TestPacketMonth(byte[] rawData, Type type)
        {
            try
            {
                var gpsObj = new calcGPS(rawData);
                Assert.AreEqual(gpsObj.EventCode, 0x1001);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
;        }


        [DataTestMethod]
        [DataRow(new byte[]{
             0x40, 0x40,0x56, 0x00, 0x32, 0x47, 0x51, 0x2d, 0x31, 0x36, 0x30, 0x31, 0x30, 0x36,0x33, 0x38,0x01, 0x10,0x06,
            0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31,
            0x01,0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00, 0x01,0x01, 0x00,0x00, 0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,
            0xf2, 0xad,0xae, 0xaf,0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 0x6a,0x6b, 0x6c,0x06,
            0x0a, 0x13, 0x0a,0x2a, 0x07,0x53, 0xe8,0x0d, 0x0a
        })]
        public void TestLoginTrue(byte[] rawData)
        {
                var gpsObj = new calcGPS(rawData);
                Assert.AreEqual(BitConverter.ToString(gpsObj.head).Replace("-", ""), "4040");
                Assert.AreEqual(gpsObj.length, 86);
                Assert.AreEqual(BitConverter.ToString(gpsObj.UnitCode).Replace("-", ""), "3247512D3136303130363338");
                Assert.AreEqual(gpsObj.EventCode, 4097);
                Assert.AreEqual(BitConverter.ToString(gpsObj.EventData).Replace("-", ""), "060A130A2A080F3E40440A7089E616CF005004D03101010505050006000101000018A5A6A7A8A9AAF2ADAEAFB0B16162636465666768696A6B6C060A130A2A07");
                Assert.AreEqual(BitConverter.ToString(gpsObj.CRCcode).Replace("-", ""), "53E8");
                Assert.AreEqual(BitConverter.ToString(gpsObj.tail).Replace("-", ""), "0D0A");
        }

        [DataTestMethod]
        [DataRow(new byte[]{
             0x06,0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a, 
             0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0x50,0x04, 0xd0,0x31, 0x01,
             0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00,0x01,0x01, 0x00, 0x00, 
             0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,0xf2, 0xad,0xae, 0xaf,
             0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69, 
             0x6a,0x6b, 0x6c,0x06, 0x0a, 0x13, 0x0a,0x2a, 0x07
        })]
        public void TestLoginEventData(byte[] rawData)
        {
            eventData objEventData = new eventData();
            loginData login = new loginData(objEventData);
            login.calculateLoginData(4097, rawData);
            Assert.AreEqual(BitConverter.ToString(login.obdModule).Replace("-", ""), "01010505");
            Assert.AreEqual(BitConverter.ToString(login.unitFirm).Replace("-", ""), "05000600");
            Assert.AreEqual(BitConverter.ToString(login.unitHard).Replace("-", ""), "01010000");
            Assert.AreEqual(objEventData.status, (uint)15);
            Assert.AreEqual(objEventData.latitude, 47.845848333333336);
            Assert.AreEqual(objEventData.longitude, 106.72422666666667);
            Assert.AreEqual(objEventData.speed, 207);
            Assert.AreEqual(objEventData.course, 1104);
            Assert.AreEqual(objEventData.high, 12752);   
        }

        [DataTestMethod]
        [DataRow(new byte[]{
             0x06,0x0a,0x13, 0x0a,0x2a, 0x08,0x0f, 0x3e,0x40, 0x44, 0x0a,
             0x70,0x89, 0xe6,0x16, 0xcf,0x00, 0xd0,0x31, 0xd0, 0x31, 0x01,
             0x01, 0x05,0x05, 0x05,0x00, 0x06,0x00,0x01,0x01, 0x00, 0x00,
             0x18,0xa5, 0xa6,0xa7, 0xa8,0xa9, 0xaa,0xf2, 0xad,0xae, 0xaf,
             0xb0, 0xb1,0x61, 0x62,0x63, 0x64,0x65, 0x66,0x67, 0x68,0x69,
             0x6a,0x6b, 0x6c,0x06, 0x0a, 0x13, 0x0a,0x2a, 0x07
        }, typeof(TooMuchHighException))]
        public void TestLoginCourse(byte[] rawData, Type type)
        {
            try
            {
                eventData objEventData = new eventData();
                loginData login = new loginData(objEventData);
                login.calculateLoginData(4097, rawData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        [DataTestMethod]
        [DataRow(new byte[]{
            0x28, 0x0a, 0x13, 0x0a,0x2a, 0x07
        }, typeof(NullMonthException))]
        public void TestEventUTCTIME_Day(byte[] rawData, Type type)
        {
            try
            {
                eventData objEventData = new eventData();
                objEventData.UTC_TIME_CALC(rawData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        [DataTestMethod]
        [DataRow(new byte[]{
            0x06, 0x28, 0x13, 0x0a,0x2a, 0x07
        }, typeof(NullMonthException))]
        public void TestEventUTCTIME_Month(byte[] rawData, Type type)
        {
            try
            {
                eventData objEventData = new eventData();
                objEventData.UTC_TIME_CALC(rawData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        [DataTestMethod]
        [DataRow(new byte[]{
            0x06, 0x0a, 0x13, 0x28,0x2a, 0x07
        }, typeof(NullMonthException))]
        public void TestEventUTCTIME_Hour(byte[] rawData, Type type)
        {
            try
            {
                eventData objEventData = new eventData();
                objEventData.UTC_TIME_CALC(rawData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        [DataTestMethod]
        [DataRow(new byte[]{
            0x06, 0x0a, 0x13, 0x0a,0x2a, 0x07
        }, typeof(NullMonthException))]
        public void TestEventUTCTIME_Minute(byte[] rawData, Type type)
        {
            eventData objEventData = new eventData();
            objEventData.UTC_TIME_CALC(rawData);
            Assert.IsTrue(objEventData.minute == 42);
            try
            {
                objEventData.UTC_TIME_CALC(new byte[] { 0x06, 0x0a, 0x13, 0x0a, 0x46, 0x07 });   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        [DataTestMethod]
        [DataRow(new byte[]{
             0x12,0x03,0x16,0x02,0x0d,0x38,0x80,0x80,0x00,0x12,0x03,0x16,0x02,0x0d,0x3a,0x0f, 
             0x02,0x55,0x47,0x0a,0xe8,0xfb,0xef,0x16,0x02,0x00,0x00,0x00,0x23,0x32,0x05,0x05,
             0x20,0x01,0x62,0x0c,0x20,0x02,0x0e,0x14,0x0d,0x20,0x01,0x00, 0x0f,0x20,0x01,0x35,
             0x10,0x20,0x02,0xc6,0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xe7,0x33,0x00,
             0x00,0x8b,0x00,0x02,0x01,0x01,0x01,0x00,0x00,0xe0,0x97, 0x9b, 0x01, 0xcb,0x37, 0x03,0x00
        })]
        public void TestCompHistorical(byte[] rawData)
        {
            eventData objEventData = new eventData();
            Comprehensive comp = new Comprehensive(objEventData);
            comp.calculateCompData(8194, rawData);
            Assert.AreEqual(BitConverter.ToString(comp.dataSwitch).Replace("-", ""), "808000");
            Assert.AreEqual(BitConverter.ToString(comp.current_trip_fuel).Replace("-", ""), "00000000");
            Assert.AreEqual(BitConverter.ToString(comp.current_trip_mileage).Replace("-", ""), "00000000");
            Assert.AreEqual(BitConverter.ToString(comp.current_trip_duration).Replace("-", ""), "E7330000");
            Assert.AreEqual(comp.gsensor_length, 139);
            Assert.AreEqual(objEventData.status, (uint)15);
            Assert.AreEqual(objEventData.latitude, 47.901938333333334);
            Assert.AreEqual(objEventData.longitude, 106.89620666666667);
            Assert.AreEqual(objEventData.speed, 2);
            Assert.AreEqual(objEventData.course, 0);
            Assert.AreEqual(objEventData.high, 12835);
        }


        [DataTestMethod]
        [DataRow(new byte[]{
             0x28,0x05,
             0x20,0x01,0x62,0x0c,0x20,0x02,0x0e,0x14,0x0d,0x20,0x01,0x00, 0x0f,0x20,0x01,0x35,
             0x10,0x20,0x02,0xc6,0x01,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0xe7,0x33,0x00,
             0x00,0x8b,0x00,0x02,0x01,0x01,0x01,0x00,0x00,0xe0,0x97, 0x9b, 0x01, 0xcb,0x37, 0x03,0x00
        }, typeof(LongLengthException))]
        public void TestCompOBD(byte[] rawData, Type type)
        {
            try
            {
                eventData objEventData = new eventData();
                Comprehensive comp = new Comprehensive(objEventData);
                comp.calculate_obdData(rawData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), type);
            }
        }

        [DataTestMethod]
        [DataRow(new byte[]{
             0x12,
            0x03, 0x16, 0x0a, 0x30, 0x0c, 0x80, 0x80, 0x80, 0x12, 0x03, 0x16, 0x0a, 0x30, 0x0c, 0x0f, 0x9a, 0x44, 0x47, 0x0a,
            0xb4, 0x8a, 0xef, 0x16, 0x05, 0x00, 0x00, 0x00, 0xe0, 0x31, 0x06, 0x05, 0x20, 0x01, 0x64, 0x0b, 0x20, 0x01, 0x56,
            0x0c, 0x20, 0x02, 0x00, 0x00, 0x0d, 0x20, 0x01, 0x00, 0x0f, 0x20, 0x01, 0x4a, 0x10, 0x20, 0x02, 0x11, 0x00, 0x0a,
            0x00, 0x00, 0x00, 0x1f, 0x01, 0x00, 0x00, 0x01, 0xad, 0x06, 0x00, 0x1e, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x03, 0x00, 0x00, 0x00, 0xfa, 0xff, 0x11, 0x00, 0x09, 0xff, 0xfb,
            0xff, 0x10, 0x00, 0x0b, 0xff, 0x95, 0x00, 0x02, 0x01, 0x01, 0x01, 0x00, 0x00, 0x12, 0xe5, 0xfc, 0x00, 0x08, 0x04,
            0x02, 0x00,
        })]
        public void TestCompRealtime(byte[] rawData)
        {
            eventData objEventData = new eventData();
            Comprehensive comp = new Comprehensive(objEventData);
            comp.calculateCompData(8194, rawData);
            Assert.AreEqual(BitConverter.ToString(comp.dataSwitch).Replace("-", ""), "808080");
            Assert.AreEqual(BitConverter.ToString(comp.current_trip_fuel).Replace("-", ""), "0A000000");
            Assert.AreEqual(BitConverter.ToString(comp.current_trip_mileage).Replace("-", ""), "1F010000");
            Assert.AreEqual(BitConverter.ToString(comp.current_trip_duration).Replace("-", ""), "01AD0600");
            Assert.AreEqual(comp.gsensor_length, 30);
            Assert.AreEqual(objEventData.status, (uint)15);
            Assert.AreEqual(objEventData.latitude, 47.900771666666664);
            Assert.AreEqual(objEventData.longitude, 106.88815666666666);
            Assert.AreEqual(objEventData.speed, 5);
            Assert.AreEqual(objEventData.course, 0);
            Assert.AreEqual(objEventData.high, 12768);
        }

        [DataTestMethod]
        [DataRow(new byte[]{
            0x12,0x03, 0x16, 0x08, 0x37, 0x27, 0x12, 0x03, 0x16, 0x08, 0x37, 0x28, 0x0f, 
            0xc2, 0x6a, 0x47, 0x0a, 0xf6, 0x70, 0xf1,0x16, 0x1e, 0x00, 0x00, 0x00, 0x03, 0x33,
        })]
        public void TestSMFU(byte[] rawData)
        {
            eventData objEventData = new eventData();
            SMFU smfu = new SMFU(objEventData);
            smfu.calculateSMFUdata(8196, rawData);
            Assert.AreEqual(objEventData.status, (uint)15);
            Assert.AreEqual(objEventData.latitude, 47.903485);
            Assert.AreEqual(objEventData.longitude, 106.922735);
            Assert.AreEqual(objEventData.speed, 30);
            Assert.AreEqual(objEventData.course, 0);
            Assert.AreEqual(objEventData.high, 13059);
        }

        [TestMethod]
        public void TestSMFUcheckCode()
        {
            try
            {
                eventData objEventData = new eventData();
                SMFU smfu = new SMFU(objEventData);
                smfu.calculateSMFUdata(8195, new byte[] { 0x12, 0x03, 0x16, 0x08, 0x37, 0x27, 0x12 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), typeof(WrongUnitCodeException));
            }
        }

        [TestMethod]
        public void TestCompCheckCode()
        {
            try
            {
                eventData objEventData = new eventData();
                Comprehensive comp = new Comprehensive(objEventData);
                comp.calculateCompData(8195, new byte[] { 0x12, 0x03, 0x16, 0x08, 0x37, 0x27, 0x12 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), typeof(WrongUnitCodeException));
            }
        }

        [TestMethod]
        public void TestLoginCheckCode()
        {
            try
            {
                eventData objEventData = new eventData();
                loginData login = new loginData(objEventData);
                login.calculateLoginData(8195, new byte[] { 0x12, 0x03, 0x16, 0x08, 0x37, 0x27, 0x12 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), typeof(WrongUnitCodeException));
            }
        }

        [TestMethod]
        public void TestUTC_TIME_Length()
        {
            try
            {
                eventData objEventData = new eventData();
                objEventData.UTC_TIME_CALC(new byte[] { 0x12, 0x03, 0x16, 0x08, 0x27 });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.AreEqual(ex.GetType(), typeof(LongLengthException));
            }
        }

    }
}