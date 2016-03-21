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

namespace SerialPortEX
{
   
    public partial class Demo : Form
    {
        LLSerialPort serialport;
        public Demo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialport = LLSerialPortFactory.CreateInstance("LLSerialImplement");//create the instance
            serialport.OpenSerialPort("COM3");//open the serial port
            serialport.DataUpdated += (s, data) => {//data come event
                richTextBox1.UISafeSet(()=> {
                    foreach (var item in data.SerialData)
                    {
                        richTextBox1.AppendText("0x"+item.ToString("X")+"\t");//hex display
                    }
                    richTextBox1.AppendText("\n");
                });
                
            };
        }
        //send data
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] cmd = { 0xAA, 0xBB, 0x01, 0x0D, 0x01 };
            serialport.SendData(cmd);
        }

    }
}
