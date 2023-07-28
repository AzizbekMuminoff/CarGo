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

namespace CarGo.Controllers
{
    class TCPController
    {

        public static Int32 PORTID = 4413;
        public static string IPADDRESS = "192.168.1.11";
        public GeneralCOntroller gc;
        private TcpListener server;

        internal void close()
        {
            if (server == null)
                return;
            server.Stop();
        }

        public void Work()
        {
            server = null;
            try
            {
                /*
                ServerController s = new ServerController();
                Debug.WriteLine("SERVERS INIT");
                s.Work();
                Debug.WriteLine("SERVERS INIT FIN");*/


                IPAddress iPAddress = IPAddress.Any;
                
                server = new TcpListener(iPAddress, PORTID);

                // Start listening for client requests.
                server.Start();
                Debug.WriteLine("Server started... ");
                string request = "";

                // Enter the listening loop.
                while (true)
                {
                    Debug.WriteLine("Waiting for a connection.");
                    TcpClient client = server.AcceptTcpClient();
                    Debug.WriteLine("Client accepted.");
                    NetworkStream stream = client.GetStream();
                    StreamReader sr = new StreamReader(client.GetStream());
                    StreamWriter sw = new StreamWriter(client.GetStream());
                    try
                    {
                        byte[] buffer = new byte[1024];
                        stream.Read(buffer, 0, buffer.Length);
                        int recv = 0;
                        foreach (byte b in buffer)
                        {
                            if (b != 0)
                            {
                                recv++;
                            }
                        }
                        request = Encoding.UTF8.GetString(buffer, 0, recv);
                        Console.WriteLine("request received");
                        sw.WriteLine("You rock!");
                        sw.Flush();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Something went wrong.");
                        sw.WriteLine(e.ToString());
                    }

                    if (request != null && !request.Equals(string.Empty))
                    {
                        GPSModel pp = GPSModel.ParseGPS(request);

                        if (pp != null)
                        {
                            gc.AddGPS(pp);
                        }
                    }
                }
                

            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                Console.WriteLine("SocketException: {0}");
                //server.Stop();
            }




            Debug.WriteLine("\nHit enter to continue...");
            //Console.Read();
        }

    }
}
