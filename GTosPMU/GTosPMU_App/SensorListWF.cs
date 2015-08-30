using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GTosPMU
{
  public partial class SensorListWF : Form
  {
    public SensorListWF()
    {
      InitializeComponent();
      Load_SensorList();
    }


    private void Load_SensorList()
    {
      SensorList.Load_ListDS();
      this.SensorListBS.DataSource = SensorList.SensorDS.Tables[0].DefaultView;
      this.SensorListDGV.DataSource = this.SensorListBS;
    }



    private void RefreshTBTN_Click(object sender, EventArgs e)
    {
      SensorList.Refresh_ListDS();
      this.SensorListBS.DataSource = SensorList.SensorDS.Tables[0].DefaultView;
      this.SensorListDGV.DataSource = this.SensorListBS;
    }


    private void SaveTBTN_Click(object sender, EventArgs e)
    {
      // Save a local copy of the Sensor List and Sensor Selections
      if (SensorList.SensorDS != null)
      {
        this.SensorListDGV.EndEdit();
        this.SensorListBS.EndEdit();
        SensorList.Save_ListDS();
      }
    }

  }
}
