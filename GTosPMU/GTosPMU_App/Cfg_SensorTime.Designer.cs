namespace GTosPMU
{
  partial class Cfg_SensorTime
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cfg_SensorTime));
      this.CaptionPAN = new System.Windows.Forms.Panel();
      this.CaptionLBL = new System.Windows.Forms.Label();
      this.TimeSourceGBX = new System.Windows.Forms.GroupBox();
      this.HostSyncRK = new System.Windows.Forms.RadioButton();
      this.GpsSyncRK = new System.Windows.Forms.RadioButton();
      this.PpsEnableCHK = new System.Windows.Forms.CheckBox();
      this.SyncIntervalUD = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.MeasureGBX = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label9 = new System.Windows.Forms.Label();
      this.HzChartAdjustTxt = new System.Windows.Forms.TextBox();
      this.CyclesLB = new System.Windows.Forms.Label();
      this.IntervalUD = new System.Windows.Forms.DomainUpDown();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.Nominal50HzRK = new System.Windows.Forms.RadioButton();
      this.Nominal60HzRK = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.label10 = new System.Windows.Forms.Label();
      this.DateFormatStringDdl = new System.Windows.Forms.ComboBox();
      this.CaptionPAN.SuspendLayout();
      this.TimeSourceGBX.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SyncIntervalUD)).BeginInit();
      this.panel1.SuspendLayout();
      this.MeasureGBX.SuspendLayout();
      this.panel3.SuspendLayout();
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
      this.CaptionLBL.Text = "Time Source and Measurement Interval";
      this.CaptionLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // TimeSourceGBX
      // 
      this.TimeSourceGBX.Controls.Add(this.DateFormatStringDdl);
      this.TimeSourceGBX.Controls.Add(this.label10);
      this.TimeSourceGBX.Controls.Add(this.HostSyncRK);
      this.TimeSourceGBX.Controls.Add(this.GpsSyncRK);
      this.TimeSourceGBX.Controls.Add(this.PpsEnableCHK);
      this.TimeSourceGBX.Controls.Add(this.SyncIntervalUD);
      this.TimeSourceGBX.Controls.Add(this.label3);
      this.TimeSourceGBX.Controls.Add(this.label8);
      this.TimeSourceGBX.Controls.Add(this.panel1);
      this.TimeSourceGBX.Controls.Add(this.label7);
      this.TimeSourceGBX.Dock = System.Windows.Forms.DockStyle.Top;
      this.TimeSourceGBX.Location = new System.Drawing.Point(0, 20);
      this.TimeSourceGBX.Name = "TimeSourceGBX";
      this.TimeSourceGBX.Size = new System.Drawing.Size(580, 264);
      this.TimeSourceGBX.TabIndex = 14;
      this.TimeSourceGBX.TabStop = false;
      this.TimeSourceGBX.Text = "Time Source";
      // 
      // HostSyncRK
      // 
      this.HostSyncRK.AutoSize = true;
      this.HostSyncRK.Checked = true;
      this.HostSyncRK.Location = new System.Drawing.Point(11, 40);
      this.HostSyncRK.Name = "HostSyncRK";
      this.HostSyncRK.Size = new System.Drawing.Size(55, 17);
      this.HostSyncRK.TabIndex = 23;
      this.HostSyncRK.TabStop = true;
      this.HostSyncRK.Text = "HOST";
      this.toolTip1.SetToolTip(this.HostSyncRK, "Synchronize the Sensor periodically to the Host PC.");
      this.HostSyncRK.UseVisualStyleBackColor = true;
      // 
      // GpsSyncRK
      // 
      this.GpsSyncRK.AutoSize = true;
      this.GpsSyncRK.Location = new System.Drawing.Point(11, 63);
      this.GpsSyncRK.Name = "GpsSyncRK";
      this.GpsSyncRK.Size = new System.Drawing.Size(47, 17);
      this.GpsSyncRK.TabIndex = 24;
      this.GpsSyncRK.Text = "GPS";
      this.toolTip1.SetToolTip(this.GpsSyncRK, "Synchronize the Sensor periodically with Time input from a connected GPS unit.");
      this.GpsSyncRK.UseVisualStyleBackColor = true;
      // 
      // PpsEnableCHK
      // 
      this.PpsEnableCHK.AutoSize = true;
      this.PpsEnableCHK.Location = new System.Drawing.Point(233, 64);
      this.PpsEnableCHK.Name = "PpsEnableCHK";
      this.PpsEnableCHK.Size = new System.Drawing.Size(83, 17);
      this.PpsEnableCHK.TabIndex = 22;
      this.PpsEnableCHK.Text = "PPS Enable";
      this.toolTip1.SetToolTip(this.PpsEnableCHK, "Enable the PPS Input to the Sensor.");
      this.PpsEnableCHK.UseVisualStyleBackColor = true;
      // 
      // SyncIntervalUD
      // 
      this.SyncIntervalUD.Increment = new decimal(new int[] {
            60,
            0,
            0,
            0});
      this.SyncIntervalUD.Location = new System.Drawing.Point(233, 40);
      this.SyncIntervalUD.Maximum = new decimal(new int[] {
            864000,
            0,
            0,
            0});
      this.SyncIntervalUD.Name = "SyncIntervalUD";
      this.SyncIntervalUD.Size = new System.Drawing.Size(81, 20);
      this.SyncIntervalUD.TabIndex = 19;
      this.SyncIntervalUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.SyncIntervalUD.Value = new decimal(new int[] {
            3600,
            0,
            0,
            0});
      // 
      // label3
      // 
      this.label3.BackColor = System.Drawing.SystemColors.Control;
      this.label3.Location = new System.Drawing.Point(316, 44);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(66, 13);
      this.label3.TabIndex = 18;
      this.label3.Text = "seconds";
      // 
      // label8
      // 
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(90, 44);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(140, 13);
      this.label8.TabIndex = 16;
      this.label8.Text = "Synchronization Interval";
      this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Info;
      this.panel1.Controls.Add(this.label6);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(3, 90);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(574, 171);
      this.panel1.TabIndex = 13;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.Location = new System.Drawing.Point(5, 5);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(566, 158);
      this.label6.TabIndex = 0;
      this.label6.Text = resources.GetString("label6.Text");
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(12, 22);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(228, 13);
      this.label7.TabIndex = 12;
      this.label7.Text = "Preferred Time Synchronization Source";
      this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // MeasureGBX
      // 
      this.MeasureGBX.Controls.Add(this.label1);
      this.MeasureGBX.Controls.Add(this.label9);
      this.MeasureGBX.Controls.Add(this.HzChartAdjustTxt);
      this.MeasureGBX.Controls.Add(this.CyclesLB);
      this.MeasureGBX.Controls.Add(this.IntervalUD);
      this.MeasureGBX.Controls.Add(this.panel3);
      this.MeasureGBX.Controls.Add(this.label5);
      this.MeasureGBX.Controls.Add(this.Nominal50HzRK);
      this.MeasureGBX.Controls.Add(this.Nominal60HzRK);
      this.MeasureGBX.Controls.Add(this.label2);
      this.MeasureGBX.Dock = System.Windows.Forms.DockStyle.Top;
      this.MeasureGBX.Location = new System.Drawing.Point(0, 284);
      this.MeasureGBX.Name = "MeasureGBX";
      this.MeasureGBX.Size = new System.Drawing.Size(580, 244);
      this.MeasureGBX.TabIndex = 15;
      this.MeasureGBX.TabStop = false;
      this.MeasureGBX.Text = "Phase Measurements Interval";
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.SystemColors.Control;
      this.label1.Location = new System.Drawing.Point(495, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 13);
      this.label1.TabIndex = 28;
      this.label1.Text = "Hz";
      // 
      // label9
      // 
      this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(306, 63);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(124, 13);
      this.label9.TabIndex = 27;
      this.label9.Text = "Frequency Chart Adjust";
      this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // HzChartAdjustTxt
      // 
      this.HzChartAdjustTxt.Location = new System.Drawing.Point(432, 60);
      this.HzChartAdjustTxt.Name = "HzChartAdjustTxt";
      this.HzChartAdjustTxt.Size = new System.Drawing.Size(57, 20);
      this.HzChartAdjustTxt.TabIndex = 26;
      // 
      // CyclesLB
      // 
      this.CyclesLB.BackColor = System.Drawing.SystemColors.Control;
      this.CyclesLB.Location = new System.Drawing.Point(123, 62);
      this.CyclesLB.Name = "CyclesLB";
      this.CyclesLB.Size = new System.Drawing.Size(262, 13);
      this.CyclesLB.TabIndex = 15;
      this.CyclesLB.Text = "per Sample";
      // 
      // IntervalUD
      // 
      this.IntervalUD.Items.Add("60");
      this.IntervalUD.Items.Add("30");
      this.IntervalUD.Items.Add("20");
      this.IntervalUD.Items.Add("15");
      this.IntervalUD.Items.Add("12");
      this.IntervalUD.Items.Add("10");
      this.IntervalUD.Items.Add("6");
      this.IntervalUD.Items.Add("5");
      this.IntervalUD.Items.Add("4");
      this.IntervalUD.Items.Add("3");
      this.IntervalUD.Items.Add("2");
      this.IntervalUD.Items.Add("1");
      this.IntervalUD.Location = new System.Drawing.Point(64, 60);
      this.IntervalUD.Name = "IntervalUD";
      this.IntervalUD.Size = new System.Drawing.Size(53, 20);
      this.IntervalUD.TabIndex = 2;
      this.IntervalUD.Text = "6";
      this.IntervalUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.IntervalUD.SelectedItemChanged += new System.EventHandler(this.IntervalUD_SelectedItemChanged);
      // 
      // panel3
      // 
      this.panel3.BackColor = System.Drawing.SystemColors.Info;
      this.panel3.Controls.Add(this.label4);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(3, 86);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(574, 155);
      this.panel3.TabIndex = 13;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.Location = new System.Drawing.Point(5, 4);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(566, 149);
      this.label4.TabIndex = 0;
      this.label4.Text = resources.GetString("label4.Text");
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(61, 22);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(115, 13);
      this.label5.TabIndex = 12;
      this.label5.Text = "Nominal Frequency";
      this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // Nominal50HzRK
      // 
      this.Nominal50HzRK.AutoSize = true;
      this.Nominal50HzRK.Location = new System.Drawing.Point(126, 38);
      this.Nominal50HzRK.Name = "Nominal50HzRK";
      this.Nominal50HzRK.Size = new System.Drawing.Size(53, 17);
      this.Nominal50HzRK.TabIndex = 1;
      this.Nominal50HzRK.Text = "50 Hz";
      this.Nominal50HzRK.UseVisualStyleBackColor = true;
      // 
      // Nominal60HzRK
      // 
      this.Nominal60HzRK.AutoSize = true;
      this.Nominal60HzRK.Checked = true;
      this.Nominal60HzRK.Location = new System.Drawing.Point(64, 38);
      this.Nominal60HzRK.Name = "Nominal60HzRK";
      this.Nominal60HzRK.Size = new System.Drawing.Size(53, 17);
      this.Nominal60HzRK.TabIndex = 0;
      this.Nominal60HzRK.TabStop = true;
      this.Nominal60HzRK.Text = "60 Hz";
      this.Nominal60HzRK.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(20, 62);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(44, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "Cycles";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // label10
      // 
      this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(393, 42);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(85, 13);
      this.label10.TabIndex = 29;
      this.label10.Text = "Date Format";
      this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // DateFormatStringDdl
      // 
      this.DateFormatStringDdl.FormattingEnabled = true;
      this.DateFormatStringDdl.Items.AddRange(new object[] {
            "MM/dd/yy",
            "dd/MM/yy"});
      this.DateFormatStringDdl.Location = new System.Drawing.Point(477, 39);
      this.DateFormatStringDdl.Name = "DateFormatStringDdl";
      this.DateFormatStringDdl.Size = new System.Drawing.Size(86, 21);
      this.DateFormatStringDdl.TabIndex = 30;
      this.DateFormatStringDdl.Text = "MM/dd/yy";
      // 
      // Cfg_SensorTime
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.MeasureGBX);
      this.Controls.Add(this.TimeSourceGBX);
      this.Controls.Add(this.CaptionPAN);
      this.Name = "Cfg_SensorTime";
      this.Size = new System.Drawing.Size(580, 537);
      this.ParentChanged += new System.EventHandler(this.Cfg_SensorTime_ParentChanged);
      this.CaptionPAN.ResumeLayout(false);
      this.TimeSourceGBX.ResumeLayout(false);
      this.TimeSourceGBX.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SyncIntervalUD)).EndInit();
      this.panel1.ResumeLayout(false);
      this.MeasureGBX.ResumeLayout(false);
      this.MeasureGBX.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel CaptionPAN;
    private System.Windows.Forms.Label CaptionLBL;
    private System.Windows.Forms.GroupBox TimeSourceGBX;
    private System.Windows.Forms.NumericUpDown SyncIntervalUD;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.GroupBox MeasureGBX;
    private System.Windows.Forms.Label CyclesLB;
    private System.Windows.Forms.DomainUpDown IntervalUD;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.RadioButton Nominal50HzRK;
    private System.Windows.Forms.RadioButton Nominal60HzRK;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.RadioButton HostSyncRK;
    private System.Windows.Forms.RadioButton GpsSyncRK;
    private System.Windows.Forms.CheckBox PpsEnableCHK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox HzChartAdjustTxt;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.ComboBox DateFormatStringDdl;
  }
}
