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
  public partial class Cfg_Terminal : UserControl
  {
    public Cfg_Terminal()
    {
      InitializeComponent();
    }


    public void Config_Load()
    {
      this.TermMaxLinesUD.Value = Cfg.Term_MaxLines;
      this.TermTimerIntervalUD.Value = Cfg.Term_TimerInterval;
      this.DisplayTimerIntervalUD.Value = Cfg.Display_TimerInterval;
      this.DataPurgeIntervalUD.Value = Cfg.DataPurge_TimerInterval;
    }


    public void Config_Save()
    {
      Cfg.Term_MaxLines = Convert.ToInt32(this.TermMaxLinesUD.Value);
      Cfg.Term_TimerInterval = Convert.ToInt32(this.TermTimerIntervalUD.Value);
      Cfg.Display_TimerInterval = Convert.ToInt32(this.DisplayTimerIntervalUD.Value);
      Cfg.DataPurge_TimerInterval = Convert.ToInt32(this.DataPurgeIntervalUD.Value);
    }


    private void Cfg_Terminal_ParentChanged(object sender, EventArgs e)
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
