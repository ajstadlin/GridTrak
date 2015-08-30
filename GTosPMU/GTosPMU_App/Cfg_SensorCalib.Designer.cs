namespace GTosPMU
{
  partial class Cfg_SensorCalib
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cfg_SensorCalib));
      this.CaptionPAN = new System.Windows.Forms.Panel();
      this.CaptionLBL = new System.Windows.Forms.Label();
      this.TimeSourceGBX = new System.Windows.Forms.GroupBox();
      this.HzCalOffsetUD = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.T3CalStartUD = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.T3CalOffsetUD = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.CaptionPAN.SuspendLayout();
      this.TimeSourceGBX.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.HzCalOffsetUD)).BeginInit();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.T3CalStartUD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.T3CalOffsetUD)).BeginInit();
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
      this.CaptionLBL.Text = "Sensor Calibration";
      this.CaptionLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // TimeSourceGBX
      // 
      this.TimeSourceGBX.Controls.Add(this.T3CalOffsetUD);
      this.TimeSourceGBX.Controls.Add(this.label4);
      this.TimeSourceGBX.Controls.Add(this.label5);
      this.TimeSourceGBX.Controls.Add(this.T3CalStartUD);
      this.TimeSourceGBX.Controls.Add(this.label1);
      this.TimeSourceGBX.Controls.Add(this.label2);
      this.TimeSourceGBX.Controls.Add(this.HzCalOffsetUD);
      this.TimeSourceGBX.Controls.Add(this.label3);
      this.TimeSourceGBX.Controls.Add(this.panel1);
      this.TimeSourceGBX.Controls.Add(this.label7);
      this.TimeSourceGBX.Dock = System.Windows.Forms.DockStyle.Top;
      this.TimeSourceGBX.Location = new System.Drawing.Point(0, 20);
      this.TimeSourceGBX.Name = "TimeSourceGBX";
      this.TimeSourceGBX.Size = new System.Drawing.Size(580, 455);
      this.TimeSourceGBX.TabIndex = 14;
      this.TimeSourceGBX.TabStop = false;
      // 
      // HzCalOffsetUD
      // 
      this.HzCalOffsetUD.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.HzCalOffsetUD.Location = new System.Drawing.Point(158, 71);
      this.HzCalOffsetUD.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
      this.HzCalOffsetUD.Minimum = new decimal(new int[] {
            32766,
            0,
            0,
            -2147483648});
      this.HzCalOffsetUD.Name = "HzCalOffsetUD";
      this.HzCalOffsetUD.Size = new System.Drawing.Size(81, 20);
      this.HzCalOffsetUD.TabIndex = 19;
      this.HzCalOffsetUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.HzCalOffsetUD.Validating += new System.ComponentModel.CancelEventHandler(this.HzCalOffsetUD_Validating);
      // 
      // label3
      // 
      this.label3.BackColor = System.Drawing.SystemColors.Control;
      this.label3.Location = new System.Drawing.Point(245, 73);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(75, 13);
      this.label3.TabIndex = 18;
      this.label3.Text = "x 0.00001 Hz";
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Info;
      this.panel1.Controls.Add(this.label6);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(3, 199);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(574, 253);
      this.panel1.TabIndex = 13;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.Location = new System.Drawing.Point(5, 5);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(566, 240);
      this.label6.TabIndex = 0;
      this.label6.Text = resources.GetString("label6.Text");
      // 
      // label7
      // 
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(12, 75);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(140, 13);
      this.label7.TabIndex = 12;
      this.label7.Text = "Frequency Offset";
      this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // T3CalStartUD
      // 
      this.T3CalStartUD.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.T3CalStartUD.Location = new System.Drawing.Point(158, 45);
      this.T3CalStartUD.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
      this.T3CalStartUD.Name = "T3CalStartUD";
      this.T3CalStartUD.Size = new System.Drawing.Size(81, 20);
      this.T3CalStartUD.TabIndex = 22;
      this.T3CalStartUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.T3CalStartUD.Validating += new System.ComponentModel.CancelEventHandler(this.T3CalStartUD_Validating);
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.SystemColors.Control;
      this.label1.Location = new System.Drawing.Point(245, 47);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(75, 13);
      this.label1.TabIndex = 21;
      this.label1.Text = "Timer Ticks";
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 49);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(140, 13);
      this.label2.TabIndex = 20;
      this.label2.Text = "Timer 3 Start Value";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // T3CalOffsetUD
      // 
      this.T3CalOffsetUD.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.T3CalOffsetUD.Location = new System.Drawing.Point(158, 19);
      this.T3CalOffsetUD.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
      this.T3CalOffsetUD.Minimum = new decimal(new int[] {
            32766,
            0,
            0,
            -2147483648});
      this.T3CalOffsetUD.Name = "T3CalOffsetUD";
      this.T3CalOffsetUD.Size = new System.Drawing.Size(81, 20);
      this.T3CalOffsetUD.TabIndex = 25;
      this.T3CalOffsetUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.T3CalOffsetUD.Value = new decimal(new int[] {
            34,
            0,
            0,
            0});
      this.T3CalOffsetUD.Validating += new System.ComponentModel.CancelEventHandler(this.T3CalOffsetUD_Validating);
      // 
      // label4
      // 
      this.label4.BackColor = System.Drawing.SystemColors.Control;
      this.label4.Location = new System.Drawing.Point(245, 21);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(75, 13);
      this.label4.TabIndex = 24;
      this.label4.Text = "Timer Ticks";
      // 
      // label5
      // 
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 23);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(140, 13);
      this.label5.TabIndex = 23;
      this.label5.Text = "Timer 3 Offset";
      this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // Cfg_SensorCalib
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.TimeSourceGBX);
      this.Controls.Add(this.CaptionPAN);
      this.Name = "Cfg_SensorCalib";
      this.Size = new System.Drawing.Size(580, 612);
      this.CaptionPAN.ResumeLayout(false);
      this.TimeSourceGBX.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.HzCalOffsetUD)).EndInit();
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.T3CalStartUD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.T3CalOffsetUD)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel CaptionPAN;
    private System.Windows.Forms.Label CaptionLBL;
    private System.Windows.Forms.GroupBox TimeSourceGBX;
    private System.Windows.Forms.NumericUpDown HzCalOffsetUD;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.NumericUpDown T3CalOffsetUD;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.NumericUpDown T3CalStartUD;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
  }
}
