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

                    // If no bytes are read, the client has disconnected.
                    if (i == 0)
                    {
                        Console.WriteLine("[Server]: Client disconnected.");
                        _connectedClient.Close();
                        _connectedClient = null;
                        break;
                    }

                    string data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("[Client]: {0}", data);

                    // If a start message is received, start sending simulated data.
                    if (data.Trim() == "Simulation Start")
                    {
                        Thread sendThread = new Thread(() => SendSimulatedData(stream));
                        sendThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that might occur while reading from the stream.
                    Console.WriteLine($"[Server]: Error - {ex.Message}");
                    _connectedClient.Close();
                    _connectedClient = null;
                    break;
                }
            }
        }

        // Send simulated data to the client.
        static void SendSimulatedData(NetworkStream stream)
        {
            for (int j = 0; j < 100; j++)
            {
                string randomLetters = GenerateRandomLetters(10); // 100 random letters per line.
                byte[] msg = Encoding.ASCII.GetBytes(randomLetters);
                stream.Write(msg, 0, msg.Length);

                Console.WriteLine("[Server]: Sending {0}", randomLetters);

                Thread.Sleep(50); // Introduce a small delay between each message.
            }
        }
    }
}