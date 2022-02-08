using System;
using System.Text;
using System.Net.Sockets;

namespace RequestTask
{
    class RequestTask
    {
        private const string server = "88.212.241.115"; //Сервер
        private const int port = 2013; //Порт

        static void Connect(String message)
        {
            EncodingProvider provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                TcpClient client = new TcpClient(server, port);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                StringBuilder response = new StringBuilder();
                data = new byte[256];

                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.GetEncoding("koi8r").GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                Console.WriteLine(response.ToString());

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        static void Main(string[] args)
        {
            Connect("Greetings\n");
        }
    }
}
