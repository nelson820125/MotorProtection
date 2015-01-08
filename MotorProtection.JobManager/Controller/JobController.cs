using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotorProtection.Core;
using MotorProtection.Core.Log;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Constant;

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

        /// <summary>
        /// Get buffer data at the port to parse
        /// </summary>
        private static void serialPort_DataReceived(byte[] readBuffer, int address)
        {
            if (readBuffer != null)
            {
                // update device information in DB
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    Device device = ctt.Devices.Where(d => d.Address == address).FirstOrDefault();
                    int alarmSec = 0, alarmMin = 0, alarmHr = 0, alarmDay = 0, alarmMon = 0, alarmYear = 0, stopSec = 0, stopMin = 0, stopHr = 0, stopDay = 0, stopMon = 0, stopYear = 0;
                    int count = 0;
                    for (int i = 0; i <= readBuffer.Length; i = i + 2)
                    {
                        count++;

                        //parsing received data.
                        byte[] data = new byte[2];
                        data[0] = readBuffer[i];
                        data[1] = readBuffer[i + 1];
                        switch (count)
                        {
                            case 1: // current A value
                                device.CurrentA = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 2: // current B value
                                device.CurrentA = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 3: // current C value
                                device.CurrentA = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 4: // Voltage A value
                                device.VoltageA = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 5: // Voltage A value
                                device.VoltageB = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 6: // Voltage A value
                                device.VoltageC = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 7: // Power value
                                device.Power = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 8: // alarm second and minute value
                                byte[] secBytes = new byte[] { data[0] };
                                byte[] minBytes = new byte[] { data[1] };
                                alarmSec = BitConverter.ToInt16(secBytes, 0);
                                alarmMin = BitConverter.ToInt16(minBytes, 0);
                                break;
                            case 9: // alarm hour and day value
                                byte[] hrBytes = new byte[] { data[0] };
                                byte[] dayBytes = new byte[] { data[1] };
                                alarmHr = BitConverter.ToInt16(hrBytes, 0);
                                alarmDay = BitConverter.ToInt16(dayBytes, 0);
                                break;
                            case 10: // alarm month and year value
                                byte[] monBytes = new byte[] { data[0] };
                                byte[] yearBytes = new byte[] { data[1] };
                                alarmMon = BitConverter.ToInt16(monBytes, 0);
                                alarmYear = BitConverter.ToInt16(yearBytes, 0);
                                break;
                            case 11: // stop second and minute value
                                byte[] stopSecBytes = new byte[] { data[0] };
                                byte[] stopMinBytes = new byte[] { data[1] };
                                stopSec = BitConverter.ToInt16(stopSecBytes, 0);
                                stopMin = BitConverter.ToInt16(stopMinBytes, 0);
                                break;
                            case 12: // stop hour and day value
                                byte[] stopHrBytes = new byte[] { data[0] };
                                byte[] stopDayBytes = new byte[] { data[1] };
                                stopHr = BitConverter.ToInt16(stopHrBytes, 0);
                                stopDay = BitConverter.ToInt16(stopDayBytes, 0);
                                break;
                            case 13: // stop month and year value
                                byte[] stopMonBytes = new byte[] { data[0] };
                                byte[] stopYearBytes = new byte[] { data[1] };
                                stopMon = BitConverter.ToInt16(stopMonBytes, 0);
                                stopYear = BitConverter.ToInt16(stopYearBytes, 0);
                                break;
                            case 14: // Temperature A value
                                device.TemperatureA = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 15: // Temperature B value
                                device.TemperatureB = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 16: // Temperature C value
                                device.TemperatureC = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 17: // Temperature value
                                device.Temperature = (decimal)(BitConverter.ToDouble(data, 0) / 100);
                                break;
                            case 18:
                                device.FirstRMStatus = BitConverter.ToInt16(data, 0) == 1 ? true : false;
                                break;
                            case 19:
                                device.SecondRMStatus = BitConverter.ToInt16(data, 0) == 1 ? true : false;
                                break;
                            case 20:
                                Int16 status = BitConverter.ToInt16(data, 0);
                                if (status == 0) // 0 - working
                                    device.Status = ProtectorStatus.Normal;
                                else if (status == 15) // 0x0f - alarm
                                    device.Status = ProtectorStatus.Alarm;
                                else if (status == 255) // 0xff - stopped
                                    device.Status = ProtectorStatus.Stopped;
                                break;
                        }
                    }

                    // set alarm and stop time to device object
                    device.AlarmAt = new DateTime(alarmYear, alarmMon, alarmDay, alarmHr, alarmMin, alarmSec);
                    device.StopAt = new DateTime(stopYear, stopMon, stopDay, stopHr, stopMin, stopSec);                        

                    device.UpdateTime = DateTime.Now;

                    ctt.SaveChanges();
                }
            }
        }
    }
}
