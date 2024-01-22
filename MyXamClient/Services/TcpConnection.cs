using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyXamClient.Services
{
    public class TcpConnection
    {
        public async static void WriteMessage(TcpClient client, byte[] message)
        {
            var stream = new StreamWriter(client.GetStream(), Encoding.ASCII);
            stream.Write(message);
            stream.Flush();
        }

        public static string ReadTextMessage(TcpClient client)
        {
            StreamReader stream = new StreamReader(client.GetStream(), Encoding.ASCII);
            return stream.ReadLine();
        }
    }
}
