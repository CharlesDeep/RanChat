using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Cliente_RanChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string TextToSend, serverIp;

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            byte[] BytesToSend; //Created array that is going to be sent.
            BytesToSend = Encoding.Default.GetBytes(textBox1.Text); //Text is converted to bytes.
            Socket ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //Socket created
            IPEndPoint ClientAdress = new IPEndPoint(IPAddress.Parse(serverIp), 8889);
            try
            {
                label4.Text = "DISCONNECTED";
                ClientSocket.Connect(ClientAdress); //It connects to server
                label4.Text = "CONNECTED";
                ClientSocket.Send(BytesToSend, 0, BytesToSend.Length, SocketFlags.None);
                MessageBox.Show("Message sent");
            }
            catch (Exception error)
            {
                label4.Text = "ERROR";
                MessageBox.Show(error.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            serverIp = textBox2.Text;
        }

    }
}
