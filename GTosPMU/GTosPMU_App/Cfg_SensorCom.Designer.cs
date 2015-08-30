namespace GTosPMU
{
  partial class Cfg_SensorCom
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
      this.Port1GBX = new System.Windows.Forms.GroupBox();
      this.WriteBufferSizeUD = new System.Windows.Forms.NumericUpDown();
      this.label15 = new System.Windows.Forms.Label();
      this.label16 = new System.Windows.Forms.Label();
      this.WriteTimeoutUD = new System.Windows.Forms.NumericUpDown();
      this.label13 = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.ReceivedBytesThresholdUD = new System.Windows.Forms.NumericUpDown();
      this.label11 = new System.Windows.Forms.Label();
      this.label12 = new System.Windows.Forms.Label();
      this.ReadBufferSizeUD = new System.Windows.Forms.NumericUpDown();
      this.label9 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.ReadTimeoutUD = new System.Windows.Forms.NumericUpDown();
      this.label3 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label6 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.PortNameTXT = new System.Windows.Forms.TextBox();
      this.CaptionPAN.SuspendLayout();
      this.Port1GBX.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.WriteBufferSizeUD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.WriteTimeoutUD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReceivedBytesThresholdUD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReadBufferSizeUD)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReadTimeoutUD)).BeginInit();
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
      this.CaptionPAN.Size = new System.Drawing.Size(650, 20);
      this.CaptionPAN.TabIndex = 0;
      // 
      // CaptionLBL
      // 
      this.CaptionLBL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.CaptionLBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CaptionLBL.Location = new System.Drawing.Point(2, 2);
      this.CaptionLBL.Name = "CaptionLBL";
      this.CaptionLBL.Size = new System.Drawing.Size(645, 16);
      this.CaptionLBL.TabIndex = 0;
      this.CaptionLBL.Text = "Communications";
      this.CaptionLBL.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // Port1GBX
      // 
      this.Port1GBX.Controls.Add(this.PortNameTXT);
      this.Port1GBX.Controls.Add(this.label1);
      this.Port1GBX.Controls.Add(this.WriteBufferSizeUD);
      this.Port1GBX.Controls.Add(this.label15);
      this.Port1GBX.Controls.Add(this.label16);
      this.Port1GBX.Controls.Add(this.WriteTimeoutUD);
      this.Port1GBX.Controls.Add(this.label13);
      this.Port1GBX.Controls.Add(this.label14);
      this.Port1GBX.Controls.Add(this.ReceivedBytesThresholdUD);
      this.Port1GBX.Controls.Add(this.label11);
      this.Port1GBX.Controls.Add(this.label12);
      this.Port1GBX.Controls.Add(this.ReadBufferSizeUD);
      this.Port1GBX.Controls.Add(this.label9);
      this.Port1GBX.Controls.Add(this.label10);
      this.Port1GBX.Controls.Add(this.ReadTimeoutUD);
      this.Port1GBX.Controls.Add(this.label3);
      this.Port1GBX.Controls.Add(this.label8);
      this.Port1GBX.Controls.Add(this.panel1);
      this.Port1GBX.Controls.Add(this.label7);
      this.Port1GBX.Dock = System.Windows.Forms.DockStyle.Top;
      this.Port1GBX.Location = new System.Drawing.Point(0, 20);
      this.Port1GBX.Name = "Port1GBX";
      this.Port1GBX.Size = new System.Drawing.Size(650, 264);
      this.Port1GBX.TabIndex = 16;
      this.Port1GBX.TabStop = false;
      this.Port1GBX.Text = "Sensor Data Serial Port";
      // 
      // WriteBufferSizeUD
      // 
      this.WriteBufferSizeUD.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
      this.WriteBufferSizeUD.Location = new System.Drawing.Point(454, 108);
      this.WriteBufferSizeUD.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
      this.WriteBufferSizeUD.Name = "WriteBufferSizeUD";
      this.WriteBufferSizeUD.Size = new System.Drawing.Size(81, 20);
      this.WriteBufferSizeUD.TabIndex = 4;
      this.WriteBufferSizeUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.WriteBufferSizeUD.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
      // 
      // label15
      // 
      this.label15.BackColor = System.Drawing.SystemColors.Control;
      this.label15.Location = new System.Drawing.Point(537, 112);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(66, 13);
      this.label15.TabIndex = 30;
      this.label15.Text = "bytes";
      // 
      // label16
      // 
      this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label16.Location = new System.Drawing.Point(320, 112);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(131, 13);
      this.label16.TabIndex = 29;
      this.label16.Text = "Write Buffer Size";
      this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // WriteTimeoutUD
      // 
      this.WriteTimeoutUD.Location = new System.Drawing.Point(454, 82);
      this.WriteTimeoutUD.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
      this.WriteTimeoutUD.Name = "WriteTimeoutUD";
      this.WriteTimeoutUD.Size = new System.Drawing.Size(81, 20);
      this.WriteTimeoutUD.TabIndex = 2;
      this.WriteTimeoutUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.WriteTimeoutUD.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      // 
      // label13
      // 
      this.label13.BackColor = System.Drawing.SystemColors.Control;
      this.label13.Location = new System.Drawing.Point(537, 86);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(66, 13);
      this.label13.TabIndex = 27;
      this.label13.Text = "milliseconds";
      // 
      // label14
      // 
      this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label14.Location = new System.Drawing.Point(320, 86);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(131, 13);
      this.label14.TabIndex = 26;
      this.label14.Text = "Write Timeout";
      this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // ReceivedBytesThresholdUD
      // 
      this.ReceivedBytesThresholdUD.Location = new System.Drawing.Point(160, 134);
      this.ReceivedBytesThresholdUD.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.ReceivedBytesThresholdUD.Name = "ReceivedBytesThresholdUD";
      this.ReceivedBytesThresholdUD.Size = new System.Drawing.Size(81, 20);
      this.ReceivedBytesThresholdUD.TabIndex = 5;
      this.ReceivedBytesThresholdUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.ReceivedBytesThresholdUD.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
      // 
      // label11
      // 
      this.label11.BackColor = System.Drawing.SystemColors.Control;
      this.label11.Location = new System.Drawing.Point(243, 138);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(66, 13);
      this.label11.TabIndex = 24;
      this.label11.Text = "bytes";
      // 
      // label12
      // 
      this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label12.Location = new System.Drawing.Point(16, 138);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(141, 13);
      this.label12.TabIndex = 23;
      this.label12.Text = "Received Bytes Threshold";
      this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // ReadBufferSizeUD
      // 
      this.ReadBufferSizeUD.Increment = new decimal(new int[] {
            64,
            0,
            0,
            0});
      this.ReadBufferSizeUD.Location = new System.Drawing.Point(160, 108);
      this.ReadBufferSizeUD.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.ReadBufferSizeUD.Name = "ReadBufferSizeUD";
      this.ReadBufferSizeUD.Size = new System.Drawing.Size(81, 20);
      this.ReadBufferSizeUD.TabIndex = 3;
      this.ReadBufferSizeUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.ReadBufferSizeUD.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
      // 
      // label9
      // 
      this.label9.BackColor = System.Drawing.SystemColors.Control;
      this.label9.Location = new System.Drawing.Point(243, 112);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(66, 13);
      this.label9.TabIndex = 21;
      this.label9.Text = "bytes";
      // 
      // label10
      // 
      this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label10.Location = new System.Drawing.Point(26, 112);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(131, 13);
      this.label10.TabIndex = 20;
      this.label10.Text = "Read Buffer Size";
      this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // ReadTimeoutUD
      // 
      this.ReadTimeoutUD.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.ReadTimeoutUD.Location = new System.Drawing.Point(160, 82);
      this.ReadTimeoutUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.ReadTimeoutUD.Name = "ReadTimeoutUD";
      this.ReadTimeoutUD.Size = new System.Drawing.Size(81, 20);
      this.ReadTimeoutUD.TabIndex = 1;
      this.ReadTimeoutUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.ReadTimeoutUD.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      // 
      // label3
      // 
      this.label3.BackColor = System.Drawing.SystemColors.Control;
      this.label3.Location = new System.Drawing.Point(243, 86);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(66, 13);
      this.label3.TabIndex = 18;
      this.label3.Text = "milliseconds";
      // 
      // label8
      // 
      this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(26, 86);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(131, 13);
      this.label8.TabIndex = 16;
      this.label8.Text = "Read Timeout";
      this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // panel1
      // 
      this.panel1.BackColor = System.Drawing.SystemColors.Info;
      this.panel1.Controls.Add(this.label6);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(3, 178);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(644, 83);
      this.panel1.TabIndex = 13;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.Location = new System.Drawing.Point(5, 5);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(636, 70);
      this.label6.TabIndex = 0;
      this.label6.Text = "Fixed Sensor UART Configuration\r\n   115200 bps Baud\r\n   8 Data Bits, 1 Stop bit, " +
          "No Parity\r\n   No Hand Shake\r\n   Text End of Line = 0x0D 0x0A  (CR LF)\r\n";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(22, 60);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(219, 13);
      this.label7.TabIndex = 12;
      this.label7.Text = "Data Communications Tuning Options";
      this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(22, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(66, 13);
      this.label1.TabIndex = 32;
      this.label1.Text = "Serial Port";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // PortNameTXT
      // 
      this.PortNameTXT.Location = new System.Drawing.Point(94, 27);
      this.PortNameTXT.Name = "PortNameTXT";
      this.PortNameTXT.Size = new System.Drawing.Size(100, 20);
      this.PortNameTXT.TabIndex = 0;
      this.PortNameTXT.Text = "COM1";
      // 
      // Cfg_SensorCom
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.Port1GBX);
      this.Controls.Add(this.CaptionPAN);
      this.Name = "Cfg_SensorCom";
      this.Size = new System.Drawing.Size(650, 374);
      this.ParentChanged += new System.EventHandler(this.Cfg_SensorCom_ParentChanged);
      this.CaptionPAN.ResumeLayout(false);
      this.Port1GBX.ResumeLayout(false);
      this.Port1GBX.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.WriteBufferSizeUD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.WriteTimeoutUD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReceivedBytesThresholdUD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReadBufferSizeUD)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ReadTimeoutUD)).EndInit();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel CaptionPAN;
    private System.Windows.Forms.Label CaptionLBL;
    private System.Windows.Forms.GroupBox Port1GBX;
    private System.Windows.Forms.NumericUpDown ReadTimeoutUD;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.NumericUpDown WriteBufferSizeUD;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.NumericUpDown WriteTimeoutUD;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.NumericUpDown ReceivedBytesThresholdUD;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.NumericUpDown ReadBufferSizeUD;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox PortNameTXT;
    private System.Windows.Forms.Label label1;
  }
}
