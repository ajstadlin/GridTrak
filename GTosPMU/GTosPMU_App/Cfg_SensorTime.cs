using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTosPMU
{
  /// <summary>
  /// Sensor Time Source Configuration
  /// 05/12/13 1.0.9.14  Date_FormatString config option added
  /// 04/18/13 1.0.9.12  Frequency Chart Adjust configuration value added
  /// 03/23/12 1.0.9.7  GPS Configuration updated
  /// </summary>
  public partial class Cfg_SensorTime : UserControl
  {
    public Cfg_SensorTime()
    {
      InitializeComponent();
    }


    public void Config_Load()
    { /// 1.0.9.14
      this.DateFormatStringDdl.Text = Cfg.Date_FormatString;
      this.HostSyncRK.Checked = Sensor.Host_Sync;   // !((UInt16)(Sensor.Use_GPS & Sensor.USE_GPS_TIME) == Sensor.USE_GPS_TIME);
      this.GpsSyncRK.Checked = !this.HostSyncRK.Checked;
      this.PpsEnableCHK.Checked = (Sensor.Use_GPS & Sensor.USE_GPS_PPS) == Sensor.USE_GPS_PPS;
      this.SyncIntervalUD.Value = Sensor.Sync_Interval;
      this.Nominal60HzRK.Checked = (Sensor.Nominal_Hz == (byte)60);
      this.Nominal50HzRK.Checked = (Sensor.Nominal_Hz == (byte)50);
      for (int ii = 0; ii < this.IntervalUD.Items.Count; ii++)
      {
        if (this.IntervalUD.Items[ii].ToString() == Sensor.Interval.ToString())
        {
          this.IntervalUD.SelectedIndex = ii;
          break;
        }
      }
      CyclesLB_Update();
      this.HzChartAdjustTxt.Text = Cfg.HzChart_Adjust.ToString();
    }


    public void Config_Save()
    { /// 1.0.9.14
      Cfg.Date_FormatString = DateFormatStringDdl.Text;
      if (this.PpsEnableCHK.Checked)
      {
        // Set the Use GPS PPS
        Sensor.Use_GPS = (UInt16)(Sensor.Use_GPS | Sensor.USE_GPS_PPS);
      }
      else
      {
        // Clear the Use GPS PPS
        Sensor.Use_GPS = (UInt16)(Sensor.Use_GPS & Sensor.USE_GPS_TIME);
      }

      if (this.GpsSyncRK.Checked)
      {
        // Set the Use GPS Time
        Sensor.Use_GPS = (UInt16)(Sensor.Use_GPS | Sensor.USE_GPS_TIME);
      }
      else
      {
        // Clear the Use_GPS Time
        Sensor.Use_GPS = (UInt16)(Sensor.Use_GPS & Sensor.USE_GPS_PPS);
      }

      Sensor.Sync_Interval = Convert.ToInt32(this.SyncIntervalUD.Value);
      if (this.Nominal50HzRK.Checked)
      {
        Sensor.Nominal_Hz = (byte)50;
      }
      else
      {
        Sensor.Nominal_Hz = (byte)60;
      }
      try
      {
        Sensor.Interval = Convert.ToByte(this.IntervalUD.Text);
      }
      catch
      {
        Sensor.Interval = Sensor.DEF_INTERVAL;
        this.IntervalUD.Text = "6";
      }

      try
      {
        this.HzChartAdjustTxt.Text = this.HzChartAdjustTxt.Text.Trim();
        Cfg.HzChart_Adjust = Convert.ToSingle(this.HzChartAdjustTxt.Text);
      }
      catch
      {
        this.HzChartAdjustTxt.Text = "0.0";
      }
    }



    private void Cfg_SensorTime_ParentChanged(object sender, EventArgs e)
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


    private void CyclesLB_Update()
    {
      try
      {
        // 1 / (cycles/sample) * Nominal_Hz Cycles/sec = samples / second
        this.CyclesLB.Text = "per Sample = " + Convert.ToString((Int32)((float)(1 / Convert.ToSingle(this.IntervalUD.SelectedItem.ToString()) * (float)Sensor.Nominal_Hz)))
                              + " samples/sec";
      }
      catch (Exception ex)
      {
      }
    }

    private void IntervalUD_SelectedItemChanged(object sender, EventArgs e)
    {
      CyclesLB_Update();
    }


  }
}
