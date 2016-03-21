using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortEX
{
    /// <summary>
    /// this the factory method to generate the instance with the Serial implement name
    /// </summary>
    class LLSerialPortFactory
    {
        static LLSerialPort _llserial;
        /// <summary>
        /// the Serial implement name
        /// </summary>
        /// <param name="SerialName"></param>
        /// <returns></returns>
        public static LLSerialPort CreateInstance(string SerialName)
        {
            _llserial = (LLSerialPort)Assembly.Load("SerialPortEX").CreateInstance("SerialPortEX."+SerialName);
            return _llserial;
        }
    }
}
