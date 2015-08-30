namespace GTosPMU
{
  partial class Cfg_Terminal
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
      this.PerformanceGBX = new System.Windows.Forms.GroupBox();
      this.DataPurgeIntervalUD = new System.Windows.Forms.NumericUpDown();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.DisplayTimerIntervalUD = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.TermTimerIntervalUD = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.TermMaxLinesUD = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.CaptionPAN.SuspendLayout();
      this.PerformanceGBX.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DataPurgeIntervalUD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.DisplayTimerIntervalUD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TermTimerIntervalUD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.TermMaxLinesUD)).BeginInit();
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
      this.CaptionLBL.Text = "Terminal Settings";
      this.CaptionLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // PerformanceGBX
      // 
      this.PerformanceGBX.Controls.Add(this.DataPurgeIntervalUD);
      this.PerformanceGBX.Controls.Add(this.label6);
      this.PerformanceGBX.Controls.Add(this.label7);
      this.PerformanceGBX.Controls.Add(this.DisplayTimerIntervalUD);
      this.PerformanceGBX.Controls.Add(this.label4);
      this.PerformanceGBX.Controls.Add(this.label5);
      this.PerformanceGBX.Controls.Add(this.TermTimerIntervalUD);
      this.PerformanceGBX.Controls.Add(this.label1);
      this.PerformanceGBX.Controls.Add(this.label2);
      this.PerformanceGBX.Controls.Add(this.TermMaxLinesUD);
      this.PerformanceGBX.Controls.Add(this.label3);
      this.PerformanceGBX.Controls.Add(this.label8);
      this.PerformanceGBX.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PerformanceGBX.Location = new System.Drawing.Point(0, 20);
      this.PerformanceGBX.Name = "PerformanceGBX";
      this.PerformanceGBX.Size = new System.Drawing.Size(485, 399);
      this.PerformanceGBX.TabIndex = 1;
      this.PerformanceGBX.TabStop = false;
      this.PerformanceGBX.Text = "Performance Settings";
      // 
      // DataPurgeIntervalUD
      // 
      this.DataPurgeIntervalUD.Location = new System.Drawing.Point(118, 108);
      this.DataPurgeIntervalUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.DataPurgeIntervalUD.Name = "DataPurgeIntervalUD";
      this.DataPurgeIntervalUD.Size = new System.Drawing.Size(81, 20);
      this.DataPurgeIntervalUD.TabIndex = 31;
      this.DataPurgeIntervalUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.DataPurgeIntervalUD.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      // 
      // label6
      // 
      this.label6.BackColor = System.Drawing.SystemColors.Control;
      this.label6.Location = new System.Drawing.Point(201, 112);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(66, 13);
      this.label6.TabIndex = 30;
      this.label6.Text = "ms";
      // 
      // label7
      // 
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(8, 112);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(107, 13);
      this.label7.TabIndex = 29;
      this.label7.Text = "Purge Timer Interval";
      this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // DisplayTimerIntervalUD
      // 
      this.DisplayTimerIntervalUD.Location = new System.Drawing.Point(118, 82);
      this.DisplayTimerIntervalUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.DisplayTimerIntervalUD.Name = "DisplayTimerIntervalUD";
      this.DisplayTimerIntervalUD.Size = new System.Drawing.Size(81, 20);
      this.DisplayTimerIntervalUD.TabIndex = 28;
      this.DisplayTimerIntervalUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.DisplayTimerIntervalUD.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      // 
      // label4
      // 
      this.label4.BackColor = System.Drawing.SystemColors.Control;
      this.label4.Location = new System.Drawing.Point(201, 86);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(66, 13);
      this.label4.TabIndex = 27;
      this.label4.Text = "ms";
      // 
      // label5
      // 
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(8, 86);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(107, 13);
      this.label5.TabIndex = 26;
      this.label5.Text = "Chart Timer Interval";
      this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // TermTimerIntervalUD
      // 
      this.TermTimerIntervalUD.Location = new System.Drawing.Point(118, 56);
      this.TermTimerIntervalUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.TermTimerIntervalUD.Name = "TermTimerIntervalUD";
      this.TermTimerIntervalUD.Size = new System.Drawing.Size(81, 20);
      this.TermTimerIntervalUD.TabIndex = 25;
      this.TermTimerIntervalUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.TermTimerIntervalUD.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      // 
      // label1
      // 
      this.label1.BackColor = System.Drawing.SystemColors.Control;
      this.label1.Location = new System.Drawing.Point(201, 60);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 13);
      this.label1.TabIndex = 24;
      this.label1.Text = "ms";
      // 
      // label2
      // 
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(8, 60);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 13);
      this.label2.TabIndex = 23;
      this.label2.Text = "Process Interval";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // TermMaxLinesUD
      // 
      this.TermMaxLinesUD.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
      this.TermMaxLinesUD.Location = new System.Drawing.Point(118, 30);
      this.TermMaxLinesUD.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.TermMaxLinesUD.Name = "TermMaxLinesUD";
      this.TermMaxLinesUD.Size = new System.Drawing.Size(81, 20);
      this.TermMaxLinesUD.TabIndex = 22;
      this.TermMaxLinesUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.TermMaxLinesUD.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
      // 
      // label3
      // 
      this.label3.BackColor = System.Drawing.SystemColors.Control;
      this.label3.Location = new System.Drawing.Point(201, 34);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(66, 13);
      this.label3.TabIndex = 21;
      this.label3.Text = "lines";
      // 
      // label8
      // 
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(8, 34);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(107, 13);
      this.label8.TabIndex = 20;
      this.label8.Text = "Terminal Max Lines";
      this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // Cfg_Terminal
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.PerformanceGBX);
      this.Controls.Add(this.CaptionPAN);
      this.Name = "Cfg_Terminal";
      this.Size = new System.Drawing.Size(485, 419);
      this.ParentChanged += new System.EventHandler(this.Cfg_Terminal_ParentChanged);
      this.CaptionPAN.ResumeLayout(false);
      this.PerformanceGBX.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.DataPurgeIntervalUD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.DisplayTimerIntervalUD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TermTimerIntervalUD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.TermMaxLinesUD)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel CaptionPAN;
    private System.Windows.Forms.Label CaptionLBL;
    private System.Windows.Forms.GroupBox PerformanceGBX;
    private System.Windows.Forms.NumericUpDown TermMaxLinesUD;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.NumericUpDown TermTimerIntervalUD;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown DisplayTimerIntervalUD;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.NumericUpDown DataPurgeIntervalUD;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
  }
}
