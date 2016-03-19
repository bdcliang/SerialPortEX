using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortEX
{
    /// <summary>
    /// this the factory method to generate the instance
    /// </summary>
    class LLSerialPortFactory
    {
        static LLSerialPortDriver _llserial = new LLSerialPortDriver();
        public static LLSerialPortDriver CreateInstance()
        {
            return _llserial;
        }
    }
}
