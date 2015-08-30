using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GTosPMU
{
  public partial class Cfg_SensorCalib : UserControl
  {
    public Cfg_SensorCalib()
    {
      InitializeComponent();
    }


    public void Config_Load()
    {
      this.T3CalOffsetUD.Value = Sensor.T3CalOffset;
      this.T3CalStartUD.Value = Sensor.T3CalStart;
      this.HzCalOffsetUD.Value = Sensor.HzCalOffset;
    }


    public void Config_Save()
    {
      try
      {
        Sensor.T3CalOffset = Convert.ToInt16(this.T3CalOffsetUD.Value);
        Sensor.T3CalStart = Convert.ToInt16(this.T3CalStartUD.Value);
        Sensor.HzCalOffset = Convert.ToInt16(this.HzCalOffsetUD.Value);
      }
      catch (Exception ex)
      {
        Sensor.HzCalOffset = Sensor.DEF_HZCALOFFSET;
        this.HzCalOffsetUD.Value = Sensor.DEF_HZCALOFFSET;
      }
    }

    private void T3CalOffsetUD_Validating(object sender, CancelEventArgs e)
    {
      try
      {
        Sensor.T3CalOffset = Convert.ToInt16(this.T3CalOffsetUD.Value);
      }
      catch (Exception ex)
      {
        this.T3CalOffsetUD.Focus();
      }
    }

    private void T3CalStartUD_Validating(object sender, CancelEventArgs e)
    {
      try
      {
        Sensor.T3CalStart = Convert.ToInt16(this.T3CalStartUD.Value);
      }
      catch (Exception ex)
      {
        this.T3CalStartUD.Focus();
      }
    }

    private void HzCalOffsetUD_Validating(object sender, CancelEventArgs e)
    {
      try
      {
        Sensor.HzCalOffset = Convert.ToInt16(this.HzCalOffsetUD.Value);
      }
      catch (Exception ex)
      {
        this.HzCalOffsetUD.Focus();
      }
    }


  }
}
