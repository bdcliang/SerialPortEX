using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortEx
{
    class SerialPortMonitor
    {
        private System.Timers.Timer timer;
        private List<string> _Ports;
        public List<string> Ports {
            get {
                return _Ports;
            }
            set {
                if (IsChanged(_Ports, value))
                     OnPortsChanged(value);
                _Ports = value;
            }
        }

        static SerialPortMonitor _serial=new SerialPortMonitor();
        public static SerialPortMonitor CreateInstance()
        {
            return _serial;
        }
        private SerialPortMonitor()
        {
            _Ports = new List<string>();
            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Enabled = true;
            timer.Elapsed += delegate {
                string[] tempPorts = SerialPort.GetPortNames();
                //icounter++;
                
                    
                List<string> tmplist = new List<string>(tempPorts);
                //if (icounter % 5 == 0)
                  //  tmplist.Add("Port" + icounter);
                Ports = tmplist;
               // Console.WriteLine("Ticker++"+tempPorts.Length);
            };
        }
       // int icounter = 0;
        private bool IsChanged(List<string> a, List<string> b)
        {
            if (a.Count != b.Count)
                return true;

            foreach (var item in a)
            {
                if (!b.Contains(item))
                    return true;
            }
            foreach (var item in b)
            {
                if (!a.Contains(item))
                    return true;
            }
            return false;
        }

        public event EventHandler<PortsChangedArgs> PortsChangedEvent;

        protected void OnPortsChanged(List<string> Ports)
        {
            if (PortsChangedEvent != null)
                PortsChangedEvent(this,new PortsChangedArgs(Ports));

        }
    }

        class PortsChangedArgs : EventArgs
        {
        public List<string> Ports = new List<string>();
        public PortsChangedArgs(List<string> Ports)
        {
            this.Ports = Ports;
        }

        }
}
