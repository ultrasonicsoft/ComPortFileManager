using System;
using System.Configuration;
using System.IO.Ports;

namespace ComPortAssignment
{
    internal class ComPortConfigurationBuilder
    {
        internal ComPortConfiguration BuildComPort()
        {
            var comPortConfiguration = new ComPortConfiguration();
            comPortConfiguration.PortName = ConfigurationManager.AppSettings["PortName"];
            comPortConfiguration.BaudRate = int.Parse(ConfigurationManager.AppSettings["BaudRate"]);
            comPortConfiguration.DataBits = int.Parse(ConfigurationManager.AppSettings["DataBits"]);
            comPortConfiguration.Handshake = (Handshake)Enum.Parse(typeof(Handshake), ConfigurationManager.AppSettings["Handshake"]);
            comPortConfiguration.Parity = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["Parity"]);
            comPortConfiguration.StopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigurationManager.AppSettings["StopBits"]);
            comPortConfiguration.ReadTimeOut = int.Parse(ConfigurationManager.AppSettings["ReadTimeOut"]);
            comPortConfiguration.WriteTimeOut = int.Parse(ConfigurationManager.AppSettings["WriteTimeOut"]);
            return comPortConfiguration;
        }
    }
}
