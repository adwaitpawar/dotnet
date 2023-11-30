using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a TCP listener on port 8080
            TcpListener server = new TcpListener(IPAddress.Any, 8080);
            server.Start();

            Console.WriteLine("Server is listening on port 8080...");

            while (true)
            {
                // Accept incoming connections
                TcpClient client = server.AcceptTcpClient();

                // Handle each connection in a separate thread
                NetworkStream stream = client.GetStream();

                // Read the client's message (HTTP request)
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                // Process the message
                string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received request: " + request);

                // Send an HTTP response
                string response = "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\n\r\nHello, World!";
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);

                // Close the connection
                client.Close();
            }
        }
    }
}

