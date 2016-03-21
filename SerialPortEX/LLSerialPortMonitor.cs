using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortEX
{
    /// <summary>
    /// This class used to monitor the IO Ports status on computer,like port is plug in or plug in, it will return the current ports list ,with a singleton pattern
    /// </summary>
    class LLSerialPortMonitor
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
        #region  return a singleton
        static LLSerialPortMonitor _serial=new LLSerialPortMonitor();
        public static LLSerialPortMonitor CreateInstance()
        {
            return _serial;
        }
        #endregion
        private LLSerialPortMonitor()
        {
            _Ports = new List<string>();
            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Enabled = true;
            timer.Elapsed += delegate {
                string[] tempPorts = SerialPort.GetPortNames();   
                List<string> tmplist = new List<string>(tempPorts);
                Ports = tmplist;
            };
        }
        /// <summary>
        /// to compare whether the port did find something change
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
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
        /// <summary>
        /// When Ports have changed trigered this event
        /// </summary>
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
