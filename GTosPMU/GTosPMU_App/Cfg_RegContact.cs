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
  public partial class Cfg_RegContact : UserControl
  {
    public Cfg_RegContact()
    {
      InitializeComponent();
    }


    public void Config_Load()
    {
      this.AccountIdTXT.Text = Cfg.Account_Id.ToString();
      this.AccountEMailTXT.Text = Cfg.Account_EMail;
      this.AccountPswdTXT.Text = Cfg.Account_Pswd;
    }

    public void Config_Save()
    {
      Cfg.Account_Id = Convert.ToUInt16(this.AccountIdTXT.Text.Trim());
      Cfg.Account_EMail = this.AccountEMailTXT.Text.Trim();
      Cfg.Account_Pswd = this.AccountPswdTXT.Text.Trim();
    }


    private void Cfg_RegContact_ParentChanged(object sender, EventArgs e)
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
