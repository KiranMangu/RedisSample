using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using StackExchange.Redis;
namespace RedisSample
{
    class Program
    {
        private static IDatabase database;
        public Program()
        {

        }

        //public ConnectionMultiplexer ConnectRedis
        //{
        //    get
        //    {
        //        return 
        //    }
        //}

        public static void Main(string[] args)
        {
            try
            {

                ConnectionMultiplexer ConnectRedis = ConnectionMultiplexer.Connect("192.168.0.94");
                database = ConnectRedis.GetDatabase();
                var retValue = database.StringGet("first");
                Console.WriteLine("Redis Retvalue: " + retValue);
                
                #region Object => Byte => Compressed => Decompressed => Json
                
                string stringToFile = "";
                string filePath = @"C:\Users\91991\Kiran\Encryption\FullTest\";
                string mainJSON = "MainJSONFile.json";
                string mainByteArray = "NormalByteArrayString.txt";
                string byte2Obj2Json = "Byte_Obj_Json.json";
                string compressedByte = "CompressedByte.txt";
                string decompressedByte = "DeCompressedByte.txt";
                string deCom2Obj2Json = "Decom_Obj_Json.json";

                MainClass mainClass = InitiateClass();
                String jsonObj = JsonSerializer.Serialize(mainClass);
                stringToFile = string.Empty;
                stringToFile = jsonObj;
                //Console.WriteLine($"JSON String: {jsonObj}");
                File.WriteAllText(filePath + mainJSON, jsonObj);

                Console.WriteLine("Convertion Object To Bytes using Memory Stream");
                byte[] byteArray = ObjectToByteArray(mainClass);
                Console.WriteLine("---Bytes to Array---");
                stringToFile = string.Empty;
                stringToFile = BitConverter.ToString(byteArray);
                //Console.WriteLine(BitConverter.ToString(byteArray));
                File.WriteAllText(filePath + mainByteArray, stringToFile);
                //Console.ReadKey();

                Console.WriteLine("Convertion Byte Array to Object");
                MainClass returnClassObj = ByteArrayToObject(byteArray);
                Console.WriteLine("Convert return class object to Json");
                stringToFile = string.Empty;
                stringToFile = JsonSerializer.Serialize(returnClassObj);
                //String jsonObj_2 = JsonSerializer.Serialize(returnClassObj);
                File.WriteAllText(filePath + byte2Obj2Json, stringToFile);

                //Console.ReadKey();

                Console.WriteLine("Compressed Byte Object");
                byte[] compressedByteArray = Compress(byteArray);
                Console.WriteLine("After Compressed");
                stringToFile = string.Empty;
                stringToFile = BitConverter.ToString(compressedByteArray);
                //Console.WriteLine(BitConverter.ToString(compressedByteArray));
                File.WriteAllText(filePath + compressedByte, stringToFile);
                //Console.ReadKey();

                Console.WriteLine("Decompress Bytes");
                byte[] deCompressedByteArray = Decompress(compressedByteArray);
                Console.WriteLine("After De-compresssion");
                stringToFile = string.Empty;
                stringToFile = BitConverter.ToString(deCompressedByteArray);
                //Console.WriteLine(BitConverter.ToString(deCompressedByteArray));
                File.WriteAllText(filePath + decompressedByte, stringToFile);
                //Console.ReadKey();

                Console.WriteLine("Converstion DeCompressed to JSON Object");
                MainClass decomPressedReturnObject = ByteArrayToObject(deCompressedByteArray);
                String jsonObj_3 = JsonSerializer.Serialize(decomPressedReturnObject);
                stringToFile = string.Empty;
                stringToFile = JsonSerializer.Serialize(decomPressedReturnObject);
                File.WriteAllText(filePath + deCom2Obj2Json, stringToFile);

                Console.ReadKey();

                if (jsonObj == jsonObj_3)
                {
                    Console.WriteLine("Compression is good");
                }
                else
                {
                    Console.WriteLine("Compression not helping");
                }

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.Read();
        }

        private static byte[] ObjectToByteArray(MainClass obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        // Convert a byte array to an Object
        private static MainClass ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            MainClass obj = (MainClass)binForm.Deserialize(memStream);
            return obj;
        }

        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }

