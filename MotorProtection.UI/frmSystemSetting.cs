using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using MotorProtection.Core.Log;
using MotorProtection.Core.Data.Entities;
using MotorProtection.Core.Cache;
using MotorProtection.Core;

namespace MotorProtection.UI
{
    public partial class frmSystemSetting : Form
    {
        public frmSystemSetting()
        {
            InitializeComponent();
        }

        private void UpdateAudioTestButtonStatus()
        {
            if (string.IsNullOrEmpty(txtAlarmAudioPath.Text.Trim()))
            {
                btnAudioTest.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                btnAudioTest.Enabled = true;
                btnSave.Enabled = true;
            }
        }

        private void frmSystemSetting_Load(object sender, EventArgs e)
        {
            Initialize();
            UpdateAudioTestButtonStatus();
        }

        private void Initialize()
        {
            txtAlarmAudioPath.Text = AppConfig.Audio_Alarm_FilePath == null ? "" : AppConfig.Audio_Alarm_FilePath.Trim();
        }

        private void btnAudioTest_Click(object sender, EventArgs e)
        {
            var filePath = txtAlarmAudioPath.Text.Trim();

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("请先选择音频文件");
            }
            else
            {
                var fileExtension = System.IO.Path.GetExtension(filePath);
                if (fileExtension.Trim('.').ToLower() != "wav")
                {
                    MessageBox.Show("系统仅支持WAV格式文件，请重新选择");
                }
                else
                {
                    try
                    {
                        SoundPlayer player = new SoundPlayer(filePath);
                        player.Load();
                        player.Play();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("请联系管理员\\n" + ex.Message);
                        LogController.LogError(LoggingLevel.Error, ex).Add("Description", "导入文件出错").Write();
                    }
                }
            }
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            ofdAlarmAudio.ShowDialog();
            txtAlarmAudioPath.Text = ofdAlarmAudio.FileName;
            UpdateAudioTestButtonStatus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (MotorProtectorEntities ctt = new MotorProtectorEntities())
                {
                    var filePath = ctt.SystemConfigs.Where(sc => sc.Name == "Audio_Alarm_FilePath").FirstOrDefault();
                    if (filePath != null)
                    {
                        filePath.Value = txtAlarmAudioPath.Text.Trim();
                    }
                    else
                    {
                        SystemConfig config = new SystemConfig()
                        {
                            Name = "Audio_Alarm_FilePath",
                            Value = txtAlarmAudioPath.Text.Trim()
                        };
                        ctt.SystemConfigs.AddObject(config);
                    }

                    ctt.SaveChanges();
                }

                // sync caches
                CacheController.UpdateAllCacheGroupTimestamp();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存出错，请联系管理员");
                LogController.LogError(LoggingLevel.Error, ex).Add("Description", "保存音频文件路径出错").Write();
            }
        }
    }
}
