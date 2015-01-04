using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core;
using MotorProtection.Core.Log;

namespace MotorProtection.JobManager.Controller
{
    public static class JobController
    {
        private static SerialComm _comm = new SerialComm();

        public static void Start()
        {
            _comm.serialPort.PortName = AppConfig.SerialComm_PortName;
            _comm.serialPort.BaudRate = string.IsNullOrEmpty(AppConfig.SerialComm_BaudRate) ? 9600 : Convert.ToInt32(AppConfig.SerialComm_BaudRate);
            _comm.serialPort.DataBits = 8;
            _comm.serialPort.StopBits = System.IO.Ports.StopBits.None;
            _comm.serialPort.Parity = System.IO.Ports.Parity.None;
            _comm.DataReceived += serialPort_DataReceived;

            try
            {
                string err;
                _comm.Open(out err);

                if (!string.IsNullOrEmpty(err))
                {
                    LogController.LogError(LoggingLevel.Level1).Add("Description", err).Write();
                }
            }
            catch (Exception ex)
            {
                LogController.LogError(LoggingLevel.Level1, ex).Add("Description", ex.Message).Write();
            }
        }

        public static void Stop()
        {
            if (_comm.IsOpen)
                _comm.Close();
        }

        private static void serialPort_DataReceived(byte[] readBuffer)
        {
            if (readBuffer != null)
            {
                foreach (byte b in readBuffer)
                {
                    //TODO
                }
            }
        }
    }
}