        public static MainClass InitiateClass()
        {
            return new MainClass()
            {
                MyProperty = 10,
                prop1 = "String1",
                prop2 = 1212,
                prop3 = "11111",
                prop4 = "PPPPPPP",
                prop41 = "45645654",
                prop42 = "ipoipoipoi",
                prop43 = "asasasasa",
                prop44 = "asdfasdfasd",
                prop45 = "werewqr",
                prop46 = "wervere",
                prop5 = "addvdaer",
                prop6 = "asdfewqrwer",
                prop7 = "sdfasdfdsf",
                prop8 = "1212121",
                prop9 = "qwqwqwqwqwqw",
                subClass1 = new List<SubClass1>()
                 {
                       new SubClass1(){ subprop1=1212, subprop2= 1212, subprop3=09988, subprop4=877, subprop5=377373},
                       new SubClass1(){ subprop1=1212, subprop2= 1212, subprop3=09988, subprop4=877, subprop5=377373},
                       new SubClass1(){ subprop1=1212, subprop2= 1212, subprop3=09988, subprop4=877, subprop5=377373},
                       new SubClass1(){ subprop1=1212, subprop2= 1212, subprop3=09988, subprop4=877, subprop5=377373},
                       new SubClass1(){ subprop1=1212, subprop2= 1212, subprop3=09988, subprop4=877, subprop5=377373},
                       new SubClass1(){ subprop1=1212, subprop2= 1212, subprop3=09988, subprop4=877, subprop5=377373},
                       new SubClass1(){ subprop1=1212, subprop2= 1212, subprop3=09988, subprop4=877, subprop5=377373},
                 },
                subClass2 = new List<SubClass2>()
                {
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                    new SubClass2() { subprop5 = 12112, subprop2 = 121,  subprop1 = 1212},
                },
                subClass3 = new List<SubClass3>()
                 {
                      new SubClass3(){ subprop2=1, subprop5 =2, subprop4 = 333, subprop3= 1212},
                      new SubClass3(){ subprop2=1, subprop5 =2, subprop4 = 333, subprop3= 1212},
                      new SubClass3(){ subprop2=1, subprop5 =2, subprop4 = 333, subprop3= 1212},
                      new SubClass3(){ subprop2=1, subprop5 =2, subprop4 = 333, subprop3= 1212},
                      new SubClass3(){ subprop2=1, subprop5 =2, subprop4 = 333, subprop3= 1212},
                      new SubClass3(){ subprop2=1, subprop5 =2, subprop4 = 333, subprop3= 1212},
                 },
                subClass4 = new List<SubClass4>()
                  {
                      new SubClass4(){ subprop1= 121, subprop3 = 1222, subprop2=111, subprop4=999, subprop5=17171},
                      new SubClass4(){ subprop1= 121, subprop3 = 1222, subprop2=111, subprop4=999, subprop5=17171},
                      new SubClass4(){ subprop1= 121, subprop3 = 1222, subprop2=111, subprop4=999, subprop5=17171},
                      new SubClass4(){ subprop1= 121, subprop3 = 1222, subprop2=111, subprop4=999, subprop5=17171},
                      new SubClass4(){ subprop1= 121, subprop3 = 1222, subprop2=111, subprop4=999, subprop5=17171},
                      new SubClass4(){ subprop1= 121, subprop3 = 1222, subprop2=111, subprop4=999, subprop5=17171},
                      new SubClass4(){ subprop1= 121, subprop3 = 1222, subprop2=111, subprop4=999, subprop5=17171},
                  }
                    ,
                subClass5 = new List<SubClass5>()
                   {
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                       new SubClass5(){ subprop5= 12,  subprop4 = 77, subprop3=99, subprop2 = 88, subprop1= 55},
                   }
            };
            #endregion
        }
    }
}
