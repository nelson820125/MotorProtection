using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlphaProtocal.Core.MODBUS;
using MotorProtection.Core;
using System.IO.Ports;

namespace ModbusTester
{
    public partial class SerialPortTester : Form
    {
        public delegate void SetTxtShowText(string msg);
        private SerialComm _comm = new SerialComm();

        public SerialPortTester()
        {
            InitializeComponent();

            BindComputerComms();

            InitBaudRate();
            InitParity();
            InitDataBits();
            InitStopBits();

            _comm.DataReceived += Comm_DataReceived;
        }

        private void Comm_DataReceived(byte[] readBuffer)
        {
            if (readBuffer != null)
            {
                foreach (byte b in readBuffer)
                {
                    string content = txtShow.Text;
                    var newStr = Convert.ToString(b, 16).ToUpper();
                    content = content + (string.IsNullOrEmpty(content) ? "" + newStr.ToString().PadLeft(2, '0') : " " + newStr.ToString().PadLeft(2, '0'));
                    if (txtShow.InvokeRequired)
                    {
                        txtShow.Invoke(new SetTxtShowText(SetShowBox), new object[] { content });
                    }
                    else
                    {
                        txtShow.Text = content;
                    }
                }
            }
        }

        private void SetShowBox(string msg)
        {
            txtShow.Text = msg;
        }

        private void BindComputerComms()
        {
            Microsoft.VisualBasic.Devices.Computer cmbCOM = new Microsoft.VisualBasic.Devices.Computer();

            foreach (string s in cmbCOM.Ports.SerialPortNames)
            {
                cbxPortName.Items.Add(s);
            }

            cbxPortName.SelectedIndex = 0;
        }

        private void InitBaudRate()
        {
            cbxBaudRate.SelectedItem = "9600";
        }

        private void InitParity()
        {
            cbxParity.SelectedItem = "None";
        }

        private void InitDataBits()
        {
            cbxDataBits.SelectedItem = "8";
        }

        private void InitStopBits()
        {
            cbxStopBits.SelectedItem = "1";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
                string portName = cbxPortName.SelectedItem.ToString();
                int baudRate = Convert.ToInt32(cbxBaudRate.SelectedItem.ToString());
                int dataBits = Convert.ToInt32(cbxDataBits.SelectedItem.ToString());
                Parity parity = Parity.None;
                switch (cbxParity.SelectedItem.ToString())
                {
                    case "Odd":
                        parity = Parity.Odd;
                        break;
                    case "Even":
                        parity = Parity.Even;
                        break;
                    case "Mark":
                        parity = Parity.Mark;
                        break;
                    case "Space":
                        parity = Parity.Space;
                        break;
                    case "None":
                    default:
                        parity = Parity.None;
                        break;
                }
                StopBits stopBits = StopBits.One;
                switch (cbxStopBits.SelectedItem.ToString())
                {
                    case "1":
                        stopBits = StopBits.One;
                        break;
                    case "1.5":
                        stopBits = StopBits.OnePointFive;
                        break;
                    case "2":
                        stopBits = StopBits.Two;
                        break;
                    default:
                        stopBits = StopBits.None;
                        break;
                }

                string err = string.Empty;
                _comm.serialPort.PortName = portName;
                _comm.serialPort.Parity = parity;
                _comm.serialPort.DataBits = dataBits;
                _comm.serialPort.StopBits = stopBits;
                _comm.serialPort.BaudRate = baudRate;
                _comm.Open(out err);

                btnStart.Text = "Stop";
                btnSend.Enabled = true;
            }
            else
            {
                _comm.Close();
                btnStart.Text = "Start";
                btnSend.Enabled = false;
            }
        }

        private void SerialPortTester_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_comm.IsOpen)
            {
                _comm.Close();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte addr = 0x01;
            var sendStr = txtSend.Text;
            string[] dataStrs = sendStr.Split(' ');
            byte[] data = new byte[dataStrs.Length];
            for (int i = 0; i < dataStrs.Length; i++)
            {
                data[i] = Convert.ToByte(dataStrs[i], 16);
            }

            MODBUSRTU modbus = new MODBUSRTU();
            byte[] buffer = modbus.ReadHoldingRegisters(addr, data, data.Length);

            _comm.WritePort(buffer, 0, buffer.Length);
        }
    }
}
