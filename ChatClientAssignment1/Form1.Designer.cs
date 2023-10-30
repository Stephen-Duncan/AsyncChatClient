namespace ChatClientAssignment1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            gameToolStripMenuItem = new ToolStripMenuItem();
            simulateToolStripMenuItem = new ToolStripMenuItem();
            networkToolStripMenuItem = new ToolStripMenuItem();
            connectMenu = new ToolStripMenuItem();
            disconnectMenu = new ToolStripMenuItem();
            pictureBox1 = new PictureBox();
            textSend = new TextBox();
            label2 = new Label();
            label3 = new Label();
            txtConv = new TextBox();
            sendButton = new Button();
            exitToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { gameToolStripMenuItem, networkToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 2, 0, 2);
            menuStrip1.Size = new Size(933, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            gameToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { simulateToolStripMenuItem, exitToolStripMenuItem });
            gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            gameToolStripMenuItem.Size = new Size(50, 20);
            gameToolStripMenuItem.Text = "Game";
            // 
            // simulateToolStripMenuItem
            // 
            simulateToolStripMenuItem.Name = "simulateToolStripMenuItem";
            simulateToolStripMenuItem.Size = new Size(180, 22);
            simulateToolStripMenuItem.Text = "Simulate";
            simulateToolStripMenuItem.Click += simulateToolStripMenuItem_Click;
            // 
            // networkToolStripMenuItem
            // 
            networkToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { connectMenu, disconnectMenu });
            networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            networkToolStripMenuItem.Size = new Size(64, 20);
            networkToolStripMenuItem.Text = "Network";
            // 
            // connectMenu
            // 
            connectMenu.Name = "connectMenu";
            connectMenu.Size = new Size(133, 22);
            connectMenu.Text = "Connect";
            connectMenu.Click += connectToolStripMenuItem_Click_1;
            // 
            // disconnectMenu
            // 
            disconnectMenu.Name = "disconnectMenu";
            disconnectMenu.Size = new Size(133, 22);
            disconnectMenu.Text = "Disconnect";
            disconnectMenu.Click += disconnectToolStripMenuItem_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(6, 30);
            pictureBox1.Margin = new Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(933, 193);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // textSend
            // 
            textSend.Location = new Point(6, 269);
            textSend.Margin = new Padding(4, 3, 4, 3);
            textSend.Name = "textSend";
            textSend.Size = new Size(820, 23);
            textSend.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 300);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 5;
            label2.Text = "Conversation";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(360, 108);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(92, 15);
            label3.TabIndex = 6;
            label3.Text = "Game goes here";
            // 
            // txtConv
            // 
            txtConv.AllowDrop = true;
            txtConv.Location = new Point(14, 318);
            txtConv.Margin = new Padding(4, 3, 4, 3);
            txtConv.Multiline = true;
            txtConv.Name = "txtConv";
            txtConv.ReadOnly = true;
            txtConv.ScrollBars = ScrollBars.Vertical;
            txtConv.Size = new Size(909, 176);
            txtConv.TabIndex = 7;
            //txtConv.TextChanged += txtMessage_TextChanged;
            // 
            // sendButton
            // 
            sendButton.Location = new Point(836, 265);
            sendButton.Margin = new Padding(4, 3, 4, 3);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(88, 27);
            sendButton.TabIndex = 10;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 519);
            Controls.Add(sendButton);
            Controls.Add(txtConv);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textSend);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem gameToolStripMenuItem;
        private ToolStripMenuItem networkToolStripMenuItem;
        private ToolStripMenuItem connectMenu;
        private ToolStripMenuItem disconnectMenu;
        private PictureBox pictureBox1;
        private TextBox textSend;
        private Label label2;
        private Label label3;
        private TextBox txtConv;
        private Button sendButton;
        private ToolStripMenuItem simulateToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        // private EventHandler txtMessage_TextChanged;
    }
}

