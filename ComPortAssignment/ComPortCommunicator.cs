using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace ComPortAssignment
{
    internal class ComPortCommunicator
    {
        internal ComPortConfiguration Configuration { get; set; }
        private CopyFileCommandExecutor copyFileCommandExecutor;
        private SerialPort serialPort;
        internal delegate void FileCopiedDelegate(string message);
        internal event FileCopiedDelegate FileCopied;
        internal bool IsPortOpen
        {
            get
            {
                return serialPort.IsOpen;
            }
        }

        internal ComPortCommunicator()
        {
            copyFileCommandExecutor = new CopyFileCommandExecutor();

            ComPortConfigurationBuilder builder = new ComPortConfigurationBuilder();
            Configuration = builder.BuildComPort();
            serialPort = new SerialPort();

            serialPort.BaudRate = Configuration.BaudRate;
            serialPort.DataBits = Configuration.DataBits;
            serialPort.Handshake = Configuration.Handshake;
            serialPort.Parity = Configuration.Parity;
            serialPort.PortName = Configuration.PortName;
            serialPort.StopBits = Configuration.StopBits;
            try { serialPort.DtrEnable = true; }
            catch { }
            try { serialPort.RtsEnable = true; }
            catch { }

            serialPort.ReadTimeout = Configuration.ReadTimeOut;
            serialPort.WriteTimeout = Configuration.WriteTimeOut;

            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);

        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort currentSerialPort = (SerialPort)sender;

                if (currentSerialPort == null)
                {
                    System.Windows.Forms.MessageBox.Show("There is error while reading data from COM port");
                    return;
                }
                int bufSize = 20;
                Byte[] dataBuffer = new Byte[bufSize];
                Console.WriteLine("Data Received at" + DateTime.Now);
                Console.WriteLine(currentSerialPort.Read(dataBuffer, 0, bufSize));
                string fileName = System.Text.ASCIIEncoding.ASCII.GetString(dataBuffer).Replace("\0", string.Empty).Trim();
                //string fileName = currentSerialPort.ReadExisting();
                Console.WriteLine("File name received on COM port is: " + fileName);

                bool isFileCopied = copyFileCommandExecutor.CopyFile(fileName);
                if (isFileCopied)
                {
                    var message = "File copied: " + fileName;
                    Console.WriteLine(message);
                    if (FileCopied != null)
                    {
                        FileCopied(message);
                    }
                }
                else
                    Console.WriteLine("File couldn't be copied: " + fileName);

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        //TODO: Just for testing purpose.
        internal void SendHelloWorld()
        {
            SerialPort com1Port = new SerialPort();
            ComPortConfigurationBuilder builder = new ComPortConfigurationBuilder();
            Configuration = builder.BuildComPort();
            serialPort = new SerialPort();

            com1Port.BaudRate = Configuration.BaudRate;
            com1Port.DataBits = Configuration.DataBits;
            com1Port.Handshake = Configuration.Handshake;
            com1Port.Parity = Configuration.Parity;
            com1Port.PortName = "COM1";
            com1Port.StopBits = Configuration.StopBits;
            try { com1Port.DtrEnable = true; }
            catch { }
            try { com1Port.RtsEnable = true; }
            catch { }

            com1Port.ReadTimeout = Configuration.ReadTimeOut;
            com1Port.WriteTimeout = Configuration.WriteTimeOut;

            if (com1Port.IsOpen == false)
                com1Port.Open();
            com1Port.Write("data");
            com1Port.Close();

        }

        internal bool Open()
        {
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            return serialPort.IsOpen;
        }

        internal void Close()
        {
            try
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
