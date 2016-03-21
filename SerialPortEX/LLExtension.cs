using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortEX
{
    /// <summary>
    /// This class to help build a more comfortable class extension
    /// </summary>
  public static  class LLExtension
    {
        /// <summary>
        /// Serial port extension method, with a delegate method to cope the return port list data
        /// </summary>
        /// <param name="self"></param>
        /// <param name="handler"></param>
        public static void PortsOnChanged(this SerialPort self, Action<List<string>> handler)
        {
            LLSerialPortMonitor moni = LLSerialPortMonitor.CreateInstance();
            moni.PortsChangedEvent += (s,e) => {
                    handler(e.Ports);
            };

        }
        /// <summary>
        /// UI Thread safe method
        /// </summary>
        /// <param name="self"></param>
        /// <param name="handler"></param>
        public static void UISafeSet(this Control self,Action handler)
        {
            if (self.InvokeRequired)
            {
                self.Invoke(handler);
            }
            else
            {
                handler();
            }
        }
    }
}
