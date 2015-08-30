using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GTosPMU
{
  public partial class Cfg_SensorStart : UserControl
  {
    public Cfg_SensorStart()
    {
      InitializeComponent();
    }


    public void Config_Load()
    {
      this.AutoConnectCHK.Checked = Sensor.Auto_Connect;
      this.ConnectOnlyRBtn.Checked = Sensor.Connect_Only;
      this.AutoUpdateRBtn.Checked = Sensor.Auto_Update;
      this.AutoResetRBtn.Checked = Sensor.Auto_Reset;
      this.AutoRebootRBtn.Checked = Sensor.Auto_Reboot;
      this.ConfigFromSensorRBtn.Checked = Sensor.Auto_Config;
      this.FactoryRestoreRBtn.Checked = Sensor.Factory_Restore;
    }


    public void Config_Save()
    {
      Sensor.Auto_Connect = this.AutoConnectCHK.Checked;
      Sensor.Connect_Only = this.ConnectOnlyRBtn.Checked;
      Sensor.Auto_Update = this.AutoUpdateRBtn.Checked;
      Sensor.Auto_Reset = this.AutoResetRBtn.Checked;
      Sensor.Auto_Reboot = this.AutoRebootRBtn.Checked;
      Sensor.Auto_Config = this.ConfigFromSensorRBtn.Checked;
      Sensor.Factory_Restore = this.FactoryRestoreRBtn.Checked;
    }


    private void Cfg_SensorStart_ParentChanged(object sender, EventArgs e)
    {
      if (this.Parent == null)
      {
        // Save our settings to the Config DataSet when this control is swapped out.
        Config_Save();
      }
      else
      {
        // Load our settings from the Config DataSet when this control is selected
        Config_Load();
      }
    }


  }
}
