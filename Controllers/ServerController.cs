using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CarGo.Controllers
{
    class ServerController
    {
        SimpleTcpServer server;

        public void Work()
        {
            Debug.WriteLine("START...");

            server = new SimpleTcpServer();
            server.Start(IPAddress.Any, TCPController.PORTID);
            Debug.WriteLine("STARTED");

            Debug.WriteLine(server.GetListeningIPs()[0]);
   
            server.ClientConnected += ClientConnected;
        }

        private void ClientConnected(object? sender, TcpClient e)
        {
            Debug.WriteLine("CONNECTED: " + e.ToString());
        }
    }
}
