using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CarGo.Models;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Windows;

namespace CarGo.Controllers
{
    class TCPController
    {

        public static Int32 PORTID = 65355;
        public static string IPADDRESS = "83.222.7.183";
        public GeneralCOntroller? gc;
        private TcpListener server;

        internal void close()
        {
            if (server == null)
                return;
            server.Stop();
        }

        public async void Work()
        {
            try
            {

                Debug.WriteLine("Starting the server");
                server = new TcpListener(IPAddress.Any, PORTID);
                
                DebugLogController.WriteLine("Старт сервера...");
                server.Start();

                while (true)
                {
                    DebugLogController.WriteLine("Ждем подключение...");
                    TcpClient client = await server.AcceptTcpClientAsync();

                    HandleClient(client);
                }

            }
            catch (SocketException e)
            {
                DebugLogController.WriteLine("ОШИБКА СЕРВЕРА...");
                Debug.WriteLine("SocketException: {0}", " 2" + e);
            }
            catch (Exception e)
            {
                DebugLogController.WriteLine("ОШИБКА СЕРВЕРА...");
                Console.WriteLine("SocketException: {0}", " 2" + e);
            }
            finally
            {
                Console.WriteLine("SocketException: {0}" + " 2");
                DebugLogController.WriteLine("ОШИБКА СЕРВЕРА...");
                server.Stop();
            }

            DebugLogController.WriteLine("\nСервер отключен...");
        }

        private void HandleClient(TcpClient client)
        {
            DebugLogController.WriteLine("ЕСТЬ ПОДКЛЮЧЕНИЕ!");

            NetworkStream ns = client.GetStream();
            
            Byte[] buffer = new byte[2048];

            int numOfBits = ns.Read(buffer, 0, buffer.Length);

            DebugLogController.WriteLine("ПОЛУЧЕН ПАКЕТ 1: " + numOfBits);

            if (numOfBits != 17)
            {
                DebugLogController.WriteLine("Подключение не удалось: IMEI не действительный");
                return;
            }

            var imei = Convert.ToHexString(buffer, 0, numOfBits).Substring(4);

            DebugLogController.WriteLine("ПОЛУЧЕН IMEI: " + imei);


            Byte[] res = { 0x01 };

            ns.Write(res, 0, 1);
            ns.Flush();


            numOfBits = ns.Read(buffer);

            try
            {

                List<Position> list = Parser.ParseData(buffer, numOfBits);
                DebugLogController.WriteLine("ПОЛУЧЕН ПАКЕТ 2: " + numOfBits);

                foreach (Position pol in list)
                {
                    if (pol.La == 0 && pol.Lo == 0)
                    {
                        DebugLogController.WriteLine("недействительный GPS");
                        continue;
                    }
                    else
                    {
                        gc.AddGPS(pol, imei);
                    }
                }
            }catch(Exception e)
            {
                DebugLogController.WriteLine("Ошибка парсинга");
            }
        }
    }
}
