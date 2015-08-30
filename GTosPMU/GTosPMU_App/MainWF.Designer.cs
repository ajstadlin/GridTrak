namespace GTosPMU
{
  partial class MainWF
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
      System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Contact Information");
      System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Registration", new System.Windows.Forms.TreeNode[] {
            treeNode1});
      System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Sensor ID and Location");
      System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Time Source and Frequency");
      System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Communications");
      System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Startup Parameters");
      System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Calibration");
      System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Sensor", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7});
      System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Terminal Settings");
      System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Network Services");
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWF));
      this.MainMENU = new System.Windows.Forms.MenuStrip();
      this.FileMENU = new System.Windows.Forms.ToolStripMenuItem();
      this.SaveMNU = new System.Windows.Forms.ToolStripMenuItem();
      this.QuitMNU = new System.Windows.Forms.ToolStripMenuItem();
      this.SensorMENU = new System.Windows.Forms.ToolStripMenuItem();
      this.ConnectMN = new System.Windows.Forms.ToolStripMenuItem();
      this.DisconnectMNU = new System.Windows.Forms.ToolStripMenuItem();
      this.ViewMNU = new System.Windows.Forms.ToolStripMenuItem();
      this.DisplayMNU = new System.Windows.Forms.ToolStripMenuItem();
      this.TerminalMNU = new System.Windows.Forms.ToolStripMenuItem();
      this.ConfigMNU = new System.Windows.Forms.ToolStripMenuItem();
      this.HelpMENU = new System.Windows.Forms.ToolStripMenuItem();
      this.AboutMNU = new System.Windows.Forms.ToolStripMenuItem();
      this.MainSTAT = new System.Windows.Forms.StatusStrip();
      this.StatusLB = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
      this.panTBar = new System.Windows.Forms.Panel();
      this.MainTBAR = new System.Windows.Forms.ToolStrip();
      this.SaveTBTN = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.ConnectTBTN = new System.Windows.Forms.ToolStripButton();
      this.DisconnectTBTN = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.ClearTBTN = new System.Windows.Forms.ToolStripButton();
      this.PingTBTN = new System.Windows.Forms.ToolStripButton();
      this.PHzRunTBTN = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.LiveHistDDL = new System.Windows.Forms.ToolStripComboBox();
      this.ScaleLBL = new System.Windows.Forms.ToolStripLabel();
      this.HzChartScaleDDL = new System.Windows.Forms.ToolStripComboBox();
      this.NavNextTBTN = new System.Windows.Forms.ToolStripButton();
      this.MainTABS = new System.Windows.Forms.TabControl();
      this.DisplayTAB = new System.Windows.Forms.TabPage();
      this.PhasePAN = new System.Windows.Forms.Panel();
      this.panPhaseLegend = new System.Windows.Forms.Panel();
      this.splitter5 = new System.Windows.Forms.Splitter();
      this.PhaseChartPAN = new System.Windows.Forms.Panel();
      this.splitter6 = new System.Windows.Forms.Splitter();
      this.PhasorPAN = new System.Windows.Forms.Panel();
      this.splitter4 = new System.Windows.Forms.Splitter();
      this.HzPAN = new System.Windows.Forms.Panel();
      this.HzLegendPAN = new System.Windows.Forms.Panel();
      this.splitter3 = new System.Windows.Forms.Splitter();
      this.HzChartPAN = new System.Windows.Forms.Panel();
      this.splitter2 = new System.Windows.Forms.Splitter();
      this.HzSpectPAN = new System.Windows.Forms.Panel();
      this.TerminalTAB = new System.Windows.Forms.TabPage();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.TermInfoGBX = new System.Windows.Forms.GroupBox();
      this.TermLogEnableCHK = new System.Windows.Forms.CheckBox();
      this.DataLogFolderBrowseBTN = new System.Windows.Forms.Button();
      this.DataLogFolderTXT = new System.Windows.Forms.TextBox();
      this.DataLogEnableCHK = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.ConfigTAB = new System.Windows.Forms.TabPage();
      this.ConfigPAN = new System.Windows.Forms.Panel();
      this.splitter7 = new System.Windows.Forms.Splitter();
      this.ConfigTvPAN = new System.Windows.Forms.Panel();
      this.ConfigTV = new System.Windows.Forms.TreeView();
      this.StatusTIMER = new System.Windows.Forms.Timer(this.components);
      this.TermTIMER = new System.Windows.Forms.Timer(this.components);
      this.DisplayTIMER = new System.Windows.Forms.Timer(this.components);
      this.WTFBTN = new System.Windows.Forms.ToolStripButton();
      this.WTFBTN2 = new System.Windows.Forms.ToolStripButton();
      this.DataPurgeTIMER = new System.Windows.Forms.Timer(this.components);
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.label2 = new System.Windows.Forms.Label();
      this.LogFileTXT = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.TermOutTXT = new System.Windows.Forms.TextBox();
      this.DataLogFileIntervalUD = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.DataLogFileNameFormatTXT = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.MainMENU.SuspendLayout();
      this.panTBar.SuspendLayout();
      this.MainTBAR.SuspendLayout();
      this.MainTABS.SuspendLayout();
      this.DisplayTAB.SuspendLayout();
      this.PhasePAN.SuspendLayout();
      this.HzPAN.SuspendLayout();
      this.TerminalTAB.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.TermInfoGBX.SuspendLayout();
      this.ConfigTAB.SuspendLayout();
      this.ConfigTvPAN.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DataLogFileIntervalUD)).BeginInit();
      this.SuspendLayout();
      // 
      // MainMENU
      // 
      this.MainMENU.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMENU,
            this.SensorMENU,
            this.ViewMNU,
            this.HelpMENU});
      this.MainMENU.Location = new System.Drawing.Point(0, 0);
      this.MainMENU.Name = "MainMENU";
      this.MainMENU.Size = new System.Drawing.Size(992, 24);
      this.MainMENU.TabIndex = 0;
      // 
      // FileMENU
      // 
      this.FileMENU.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveMNU,
            this.QuitMNU});
      this.FileMENU.Name = "FileMENU";
      this.FileMENU.Size = new System.Drawing.Size(35, 20);
      this.FileMENU.Text = "&File";
      // 
      // SaveMNU
      // 
      this.SaveMNU.Name = "SaveMNU";
      this.SaveMNU.Size = new System.Drawing.Size(98, 22);
      this.SaveMNU.Text = "&Save";
      this.SaveMNU.Click += new System.EventHandler(this.SaveTBTN_Click);
      // 
      // QuitMNU
      // 
      this.QuitMNU.Name = "QuitMNU";
      this.QuitMNU.Size = new System.Drawing.Size(98, 22);
      this.QuitMNU.Text = "&Quit";
      this.QuitMNU.Click += new System.EventHandler(this.QuitMNU_Click);
      // 
      // SensorMENU
      // 
      this.SensorMENU.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectMN,
            this.DisconnectMNU});
      this.SensorMENU.Name = "SensorMENU";
      this.SensorMENU.Size = new System.Drawing.Size(52, 20);
      this.SensorMENU.Text = "&Sensor";
      // 
      // ConnectMN
      // 
      this.ConnectMN.Name = "ConnectMN";
      this.ConnectMN.Size = new System.Drawing.Size(126, 22);
      this.ConnectMN.Text = "&Connect";
      this.ConnectMN.Click += new System.EventHandler(this.ConnectTBTN_Click);
      // 
      // DisconnectMNU
      // 
      this.DisconnectMNU.Name = "DisconnectMNU";
      this.DisconnectMNU.Size = new System.Drawing.Size(126, 22);
      this.DisconnectMNU.Text = "&Disconnect";
      this.DisconnectMNU.Click += new System.EventHandler(this.DisconnectTBTN_Click);
      // 
      // ViewMNU
      // 
      this.ViewMNU.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisplayMNU,
            this.TerminalMNU,
            this.ConfigMNU});
      this.ViewMNU.Name = "ViewMNU";
      this.ViewMNU.Size = new System.Drawing.Size(41, 20);
      this.ViewMNU.Text = "&View";
      // 
      // DisplayMNU
      // 
      this.DisplayMNU.Checked = true;
      this.DisplayMNU.CheckState = System.Windows.Forms.CheckState.Checked;
      this.DisplayMNU.Name = "DisplayMNU";
      this.DisplayMNU.Size = new System.Drawing.Size(152, 22);
      this.DisplayMNU.Text = "&Display";
      // 
      // TerminalMNU
      // 
      this.TerminalMNU.Name = "TerminalMNU";
      this.TerminalMNU.Size = new System.Drawing.Size(152, 22);
      this.TerminalMNU.Text = "&Terminal";
      // 
      // ConfigMNU
      // 
      this.ConfigMNU.Name = "ConfigMNU";
      this.ConfigMNU.Size = new System.Drawing.Size(152, 22);
      this.ConfigMNU.Text = "&Configuration";
      // 
      // HelpMENU
      // 
      this.HelpMENU.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMNU});
      this.HelpMENU.Name = "HelpMENU";
      this.HelpMENU.Size = new System.Drawing.Size(40, 20);
      this.HelpMENU.Text = "&Help";
      // 
      // AboutMNU
      // 
      this.AboutMNU.Name = "AboutMNU";
      this.AboutMNU.Size = new System.Drawing.Size(103, 22);
      this.AboutMNU.Text = "&About";
      this.AboutMNU.Click += new System.EventHandler(this.AboutMNU_Click);
      // 
      // MainSTAT
      // 
      this.MainSTAT.Location = new System.Drawing.Point(0, 651);
      this.MainSTAT.Name = "MainSTAT";
      this.MainSTAT.Size = new System.Drawing.Size(992, 22);
      this.MainSTAT.TabIndex = 1;
      this.MainSTAT.Text = "statusStrip1";
      // 
      // StatusLB
      // 
      this.StatusLB.AutoSize = false;
      this.StatusLB.Name = "StatusLB";
      this.StatusLB.Size = new System.Drawing.Size(877, 17);
      this.StatusLB.Spring = true;
      this.StatusLB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // toolStripStatusLabel2
      // 
      this.toolStripStatusLabel2.AutoSize = false;
      this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
      this.toolStripStatusLabel2.Size = new System.Drawing.Size(100, 17);
      this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panTBar
      // 
      this.panTBar.Controls.Add(this.MainTBAR);
      this.panTBar.Dock = System.Windows.Forms.DockStyle.Top;
      this.panTBar.Location = new System.Drawing.Point(0, 24);
      this.panTBar.Name = "panTBar";
      this.panTBar.Size = new System.Drawing.Size(992, 24);
      this.panTBar.TabIndex = 2;
      // 
      // MainTBAR
      // 
      this.MainTBAR.AutoSize = false;
      this.MainTBAR.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.MainTBAR.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveTBTN,
            this.toolStripSeparator1,
            this.ConnectTBTN,
            this.DisconnectTBTN,
            this.toolStripSeparator2,
            this.ClearTBTN,
            this.PingTBTN,
            this.PHzRunTBTN,
            this.toolStripSeparator3,
            this.LiveHistDDL,
            this.ScaleLBL,
            this.HzChartScaleDDL,
            this.toolStripSeparator4,
            this.NavNextTBTN});
      this.MainTBAR.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.MainTBAR.Location = new System.Drawing.Point(0, 0);
      this.MainTBAR.Name = "MainTBAR";
      this.MainTBAR.Size = new System.Drawing.Size(992, 23);
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
      // ConnectTBTN
      // 
      this.ConnectTBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ConnectTBTN.Image = global::GTosPMU.Properties.Resources.Device_Connect;
      this.ConnectTBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ConnectTBTN.Name = "ConnectTBTN";
      this.ConnectTBTN.Size = new System.Drawing.Size(23, 20);
      this.ConnectTBTN.ToolTipText = "Connect Sensor";
      this.ConnectTBTN.Click += new System.EventHandler(this.ConnectTBTN_Click);
      // 
      // DisconnectTBTN
      // 
      this.DisconnectTBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.DisconnectTBTN.Image = global::GTosPMU.Properties.Resources.Device_Disconnect;
      this.DisconnectTBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.DisconnectTBTN.Name = "DisconnectTBTN";
      this.DisconnectTBTN.Size = new System.Drawing.Size(23, 20);
      this.DisconnectTBTN.ToolTipText = "Disconnect Sensor";
      this.DisconnectTBTN.Click += new System.EventHandler(this.DisconnectTBTN_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
      // 
      // ClearTBTN
      // 
      this.ClearTBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.ClearTBTN.Image = global::GTosPMU.Properties.Resources.Clear;
      this.ClearTBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.ClearTBTN.Name = "ClearTBTN";
      this.ClearTBTN.Size = new System.Drawing.Size(23, 20);
      this.ClearTBTN.ToolTipText = "Clear Terminal Display";
      this.ClearTBTN.Click += new System.EventHandler(this.ClearTBTN_Click);
      // 
      // PingTBTN
      // 
      this.PingTBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.PingTBTN.Image = global::GTosPMU.Properties.Resources.Ping;
      this.PingTBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.PingTBTN.Name = "PingTBTN";
      this.PingTBTN.Size = new System.Drawing.Size(23, 20);
      this.PingTBTN.ToolTipText = "PING the Sensor";
      this.PingTBTN.Click += new System.EventHandler(this.PingTBTN_Click);
      // 
      // PHzRunTBTN
      // 
      this.PHzRunTBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.PHzRunTBTN.Image = global::GTosPMU.Properties.Resources.Prog_TextMode;
      this.PHzRunTBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.PHzRunTBTN.Name = "PHzRunTBTN";
      this.PHzRunTBTN.Size = new System.Drawing.Size(23, 20);
      this.PHzRunTBTN.ToolTipText = "Run Sensor in Text Mode";
      this.PHzRunTBTN.Click += new System.EventHandler(this.PHzRunTBTN_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
      // 
      // LiveHistDDL
      // 
      this.LiveHistDDL.AutoCompleteCustomSource.AddRange(new string[] {
            "Live",
            "History"});
      this.LiveHistDDL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.LiveHistDDL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.LiveHistDDL.AutoSize = false;
      this.LiveHistDDL.DropDownWidth = 60;
      this.LiveHistDDL.Items.AddRange(new object[] {
            "Live"});
      this.LiveHistDDL.Name = "LiveHistDDL";
      this.LiveHistDDL.Size = new System.Drawing.Size(60, 21);
      this.LiveHistDDL.Text = "Live";
      this.LiveHistDDL.Enter += new System.EventHandler(this.LiveHistDDL_Enter);
      // 
      // ScaleLBL
      // 
      this.ScaleLBL.AutoSize = false;
      this.ScaleLBL.Name = "ScaleLBL";
      this.ScaleLBL.Size = new System.Drawing.Size(52, 15);
      this.ScaleLBL.Text = "Hz Scale";
      this.ScaleLBL.TextAlign = System.Drawing.ContentAlignment.BottomRight;
      this.ScaleLBL.Click += new System.EventHandler(this.ScaleLBL_Click);
      // 
      // HzChartScaleDDL
      // 
      this.HzChartScaleDDL.AutoCompleteCustomSource.AddRange(new string[] {
            "1.0 %, +/- 600mHz",
            "0.5 %, +/- 300mHz",
            "0.2 %, +/- 120mHz",
            "0.1 %, +/- 60mHz",
            "0.05 %, +/- 30mHz",
            "0.02 %, +/- 12mHz",
            "0.01 %, +/- 6mHz"});
      this.HzChartScaleDDL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
      this.HzChartScaleDDL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.HzChartScaleDDL.Items.AddRange(new object[] {
            "1.0 %, +/- 600mHz",
            "0.5 %, +/- 300mHz",
            "0.2 %, +/- 120mHz",
            "0.1 %, +/- 60mHz",
            "0.05 %, +/- 30mHz",
            "0.02 %, +/- 12mHz",
            "0.01 %, +/- 6mHz"});
      this.HzChartScaleDDL.Name = "HzChartScaleDDL";
      this.HzChartScaleDDL.Size = new System.Drawing.Size(121, 21);
      this.HzChartScaleDDL.Text = "0.1 %, +/- 60mHz";
      this.HzChartScaleDDL.Enter += new System.EventHandler(this.HzScaleDDL_Enter);
      // 
      // NavNextTBTN
      // 
      this.NavNextTBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.NavNextTBTN.Image = global::GTosPMU.Properties.Resources.Nav_Next;
      this.NavNextTBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.NavNextTBTN.Name = "NavNextTBTN";
      this.NavNextTBTN.Size = new System.Drawing.Size(23, 20);
      this.NavNextTBTN.Text = "Run";
      this.NavNextTBTN.Click += new System.EventHandler(this.NavNextTBTN_Click);
      // 
      // MainTABS
      // 
      this.MainTABS.Controls.Add(this.DisplayTAB);
      this.MainTABS.Controls.Add(this.TerminalTAB);
      this.MainTABS.Controls.Add(this.ConfigTAB);
      this.MainTABS.Dock = System.Windows.Forms.DockStyle.Fill;
      this.MainTABS.Location = new System.Drawing.Point(0, 48);
      this.MainTABS.Name = "MainTABS";
      this.MainTABS.SelectedIndex = 0;
      this.MainTABS.Size = new System.Drawing.Size(992, 603);
      this.MainTABS.TabIndex = 3;
      this.MainTABS.SelectedIndexChanged += new System.EventHandler(this.MainTABS_SelectedIndexChanged);
      this.MainTABS.Selected += new System.Windows.Forms.TabControlEventHandler(this.MainTABS_Selected);
      // 
      // DisplayTAB
      // 
      this.DisplayTAB.Controls.Add(this.PhasePAN);
      this.DisplayTAB.Controls.Add(this.splitter4);
      this.DisplayTAB.Controls.Add(this.HzPAN);
      this.DisplayTAB.Location = new System.Drawing.Point(4, 22);
      this.DisplayTAB.Name = "DisplayTAB";
      this.DisplayTAB.Padding = new System.Windows.Forms.Padding(3);
      this.DisplayTAB.Size = new System.Drawing.Size(984, 577);
      this.DisplayTAB.TabIndex = 0;
      this.DisplayTAB.Text = "Display";
      this.DisplayTAB.UseVisualStyleBackColor = true;
      this.DisplayTAB.Paint += new System.Windows.Forms.PaintEventHandler(this.DisplayTAB_Paint);
      this.DisplayTAB.Resize += new System.EventHandler(this.DisplayTAB_Resize);
      // 
      // PhasePAN
      // 
      this.PhasePAN.Controls.Add(this.panPhaseLegend);
      this.PhasePAN.Controls.Add(this.splitter5);
      this.PhasePAN.Controls.Add(this.PhaseChartPAN);
      this.PhasePAN.Controls.Add(this.splitter6);
      this.PhasePAN.Controls.Add(this.PhasorPAN);
      this.PhasePAN.Dock = System.Windows.Forms.DockStyle.Fill;
      this.PhasePAN.Location = new System.Drawing.Point(3, 287);
      this.PhasePAN.Name = "PhasePAN";
      this.PhasePAN.Size = new System.Drawing.Size(978, 287);
      this.PhasePAN.TabIndex = 2;
      // 
      // panPhaseLegend
      // 
      this.panPhaseLegend.BackColor = System.Drawing.Color.White;
      this.panPhaseLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panPhaseLegend.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panPhaseLegend.Location = new System.Drawing.Point(894, 0);
      this.panPhaseLegend.Name = "panPhaseLegend";
      this.panPhaseLegend.Size = new System.Drawing.Size(84, 287);
      this.panPhaseLegend.TabIndex = 4;
      this.panPhaseLegend.Paint += new System.Windows.Forms.PaintEventHandler(this.PhaseLegendPAN_Paint);
      // 
      // splitter5
      // 
      this.splitter5.Location = new System.Drawing.Point(890, 0);
      this.splitter5.Name = "splitter5";
      this.splitter5.Size = new System.Drawing.Size(4, 287);
      this.splitter5.TabIndex = 3;
      this.splitter5.TabStop = false;
      this.splitter5.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter5_SplitterMoved);
      // 
      // PhaseChartPAN
      // 
      this.PhaseChartPAN.BackColor = System.Drawing.Color.White;
      this.PhaseChartPAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.PhaseChartPAN.Dock = System.Windows.Forms.DockStyle.Left;
      this.PhaseChartPAN.Location = new System.Drawing.Point(264, 0);
      this.PhaseChartPAN.Name = "PhaseChartPAN";
      this.PhaseChartPAN.Size = new System.Drawing.Size(626, 287);
      this.PhaseChartPAN.TabIndex = 2;
      this.PhaseChartPAN.Paint += new System.Windows.Forms.PaintEventHandler(this.PhaseChartPAN_Paint);
      this.PhaseChartPAN.Resize += new System.EventHandler(this.PhaseChartPAN_Resize);
      // 
      // splitter6
      // 
      this.splitter6.Location = new System.Drawing.Point(260, 0);
      this.splitter6.Name = "splitter6";
      this.splitter6.Size = new System.Drawing.Size(4, 287);
      this.splitter6.TabIndex = 1;
      this.splitter6.TabStop = false;
      this.splitter6.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter6_SplitterMoved);
      // 
      // PhasorPAN
      // 
      this.PhasorPAN.BackColor = System.Drawing.Color.White;
      this.PhasorPAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.PhasorPAN.Dock = System.Windows.Forms.DockStyle.Left;
      this.PhasorPAN.Location = new System.Drawing.Point(0, 0);
      this.PhasorPAN.Name = "PhasorPAN";
      this.PhasorPAN.Size = new System.Drawing.Size(260, 287);
      this.PhasorPAN.TabIndex = 0;
      this.PhasorPAN.Paint += new System.Windows.Forms.PaintEventHandler(this.PhasorPAN_Paint);
      this.PhasorPAN.Resize += new System.EventHandler(this.PhasorPAN_Resize);
      // 
      // splitter4
      // 
      this.splitter4.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter4.Location = new System.Drawing.Point(3, 283);
      this.splitter4.Name = "splitter4";
      this.splitter4.Size = new System.Drawing.Size(978, 4);
      this.splitter4.TabIndex = 1;
      this.splitter4.TabStop = false;
      // 
      // HzPAN
      // 
      this.HzPAN.Controls.Add(this.HzLegendPAN);
      this.HzPAN.Controls.Add(this.splitter3);
      this.HzPAN.Controls.Add(this.HzChartPAN);
      this.HzPAN.Controls.Add(this.splitter2);
      this.HzPAN.Controls.Add(this.HzSpectPAN);
      this.HzPAN.Dock = System.Windows.Forms.DockStyle.Top;
      this.HzPAN.Location = new System.Drawing.Point(3, 3);
      this.HzPAN.Name = "HzPAN";
      this.HzPAN.Size = new System.Drawing.Size(978, 280);
      this.HzPAN.TabIndex = 0;
      // 
      // HzLegendPAN
      // 
      this.HzLegendPAN.BackColor = System.Drawing.Color.White;
      this.HzLegendPAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.HzLegendPAN.Dock = System.Windows.Forms.DockStyle.Fill;
      this.HzLegendPAN.Location = new System.Drawing.Point(894, 0);
      this.HzLegendPAN.Name = "HzLegendPAN";
      this.HzLegendPAN.Size = new System.Drawing.Size(84, 280);
      this.HzLegendPAN.TabIndex = 4;
      this.HzLegendPAN.Paint += new System.Windows.Forms.PaintEventHandler(this.HzLegendPAN_Paint);
      this.HzLegendPAN.Resize += new System.EventHandler(this.HzLegendPAN_Resize);
      // 
      // splitter3
      // 
      this.splitter3.Location = new System.Drawing.Point(890, 0);
      this.splitter3.Name = "splitter3";
      this.splitter3.Size = new System.Drawing.Size(4, 280);
      this.splitter3.TabIndex = 3;
      this.splitter3.TabStop = false;
      this.splitter3.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter3_SplitterMoved);
      // 
      // HzChartPAN
      // 
      this.HzChartPAN.BackColor = System.Drawing.Color.White;
      this.HzChartPAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.HzChartPAN.Dock = System.Windows.Forms.DockStyle.Left;
      this.HzChartPAN.Location = new System.Drawing.Point(264, 0);
      this.HzChartPAN.Name = "HzChartPAN";
      this.HzChartPAN.Size = new System.Drawing.Size(626, 280);
      this.HzChartPAN.TabIndex = 2;
      this.HzChartPAN.Paint += new System.Windows.Forms.PaintEventHandler(this.HzChartPAN_Paint);
      this.HzChartPAN.Resize += new System.EventHandler(this.HzChartPAN_Resize);
      // 
      // splitter2
      // 
      this.splitter2.Location = new System.Drawing.Point(260, 0);
      this.splitter2.Name = "splitter2";
      this.splitter2.Size = new System.Drawing.Size(4, 280);
      this.splitter2.TabIndex = 1;
      this.splitter2.TabStop = false;
      this.splitter2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter2_SplitterMoved);
      // 
      // HzSpectPAN
      // 
      this.HzSpectPAN.BackColor = System.Drawing.Color.White;
      this.HzSpectPAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.HzSpectPAN.Dock = System.Windows.Forms.DockStyle.Left;
      this.HzSpectPAN.Location = new System.Drawing.Point(0, 0);
      this.HzSpectPAN.Name = "HzSpectPAN";
      this.HzSpectPAN.Size = new System.Drawing.Size(260, 280);
      this.HzSpectPAN.TabIndex = 0;
      this.HzSpectPAN.Paint += new System.Windows.Forms.PaintEventHandler(this.HzSpectPAN_Paint);
      this.HzSpectPAN.Resize += new System.EventHandler(this.HzSpectPAN_Resize);
      // 
      // TerminalTAB
      // 
      this.TerminalTAB.Controls.Add(this.groupBox1);
      this.TerminalTAB.Controls.Add(this.splitter1);
      this.TerminalTAB.Controls.Add(this.TermInfoGBX);
      this.TerminalTAB.Location = new System.Drawing.Point(4, 22);
      this.TerminalTAB.Name = "TerminalTAB";
      this.TerminalTAB.Padding = new System.Windows.Forms.Padding(3);
      this.TerminalTAB.Size = new System.Drawing.Size(984, 577);
      this.TerminalTAB.TabIndex = 1;
      this.TerminalTAB.Text = "Terminal";
      this.TerminalTAB.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.TermOutTXT);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(3, 118);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(978, 456);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      // 
      // splitter1
      // 
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
      this.splitter1.Location = new System.Drawing.Point(3, 115);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(978, 3);
      this.splitter1.TabIndex = 1;
      this.splitter1.TabStop = false;
      // 
      // TermInfoGBX
      // 
      this.TermInfoGBX.Controls.Add(this.DataLogFileNameFormatTXT);
      this.TermInfoGBX.Controls.Add(this.label5);
      this.TermInfoGBX.Controls.Add(this.label4);
      this.TermInfoGBX.Controls.Add(this.DataLogFileIntervalUD);
      this.TermInfoGBX.Controls.Add(this.label3);
      this.TermInfoGBX.Controls.Add(this.LogFileTXT);
      this.TermInfoGBX.Controls.Add(this.label2);
      this.TermInfoGBX.Controls.Add(this.TermLogEnableCHK);
      this.TermInfoGBX.Controls.Add(this.DataLogFolderBrowseBTN);
      this.TermInfoGBX.Controls.Add(this.DataLogFolderTXT);
      this.TermInfoGBX.Controls.Add(this.DataLogEnableCHK);
      this.TermInfoGBX.Controls.Add(this.label1);
      this.TermInfoGBX.Dock = System.Windows.Forms.DockStyle.Top;
      this.TermInfoGBX.Location = new System.Drawing.Point(3, 3);
      this.TermInfoGBX.Name = "TermInfoGBX";
      this.TermInfoGBX.Size = new System.Drawing.Size(978, 112);
      this.TermInfoGBX.TabIndex = 0;
      this.TermInfoGBX.TabStop = false;
      // 
      // TermLogEnableCHK
      // 
      this.TermLogEnableCHK.Location = new System.Drawing.Point(274, 14);
      this.TermLogEnableCHK.Name = "TermLogEnableCHK";
      this.TermLogEnableCHK.Size = new System.Drawing.Size(220, 18);
      this.TermLogEnableCHK.TabIndex = 1;
      this.TermLogEnableCHK.Text = "Enable Live Data Display in Terminal";
      this.toolTip1.SetToolTip(this.TermLogEnableCHK, "Not Recommended for sample rates \r\ngreater than 10 samples per second.");
      this.TermLogEnableCHK.UseVisualStyleBackColor = true;
      // 
      // DataLogFolderBrowseBTN
      // 
      this.DataLogFolderBrowseBTN.ImageKey = "(none)";
      this.DataLogFolderBrowseBTN.Location = new System.Drawing.Point(487, 33);
      this.DataLogFolderBrowseBTN.Name = "DataLogFolderBrowseBTN";
      this.DataLogFolderBrowseBTN.Size = new System.Drawing.Size(75, 23);
      this.DataLogFolderBrowseBTN.TabIndex = 3;
      this.DataLogFolderBrowseBTN.Text = "Browse";
      this.DataLogFolderBrowseBTN.UseVisualStyleBackColor = true;
      this.DataLogFolderBrowseBTN.Click += new System.EventHandler(this.DataLogFolderBrowseBTN_Click);
      // 
      // DataLogFolderTXT
      // 
      this.DataLogFolderTXT.Location = new System.Drawing.Point(110, 35);
      this.DataLogFolderTXT.Name = "DataLogFolderTXT";
      this.DataLogFolderTXT.Size = new System.Drawing.Size(371, 20);
      this.DataLogFolderTXT.TabIndex = 2;
      this.DataLogFolderTXT.Leave += new System.EventHandler(this.DataLogFolderTXT_Leave);
      // 
      // DataLogEnableCHK
      // 
      this.DataLogEnableCHK.Location = new System.Drawing.Point(110, 14);
      this.DataLogEnableCHK.Name = "DataLogEnableCHK";
      this.DataLogEnableCHK.Size = new System.Drawing.Size(172, 18);
      this.DataLogEnableCHK.TabIndex = 0;
      this.DataLogEnableCHK.Text = "Enable Data Logging";
      this.DataLogEnableCHK.UseVisualStyleBackColor = true;
      this.DataLogEnableCHK.CheckedChanged += new System.EventHandler(this.DataLogEnableCHK_CheckedChanged);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(6, 39);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Data Logs Folder";
      this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // ConfigTAB
      // 
      this.ConfigTAB.Controls.Add(this.ConfigPAN);
      this.ConfigTAB.Controls.Add(this.splitter7);
      this.ConfigTAB.Controls.Add(this.ConfigTvPAN);
      this.ConfigTAB.Location = new System.Drawing.Point(4, 22);
      this.ConfigTAB.Name = "ConfigTAB";
      this.ConfigTAB.Size = new System.Drawing.Size(984, 577);
      this.ConfigTAB.TabIndex = 2;
      this.ConfigTAB.Text = "Configuration";
      this.ConfigTAB.UseVisualStyleBackColor = true;
      // 
      // ConfigPAN
      // 
      this.ConfigPAN.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ConfigPAN.Location = new System.Drawing.Point(199, 0);
      this.ConfigPAN.Name = "ConfigPAN";
      this.ConfigPAN.Size = new System.Drawing.Size(785, 577);
      this.ConfigPAN.TabIndex = 2;
      // 
      // splitter7
      // 
      this.splitter7.Location = new System.Drawing.Point(195, 0);
      this.splitter7.Name = "splitter7";
      this.splitter7.Size = new System.Drawing.Size(4, 577);
      this.splitter7.TabIndex = 1;
      this.splitter7.TabStop = false;
      // 
      // ConfigTvPAN
      // 
      this.ConfigTvPAN.Controls.Add(this.ConfigTV);
      this.ConfigTvPAN.Dock = System.Windows.Forms.DockStyle.Left;
      this.ConfigTvPAN.Location = new System.Drawing.Point(0, 0);
      this.ConfigTvPAN.Name = "ConfigTvPAN";
      this.ConfigTvPAN.Size = new System.Drawing.Size(195, 577);
      this.ConfigTvPAN.TabIndex = 0;
      // 
      // ConfigTV
      // 
      this.ConfigTV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.ConfigTV.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ConfigTV.HideSelection = false;
      this.ConfigTV.Location = new System.Drawing.Point(0, 0);
      this.ConfigTV.Name = "ConfigTV";
      treeNode1.Name = "Node01";
      treeNode1.Text = "Contact Information";
      treeNode2.Name = "Node00";
      treeNode2.Text = "Registration";
      treeNode3.Name = "Node11";
      treeNode3.Text = "Sensor ID and Location";
      treeNode4.Name = "Node12";
      treeNode4.Text = "Time Source and Frequency";
      treeNode5.Name = "Node13";
      treeNode5.Text = "Communications";
      treeNode6.Name = "Node14";
      treeNode6.Text = "Startup Parameters";
      treeNode7.Name = "Node15";
      treeNode7.Text = "Calibration";
      treeNode8.Name = "Node10";
      treeNode8.Text = "Sensor";
      treeNode9.Name = "Node20";
      treeNode9.Text = "Terminal Settings";
      treeNode10.Name = "Node30";
      treeNode10.Text = "Network Services";
      this.ConfigTV.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode8,
            treeNode9,
            treeNode10});
      this.ConfigTV.Size = new System.Drawing.Size(195, 577);
      this.ConfigTV.TabIndex = 0;
      this.ConfigTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ConfigTV_AfterSelect);
      // 
      // StatusTIMER
      // 
      this.StatusTIMER.Interval = 20000;
      this.StatusTIMER.Tick += new System.EventHandler(this.StatusTIMER_Tick);
      // 
      // TermTIMER
      // 
      this.TermTIMER.Tick += new System.EventHandler(this.TermTIMER_Tick);
      // 
      // DisplayTIMER
      // 
      this.DisplayTIMER.Interval = 1000;
      this.DisplayTIMER.Tick += new System.EventHandler(this.DisplayTIMER_Tick);
      // 
      // WTFBTN
      // 
      this.WTFBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.WTFBTN.Image = ((System.Drawing.Image)(resources.GetObject("WTFBTN.Image")));
      this.WTFBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.WTFBTN.Name = "WTFBTN";
      this.WTFBTN.Size = new System.Drawing.Size(23, 20);
      this.WTFBTN.Text = "toolStripButton1";
      // 
      // WTFBTN2
      // 
      this.WTFBTN2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.WTFBTN2.Image = ((System.Drawing.Image)(resources.GetObject("WTFBTN2.Image")));
      this.WTFBTN2.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.WTFBTN2.Name = "WTFBTN2";
      this.WTFBTN2.Size = new System.Drawing.Size(23, 20);
      this.WTFBTN2.Text = "toolStripButton1";
      // 
      // DataPurgeTIMER
      // 
      this.DataPurgeTIMER.Interval = 1000;
      this.DataPurgeTIMER.Tick += new System.EventHandler(this.DataPurgeTIMER_Tick);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(6, 64);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(100, 16);
      this.label2.TabIndex = 5;
      this.label2.Text = "File Time Interval";
      this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.toolTip1.SetToolTip(this.label2, "The Log File Name\'s DateTime = Date + ( Floor( TimeOfDay_Minutes / Interval ) * I" +
        "nterval )");
      // 
      // LogFileTXT
      // 
      this.LogFileTXT.BackColor = System.Drawing.SystemColors.Info;
      this.LogFileTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.LogFileTXT.Location = new System.Drawing.Point(110, 85);
      this.LogFileTXT.Name = "LogFileTXT";
      this.LogFileTXT.ReadOnly = true;
      this.LogFileTXT.Size = new System.Drawing.Size(452, 20);
      this.LogFileTXT.TabIndex = 6;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(9, 89);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(97, 16);
      this.label3.TabIndex = 8;
      this.label3.Text = "Current Log File";
      this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // TermOutTXT
      // 
      this.TermOutTXT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.TermOutTXT.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TermOutTXT.Location = new System.Drawing.Point(3, 16);
      this.TermOutTXT.MaxLength = 10240000;
      this.TermOutTXT.Multiline = true;
      this.TermOutTXT.Name = "TermOutTXT";
      this.TermOutTXT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TermOutTXT.Size = new System.Drawing.Size(972, 437);
      this.TermOutTXT.TabIndex = 0;
      // 
      // DataLogFileIntervalUD
      // 
      this.DataLogFileIntervalUD.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.DataLogFileIntervalUD.Location = new System.Drawing.Point(110, 60);
      this.DataLogFileIntervalUD.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
      this.DataLogFileIntervalUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.DataLogFileIntervalUD.Name = "DataLogFileIntervalUD";
      this.DataLogFileIntervalUD.Size = new System.Drawing.Size(60, 20);
      this.DataLogFileIntervalUD.TabIndex = 4;
      this.DataLogFileIntervalUD.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(176, 64);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(48, 16);
      this.label4.TabIndex = 10;
      this.label4.Text = "minutes";
      // 
      // DataLogFileNameFormatTXT
      // 
      this.DataLogFileNameFormatTXT.Location = new System.Drawing.Point(425, 60);
      this.DataLogFileNameFormatTXT.Name = "DataLogFileNameFormatTXT";
      this.DataLogFileNameFormatTXT.Size = new System.Drawing.Size(137, 20);
      this.DataLogFileNameFormatTXT.TabIndex = 5;
      this.DataLogFileNameFormatTXT.Text = "yyyyMMdd_HHmm";
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(275, 64);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(147, 16);
      this.label5.TabIndex = 11;
      this.label5.Text = "File Name DateTime Format";
      this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
      this.toolTip1.SetToolTip(this.label5, "Uses Microsoft Visual Studio DateTime Format String syntax.");
      // 
      // MainWF
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(992, 673);
      this.Controls.Add(this.MainTABS);
      this.Controls.Add(this.panTBar);
      this.Controls.Add(this.MainSTAT);
      this.Controls.Add(this.MainMENU);
      this.MainMenuStrip = this.MainMENU;
      this.Name = "MainWF";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "GTosPMU SynchroPhasor Measurement Application";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWF_FormClosing);
      this.Shown += new System.EventHandler(this.MainWF_Shown);
      this.MainMENU.ResumeLayout(false);
      this.MainMENU.PerformLayout();
      this.panTBar.ResumeLayout(false);
      this.MainTBAR.ResumeLayout(false);
      this.MainTBAR.PerformLayout();
      this.MainTABS.ResumeLayout(false);
      this.DisplayTAB.ResumeLayout(false);
      this.PhasePAN.ResumeLayout(false);
      this.HzPAN.ResumeLayout(false);
      this.TerminalTAB.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.TermInfoGBX.ResumeLayout(false);
      this.TermInfoGBX.PerformLayout();
      this.ConfigTAB.ResumeLayout(false);
      this.ConfigTvPAN.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.DataLogFileIntervalUD)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip MainMENU;
    private System.Windows.Forms.StatusStrip MainSTAT;
    private System.Windows.Forms.Panel panTBar;
    private System.Windows.Forms.ToolStripMenuItem FileMENU;
    private System.Windows.Forms.ToolStripMenuItem SaveMNU;
    private System.Windows.Forms.ToolStripMenuItem QuitMNU;
    private System.Windows.Forms.ToolStripMenuItem HelpMENU;
    private System.Windows.Forms.ToolStripMenuItem AboutMNU;
    private System.Windows.Forms.ToolStrip MainTBAR;
    private System.Windows.Forms.ToolStripButton SaveTBTN;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton ConnectTBTN;
    private System.Windows.Forms.ToolStripButton DisconnectTBTN;
    private System.Windows.Forms.ToolStripMenuItem ViewMNU;
    private System.Windows.Forms.ToolStripMenuItem DisplayMNU;
    private System.Windows.Forms.ToolStripMenuItem TerminalMNU;
    private System.Windows.Forms.ToolStripMenuItem ConfigMNU;
    private System.Windows.Forms.TabControl MainTABS;
    private System.Windows.Forms.TabPage DisplayTAB;
    private System.Windows.Forms.TabPage TerminalTAB;
    private System.Windows.Forms.TabPage ConfigTAB;
    private System.Windows.Forms.ToolStripMenuItem SensorMENU;
    private System.Windows.Forms.ToolStripMenuItem ConnectMN;
    private System.Windows.Forms.ToolStripMenuItem DisconnectMNU;
    private System.Windows.Forms.ToolStripStatusLabel StatusLB;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.Timer StatusTIMER;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.GroupBox TermInfoGBX;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton PingTBTN;
    private System.Windows.Forms.Timer TermTIMER;
    private System.Windows.Forms.ToolStripButton PHzRunTBTN;
    private System.Windows.Forms.ToolStripButton ClearTBTN;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.Panel HzPAN;
    private System.Windows.Forms.Panel HzLegendPAN;
    private System.Windows.Forms.Splitter splitter3;
    private System.Windows.Forms.Panel HzChartPAN;
    private System.Windows.Forms.Splitter splitter2;
    private System.Windows.Forms.Panel HzSpectPAN;
    private System.Windows.Forms.Splitter splitter4;
    private System.Windows.Forms.Panel PhasePAN;
    private System.Windows.Forms.Panel panPhaseLegend;
    private System.Windows.Forms.Splitter splitter5;
    private System.Windows.Forms.Panel PhaseChartPAN;
    private System.Windows.Forms.Splitter splitter6;
    private System.Windows.Forms.Panel PhasorPAN;
    private System.Windows.Forms.ToolStripComboBox LiveHistDDL;
    private System.Windows.Forms.Timer DisplayTIMER;
    private System.Windows.Forms.ToolStripButton NavNextTBTN;
    private System.Windows.Forms.ToolStripComboBox HzChartScaleDDL;
    private System.Windows.Forms.ToolStripLabel ScaleLBL;
    private System.Windows.Forms.Splitter splitter7;
    private System.Windows.Forms.Panel ConfigTvPAN;
    private System.Windows.Forms.TreeView ConfigTV;
    private System.Windows.Forms.Panel ConfigPAN;
    private System.Windows.Forms.TextBox DataLogFolderTXT;
    private System.Windows.Forms.CheckBox DataLogEnableCHK;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button DataLogFolderBrowseBTN;
    private System.Windows.Forms.ToolStripButton WTFBTN;
    private System.Windows.Forms.ToolStripButton WTFBTN2;
    private System.Windows.Forms.Timer DataPurgeTIMER;
    private System.Windows.Forms.CheckBox TermLogEnableCHK;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox LogFileTXT;
    private System.Windows.Forms.TextBox TermOutTXT;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown DataLogFileIntervalUD;
    private System.Windows.Forms.TextBox DataLogFileNameFormatTXT;
    private System.Windows.Forms.Label label5;
  }
}

