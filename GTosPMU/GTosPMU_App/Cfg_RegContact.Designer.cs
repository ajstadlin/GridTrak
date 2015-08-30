namespace GTosPMU
{
  partial class Cfg_RegContact
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.CaptionPAN = new System.Windows.Forms.Panel();
      this.CaptionLBL = new System.Windows.Forms.Label();
      this.AccountPswdTXT = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.AccountEMailTXT = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.AccountIdTXT = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.CaptionPAN.SuspendLayout();
      this.SuspendLayout();
      // 
      // CaptionPAN
      // 
      this.CaptionPAN.BackColor = System.Drawing.Color.SteelBlue;
      this.CaptionPAN.Controls.Add(this.CaptionLBL);
      this.CaptionPAN.Dock = System.Windows.Forms.DockStyle.Top;
      this.CaptionPAN.ForeColor = System.Drawing.Color.White;
      this.CaptionPAN.Location = new System.Drawing.Point(0, 0);
      this.CaptionPAN.Name = "CaptionPAN";
      this.CaptionPAN.Size = new System.Drawing.Size(485, 20);
      this.CaptionPAN.TabIndex = 0;
      // 
      // CaptionLBL
      // 
      this.CaptionLBL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.CaptionLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CaptionLBL.Location = new System.Drawing.Point(2, 2);
      this.CaptionLBL.Name = "CaptionLBL";
      this.CaptionLBL.Size = new System.Drawing.Size(480, 16);
      this.CaptionLBL.TabIndex = 0;
      this.CaptionLBL.Text = "Account Information";
      this.CaptionLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // AccountPswdTXT
      // 
      this.AccountPswdTXT.Location = new System.Drawing.Point(117, 78);
      this.AccountPswdTXT.Name = "AccountPswdTXT";
      this.AccountPswdTXT.Size = new System.Drawing.Size(192, 20);
      this.AccountPswdTXT.TabIndex = 12;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(11, 82);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 16);
      this.label3.TabIndex = 11;
      this.label3.Text = "Account Password";
      this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // AccountEMailTXT
      // 
      this.AccountEMailTXT.Location = new System.Drawing.Point(117, 52);
      this.AccountEMailTXT.Name = "AccountEMailTXT";
      this.AccountEMailTXT.Size = new System.Drawing.Size(192, 20);
      this.AccountEMailTXT.TabIndex = 10;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(11, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 16);
      this.label2.TabIndex = 9;
      this.label2.Text = "Account EMail";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // AccountIdTXT
      // 
      this.AccountIdTXT.Location = new System.Drawing.Point(117, 26);
      this.AccountIdTXT.Name = "AccountIdTXT";
      this.AccountIdTXT.Size = new System.Drawing.Size(100, 20);
      this.AccountIdTXT.TabIndex = 8;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(11, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 16);
      this.label1.TabIndex = 7;
      this.label1.Text = "Account ID#";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // Config_RegContact
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.AccountPswdTXT);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.AccountEMailTXT);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.AccountIdTXT);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.CaptionPAN);
      this.Name = "Config_RegContact";
      this.Size = new System.Drawing.Size(485, 419);
      this.ParentChanged += new System.EventHandler(this.Cfg_RegContact_ParentChanged);
      this.CaptionPAN.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel CaptionPAN;
    private System.Windows.Forms.Label CaptionLBL;
    private System.Windows.Forms.TextBox AccountPswdTXT;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox AccountEMailTXT;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox AccountIdTXT;
    private System.Windows.Forms.Label label1;
  }
}
