using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChatLibrary
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; }
        public DateTime Timestamp { get; }
        public string Sender { get; }

        public MessageReceivedEventArgs(string message, string sender)
        {
            Message = message;
            Sender = sender;
            Timestamp = DateTime.Now;
        }
    }

    public class ChatClient
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly string _host;
        private readonly int _port;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public ChatClient(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect()
        {
            _client = new TcpClient(_host, _port);
            _stream = _client.GetStream();

            Task.Run(() => ReadServerMessages());
        }

        private async Task ReadServerMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];

                while (_client.Connected)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(message, "Server"));
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

        public void SendMessage(string message, string sender = "System")
        {
            if (_client == null || !_client.Connected)
            {
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs("Client is not connected.", "System"));
                return;
            }

            byte[] data = Encoding.ASCII.GetBytes($"[{sender}]: {message}\n");
            try
            {
                _stream.Write(data, 0, data.Length);
                _stream.Flush();
            }
            catch (Exception ex)
            {
                MessageReceived?.Invoke(this, new MessageReceivedEventArgs($"Error while sending: {ex.Message}", "System"));
            }
        }

        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
        }
    }
}
