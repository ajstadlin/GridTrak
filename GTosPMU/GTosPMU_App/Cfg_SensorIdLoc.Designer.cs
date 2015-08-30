namespace GTosPMU
{
  partial class Cfg_SensorIdLoc
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
      this.label1 = new System.Windows.Forms.Label();
      this.SensorIdTXT = new System.Windows.Forms.TextBox();
      this.SensorNameTXT = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.LocNameTXT = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.SensorPinTXT = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
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
      this.CaptionLBL.Text = "Sensor ID and Location";
      this.CaptionLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(15, 34);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 16);
      this.label1.TabIndex = 1;
      this.label1.Text = "Sensor ID#";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // SensorIdTXT
      // 
      this.SensorIdTXT.Location = new System.Drawing.Point(121, 31);
      this.SensorIdTXT.Name = "SensorIdTXT";
      this.SensorIdTXT.Size = new System.Drawing.Size(80, 20);
      this.SensorIdTXT.TabIndex = 0;
      this.SensorIdTXT.Validating += new System.ComponentModel.CancelEventHandler(this.SensorIdTXT_Validating);
      // 
      // SensorNameTXT
      // 
      this.SensorNameTXT.Location = new System.Drawing.Point(121, 57);
      this.SensorNameTXT.Name = "SensorNameTXT";
      this.SensorNameTXT.Size = new System.Drawing.Size(220, 20);
      this.SensorNameTXT.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(15, 61);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 16);
      this.label2.TabIndex = 3;
      this.label2.Text = "Sensor Name";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // LocNameTXT
      // 
      this.LocNameTXT.Location = new System.Drawing.Point(121, 83);
      this.LocNameTXT.Name = "LocNameTXT";
      this.LocNameTXT.Size = new System.Drawing.Size(220, 20);
      this.LocNameTXT.TabIndex = 3;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(15, 87);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(100, 16);
      this.label3.TabIndex = 5;
      this.label3.Text = "Location Name";
      this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // SensorPinTXT
      // 
      this.SensorPinTXT.Location = new System.Drawing.Point(261, 31);
      this.SensorPinTXT.Name = "SensorPinTXT";
      this.SensorPinTXT.Size = new System.Drawing.Size(80, 20);
      this.SensorPinTXT.TabIndex = 1;
      this.SensorPinTXT.Validating += new System.ComponentModel.CancelEventHandler(this.SensorPinTXT_Validating);
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(211, 34);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(44, 16);
      this.label4.TabIndex = 7;
      this.label4.Text = "PIN#";
      this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // Cfg_SensorIdLoc
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.SensorPinTXT);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.LocNameTXT);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.SensorNameTXT);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.SensorIdTXT);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.CaptionPAN);
      this.Name = "Cfg_SensorIdLoc";
      this.Size = new System.Drawing.Size(485, 419);
      this.ParentChanged += new System.EventHandler(this.Cfg_SensorIdLoc_ParentChanged);
      this.CaptionPAN.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel CaptionPAN;
    private System.Windows.Forms.Label CaptionLBL;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox SensorIdTXT;
    private System.Windows.Forms.TextBox SensorNameTXT;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox LocNameTXT;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox SensorPinTXT;
    private System.Windows.Forms.Label label4;
  }
}
