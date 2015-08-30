using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GTosPMU
{
  public partial class Cfg_SensorIdLoc : UserControl
  {
    public Cfg_SensorIdLoc()
    {
      InitializeComponent();
    }

    public void Config_Load()
    {
      this.SensorIdTXT.Text = Sensor.Sensor_ID.ToString();
      this.SensorPinTXT.Text = Sensor.Sensor_PIN.ToString();
      this.SensorNameTXT.Text = Sensor.Sensor_Name;
      this.LocNameTXT.Text = Sensor.Location_Name;
    }

    public void Config_Save()
    {
      Sensor.Sensor_ID = Convert.ToUInt16(this.SensorIdTXT.Text.Trim());
      Sensor.Sensor_PIN = Convert.ToUInt16(this.SensorPinTXT.Text.Trim());
      Sensor.Sensor_Name = this.SensorNameTXT.Text.Trim();
      Sensor.Location_Name = this.LocNameTXT.Text.Trim();
    }

    private void Cfg_SensorIdLoc_ParentChanged(object sender, EventArgs e)
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


    private void SensorIdTXT_Validating(object sender, CancelEventArgs e)
    {
      try
      {
        Sensor.Sensor_ID = Convert.ToUInt16(this.SensorIdTXT.Text.Trim());
      }
      catch (Exception ex)
      {
        Log.Err(ex, "Cfg_SensorIdLoc", "Invalid Sensor ID #.  Please enter a 16bit unsigned integer for Sensor ID #.", Log.LogDevice.LOG_DLG);
        this.SensorIdTXT.Text = Sensor.Sensor_ID.ToString();
        this.SensorIdTXT.Focus();
      }
    }

    private void SensorPinTXT_Validating(object sender, CancelEventArgs e)
    {
      try
      {
        Sensor.Sensor_PIN = Convert.ToUInt16(this.SensorPinTXT.Text.Trim());
      }
      catch (Exception ex)
      {
        Log.Err(ex, "Cfg_SensorPinLoc", "Invalid Sensor PIN #.  Please enter a 16bit unsigned integer for Sensor PIN #.", Log.LogDevice.LOG_DLG);
        this.SensorPinTXT.Text = Sensor.Sensor_PIN.ToString();
        this.SensorPinTXT.Focus();
      }
    }


  }
}
