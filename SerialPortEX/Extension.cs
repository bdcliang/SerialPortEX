using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortEx
{
  public static  class Extension
    {
        public static void PortsOnChanged(this SerialPort self, Action<List<string>> handler)
        {
            SerialPortMonitor moni = SerialPortMonitor.CreateInstance();
            moni.PortsChangedEvent += (s,e) => {
                    handler(e.Ports);
            };

        }
    }
}
