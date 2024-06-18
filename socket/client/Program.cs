using System.Net.Sockets;
using System.Net;
using System.Text;

namespace client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Client().Wait();
        }

        private static async Task Client()
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint connect = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6400);

            await client.ConnectAsync(connect);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectado al servidor...!!!");
            Console.ResetColor();

            try
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Ingrese el mensaje para enviar (CTRL+C para salir): ");
                    Console.ResetColor();

                    var message = Console.ReadLine();
                    if (string.IsNullOrEmpty(message)) continue;

                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    await client.SendAsync(messageBytes, SocketFlags.None);

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Mensaje enviado: \"{message}\"");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }
    }
}
