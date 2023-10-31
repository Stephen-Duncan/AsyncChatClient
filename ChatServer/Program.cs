//*****************************************************
//Steve Duncan - w0219156     Assignment 2 Advance OPP
//Oct 18 2023
//*****************************************************

//Code modified from singlesolutionTCPlistener provided on brightspace
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AsyncChatServer
{
    class AsyncChatServer
    {
        // TCP client representing the connected client.
        private static TcpClient _connectedClient;

        static void Main(string[] args)
        {
            // Start the server in a separate thread.
            Thread serverThread = new Thread(() => RunServer(13000));
            serverThread.Start();

            // Infinite loop to continuously send messages to the client.
            while (true)
            {
                if (_connectedClient != null && _connectedClient.Connected)
                {
                    Console.WriteLine("Type a message for the client: ");
                    string message = Console.ReadLine();

                    byte[] msg = Encoding.ASCII.GetBytes(message);
                    _connectedClient.GetStream().Write(msg, 0, msg.Length);

                    Console.WriteLine("[Server]: Sent {0}", message); // Label added
                }
            }
        }

        private static Random _random = new Random();

        // Generate a string of random letters of a given length.
        private static string GenerateRandomLetters(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        // Run the server, waiting for client connections.
        static void RunServer(int port = 13000)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                _connectedClient = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                NetworkStream stream = _connectedClient.GetStream();

                // Start a thread to receive data from the client.
                Thread receiveThread = new Thread(() => ReceiveData(stream));
                receiveThread.Start();
            }
        }

        // Handle incoming data from the client.
        static void ReceiveData(NetworkStream stream)
        {
            byte[] bytes = new byte[256];

            while (true)
            {
                try
                {
                    int i = stream.Read(bytes, 0, bytes.Length);

                    if (i == 0)
                    {
                        Console.WriteLine("[Server]: Client disconnected.");
                        _connectedClient.Close();
                        _connectedClient = null;
                        break;
                    }

                    string data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("{0}", data);

                    if (data.Trim() == "[Client]: Simulation Start")
                    {
                        SendSimulatedData(stream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Server]: Error - {ex.Message}");
                    _connectedClient.Close();
                    _connectedClient = null;
                    break;
                }
            }
        }

        // Send simulated data to the client.
        // Simplified function to send simulated data.
        static void SendSimulatedData(NetworkStream stream)
        {
            Console.WriteLine("[Server]: Starting simulation...");

            for (int i = 0; i < 100; i++)
            {
                string simulatedMessage = GenerateRandomLetters(10); // Generate random letters for the message
                byte[] msg = Encoding.ASCII.GetBytes(simulatedMessage);
                stream.Write(msg, 0, msg.Length);

                Console.WriteLine("[Server]: Sent {0}", simulatedMessage); // Label added

                Thread.Sleep(50); // Add a small delay between each message.
            }

            Console.WriteLine("[Server]: Simulation complete.");
        }
    }
}