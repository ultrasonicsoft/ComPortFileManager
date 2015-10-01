using System;
using System.Windows.Forms;

namespace ComPortAssignment
{
    public partial class ComPortManager : Form
    {
        ComPortCommunicator communicator;
        public ComPortManager()
        {
            InitializeComponent();

            InitializeComPortSettings();
        }

        private void InitializeComPortSettings()
        {
            communicator = new ComPortCommunicator();
            txtPortName.Text = communicator.Configuration.PortName;
            txtBaudRate.Text = communicator.Configuration.BaudRate.ToString();
            txtDataBits.Text = communicator.Configuration.DataBits.ToString();
            txtHandshake.Text = communicator.Configuration.Handshake.ToString();
            txtParity.Text = communicator.Configuration.Parity.ToString();
            txtStopBits.Text = communicator.Configuration.StopBits.ToString();

            UpdatePortStatus();

            communicator.FileCopied += Communicator_FileCopied;
        }

        private void Communicator_FileCopied(string message)
        {
            if (txtLogs.InvokeRequired)
            {
                txtLogs.Invoke(new MethodInvoker(delegate { txtLogs.Text += message + Environment.NewLine; }));
            }
        }

        private void UpdatePortStatus()
        {
            lbComPortStatus.Text = communicator.IsPortOpen ? txtPortName.Text + " port is open" : txtPortName.Text + " port is closed";
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            if (!communicator.IsPortOpen)
                communicator.Open();
            UpdatePortStatus();
            btnOpenPort.Enabled = false;
            btnClosePort.Enabled = true;
        }

        private void btnClosePort_Click(object sender, EventArgs e)
        {
            if (communicator.IsPortOpen)
                communicator.Close();
            UpdatePortStatus();
            btnOpenPort.Enabled = true;
            btnClosePort.Enabled = false;
        }

        private void btnSendHelloWorld_Click(object sender, EventArgs e)
        {
            communicator.SendHelloWorld();
        }
    }
}
