using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SerialPortEX
{
    /// <summary>
    /// An abstract Factory class ,needed to be instanced
    /// </summary>
    public abstract class LLSerialPort : LLSerialPortBase
    {
        /// <summary>
        /// when detected the given PortName,then Open the Port,when the Port is no longer exist then unload the Serial port
        /// </summary>
        /// <param name="portName"></param>
        public void OpenSerialPort(string portName)
        {
            PortName = portName;
            serialPort.PortsOnChanged(list=> {
                if (list.Contains(PortName))
                {
                    Load(PortName);
                    Console.WriteLine("***********Serial port has been connect***************");
                }
                else
                    UnLoad();
            });
        }
        /// <summary>
        /// when close the serial port just put the PortName is empty
        /// </summary>
        public void CloseSerialPort()
        {
            PortName = "";
        }
        /// <summary>
        /// Send data to Serial Port
        /// </summary>
        /// <param name="data"></param>
        public void SendData(byte [] data)
        {
            AddToSendBuffer(data);
        }

        public void SendData(string data)
        {
            AddToSendBuffer(data);
        }


        #region 状态更新事件
        /// <summary>
        /// Data Update event
        /// </summary>
        public event EventHandler<StatusUpdatedEventArgs> DataUpdated;
        /// <summary>
        /// event triger
        /// </summary>
        /// <param name="e"></param>
        protected void OnStatusUpdated(StatusUpdatedEventArgs e)
        {
            if (DataUpdated != null)
                DataUpdated(this, e);
        }
        #endregion
    }

    public class StatusUpdatedEventArgs : EventArgs
    {

        public List<byte> SerialData { get; private set; }
        public StatusUpdatedEventArgs(List<byte> datain)
        {
            SerialData = datain;
        }
    }
}

