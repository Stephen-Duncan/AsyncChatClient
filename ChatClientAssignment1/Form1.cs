using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using ChatLibrary;
using System.Threading.Tasks;

namespace ChatClientAssignment1
{
    public partial class Form1 : Form
    {
        // The chat client that manages the connection and communication.
        private ChatClient _chatClient;

        public Form1()
        {
            // Initialize the form components.
            InitializeComponent();

            // Initialize the chat client.
            _chatClient = new ChatClient("127.0.0.1", 13000);
            _chatClient.MessageReceived += ChatClient_MessageReceived;
        }

        // Random generator used for generating random numbers.
        private Random _random = new Random();

        // Generate a random number as a string.
        private string GenerateRandomNumber()
        {
            string firstHalf = _random.Next(10_000, 100_000).ToString();
            string secondHalf = _random.Next(10_000, 100_000).ToString();
            return firstHalf + secondHalf;
        }

        // Event handler for when a message is received from the chat client.
        private void ChatClient_MessageReceived(object sender, ChatLibrary.MessageReceivedEventArgs e)
        {
            string formattedMessage = $"{e.Timestamp:HH:mm:ss} [{e.Sender}] {e.Message}";
            AppendTextToTextbox(txtConv, formattedMessage + Environment.NewLine);
        }

        // Lock object to ensure thread-safe access to the textbox.
        private readonly object _textboxLock = new object();

        // Safely append text to the textbox.
        private void AppendTextToTextbox(TextBox textbox, string text)
        {
            // Check if the method was called from a different thread than the UI thread.
            if (textbox.InvokeRequired)
            {
                textbox.Invoke(new Action(() => AppendTextToTextbox(textbox, text)));
                return;
            }

            //Locking added per Thread Safety - Resource provided on brightspace
            // Thread-safe addition of text to the textbox.
            //lock (_textboxLock)
            //{
            //    textbox.AppendText(text);
            //}
        }

        // Event handler for connecting to the server.
        private void connectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                _chatClient.Connect();
                txtConv.AppendText("Connected to server!" + Environment.NewLine);
                // Change the window title when connected.
                this.Text = "Chat Client - Connected";
            }
            catch (Exception ex)
            {
                txtConv.AppendText("Error connecting to server: " + ex.Message + Environment.NewLine);
            }
        }

        // Event handler for sending messages.
        private void sendButton_Click(object sender, EventArgs e)
        {
            string message = textSend.Text;
            Task.Run(() =>     //task usage provided by https://www.bytehide.com/blog/task-run-csharp  and https://stackoverflow.com/questions/17119075/do-you-have-to-put-task-run-in-a-method-to-make-it-async
            {
                _chatClient.SendMessage(message);
                //_chatClient.SendMessage(message, "Client");
            });
            textSend.Clear();
        }

        // Set the initial title of the form when it's loaded.
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Chat Client - Disconnected";
        }

        // Event handler for form closing. Ensuring disconnect from the server.
        protected override void OnClosing(CancelEventArgs e)
        {
            _chatClient.Disconnect();
            base.OnClosing(e);
        }

        // Event handler for disconnecting from the server.
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _chatClient.Disconnect();
            txtConv.AppendText("Disconnected from the server." + Environment.NewLine);
            // Change the window title when disconnected.
            this.Text = "Chat Client - Disconnected";
        }

        // Event handler for starting the message simulation.
        private void simulateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                _chatClient.SendMessage("Simulation Start", "Client");
                for (int i = 0; i < 100; i++)
                {
                    string simulatedMessage = GenerateRandomNumber();
                    _chatClient.SendMessage(simulatedMessage, "Client");
                    Thread.Sleep(50);
                }
                _chatClient.SendMessage("Simulation End", "Client");
            });
        }

        // Event handler for exiting the application.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
