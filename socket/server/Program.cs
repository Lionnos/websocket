using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Server().Wait();
        }

        private static async Task Server()
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6400);

            listener.Bind(ipEndPoint);
            listener.Listen(10);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("IP: 127.0.0.1  Port: 6400");
            Console.ResetColor();
            Console.WriteLine("Esperando conexiones ...!!!");

            byte clientCounter = 0;
            while (true)
            {
                Socket handler = await listener.AcceptAsync();
                clientCounter++;
                byte clientId = clientCounter;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Cliente {clientId} conectado...");
                Console.ResetColor();

                _ = HandleClientAsync(handler, clientId);
            }
        }

        private static async Task HandleClientAsync(Socket handler, int clientId)
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                    if (received == 0) break;

                    string response = Encoding.UTF8.GetString(buffer, 0, received);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Mensaje recibido de Cliente {clientId}: \"{response}\"");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Cliente {clientId} se ha desconectado.");
                Console.ResetColor();

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
    }
}
