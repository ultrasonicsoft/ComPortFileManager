using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace ComPortAssignment
{
    internal class ComPortConfiguration
    {
        internal string PortName { get; set; }
        internal int BaudRate { get; set; }
        internal int DataBits { get; set; }
        internal Handshake Handshake { get; set; }
        internal Parity Parity { get; set; }
        internal StopBits StopBits { get; set; }
        internal int ReadTimeOut { get; set; }
        internal int WriteTimeOut { get; set; }
    }
}
