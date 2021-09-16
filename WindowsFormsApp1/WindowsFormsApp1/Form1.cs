using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        UdpClient U;
        Thread Th;
        private void Listen()
        {
            int Port = int.Parse(textBox1.Text);
            U = new UdpClient(Port);
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);
            while(true)
            {
                byte[] B = U.Receive(ref EP);
                textBox2.Text = Encoding.Default.GetString(B);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Th = new Thread(Listen);
            Th.Start();
            button1.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Th.Abort();
                U.Close();
            }
            catch
            {
                //忽略
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string IP = textBox3.Text;
            int Port = int.Parse(textBox4.Text);
            byte[] B = Encoding.Default.GetBytes(textBox5.Text);
            UdpClient S = new UdpClient();
            S.Send(B, B.Length, IP, Port);
            S.Close();
        }
    }
}
