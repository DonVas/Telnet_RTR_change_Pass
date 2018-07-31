using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace Telnet_RTR_change_Pass
{
    public partial class Form1 : Form
    {
        //string[] ipaddrese = new string[0];
        
        List<string> passwords = new List<string>();
        public Form1()
        {
            InitializeComponent();
            

        }

   

        private void Form1_Load(object sender, EventArgs e)
        {

            TelnetInterface tc = new TelnetInterface("192.168.1.203", 23);

            //login with user "root",password "rootpassword", using a timeout of 100ms, and show server output
            string s = tc.Login("admin", "rootpassword", 100);
            Console.Write(s);

            // server output should end with "$" or ">", otherwise the connection failed
            string prompt = s.TrimEnd();
            prompt = s.Substring(prompt.Length - 1, 1);
            if (prompt != "$" && prompt != ">")
                throw new Exception("Connection failed");

            prompt = "";

            // while connected
            while (tc.IsConnected && prompt.Trim() != "exit")
            {
                // display server output
                Console.Write(tc.Read());

                // send client input to server
                prompt = Console.ReadLine();
                tc.WriteLine(prompt);

                // display server output
                Console.Write(tc.Read());
            }
            Console.WriteLine("***DISCONNECTED");
            Console.ReadLine();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Lines.Length > richTextBox2.Lines.Length)
            {
                MessageBox.Show("We have more IP then Passwords");
            }
            else if (richTextBox2.Lines.Length > richTextBox1.Lines.Length)
            {
                MessageBox.Show("We have more Passwords then IP");
            }
            else
            {
                List<string> ipaddres = new List<string>(richTextBox1.Lines);
                List<string> passwords = new List<string>(richTextBox2.Lines);
            }
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
