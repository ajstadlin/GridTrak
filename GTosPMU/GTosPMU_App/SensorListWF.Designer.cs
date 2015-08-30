namespace GTosPMU
{
  partial class SensorListWF
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
      this.panTBar = new System.Windows.Forms.Panel();
      this.MainTBAR = new System.Windows.Forms.ToolStrip();
      this.SaveTBTN = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.RefreshTBTN = new System.Windows.Forms.ToolStripButton();
      this.SensorListBS = new System.Windows.Forms.BindingSource(this.components);
      this.SensorListDGV = new System.Windows.Forms.DataGridView();
      this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panTBar.SuspendLayout();
      this.MainTBAR.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SensorListBS)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.SensorListDGV)).BeginInit();
      this.SuspendLayout();
      // 
      // panTBar
      // 
      this.panTBar.Controls.Add(this.MainTBAR);
      this.panTBar.Dock = System.Windows.Forms.DockStyle.Top;
      this.panTBar.Location = new System.Drawing.Point(0, 0);
      this.panTBar.Name = "panTBar";
      this.panTBar.Size = new System.Drawing.Size(658, 24);
      this.panTBar.TabIndex = 3;
      // 
      // MainTBAR
      // 
      this.MainTBAR.AutoSize = false;
      this.MainTBAR.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.MainTBAR.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveTBTN,
            this.toolStripSeparator1,
            this.RefreshTBTN});
      this.MainTBAR.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.MainTBAR.Location = new System.Drawing.Point(0, 0);
      this.MainTBAR.Name = "MainTBAR";
      this.MainTBAR.Size = new System.Drawing.Size(658, 23);
      this.MainTBAR.TabIndex = 0;
      // 
      // SaveTBTN
      // 
      this.SaveTBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.SaveTBTN.Image = global::GTosPMU.Properties.Resources.Save_1;
      this.SaveTBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.SaveTBTN.Name = "SaveTBTN";
      this.SaveTBTN.Size = new System.Drawing.Size(23, 20);
      this.SaveTBTN.ToolTipText = "Save";
      this.SaveTBTN.Click += new System.EventHandler(this.SaveTBTN_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
      // 
      // RefreshTBTN
      // 
      this.RefreshTBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.RefreshTBTN.Image = global::GTosPMU.Properties.Resources.Refresh;
      this.RefreshTBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.RefreshTBTN.Name = "RefreshTBTN";
      this.RefreshTBTN.Size = new System.Drawing.Size(23, 20);
      this.RefreshTBTN.ToolTipText = "Refresh Sensor List from Web Service";
      this.RefreshTBTN.Click += new System.EventHandler(this.RefreshTBTN_Click);
      // 
      // SensorListDGV
      // 
      this.SensorListDGV.AllowUserToAddRows = false;
      this.SensorListDGV.AllowUserToDeleteRows = false;
      this.SensorListDGV.AutoGenerateColumns = false;
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.SensorListDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
      this.SensorListDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.SensorListDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
      this.SensorListDGV.DataSource = this.SensorListBS;
      dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.SensorListDGV.DefaultCellStyle = dataGridViewCellStyle8;
      this.SensorListDGV.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SensorListDGV.Location = new System.Drawing.Point(0, 24);
      this.SensorListDGV.Name = "SensorListDGV";
      dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.SensorListDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
      this.SensorListDGV.RowHeadersWidth = 24;
      this.SensorListDGV.Size = new System.Drawing.Size(658, 424);
      this.SensorListDGV.TabIndex = 4;
      // 
      // Column3
      // 
      this.Column3.DataPropertyName = "Is_Pick";
      this.Column3.FalseValue = "0";
      this.Column3.HeaderText = "Pick";
      this.Column3.Name = "Column3";
      this.Column3.TrueValue = "1";
      this.Column3.Width = 40;
      // 
      // Column1
      // 
      this.Column1.DataPropertyName = "Sensor_ID";
      this.Column1.HeaderText = "Sensor #";
      this.Column1.Name = "Column1";
      // 
      // Column2
      // 
      this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Column2.DataPropertyName = "Location_Name";
      this.Column2.HeaderText = "Location";
      this.Column2.Name = "Column2";
      // 
      // SensorListWF
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(658, 448);
      this.Controls.Add(this.SensorListDGV);
      this.Controls.Add(this.panTBar);
      this.Name = "SensorListWF";
      this.Text = "Select Sensors";
      this.panTBar.ResumeLayout(false);
      this.MainTBAR.ResumeLayout(false);
      this.MainTBAR.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SensorListBS)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.SensorListDGV)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.BindingSource SensorListBS;
    private System.Windows.Forms.Panel panTBar;
    private System.Windows.Forms.ToolStrip MainTBAR;
    private System.Windows.Forms.ToolStripButton SaveTBTN;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton RefreshTBTN;
    public System.Windows.Forms.DataGridView SensorListDGV;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
  }
}