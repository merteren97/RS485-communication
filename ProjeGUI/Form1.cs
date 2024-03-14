using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjeGUI
{
    public partial class Form1 : Form
    {
        public SerialPort port;
        public string bufText;
        public int len = 0;
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void comPorts_MouseClick(object sender, MouseEventArgs e)
        {
            comPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();

            foreach(string port in ports)
            {
                comPorts.Items.Add(port);
            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int length = port.BytesToRead;
            int orjLen = 7;
            byte[] buffer = new byte[length];

            port.Read(buffer, 0, length);

            for (int i = 0; i<length; i++)
            {
                bufText += buffer[i].ToString("X2") + " ";
            }
            len += length;
            if(len >= orjLen)
            {
                receivedList.Items.Add(bufText);
                decoding(bufText);
                len = 0;
                bufText = "";
            }
        }

        private void comPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string comAdd = comPorts.Text;
            if (comAdd == null)
                return;
            port = new SerialPort(comAdd, 9600, Parity.None, 8, StopBits.One);
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.Open();
        }

        public void decoding(string mesaj)
        {
            string[] msgs = mesaj.Split(' ');
            string addrFrom = msgs[0]; // Address From
            string addrTo = msgs[1]; // Address To
            string control = msgs[2]; // ACK - NACK
            string func = msgs[3]; // Function
            string data = msgs[5]; // Data
            string decode = "";

            // Address From
            if (addrFrom == "11")
                decode = "MASTER";
            else if (addrFrom == "22" && control == "01")
            {
                decode = "SLAVE1 - ACKNOWLEDGE! - (Function: 0x" + func + ")";
                decodedList.Items.Add(decode);
                return;
            }
            else if (addrFrom == "33" && control == "01")
            {
                decode = "SLAVE2 - ACKNOWLEDGE! - (Function: 0x" + func + ")";
                decodedList.Items.Add(decode);
                return;
            }
            else if (addrFrom == "44" && control == "01")
            {
                decode = "SLAVE3 - ACKNOWLEDGE! - (Function: 0x" + func + ")";
                decodedList.Items.Add(decode);
                return;
            }
            decode += " -> ";

            // Address To
            if (addrTo == "22")
                decode += "SLAVE1";
            else if (addrTo == "33")
                decode += "SLAVE2";
            else if (addrTo == "44")
                decode += "SLAVE3";
            decode += " - ";

            // Function
            if (func == "01")
                decode += "Tam Tur Sağ";
            else if (func == "02")
                decode += "Tam Tur Sol";
            else if (func == "03")
                decode += "Yarım Tur Sağ";
            else if (func == "04")
                decode += "Yarım Tur Sol";
            else if (func == "05")
                decode += "Toggle Sağ";
            else if (func == "06")
                decode += "Toggle Sol";
            else if (func == "07")
                decode += "Motoru Durdur";
            else if (func == "08")
            {
                int d = Int16.Parse(data, System.Globalization.NumberStyles.HexNumber);
                decode += "Motorun Yeni Hızı = " + d.ToString();
                hiz_yazdirma(addrTo, d);
            }

            decodedList.Items.Add(decode);
        }

        public void hiz_yazdirma(string slv, int data)
        {
            int delay = data * 10;
            int rpm = (60 * 1000) / (delay * 48);

            if(slv == "22") // SLAVE1
            {
                slv1_data.Text = "data = " + data.ToString();
                slv1_rpm.Text = rpm.ToString() + " rpm";
            }
            else if(slv == "33") // SLAVE2
            {
                slv2_data.Text = "data = " + data.ToString();
                slv2_rpm.Text = rpm.ToString() + " rpm";
            }
            else if(slv == "44") // SLAVE3
            {
                slv3_data.Text = "data = " + data.ToString();
                slv3_rpm.Text = rpm.ToString() + " rpm";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
