using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO.Ports;
namespace SerialPortEX
{
    public abstract class LLSerialPortBase : IDisposable
    {
        protected SerialPort serialPort;
        protected List<byte[]> sendBuffer;
        protected List<byte> readBuffer;
        protected Timer sendTimer;
        public LLSerialPortBase()
        {
            serialPort = new SerialPort();
            sendBuffer = new List<byte[]>();
            readBuffer = new List<byte>();
            sendTimer = new Timer(30);
            sendTimer.Elapsed += new ElapsedEventHandler(sendTimer_Elapsed);
        }

        #region 属性
        /// <summary>
        /// port name
        /// </summary>
        public string PortName
        {
            get { return serialPort.PortName; }
            set { serialPort.PortName = value; }
        }
        /// <summary>
        /// is open
        /// </summary>
        public bool IsOpen
        {
            get { return serialPort.IsOpen; }
        }
        #endregion

        #region 实体方法

        public void Load()
        {
            try
            {
                serialPort.Open();
                serialPort.DataReceived += new SerialDataReceivedEventHandler(OnDataReceived);
            }
            catch { }
        }
        /// <summary>
        /// Serial port load
        /// </summary>
        public void Load(string portName)
        {
            serialPort.PortName = portName;
            Load();
        }
        /// <summary>
        /// Serial port unload
        /// </summary>
        public void UnLoad()
        {
            serialPort.DiscardInBuffer();
            serialPort.DiscardOutBuffer();
            serialPort.Close();

            serialPort.Dispose();
            ((IDisposable)this).Dispose();
        }
        /// <summary>
        /// data receive event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int len = serialPort.BytesToRead;
            byte[] buff = new byte[len];
            serialPort.Read(buff, 0, len);
            readBuffer.AddRange(buff);
            serialPort.DiscardInBuffer();
            if (buff.Length > 0)
            {
                DataProcess();

            }
        }
        private void sendTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!serialPort.IsOpen || sendBuffer.Count <= 0)
            {
                sendTimer.Enabled = false;
                return;
            }
            try
            {
                byte[] data = sendBuffer[0];
                serialPort.Write(data, 0, data.Length);
                sendBuffer.Remove(data);
            }
            catch { }
        }
        /// <summary>
        /// add send data to list
        /// </summary>
        protected void AddToSendBuffer(byte[] code)
        {
            if (serialPort.IsOpen)
            {
                sendBuffer.Add(code);
                sendTimer.Enabled = true;
            }
        }

        protected void AddToSendBuffer(string code)
        {
            if (serialPort.IsOpen)
            {
                byte[] data = Encoding.Default.GetBytes(code);
                sendBuffer.Add(data);
                sendTimer.Enabled = true;
            }
        }
        #endregion

        #region 抽象方法
        /// <summary>
        /// Data process
        /// </summary>
        protected abstract void DataProcess();
        #endregion

        #region 资源释放
        /// <summary>
        /// Dispose
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Dispose interface
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
        }
        #endregion
    }
}

