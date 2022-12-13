using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 7000);
                Console.WriteLine("Client connected");

                NetworkStream stream = client.GetStream();

                string request = "I have an question";
                byte[] bytesWrite = Encoding.ASCII.GetBytes(request);
                stream.Write(bytesWrite, 0, bytesWrite.Length);
                stream.Flush();
                Console.WriteLine("Client sent request: " + bytesWrite.Length);

                byte[] bytesRead = new byte[256];
                int length = stream.Read(bytesRead, 0, bytesRead.Length);
                string answer = Encoding.ASCII.GetString(bytesRead, 0, length);
                Console.WriteLine(answer);


                client.Close();
                Console.WriteLine("Client closed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }
    }
}
