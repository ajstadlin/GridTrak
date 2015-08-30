namespace GTosPMU
{
  partial class Cfg_SensorStart
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cfg_SensorStart));
      this.CaptionPAN = new System.Windows.Forms.Panel();
      this.CaptionLBL = new System.Windows.Forms.Label();
      this.SensorParamsGBX = new System.Windows.Forms.GroupBox();
      this.ConfigFromSensorRBtn = new System.Windows.Forms.RadioButton();
      this.ConnectOnlyRBtn = new System.Windows.Forms.RadioButton();
      this.FactoryRestoreRBtn = new System.Windows.Forms.RadioButton();
      this.AutoRebootRBtn = new System.Windows.Forms.RadioButton();
      this.AutoResetRBtn = new System.Windows.Forms.RadioButton();
      this.AutoUpdateRBtn = new System.Windows.Forms.RadioButton();
      this.AutoConnectCHK = new System.Windows.Forms.CheckBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label6 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.CaptionPAN.SuspendLayout();
      this.SensorParamsGBX.SuspendLayout();
      this.panel1.SuspendLayout();
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
      this.CaptionPAN.Size = new System.Drawing.Size(580, 20);
      this.CaptionPAN.TabIndex = 0;
      // 
      // CaptionLBL
      // 
      this.CaptionLBL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.CaptionLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CaptionLBL.Location = new System.Drawing.Point(2, 2);
      this.CaptionLBL.Name = "CaptionLBL";
      this.CaptionLBL.Size = new System.Drawing.Size(575, 16);
      this.CaptionLBL.TabIndex = 0;
      this.CaptionLBL.Text = "Startup Parameters";
      this.CaptionLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // SensorParamsGBX
      // 
      this.SensorParamsGBX.Controls.Add(this.ConfigFromSensorRBtn);
      this.SensorParamsGBX.Controls.Add(this.ConnectOnlyRBtn);
      this.SensorParamsGBX.Controls.Add(this.FactoryRestoreRBtn);
      this.SensorParamsGBX.Controls.Add(this.AutoRebootRBtn);
      this.SensorParamsGBX.Controls.Add(this.AutoResetRBtn);
      this.SensorParamsGBX.Controls.Add(this.AutoUpdateRBtn);
      this.SensorParamsGBX.Controls.Add(this.AutoConnectCHK);
      this.SensorParamsGBX.Controls.Add(this.panel1);
      this.SensorParamsGBX.Dock = System.Windows.Forms.DockStyle.Top;
      this.SensorParamsGBX.Location = new System.Drawing.Point(0, 20);
      this.SensorParamsGBX.Name = "SensorParamsGBX";
      this.SensorParamsGBX.Size = new System.Drawing.Size(580, 439);
      this.SensorParamsGBX.TabIndex = 14;
      this.SensorParamsGBX.TabStop = false;
      // 
      // ConfigFromSensorRBtn
      // 
      this.ConfigFromSensorRBtn.AutoSize = true;
      this.ConfigFromSensorRBtn.Checked = true;
      this.ConfigFromSensorRBtn.Location = new System.Drawing.Point(16, 50);
      this.ConfigFromSensorRBtn.Name = "ConfigFromSensorRBtn";
      this.ConfigFromSensorRBtn.Size = new System.Drawing.Size(231, 17);
      this.ConfigFromSensorRBtn.TabIndex = 2;
      this.ConfigFromSensorRBtn.TabStop = true;
      this.ConfigFromSensorRBtn.Text = "Load Configuration from Sensor on Connect";
      this.ConfigFromSensorRBtn.UseVisualStyleBackColor = true;
      // 
      // ConnectOnlyRBtn
      // 
      this.ConnectOnlyRBtn.AutoSize = true;
      this.ConnectOnlyRBtn.Location = new System.Drawing.Point(16, 96);
      this.ConnectOnlyRBtn.Name = "ConnectOnlyRBtn";
      this.ConnectOnlyRBtn.Size = new System.Drawing.Size(237, 17);
      this.ConnectOnlyRBtn.TabIndex = 4;
      this.ConnectOnlyRBtn.Text = "Open Connection Only - Resume Connection";
      this.toolTip1.SetToolTip(this.ConnectOnlyRBtn, "Automatically Reset the Sensor when a Connection to it is Opened.  This option do" +
        "es not change the Sensor\'s running configuration it only resets the Sensor\'s cou" +
        "nters.");
      this.ConnectOnlyRBtn.UseVisualStyleBackColor = true;
      // 
      // FactoryRestoreRBtn
      // 
      this.FactoryRestoreRBtn.AutoSize = true;
      this.FactoryRestoreRBtn.Location = new System.Drawing.Point(16, 165);
      this.FactoryRestoreRBtn.Name = "FactoryRestoreRBtn";
      this.FactoryRestoreRBtn.Size = new System.Drawing.Size(308, 17);
      this.FactoryRestoreRBtn.TabIndex = 7;
      this.FactoryRestoreRBtn.Text = "Restore Sensor to Factory Default Configuration on Connect";
      this.FactoryRestoreRBtn.UseVisualStyleBackColor = true;
      // 
      // AutoRebootRBtn
      // 
      this.AutoRebootRBtn.AutoSize = true;
      this.AutoRebootRBtn.Location = new System.Drawing.Point(16, 142);
      this.AutoRebootRBtn.Name = "AutoRebootRBtn";
      this.AutoRebootRBtn.Size = new System.Drawing.Size(303, 17);
      this.AutoRebootRBtn.TabIndex = 6;
      this.AutoRebootRBtn.Text = "Reboot Sensor on Connect to Remove Temporary Settings";
      this.toolTip1.SetToolTip(this.AutoRebootRBtn, "Automatically Reboot the Sensor when a Connection to it is Opened.  This option r" +
        "estarts the Sensor from its EEPROM saved configration.");
      this.AutoRebootRBtn.UseVisualStyleBackColor = true;
      // 
      // AutoResetRBtn
      // 
      this.AutoResetRBtn.AutoSize = true;
      this.AutoResetRBtn.Location = new System.Drawing.Point(16, 119);
      this.AutoResetRBtn.Name = "AutoResetRBtn";
      this.AutoResetRBtn.Size = new System.Drawing.Size(147, 17);
      this.AutoResetRBtn.TabIndex = 5;
      this.AutoResetRBtn.Text = "Reset Sensor on Connect";
      this.toolTip1.SetToolTip(this.AutoResetRBtn, "Automatically Reset the Sensor when a Connection to it is Opened.  This option do" +
        "es not change the Sensor\'s running configuration it only resets the Sensor\'s cou" +
        "nters.");
      this.AutoResetRBtn.UseVisualStyleBackColor = true;
      // 
      // AutoUpdateRBtn
      // 
      this.AutoUpdateRBtn.AutoSize = true;
      this.AutoUpdateRBtn.Location = new System.Drawing.Point(16, 73);
      this.AutoUpdateRBtn.Name = "AutoUpdateRBtn";
      this.AutoUpdateRBtn.Size = new System.Drawing.Size(219, 17);
      this.AutoUpdateRBtn.TabIndex = 3;
      this.AutoUpdateRBtn.Text = "Update Sensor Configuration on Connect";
      this.toolTip1.SetToolTip(this.AutoUpdateRBtn, "Automatically Update the Sensor\'s Configuration on Connecting to the Sensor");
      this.AutoUpdateRBtn.UseVisualStyleBackColor = true;
      // 
      // AutoConnectCHK
      // 
      this.AutoConnectCHK.AutoSize = true;
      this.AutoConnectCHK.Location = new System.Drawing.Point(16, 17);
      this.AutoConnectCHK.Name = "AutoConnectCHK";
      this.AutoConnectCHK.Size = new System.Drawing.Size(186, 17);
      this.AutoConnectCHK.TabIndex = 0;
      this.AutoConnectCHK.Text = "Auto Connect on Application Start";
      this.toolTip1.SetToolTip(this.AutoConnectCHK, "Auto Connect to the Sensor when this Application Starts");
      this.AutoConnectCHK.UseVisualStyleBackColor = true;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Info;
      this.panel1.Controls.Add(this.label6);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(3, 211);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(574, 225);
      this.panel1.TabIndex = 13;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.Location = new System.Drawing.Point(5, 5);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(566, 212);
      this.label6.TabIndex = 0;
      this.label6.Text = resources.GetString("label6.Text");
      // 
      // Cfg_SensorStart
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.SensorParamsGBX);
      this.Controls.Add(this.CaptionPAN);
      this.Name = "Cfg_SensorStart";
      this.Size = new System.Drawing.Size(580, 462);
      this.ParentChanged += new System.EventHandler(this.Cfg_SensorStart_ParentChanged);
      this.CaptionPAN.ResumeLayout(false);
      this.SensorParamsGBX.ResumeLayout(false);
      this.SensorParamsGBX.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel CaptionPAN;
    private System.Windows.Forms.Label CaptionLBL;
    private System.Windows.Forms.GroupBox SensorParamsGBX;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.CheckBox AutoConnectCHK;
    private System.Windows.Forms.RadioButton AutoUpdateRBtn;
    private System.Windows.Forms.RadioButton AutoResetRBtn;
    private System.Windows.Forms.RadioButton FactoryRestoreRBtn;
    private System.Windows.Forms.RadioButton AutoRebootRBtn;
    private System.Windows.Forms.RadioButton ConnectOnlyRBtn;
    private System.Windows.Forms.RadioButton ConfigFromSensorRBtn;
  }
}
