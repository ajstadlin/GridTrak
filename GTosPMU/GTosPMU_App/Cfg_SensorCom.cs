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
  public partial class Cfg_SensorCom : UserControl
  {
    public Cfg_SensorCom()
    {
      InitializeComponent();
    }


    public void Config_Load()
    {
      this.PortNameTXT.Text = ComPorts.Port1_Name;
      this.ReadTimeoutUD.Value = ComPorts.Port1_ReadTimeout;
      this.ReadBufferSizeUD.Value = ComPorts.Port1_ReadBufferSize;
      this.ReceivedBytesThresholdUD.Value = ComPorts.Port1_ReceivedBytesThreshold;
      this.WriteTimeoutUD.Value = ComPorts.Port1_WriteTimeout;
      this.WriteBufferSizeUD.Value = ComPorts.Port1_WriteBufferSize;
    }


    public void Config_Save()
    {
      ComPorts.Port1_Name = this.PortNameTXT.Text.Trim();
      ComPorts.Port1_ReadTimeout = Convert.ToInt32(this.ReadTimeoutUD.Value);
      ComPorts.Port1_ReadBufferSize = Convert.ToInt32(this.ReadBufferSizeUD.Value);
      ComPorts.Port1_ReceivedBytesThreshold = Convert.ToInt32(this.ReceivedBytesThresholdUD.Value);
      ComPorts.Port1_WriteTimeout = Convert.ToInt32(this.WriteTimeoutUD.Value);
      ComPorts.Port1_WriteBufferSize = Convert.ToInt32(this.WriteBufferSizeUD.Value);
    }



    private void Cfg_SensorCom_ParentChanged(object sender, EventArgs e)
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
