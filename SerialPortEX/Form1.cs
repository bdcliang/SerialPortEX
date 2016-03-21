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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SerialPort myPorts = new SerialPort();
            myPorts.PortsOnChanged((list)=>{
                richTextBox1.UISafeSet(()=> {
                    foreach (var item in list)
                    {
                        richTextBox1.AppendText(item+"\n");
                    }
                });

            });
        }
    }
}
