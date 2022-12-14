using System;
using System.Text;
using System.Net.Sockets;
using Serilog;
using System.IO;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Log.Logger = new LoggerConfiguration().WriteTo.File("FileLog.log").CreateLogger();

            Log.Information("The is a sample information");

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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Log.Error(ex, "Some error occurred");
            }
            Console.ReadLine();

            Log.CloseAndFlush();
            Console.WriteLine("Completed");
            Console.ReadKey();
        }
    }
}
