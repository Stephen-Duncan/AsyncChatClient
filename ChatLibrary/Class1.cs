//*****************************************************
//Steve Duncan - w0219156     Assignment 2 Advance OPP
//Oct 18 2023
//*****************************************************

using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLibrary
{
    // Event arguments class for when a message is received
    //Split out for possible usage in server
    public class MessageReceivedEventArgs : EventArgs
    {
        // Properties for the message, its timestamp, and its sender
        public string Message { get; }
        public DateTime Timestamp { get; }
        public string Sender { get; }  

        // Constructor initializing the properties
        public MessageReceivedEventArgs(string message, string sender)
        {
            Message = message;
            Sender = sender;
            Timestamp = DateTime.Now; // Capture the current time as the timestamp
        }
    }

    // Main class representing the chat client
    public class ChatClient
    {
        // Private fields for storing the client, stream, host, and port
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly string _host;
        private readonly int _port;
        // Flag to determine if the client is in simulation mode
        private bool _isInSimulationMode = false;

        // Event that's fired when a message is received
        // Inspired by https://learn.microsoft.com/en-us/previous-versions/windows/silverlight/dotnet-windows-silverlight/dd491164(v=vs.95)
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        // Constructor for initializing the chat client
        public ChatClient(string host, int port)
        {
            _host = host;
            _port = port;
        }

        // Method to connect the client to the server
        public void Connect()
        {
            _client = new TcpClient(_host, _port);
            _stream = _client.GetStream();

            // Start a background task to continuously read messages from the server
            Task.Run(() => ReadServerMessages());
        }

        // Asynchronous method to read incoming server messages
        private async Task ReadServerMessages()
        {
            try
            {
                byte[] buffer = new byte[1024]; // Buffer to hold the incoming bytes

                // Keep reading messages while the client is connected
                while (_client.Connected)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                        // Handle simulation mode flags
                        if (message.Trim() == "Simulation Start")
                        {
                            _isInSimulationMode = true;
                        }
                        if (message.Trim() == "Simulation End")
                        {
                            _isInSimulationMode = false;
                        }

                        // Change the sender name based on whether we're in simulation mode
                        // Broken look into fixing
                        var sender = _isInSimulationMode ? "Guardian" : "Server";

                        // Trigger the MessageReceived event with the new message
                        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message, sender));  //Invoke usage https://stackoverflow.com/questions/16153047/net-invoke-async-method-and-await
                                                                                                       //And from bright space resource
                    }
                }
            }
            catch (IOException)
            {
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs("Disconnected from the server.", "System"));
            }
            catch (Exception ex)
            {
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs($"Error: {ex.Message}", "System"));
            }
        }

        // Method to send messages to the server
        //System used for connection errors, non client or server
        public void SendMessage(string message, string sender = "System")
        {
            if (_client == null || !_client.Connected)
            {
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs("Client is null or not connected.", "System"));
                return;
            }

            if (_stream == null || !_stream.CanWrite)
            {
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs("Stream is null or can't write.", "System"));
                return;
            }

            //byte[] data = Encoding.ASCII.GetBytes($"[{sender}]: {message}");
            byte[] data = Encoding.ASCII.GetBytes($"[{sender}]: {message}\n");
            try
            {
                _stream.Write(data, 0, data.Length);
                _stream.Flush(); // Ensure that data is sent out immediately
            }
            catch (Exception ex)
            {
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs($"Error while sending: {ex.Message}", "System"));
            }
        }

        // Method to disconnect the client from the server
        public void Disconnect()
        {
            _stream?.Close();  // Close the network stream
            _client?.Close();  // Close the TCP client
        }
    }
}