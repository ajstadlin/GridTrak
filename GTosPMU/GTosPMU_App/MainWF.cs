using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;


namespace GTosPMU
{
  /// <summary>
  /// Application Main Form
  /// 05/12/13 1.0.9.14  Date_FormatString config option added
  /// 11/10/12 1.0.9.8  Exception Handlers for Log SW Flush on closed file. Removed unused "History" and Sensor List interfaces.
  ///                   Log File Name Format and Display Log File Name added.  Changed Terminal ListBox to TextBox
  ///                   Data Output fixups
  /// 11/19/11 1.0.9.1  Display Last Sample GPS and Host times with difference
  /// 11/11/11 1.0.9.0  GPS Integrated Implementation
  /// </summary>

  public partial class MainWF : Form
  {
    private const string CRLF = "\x000d\x000a";   // Input EOL

    public MainWF()
    {
      InitializeComponent();
    }


    private void MainWF_Shown(object sender, EventArgs e)
    {
      Config_Load();
      this.DataPurgeTIMER.Interval = Cfg.DataPurge_TimerInterval;
      this.DataPurgeTIMER.Start();
      if (Sensor.Auto_Connect)
      {
        this.MainTABS.SelectedTab = this.DisplayTAB;
        this.LiveHistDDL.SelectedIndex = 0;
        // Live View:  Next = Add current data on a timer event
        Sensor_RunPHzMode();
        this.DisplayTIMER.Start();
      }
      if (Log.Session_Nbr == 1)
      {
        About oDlg = new About();
        oDlg.ShowDialog();
        oDlg.Dispose();
      }
    }


    // Graphics Objects
    private Pen TitlePEN = new Pen(Color.Black, 0.5f);
    private Font TitleFNT = new Font("Arial", 9.0f, FontStyle.Bold);
    private Pen GridBlackPEN = new Pen(Color.Gray, 0.5f);
    private Pen GridGrayPEN = new Pen(Color.Silver, 1f);
    private Font GridLblFNT = new Font("Arial", 6.4f, FontStyle.Regular);
    private Font LegendFNT = new Font("Arial", 8.0f, FontStyle.Regular);
    
    private void MainWF_FormClosing(object sender, FormClosingEventArgs e)
    { /// 1.0.9.8
      /// Event Handler for Application Closing
    
      //  Disconnect the Sensor
      Sensor_Disconnect();

      //  Turn Off Timers
      this.DataPurgeTIMER.Stop();
      this.TermTIMER.Stop();
      this.StatusTIMER.Stop();
      this.DisplayTIMER.Stop();

      if (m_RecordSW != null)
      {
        // Should already be Closed and Flushed
        string sMsg = "Disposing StreamWriter";
        try
        {
          sMsg += ", Disposing...";
          m_RecordSW.Dispose();
          sMsg += ", Dereferencing...";
          m_RecordSW = null;
        }
        catch (Exception ex)
        {
          Log.Err(ex, "MainWF, FormClosing", "Closing Error", Log.LogDevice.LOG);
        }
      }

      // Dispose of Graphics objects
      try
      {
        if (TitlePEN != null)
        {
          TitlePEN.Dispose();
        }
        if (TitleFNT != null)
        {
          TitleFNT.Dispose();
        }
        if (GridBlackPEN != null)
        {
          GridBlackPEN.Dispose();
        }
        if (GridGrayPEN != null)
        {
          GridGrayPEN.Dispose();
        }
        if (GridLblFNT != null)
        {
          GridLblFNT.Dispose();
        }
      }
      catch (Exception ex)
      {
        Log.Err(ex, "MainWF, FormClosing", "Closing Error", Log.LogDevice.LOG_DLG);
      }
      GC.Collect();
    }


    private void WrStatus(string sMsg)
    {
      /// Write a message to the Status Bar
      this.StatusLB.Text = sMsg;

      // Reset the Status Message Clear Timer
      this.StatusTIMER.Stop();
      this.StatusTIMER.Interval = Cfg.Status_TimerInterval;
      this.StatusTIMER.Start();
    }

    private void WrStatusTerm(string sMsg)
    {
      WrTerm(sMsg);
      WrStatus(sMsg);
    }


    private void StatusTIMER_Tick(object sender, EventArgs e)
    {
      /// On Timer Elapsed, Clear the Status Label
      this.StatusLB.Text = "";
      this.StatusTIMER.Stop();
    }


    #region  ////  User Interface Routines  ////

    private void QuitMNU_Click(object sender, EventArgs e)
    {
      /// Event Handler for File / Quit
      this.Close();
    }


    private void ConnectTBTN_Click(object sender, EventArgs e)
    {
      /// Event handler for Connect Button and Menu
      this.MainTABS.SelectedTab = this.TerminalTAB;
      Sensor_Connect();
    }


    private void DisconnectTBTN_Click(object sender, EventArgs e)
    {
      /// Event Handler for Disconnect Button and Menu
      this.MainTABS.SelectedTab = this.TerminalTAB;
      Sensor_Disconnect();
    }

    
    private void SaveTBTN_Click(object sender, EventArgs e)
    {
      /// Event Handler for File / Save 
      Config_Save();
    }


    private void PingTBTN_Click(object sender, EventArgs e)
    {
      /// Ping the Sensor
      this.MainTABS.SelectedTab = this.TerminalTAB;
      Sensor_Ping();
    }


    private void PHzRunTBTN_Click(object sender, EventArgs e)
    {
      /// Run the Sensor in Text mode
      this.MainTABS.SelectedTab = this.TerminalTAB;
      Sensor_RunPHzMode();
    }


    private void ClearTBTN_Click(object sender, EventArgs e)
    { /// 1.0.9.8
      /// Clear the Terminal Display
      this.MainTABS.SelectedTab = this.TerminalTAB;
      //this.TermLBX.Items.Clear();
      this.TermOutTXT.Text = "";
    }


    //**** End User Interface Routines ****//
    #endregion


    #region  ////  Sensor Routines  ////

    private void Sensor_Disconnect()
    {
      /// Disconnect from the Sensor
      string sResult = Sensor.Disconnect();
      if (sResult == "OK")
      {
        WrStatusTerm("Sensor Disconnected");
      }
      else
      {
        WrStatusTerm(sResult);
      }
    }


    private void Sensor_Connect()
    {
      /// Connect to the Sensor
      if (Sensor.Connect() == "OK")
      {
        // Reset the Terminal Timer
        this.TermTIMER.Stop();
        this.TermTIMER.Start();

        // Hookup Event Handlers
        ComPorts.Port1.ErrorReceived += new SerialErrorReceivedEventHandler(Port1_OnError);
        ComPorts.Port1.DataReceived += new SerialDataReceivedEventHandler(Port1_OnDataReceived);
        try
        {
          ComPorts.Port1.Open();
          WrStatusTerm("Sensor Connected");

          if (Sensor.Auto_Reset)
          {
            // Reset the Sensor
            Sensor.Port1_Send(Sensor.CX_RESET);
            WrStatusTerm("Sensor Reset!");
          }
          if (Sensor.Auto_Reboot)
          {
            // Reset the Sensor
            Sensor.Port1_Send(Sensor.CX_REBOOT);
            WrStatusTerm("Sensor Reboot!");
          }

          if (Sensor.Auto_Config)
          {
            WrStatusTerm(Sensor.Get_ConfigFromSensor());
          }

          if (Sensor.Factory_Restore)
          {
            // Reset the Sensor
            Sensor.Port1_Send(Sensor.CX_FACTORY);
            WrStatusTerm("Sensor Restored Factory Defaults!");
            // Wait for the sensor to reset
            System.Threading.Thread.Sleep(2000);
            Sensor_Ping();
            WrStatusTerm(Sensor.Get_ConfigFromSensor());
          }

          if (Sensor.Auto_Update)
          {
            Sensor.Send_ConfigToSensor();
          }
        }
        catch (Exception ex)
        {
          WrStatusTerm("Error Connecting to Sensor:  " + ex.Message);
        }
      }
    }


    private void Port1_OnError(object sender, SerialErrorReceivedEventArgs e)
    {
      /// Display RS232 Port 1 Error
      //Sensor.Port1_Buff.Add("Serial Port 1 Error:  " + e.EventType.ToString());
    }


    private string InputStr1 = "";
    private bool Inputing = false;


    private void Port1_OnDataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
    { /// 1.0.9.14
      /// Process incoming RS232 Port 1 Data
      No_Purge = true;
      try
      {
        while (ComPorts.Port1.BytesToRead > 0)
        {
          // Read Data up to EOL 
          try
          {
            InputStr1 = ComPorts.Port1.ReadLine();
          }
          catch 
          {
            // Trap Read.Timeout
            // If we haven't received the EOL in time, then clear the Input so we can start over.
            // This can be a normal condition if the sensor is in an idle or non-text mode.
            InputStr1 = "";
          }

          // Test for PHz Text Packet
          if (InputStr1.IndexOf("PHz") == 0)
          {
            if (InputStr1.IndexOf("PHzCfg") == 0)
            {
              // Incoming Configuration
              // Read and store the next 3 sets of 34 bytes = 102 bytes
              for (int uc = 0; uc < Sensor.Config_Buff.Length; uc++)
              {
                // 
                while (ComPorts.Port1.BytesToRead < 1)
                {
                  System.Threading.Thread.Sleep(10);
                }
                Sensor.Config_Buff[uc] = (byte)ComPorts.Port1.ReadByte();
              }
              Sensor.Config_Ready = true;
            }
            else
            {
              // Create a Packet Record
              PHzPacketClass phz = new PHzPacketClass(InputStr1);

              if (phz.Status > -1)
              {
                // Valid Packet Record; Add it to the Data queue
                ComPorts.Port1_Data.Add(phz);
                string sCSV = "";
                if (Sensor.Host_Sync)
                {
                  // "\"DATE\",\"UTC_PPS\",\"FRACSEC\",\"ELAPSED\",\"INTERVAL\",\"SAMPLE_ID\",\"FREQUENCY\",\"PHASE_ANGLE\",\"AMPLITUDE\""
                  sCSV = phz.Sample_DT.ToString(Cfg.Date_FormatString) 
                         + "," + phz.Sample_PPS.ToString("HH:mm:ss") 
                         + ",0." + phz.PmuFracSec.ToString().PadRight(6, "0".ToCharArray()[0]) 
                         + "," + phz.Elapsed.TotalSeconds.ToString("0.000000") 
                         + "," + phz.Interval_Id.ToString() 
                         + "," + phz.Sample_ID.ToString() 
                         + "," + phz.Frequency.ToString("#0.00000") 
                         + "," + phz.PhaseAngle.ToString("##0.000")
                         + "," + phz.Amplitude.ToString("##0.000");

                  //sCSV = phz.Sample_DT.ToString(Cfg.Date_FormatString) + "," + phz.Sample_PPS.ToString("HH:mm:ss") + "," + phz.PPS_Offset.TotalSeconds.ToString("0.000000") + ","
                  //              + phz.Elapsed.TotalSeconds.ToString("0.000000") + "," + phz.Interval_Id.ToString() + "," + phz.Sample_ID.ToString() + ","
                  //              + phz.Frequency.ToString("#0.00000") + "," + phz.PhaseAngle.ToString("##0.000") + "," + phz.Amplitude.ToString("##0.000");
                }
                else
                {
                  sCSV = phz.GPS_Time_DT.ToString(Cfg.Date_FormatString) 
                         + "," + phz.GPS_Time_PPS.ToString("HH:mm:ss") 
                         + ",0." + phz.PmuFracSec.ToString().PadRight(6, "0".ToCharArray()[0]) 
                         + "," + phz.Elapsed.TotalSeconds.ToString("0.000000") 
                         + "," + phz.Interval_Id.ToString() 
                         + "," + phz.Sample_ID.ToString()
                         + "," + phz.Frequency.ToString("#0.00000") 
                         + "," + phz.PhaseAngle.ToString("##0.000") 
                         + "," + phz.Amplitude.ToString("##0.000");

                  //sCSV = phz.GPS_Time_DT.ToString(Cfg.Date_FormatString) + "," + phz.GPS_Time_PPS.ToString("HH:mm:ss") + "," + phz.PPS_Offset.TotalSeconds.ToString("0.000000") + ","
                  //              + phz.Elapsed.TotalSeconds.ToString("0.000000") + "," + phz.Interval_Id.ToString() + "," + phz.Sample_ID.ToString() + ","
                  //              + phz.Frequency.ToString("#0.00000") + "," + phz.PhaseAngle.ToString("##0.000") + "," + phz.Amplitude.ToString("##0.000");
                }

                //InputStr1.Substring(0, InputStr1.LastIndexOf(","));
                if (this.TermLogEnableCHK.Checked)
                {
                  ComPorts.Port1_Buff.Add(sCSV);
                }
                if (this.DataLogEnableCHK.Checked)
                {
                  Record_Data(sCSV);
                }
              }
              else
              {
                // Invalid Packet Record, Discard it
                ComPorts.Port1_Message += phz.Status.ToString() + " PACKET ERROR: " + InputStr1 + CRLF;
                phz = null;
              }
            }
            InputStr1 = "";
          }
          else
          {
            // Not a PHz Packet
            if (InputStr1.IndexOf("Rebooted!") > -1) 
            {
              ComPorts.Port1_Message += "Rebooted!" + CRLF;
            }
            else
            {
              ComPorts.Port1_Message += InputStr1 + CRLF;
            }
          }
        }
      }
      catch (Exception ex)
      {
        ComPorts.Port1_Message += "Sensor Read Error:  " + ex.Message + CRLF;
      }
      No_Purge = false;
    }

       
    private void Sensor_Ping()
    {
      /// Synchronously PING the Sensor
      try
      {
        // Switch to the Terminal Tab
        this.MainTABS.SelectedTab = this.TerminalTAB;

        // Check for Sensor Connection
        if ((ComPorts.Port1 == null) || (!ComPorts.Port1.IsOpen))
        {
          Sensor_Connect();
        }

        // Transmit the PING to the Sensor
        WrTerm(CRLF + "-> " + Sensor.Ping() + " -> ");
        WrStatus("Sensor Pinged!"); 
      }
      catch (Exception ex)
      {
        WrStatusTerm("Ping Error:  " + ex.Message);
      }
    }


    private void Sensor_RunPHzMode()
    {
      /// Run the Sensor in PHzMonitor Text Mode
      try
      {
        // Check for Sensor Connection
        if ((ComPorts.Port1 == null) || (!ComPorts.Port1.IsOpen))
        {
          Sensor_Connect();
        }

        // Transmit the PING to the Sensor
        WrTerm(CRLF + "-> " + Sensor.Run_PHzMode(Sensor.Host_Sync) + " -> ");
        WrStatus("Sensor is Now Running!");
      }
      catch (Exception ex)
      {
        WrStatusTerm("Ping Error:  " + ex.Message);
      }
    }
    
    
    #endregion  //**** End Sensor Routines  ****//


    #region  ////  Terminal Routines  ////

    private void TermTIMER_Tick(object sender, EventArgs e)
    {
      if (ComPorts.Port1_Message.Length > 0)
      {
        // Display messages from the sensor
        string[] aMsgs = ComPorts.Port1_Message.Split(CRLF.ToCharArray());
        for (int ii = 0; ii < aMsgs.Length; ++ii)
        {
          if (aMsgs[ii].Trim().Length > 0)
          {
            WrTerm(aMsgs[ii]);
            if ((aMsgs[ii].IndexOf("Rebooted!") > -1) && Sensor.Auto_Update)
            {
              WrTerm(Sensor.Get_ConfigFromSensor());
            }
          }
        }
        ComPorts.Port1_Message = "";
      }

      /// Display the Data Received from the Sensor
      //  Note:  We have to use this timer event to display the data using a different thread than the SerialPort.
      try
      {
        while (ComPorts.Port1_Buff.Count > 0)
        {
          if (ComPorts.Port1_Buff[0] != null)
          {
            WrTerm(ComPorts.Port1_Buff[0].ToString());
          }
          ComPorts.Port1_Buff.RemoveAt(0);
        }
        ComPorts.Port1_Buff.TrimToSize();
      }
      catch (Exception ex)
      {
        Log.Err(ex, "Main, Termnal Timer", "Error Writing to Terminal: " + ComPorts.Port1_Buff.Count.ToString(), Log.LogDevice.LOG);
      }

      if (m_DataFilePath != this.LogFileTXT.Text)
      {
        this.LogFileTXT.Text = m_DataFilePath;
      }
    }


    private void WrTerm(string sMsg)
    {
      if (this.TermOutTXT.Lines.Length > Cfg.Term_MaxLines)
      {
        this.TermOutTXT.Text = this.TermOutTXT.Text.Remove(0, this.TermOutTXT.Lines[0].Length + 2);
      }
      this.TermOutTXT.AppendText(sMsg + Log.CRLF);
    }


    #endregion   //****  End Terminal Routines ****//


    #region    ////  Hz Chart Routines  ////


    private BufferedGraphicsContext CurrentBGC = BufferedGraphicsManager.Current;


    private void DisplayTAB_Resize(object sender, EventArgs e)
    {
    }


    private void MainTABS_Selected(object sender, TabControlEventArgs e)
    {
      if (e.TabPage.Equals(this.DisplayTAB))
      {
        this.DisplayTAB.Invalidate();
      }
    }


    private void MainTABS_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.MainTABS.SelectedTab.Equals(this.ConfigTAB))
      {
        this.ConfigTV.ExpandAll();
        if (this.ConfigTV.SelectedNode == null)
        {
          this.ConfigTV.SelectedNode = this.ConfigTV.Nodes[1].Nodes[3];
        }
      }
    }


    private void DisplayTAB_Paint(object sender, PaintEventArgs e)
    {
      Phasor_Calc();
      PhasorPAN.Invalidate();
      PhaseChart_Calc();
      PhaseChartPAN.Invalidate();
      HzChart_Calc();
      HzChartPAN.Invalidate();
      HzSpect_Calc();
      HzSpectPAN.Invalidate();
    }


    private float HzChart_GridTop = 0f;
    private float HzChart_GridHeight = 0f;
    private float HzChart_GridLeft = 0f;
    private float HzChart_GridWidth = 0f;
    private float HzChart_GridRight = 0f;
    private float HzChart_GridBot = 0f;
    private float HzChart_GridTopHalf = 0f;
    private float HzChart_GridBotHalf = 0f;
    private float HzChart_GridCenterY = 0f;

    private float HzChart_MajorX = 0f;
    private float HzChart_MinorX = 0f;
    private float HzChart_TicX = 0f;

    private float HzChart_Scale = 0f;


    private void HzChart_Calc()
    {
      // Do this on Resizing layout
      HzChart_GridHeight = HzChartPAN.Height - 80f;  // 200f;
      HzChart_GridTop = 40f;
      HzChart_GridLeft = 50f;
      HzChart_GridWidth = HzChartPAN.Width - 100f;
      HzChart_GridRight = HzChart_GridLeft + HzChart_GridWidth;
      HzChart_GridBot = HzChart_GridTop + HzChart_GridHeight;
      HzChart_GridCenterY = HzChart_GridTop + (HzChart_GridHeight / 2f);
      HzChart_GridTopHalf = HzChart_GridTop + (HzChart_GridHeight / 4f);
      HzChart_GridBotHalf = HzChart_GridCenterY + (HzChart_GridHeight / 4f);

      HzChart_MajorX = HzChart_GridWidth / 10f;
      HzChart_MinorX = HzChart_GridWidth / 40f;
      HzChart_TicX = HzChart_MajorX / 10f;

      switch (HzChartScaleDDL.SelectedIndex)
      {
        case 0:  // 1.0 %, +/- 600mHz
          HzChart_Scale = 1f;
          break;

        case 1:  // 0.5 %, +/- 300mHz
          HzChart_Scale = 0.5f;
          break;

        case 2:  // 0.2 %, +/- 120mHz
          HzChart_Scale = 0.2f;
          break;

        case 3:  // 0.1 %, +/- 60mHz
          HzChart_Scale = 0.1f;
          break;

        case 4:  // 0.05 %, +/- 30mHz
          HzChart_Scale = 0.05f;
          break;

        case 5:  // 0.02 %, +/- 12mHz
          HzChart_Scale = 0.02f;
          break;

        case 6:  // 0.01 %, +/- 6mHz
          HzChart_Scale = 0.01f;
          break;

        default:
          break;
      }

    }


    private void HzChart_DrawGrid()
    {
      BufferedGraphics HzChartBG = null;
      bool bIsLive = (this.LiveHistDDL.Text.ToUpper().IndexOf("LIVE") == 0);
      try
      {
        HzChartBG = CurrentBGC.Allocate(this.HzChartPAN.CreateGraphics(), HzChartPAN.DisplayRectangle);
        HzChartBG.Graphics.Clear(Color.White);

        // Title
        HzChartBG.Graphics.DrawString("Grid Frequency and % Deviation VS Time", TitleFNT, Brushes.Black, (HzChartPAN.Width / 2f) - 120.0f, 4.0f);

        float fi = 0;
        for (int ii = 1; ii < 20; ii++)
        {
          fi = HzChart_GridTop + HzChart_GridHeight * (Convert.ToSingle(ii) / 20f);
          HzChartBG.Graphics.DrawLine(GridGrayPEN, HzChart_GridLeft, fi, HzChart_GridRight, fi);
        }

        float xx = HzChart_GridLeft;
        if (bIsLive)
        {
          for (int ii = 9; ii > 0; ii--)
          {
            xx += HzChart_MajorX;
            HzChartBG.Graphics.DrawLine(GridGrayPEN, xx, HzChart_GridTop, xx, HzChart_GridBot);
            HzChartBG.Graphics.DrawString("-" + Convert.ToString(ii), GridLblFNT, Brushes.Black, xx - 6f, HzChart_GridBot + 4f);
          }
          HzChartBG.Graphics.DrawString("-10", GridLblFNT, Brushes.Black, HzChart_GridLeft - 6f, HzChart_GridBot + 4f);
          HzChartBG.Graphics.DrawString("0", GridLblFNT, Brushes.Black, HzChart_GridRight - 2f, HzChart_GridBot + 4f);
          //HzChartBG.Graphics.DrawString("NOW", GridLblFNT, Brushes.Black, HzChart_GridRight - 22f, HzChart_GridBot + 26f);
          HzChartBG.Graphics.DrawString("NOW", GridLblFNT, Brushes.Black, HzChart_GridRight + 8f, HzChart_GridBot + 4f);
        }
        else
        {
          for (int ii = 1; ii < 10; ii++)
          {
            xx += HzChart_MajorX;
            HzChartBG.Graphics.DrawLine(GridGrayPEN, xx, HzChart_GridTop, xx, HzChart_GridBot);
            HzChartBG.Graphics.DrawString("+" + Convert.ToString(ii), GridLblFNT, Brushes.Black, xx - 6f, HzChart_GridBot + 4f);
          }
          HzChartBG.Graphics.DrawString("0", GridLblFNT, Brushes.Black, HzChart_GridLeft, HzChart_GridBot + 4f);
          HzChartBG.Graphics.DrawString("+10", GridLblFNT, Brushes.Black, HzChart_GridRight + -10f, HzChart_GridBot + 4f);
        }
        HzChartBG.Graphics.DrawString("MINUTES", GridLblFNT, Brushes.Black, HzChart_GridLeft + (HzChart_GridWidth / 2f) - 20f, HzChart_GridBot + 16f);

        xx = HzChart_GridLeft;
        for (int jj = 1; jj < 100; jj++)
        {
          xx += HzChart_TicX;
          HzChartBG.Graphics.DrawLine(GridGrayPEN, xx, HzChart_GridTop, xx, HzChart_GridTop + 4f);
          HzChartBG.Graphics.DrawLine(GridGrayPEN, xx, HzChart_GridCenterY - 2f, xx, HzChart_GridCenterY + 2f);
          HzChartBG.Graphics.DrawLine(GridGrayPEN, xx, HzChart_GridBot, xx, HzChart_GridBot - 4f);
        }

        xx = HzChart_GridLeft;
        for (int jj = 1; jj < 40; jj++)
        {
          xx += HzChart_MinorX;
          HzChartBG.Graphics.DrawLine(GridGrayPEN, xx, HzChart_GridTopHalf - 2f, xx, HzChart_GridTopHalf + 2f);
          HzChartBG.Graphics.DrawLine(GridGrayPEN, xx, HzChart_GridBotHalf - 2f, xx, HzChart_GridBotHalf + 2f);
        }

        PointF[] pts = new PointF[4];
        pts[0] = new PointF(HzChart_GridLeft, HzChart_GridTop);
        pts[1] = new PointF(HzChart_GridRight, HzChart_GridTop);
        pts[2] = new PointF(HzChart_GridRight, HzChart_GridBot);
        pts[3] = new PointF(HzChart_GridLeft, HzChart_GridBot);
        HzChartBG.Graphics.DrawPolygon(GridBlackPEN, pts);

        // Do the Chart Labels
        HzChartBG.Graphics.DrawString(Sensor.Nominal_Hz.ToString() + "Hz", TitleFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - 8f);
        HzChartBG.Graphics.DrawString(Sensor.Nominal_Hz.ToString() + "Hz", TitleFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - 8f);
        switch (HzChartScaleDDL.SelectedIndex)
        {
          case 0:  // 1.0 %, +/- 600mHz, +/- 500mHz
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.01).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.50%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.50%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.99).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);

            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.01).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.50%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.50%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.99).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);
            break;

          case 1:  // 0.5 %, +/- 300mHz, +/- 250mHz
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.005).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.25%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.25%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.995).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);

            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.005).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.25%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.25%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.995).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);
            break;

          case 2:  // 0.2 %, +/- 120mHz, +/- 100mHz
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.002).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.10%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.10%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.998).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);

            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.002).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.10%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.10%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.998).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);
            break;

          case 3:  // 0.1 %, +/- 60mHz, +/- 50mHz
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.001).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.050%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.050%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.999).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);

            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.001).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.050%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.050%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.999).ToString("00.00") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);
            break;

          case 4:  // 0.05 %, +/- 30mHz, +/- 25mHz
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.0005).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.025%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.025%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.9995).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);

            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.0005).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.025%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.025%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.9995).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);
            break;

          case 5:  // 0.02 %, +/- 12mHz, +/- 10mHz
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.0002).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.010%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.010%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.9998).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);

            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.0002).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.010%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.010%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.9998).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);
            break;

          case 6:  // 0.01 %, +/- 6mHz
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.0001).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.005%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.005%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.9999).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);

            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.0001).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.005%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.005%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.9999).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);
            break;

          default:
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.0001).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.050%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.050%", GridLblFNT, Brushes.Black, HzChart_GridLeft - 40f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.9999).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridLeft - 42f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);

            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 1.0001).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (6f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("+0.050%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY - (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString("-0.050%", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (3f / 12f * HzChart_GridHeight) - 6f);
            HzChartBG.Graphics.DrawString((Convert.ToSingle(Sensor.Nominal_Hz) * 0.9999).ToString("00.000") + "Hz", GridLblFNT, Brushes.Black, HzChart_GridRight + 6f, HzChart_GridCenterY + (6f / 12f * HzChart_GridHeight) - 6f);
            break;
        }
      }
      catch (Exception ex1)
      {
        Log.Err(ex1, this.Name, "HzChart_DrawGrid(1)", Log.LogDevice.LOG);
      }

      HzChart_Plot(HzChartBG.Graphics);

      // Dispose of Graphics objects
      try
      {
        HzChartBG.Render();
        HzChartBG.Render(HzChartPAN.CreateGraphics());
        HzChartBG.Dispose();
      }
      catch (Exception ex)
      {
        Log.Err(ex, this.Name, "HzChart_DrawGrid(2)", Log.LogDevice.LOG);
      }
      GC.Collect();
    }


    private void HzChart_Plot(Graphics g)
    {
      /// Plot the Frequency Chart Data
      //  Disable Data Purging
      No_Purge = true;
      bool bIsLive = (this.LiveHistDDL.Text.ToUpper().IndexOf("LIVE") == 0);

      // Plot the Live Frequency Data
      DateTime dtEnd = PHzPacketClass.PreciseDT.UtcNow;
      DateTime dtStart = dtEnd.AddMinutes(-10d);

      // Save the Data Count for what we have for our chart 
      // because the actual count may change with new incoming data 
      // while we are working on the chart.
      int iSaveCount = ComPorts.Port1_Data.Count;

      if (iSaveCount > 1)
      {
        PointF[] pts = new PointF[iSaveCount];
        float fT = 0f;
        float fX = 0f;
        float fY = 0f;
        for (int ii = 0; ii < iSaveCount; ++ii)
        {
          if (Sensor.Host_Sync)
          {
            fT = (float)((PHzPacketClass)ComPorts.Port1_Data[ii]).Sample_DT.Subtract(dtStart).TotalSeconds;
          }
          else
          {
            fT = (float)((PHzPacketClass)ComPorts.Port1_Data[ii]).GPS_Time_DT.Subtract(dtStart).TotalSeconds;
          }

          fX = HzChart_GridLeft + ((HzChart_GridWidth / 600) * fT);
          /// 1.0.9.12 add HzChart_Adjust to frequency
          fY = HzChart_GridCenterY - ((HzChart_GridHeight / HzChart_Scale) * (Convert.ToSingle(((PHzPacketClass)ComPorts.Port1_Data[ii]).Frequency + Cfg.HzChart_Adjust) - Convert.ToSingle(Sensor.Nominal_Hz)));

          if (fY > HzChart_GridBot)
          {
            fY = HzChart_GridBot;
          }
          else if (fY < HzChart_GridTop)
          {
            fY = HzChart_GridTop;
          }
          pts[ii] = new PointF(fX, fY);
        }
        Pen pen1 = new Pen(System.Drawing.Color.Red, 1.0f);
        g.DrawLines(pen1, pts);
        pen1.Dispose();
      }

      g.DrawString(dtStart.ToString("MMM d, yyyy"), GridLblFNT, Brushes.Black, HzChart_GridLeft, HzChart_GridTop - 14f);
      g.DrawString(dtEnd.ToString("MMM d, yyyy"), GridLblFNT, Brushes.Black, HzChart_GridRight - 54f, HzChart_GridTop - 14f);

      // HzChartBG.Graphics.DrawString("0", GridLblFNT, Brushes.Black, HzChart_GridLeft, HzChart_GridBot + 4f);
      g.DrawString(dtStart.ToString("HH:mm:ss.fff"), GridLblFNT, Brushes.Black, HzChart_GridLeft, HzChart_GridBot + 16f);
      // HzChartBG.Graphics.DrawString("+10", GridLblFNT, Brushes.Black, HzChart_GridRight + -10f, HzChart_GridBot + 4f);
      g.DrawString(dtEnd.ToString("HH:mm:ss.fff"), GridLblFNT, Brushes.Black, HzChart_GridRight - 54f, HzChart_GridBot + 16f);
      // HzChartBG.Graphics.DrawString("MINUTES", GridLblFNT, Brushes.Black, HzChart_GridLeft + (HzChart_GridWidth / 2f) - 20f, HzChart_GridBot + 16f);

      //  Enable Data Purging
      No_Purge = false;
    }


    private void HzChartPAN_Paint(object sender, PaintEventArgs e)
    {
      HzChart_DrawGrid();
    }


    private void HzChartPAN_Resize(object sender, EventArgs e)
    {
      HzChart_Calc();
      HzChartPAN.Invalidate();
    }


    #endregion


    #region  ////  Phasor Routines  ////

    private float Phasor_GridTop = 0f;
    private float Phasor_GridCenterX = 0f;
    private float Phasor_GridCenterY = 0f;
    private float Phasor_GridRadius = 0.0f;

    // working variables
    private float RSin0 = 0.0f;
    private float RCos0 = 0.0f;
    private float RSin1 = 0.0f;
    private float RCos1 = 0.0f;

    private void Phasor_Calc()
    {
      // Do this on Resize Layout
      Phasor_GridTop = 40;        // Fixed Top Position
      Phasor_GridRadius = 100f;   // Convert.ToSingle(0.8f * this.PhasorPAN.Width / 2f);
      Phasor_GridCenterX = 128;  // Convert.ToSingle(this.PhasorPAN.Width / 2f) - 2f;
      Phasor_GridCenterY = 140f;  // Convert.ToSingle(Phasor_GridTop + Phasor_GridRadius);
    }

    private void Phasor_DrawGrid()
    {
      BufferedGraphics PhasorBG = null;
      bool bIsLive =  (this.LiveHistDDL.Text.ToUpper().IndexOf("LIVE") == 0);
      try
      {
        PhasorBG = CurrentBGC.Allocate(this.PhasorPAN.CreateGraphics(), PhasorPAN.DisplayRectangle);
        PhasorBG.Graphics.Clear(Color.White);

        // Title
        PhasorBG.Graphics.DrawString("Phase Angle", TitleFNT, Brushes.Black, 90.0f, 4.0f);

        // Outer Grid Circle
        PhasorBG.Graphics.DrawEllipse(GridBlackPEN, Phasor_GridCenterX - Phasor_GridRadius, Phasor_GridTop, Phasor_GridRadius * 2f, Phasor_GridRadius * 2f);
        PhasorBG.Graphics.DrawEllipse(GridGrayPEN, Phasor_GridCenterX - (Phasor_GridRadius * 0.75f), Phasor_GridTop + (Phasor_GridRadius * 0.25f), Phasor_GridRadius * 1.5f, Phasor_GridRadius * 1.5f);
        PhasorBG.Graphics.DrawEllipse(GridGrayPEN, Phasor_GridCenterX - (Phasor_GridRadius * 0.5f), Phasor_GridTop + (Phasor_GridRadius * 0.5f), Phasor_GridRadius, Phasor_GridRadius);
        PhasorBG.Graphics.DrawEllipse(GridGrayPEN, Phasor_GridCenterX - (Phasor_GridRadius * 0.25f), Phasor_GridTop + (Phasor_GridRadius * 0.75f), Phasor_GridRadius * 0.5f, Phasor_GridRadius * 0.5f);
        PhasorBG.Graphics.DrawEllipse(GridGrayPEN, Phasor_GridCenterX - 4f, Phasor_GridCenterY - 4, 8, 8);

        PhasorBG.Graphics.DrawLine(GridGrayPEN, Phasor_GridCenterX - Phasor_GridRadius, Phasor_GridCenterY, Phasor_GridCenterX + Phasor_GridRadius, Phasor_GridCenterY);
        PhasorBG.Graphics.DrawLine(GridGrayPEN, Phasor_GridCenterX, Phasor_GridTop, Phasor_GridCenterX, Phasor_GridCenterY + Phasor_GridRadius);

        // Draw the tics
        for (int ii = 1; ii < 9; ii++)
        {
          // (ii * 10) degrees * PI / 180 = Radians
          RSin0 = Convert.ToSingle(Phasor_GridRadius * Math.Sin((float)(ii) * 10f * Math.PI / 180f));
          RCos0 = Convert.ToSingle(Phasor_GridRadius * Math.Cos((float)(ii) * 10f * Math.PI / 180f));
          if ((ii == 3) || (ii == 6))
          {
            RSin1 = 0.2f * RSin0;
            RCos1 = 0.2f * RCos0;
          }
          else
          {
            RSin1 = 0.95f * RSin0;
            RCos1 = 0.95f * RCos0;
          }
          PhasorBG.Graphics.DrawLine(GridGrayPEN, Phasor_GridCenterX + RCos0, Phasor_GridCenterY + RSin0,
                                               Phasor_GridCenterX + RCos1, Phasor_GridCenterY + RSin1);
          PhasorBG.Graphics.DrawLine(GridGrayPEN, Phasor_GridCenterX - RCos0, Phasor_GridCenterY + RSin0,
                                               Phasor_GridCenterX - RCos1, Phasor_GridCenterY + RSin1);
          PhasorBG.Graphics.DrawLine(GridGrayPEN, Phasor_GridCenterX + RCos0, Phasor_GridCenterY - RSin0,
                                               Phasor_GridCenterX + RCos1, Phasor_GridCenterY - RSin1);
          PhasorBG.Graphics.DrawLine(GridGrayPEN, Phasor_GridCenterX - RCos0, Phasor_GridCenterY - RSin0,
                                               Phasor_GridCenterX - RCos1, Phasor_GridCenterY - RSin1);

          if (ii == 3)
          {
            PhasorBG.Graphics.DrawString("+60", GridLblFNT, Brushes.Black, Phasor_GridCenterX + RCos0 + 4, Phasor_GridCenterY - RSin0 - 10);
            PhasorBG.Graphics.DrawString("-60", GridLblFNT, Brushes.Black, Phasor_GridCenterX - RCos0 - 20, Phasor_GridCenterY - RSin0 - 10);

            PhasorBG.Graphics.DrawString("+120", GridLblFNT, Brushes.Black, Phasor_GridCenterX + RCos0 + 2, Phasor_GridCenterY + RSin0 + 2);
            PhasorBG.Graphics.DrawString("-120", GridLblFNT, Brushes.Black, Phasor_GridCenterX - RCos0 - 20, Phasor_GridCenterY + RSin0 + 2);
          }
          if (ii == 6)
          {
            PhasorBG.Graphics.DrawString("+30", GridLblFNT, Brushes.Black, Phasor_GridCenterX + RCos0 + 2, Phasor_GridCenterY - RSin0 - 10);
            PhasorBG.Graphics.DrawString("-30", GridLblFNT, Brushes.Black, Phasor_GridCenterX - RCos0 - 18, Phasor_GridCenterY - RSin0 - 10);

            PhasorBG.Graphics.DrawString("+150", GridLblFNT, Brushes.Black, Phasor_GridCenterX + RCos0 + 2, Phasor_GridCenterY + RSin0 + 2);
            PhasorBG.Graphics.DrawString("-150", GridLblFNT, Brushes.Black, Phasor_GridCenterX - RCos0 - 20, Phasor_GridCenterY + RSin0 + 2);
          }
        }

        // Do the Phasor Chart Labels
        PhasorBG.Graphics.DrawString("0", GridLblFNT, Brushes.Black, Phasor_GridCenterX - 4f, Phasor_GridTop - 14f);
        PhasorBG.Graphics.DrawString("+90", GridLblFNT, Brushes.Black, Phasor_GridCenterX + Phasor_GridRadius + 4, Phasor_GridCenterY - 6);
        PhasorBG.Graphics.DrawString("-90", GridLblFNT, Brushes.Black, Phasor_GridCenterX - Phasor_GridRadius - 18, Phasor_GridCenterY - 6);
        PhasorBG.Graphics.DrawString("+180", GridLblFNT, Brushes.Black, Phasor_GridCenterX - 8f, Phasor_GridTop + (Phasor_GridRadius * 2f) + 4f);

        //  Disable Data Purging
        No_Purge = true;
        if (ComPorts.Port1_Data.Count > 0)
        {
          Phasor_Plot(PhasorBG.Graphics, ((PHzPacketClass)(ComPorts.Port1_Data[ComPorts.Port1_Data.Count - 1])).PhaseAngle, Color.Red);
        }
        //  Enable Data Purging
        No_Purge = false;

      }
      catch (Exception ex1)
      {
        Log.Err(ex1, this.Name, "Phasor_DrawGrid(1)", Log.LogDevice.LOG);
      }

      // Dispose of Graphics objects
      try
      {
        PhasorBG.Render();
        PhasorBG.Render(PhasorPAN.CreateGraphics());
        PhasorBG.Dispose();
      }
      catch (Exception ex)
      {
        Log.Err(ex, this.Name, "Phasor_DrawGrid(2)", Log.LogDevice.LOG);
      }
      GC.Collect();
    }


    private void PhasorPAN_Paint(object sender, PaintEventArgs e)
    {
      Phasor_DrawGrid();
    }


    private void PhasorPAN_Resize(object sender, EventArgs e)
    {
      Phasor_Calc();
      PhasorPAN.Invalidate();
    }


    private void Phasor_Plot(Graphics g, float fPhaseAngle, Color cPenColor)
    {
      // Plot a Phasor Data Record
      Pen pen1 = new Pen(cPenColor, 2.0f);
      fPhaseAngle -= 90f;
      g.DrawLine(pen1, Phasor_GridCenterX, Phasor_GridCenterY, Phasor_GridCenterX + Convert.ToSingle(Phasor_GridRadius * Math.Cos(fPhaseAngle * Math.PI / 180)),
                                                               Phasor_GridCenterY + Convert.ToSingle(Phasor_GridRadius * Math.Sin(fPhaseAngle * Math.PI / 180)));
    }

    #endregion 


    #region  ////  Chart Routines  ////

    private float PhaseChart_GridTop = 0f;
    private float PhaseChart_GridHeight = 0f;
    private float PhaseChart_GridLeft = 0f;
    private float PhaseChart_GridWidth = 0f;
    private float PhaseChart_GridRight = 0f;
    private float PhaseChart_GridBot = 0f;
    private float PhaseChart_GridTopHalf = 0f;
    private float PhaseChart_GridBotHalf = 0f;
    private float PhaseChart_GridCenterY = 0f;

    private float PhaseChart_MajorX = 0f;
    private float PhaseChart_MinorX = 0f;
    private float PhaseChart_TicX = 0f;


    private void PhaseChart_Calc()
    {
      // Do this on Resizing layout
      //   Depends on Phasor_Grid_Calc() done first
      PhaseChart_GridHeight = Phasor_GridRadius * 2f;      // Chart1PAN.Height - 40f;
      PhaseChart_GridTop = 40f;
      PhaseChart_GridLeft = 50f;
      PhaseChart_GridWidth = PhaseChartPAN.Width - 100f;
      PhaseChart_GridRight = PhaseChart_GridLeft + PhaseChart_GridWidth;
      PhaseChart_GridBot = PhaseChart_GridTop + PhaseChart_GridHeight;
      PhaseChart_GridCenterY = PhaseChart_GridTop + (PhaseChart_GridHeight / 2f);
      PhaseChart_GridTopHalf = PhaseChart_GridTop + (PhaseChart_GridHeight / 4f);
      PhaseChart_GridBotHalf = PhaseChart_GridCenterY + (PhaseChart_GridHeight / 4f);

      PhaseChart_MajorX = PhaseChart_GridWidth / 10f;
      PhaseChart_MinorX = PhaseChart_GridWidth / 40f;
      PhaseChart_TicX = PhaseChart_MajorX / 10f;
    }


    private void PhaseChart_DrawGrid()
    {
      BufferedGraphics PhaseChartBG = null;
      bool bIsLive = (this.LiveHistDDL.Text.ToUpper().IndexOf("LIVE") == 0);
      try
      {
        PhaseChartBG = CurrentBGC.Allocate(this.PhaseChartPAN.CreateGraphics(), PhaseChartPAN.DisplayRectangle);
        PhaseChartBG.Graphics.Clear(Color.White);

        // Title
        PhaseChartBG.Graphics.DrawString("Phase Angle VS Time", TitleFNT, Brushes.Black, (PhaseChartPAN.Width / 2f) - 80.0f, 4.0f);

        float fi = 0;
        for (int ii = 1; ii < 12; ii++)
        {
          fi = PhaseChart_GridTop + PhaseChart_GridHeight * (Convert.ToSingle(ii) / 12f);
          PhaseChartBG.Graphics.DrawLine(GridGrayPEN, PhaseChart_GridLeft, fi, PhaseChart_GridRight, fi);
        }

        float xx = PhaseChart_GridLeft;
        if (bIsLive)
        {
          for (int ii = 9; ii > 0; ii--)
          {
            xx += PhaseChart_MajorX;
            PhaseChartBG.Graphics.DrawLine(GridGrayPEN, xx, PhaseChart_GridTop, xx, PhaseChart_GridBot);
            PhaseChartBG.Graphics.DrawString("-" + Convert.ToString(ii), GridLblFNT, Brushes.Black, xx - 6f, PhaseChart_GridBot + 4f);
          }
          PhaseChartBG.Graphics.DrawString("-10", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 6f, PhaseChart_GridBot + 4f);
          PhaseChartBG.Graphics.DrawString("0", GridLblFNT, Brushes.Black, PhaseChart_GridRight - 2f, PhaseChart_GridBot + 4f);
        }
        else
        {
          for (int ii = 1; ii < 10; ii++)
          {
            xx += PhaseChart_MajorX;
            PhaseChartBG.Graphics.DrawLine(GridGrayPEN, xx, PhaseChart_GridTop, xx, PhaseChart_GridBot);
            PhaseChartBG.Graphics.DrawString("+" + Convert.ToString(ii), GridLblFNT, Brushes.Black, xx - 6f, PhaseChart_GridBot + 4f);
          }
          PhaseChartBG.Graphics.DrawString("0", GridLblFNT, Brushes.Black, HzChart_GridLeft, PhaseChart_GridBot + 4f);
          PhaseChartBG.Graphics.DrawString("+10", GridLblFNT, Brushes.Black, PhaseChart_GridRight + -10f, PhaseChart_GridBot + 4f);
        }

        for (int ii = 1; ii < 10; ii++)
        {
        }

        PhaseChartBG.Graphics.DrawString("Last Sample Time = ", GridLblFNT, Brushes.Black, PhaseChart_GridRight - 384, PhaseChart_GridBot + 16f);

        PhaseChartBG.Graphics.DrawString("Host:", GridLblFNT, Brushes.Black, PhaseChart_GridRight - 284f, PhaseChart_GridBot + 16f);
        if (!Sensor.Host_Sync)
        {
          PhaseChartBG.Graphics.DrawString(" GPS:", GridLblFNT, Brushes.Black, PhaseChart_GridRight - 284f, PhaseChart_GridBot + 26f);
        }

        xx = PhaseChart_GridLeft;
        for (int jj = 1; jj < 100; jj++)
        {
          xx += PhaseChart_TicX;
          PhaseChartBG.Graphics.DrawLine(GridGrayPEN, xx, PhaseChart_GridTop, xx, PhaseChart_GridTop + 4f);
          PhaseChartBG.Graphics.DrawLine(GridGrayPEN, xx, PhaseChart_GridCenterY - 2f, xx, PhaseChart_GridCenterY + 2f);
          PhaseChartBG.Graphics.DrawLine(GridGrayPEN, xx, PhaseChart_GridBot, xx, PhaseChart_GridBot - 4f);
        }

        xx = PhaseChart_GridLeft;
        for (int jj = 1; jj < 40; jj++)
        {
          xx += PhaseChart_MinorX;
          PhaseChartBG.Graphics.DrawLine(GridGrayPEN, xx, PhaseChart_GridTopHalf - 2f, xx, PhaseChart_GridTopHalf + 2f);
          PhaseChartBG.Graphics.DrawLine(GridGrayPEN, xx, PhaseChart_GridBotHalf - 2f, xx, PhaseChart_GridBotHalf + 2f);
        }

        PointF[] pts = new PointF[4];
        pts[0] = new PointF(PhaseChart_GridLeft, PhaseChart_GridTop);
        pts[1] = new PointF(PhaseChart_GridRight, PhaseChart_GridTop);
        pts[2] = new PointF(PhaseChart_GridRight, PhaseChart_GridBot);
        pts[3] = new PointF(PhaseChart_GridLeft, PhaseChart_GridBot);
        PhaseChartBG.Graphics.DrawPolygon(GridBlackPEN, pts);

        // Do the Phasor Chart Labels
        PhaseChartBG.Graphics.DrawString("+180", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 26f, PhaseChart_GridCenterY - (6f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+150", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 26f, PhaseChart_GridCenterY - (5f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+120", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 26f, PhaseChart_GridCenterY - (4f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+90", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 24f, PhaseChart_GridCenterY - (3f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+60", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 24f, PhaseChart_GridCenterY - (2f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+30", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 24f, PhaseChart_GridCenterY - (1f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("0", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 14f, PhaseChart_GridCenterY - 6f);
        PhaseChartBG.Graphics.DrawString("-30", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 22f, PhaseChart_GridCenterY + (1f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-60", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 22f, PhaseChart_GridCenterY + (2f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-90", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 22f, PhaseChart_GridCenterY + (3f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-120", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 26f, PhaseChart_GridCenterY + (4f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-150", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 26f, PhaseChart_GridCenterY + (5f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-180", GridLblFNT, Brushes.Black, PhaseChart_GridLeft - 26f, PhaseChart_GridCenterY + (6f / 12f * PhaseChart_GridHeight) - 6f);


        PhaseChartBG.Graphics.DrawString("+180", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY - (6f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+150", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY - (5f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+120", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY - (4f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+90", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY - (3f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+60", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY - (2f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("+30", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY - (1f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("0", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY - 6f);
        PhaseChartBG.Graphics.DrawString("-30", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY + (1f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-60", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY + (2f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-90", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY + (3f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-120", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY + (4f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-150", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY + (5f / 12f * PhaseChart_GridHeight) - 6f);
        PhaseChartBG.Graphics.DrawString("-180", GridLblFNT, Brushes.Black, PhaseChart_GridRight + 8f, PhaseChart_GridCenterY + (6f / 12f * PhaseChart_GridHeight) - 6f);

      }
      catch (Exception ex1)
      {
        Log.Err(ex1, this.Name, "PhaseChart_DrawGrid(1)", Log.LogDevice.LOG);
      }

      Plot_PhaseChart(PhaseChartBG.Graphics, Color.Red);

      // Dispose of Graphics objects
      try
      {
        PhaseChartBG.Render();
        PhaseChartBG.Render(PhaseChartPAN.CreateGraphics());
        PhaseChartBG.Dispose();
      }
      catch (Exception ex)
      {
        Log.Err(ex, this.Name, "Chart1_DrawGrid(2)", Log.LogDevice.LOG);
      }
      GC.Collect();
    }


    private void PhaseChartPAN_Paint(object sender, PaintEventArgs e)
    {
      PhaseChart_DrawGrid();
    }


    private void PhaseChartPAN_Resize(object sender, EventArgs e)
    {
      PhaseChart_Calc();
    }

    private void Plot_PhaseChart(Graphics g, Color cPenColor)
    {
      // Plot the Phase Chart of the DataSet
      No_Purge = true;
      DateTime dtEnd = PHzPacketClass.PreciseDT.UtcNow;
      DateTime dtStart = dtEnd.AddMinutes(-10d);

      // Save the Data Count for what we have for our chart 
      // because the actual count may change with new incoming data 
      // while we are working on the chart.
      int iSaveCount = ComPorts.Port1_Data.Count;

      if (iSaveCount > 1)
      {
        g.DrawString(dtStart.ToString("MMM d, yyyy"), GridLblFNT, Brushes.Black, PhaseChart_GridLeft, PhaseChart_GridTop - 14f);
        g.DrawString(dtEnd.ToString("MMM d, yyyy"), GridLblFNT, Brushes.Black, PhaseChart_GridRight - 60f, PhaseChart_GridTop - 14f);
        g.DrawString(dtStart.ToString("HH:mm:ss.fff"), GridLblFNT, Brushes.Black, PhaseChart_GridLeft, PhaseChart_GridBot + 16f);
        g.DrawString(dtEnd.ToString("HH:mm:ss.fff"), GridLblFNT, Brushes.Black, PhaseChart_GridRight - 54f, PhaseChart_GridBot + 16f);

        g.DrawString(((PHzPacketClass)(ComPorts.Port1_Data[iSaveCount -1])).Sample_DT.ToString("HH:mm:ss.fff"), GridLblFNT, Brushes.Black, 
                     PhaseChart_GridRight - 244f, PhaseChart_GridBot + 16f);
        if (!Sensor.Host_Sync)
        {
          g.DrawString(((PHzPacketClass)(ComPorts.Port1_Data[iSaveCount - 1])).GPS_Time_DT.ToString("HH:mm:ss.fff"), GridLblFNT, Brushes.Black, 
                       PhaseChart_GridRight - 244f, PhaseChart_GridBot + 26f);

          g.DrawString(" Diff= "
                       + ((PHzPacketClass)(ComPorts.Port1_Data[iSaveCount - 1])).GPS_Time_DT.Subtract(((PHzPacketClass)(ComPorts.Port1_Data[iSaveCount - 1])).Sample_DT).TotalSeconds.ToString("####0.000"),
                       GridLblFNT, Brushes.Black, PhaseChart_GridRight - 184f, PhaseChart_GridBot + 26f);
        }
        PointF[] pts = new PointF[iSaveCount];
        float fT = 0f;
        float fX = 0f;
        float fY = 0f;
        Pen pen1 = new Pen(cPenColor, 1.0f);
        try
        {
          for (int ii = 0; ii < iSaveCount; ++ii)
          {
            if (Sensor.Host_Sync)
            {
              fT = (float)((PHzPacketClass)(ComPorts.Port1_Data[ii])).Sample_DT.Subtract(dtStart).TotalSeconds;
            }
            else
            {
              fT = (float)((PHzPacketClass)(ComPorts.Port1_Data[ii])).GPS_Time_DT.Subtract(dtStart).TotalSeconds;
            }

            fX = PhaseChart_GridLeft + ((PhaseChart_GridWidth / 600) * fT);
            fY = PhaseChart_GridCenterY - ((PhaseChart_GridHeight / 360) * ((PHzPacketClass)(ComPorts.Port1_Data[ii])).PhaseAngle);
            pts[ii] = new PointF(fX, fY);
          }

          g.DrawLines(pen1, pts);
        }
        catch (Exception ex)
        {
          Log.Err(ex, "MainWF, Plot_PhaseChart", "Count = " + iSaveCount.ToString() + ", Data = " + ComPorts.Port1_Data.Count.ToString(), Log.LogDevice.LOG);
        }
        pen1.Dispose();
      }
      No_Purge = false;
    }


    #endregion 


    #region  ////  Frequency Spectrum  ////

    private float HzSpect_GridTop = 0f;
    private float HzSpect_GridHeight = 0f;
    private float HzSpect_GridLeft = 0f;
    private float HzSpect_GridWidth = 0f;
    private float HzSpect_GridRight = 0f;
    private float HzSpect_GridBot = 0f;
    private float HzSpect_GridTopHalf = 0f;
    private float HzSpect_GridBotHalf = 0f;
    private float HzSpect_GridCenterX = 0f;
    private float HzSpect_GridCenterY = 0f;

    private float HzSpect_MajorX = 0f;
    private float HzSpect_MinorX = 0f;
    private float HzSpect_TicX = 0f;

    float HzSpect_Scale = 0.5f;

    private void HzSpect_Calc()
    {
      // Do this on Resizing layout
      //   Depends on Phasor_Grid_Calc() done first
      HzSpect_GridHeight = 200f;
      HzSpect_GridTop = 40f;
      HzSpect_GridLeft = 30f;
      HzSpect_GridWidth = HzSpectPAN.Width - 60f;

      HzSpect_GridRight = HzSpect_GridLeft + HzSpect_GridWidth;
      HzSpect_GridBot = HzSpect_GridTop + HzSpect_GridHeight;
      HzSpect_GridCenterY = HzSpect_GridTop + (HzSpect_GridHeight / 2f);
      HzSpect_GridTopHalf = HzSpect_GridTop + (HzSpect_GridHeight / 4f);

      HzSpect_GridCenterX = HzSpect_GridLeft + (HzSpect_GridWidth / 2f);
      HzSpect_GridBotHalf = HzSpect_GridCenterY + (HzSpect_GridHeight / 4f);

      HzSpect_MajorX = HzSpect_GridWidth / 10f;
      HzSpect_MinorX = HzSpect_GridWidth / 40f;
      HzSpect_TicX = HzSpect_MajorX / 10f;
    
    }


    private void HzSpect_DrawGrid()
    {
      BufferedGraphics HzSpectBG = null;
      try
      {
        HzSpectBG = CurrentBGC.Allocate(this.HzSpectPAN.CreateGraphics(), HzSpectPAN.DisplayRectangle);
        HzSpectBG.Graphics.Clear(Color.White);

        // Title
        HzSpectBG.Graphics.DrawString("% Deviation Histogram", TitleFNT, Brushes.Black, (HzSpectPAN.Width / 2f) - 70.0f, 4.0f);

        float fi = 0;
        for (int ii = 1; ii < 12; ii++)
        {
          fi = HzSpect_GridTop + HzSpect_GridHeight * (Convert.ToSingle(ii) / 12f);
          HzSpectBG.Graphics.DrawLine(GridGrayPEN, HzSpect_GridLeft, fi, HzSpect_GridRight, fi);
        }

        float xx = HzSpect_GridLeft;
        for (int ii = 1; ii < 10; ii++)
        {
          xx += HzSpect_MajorX;
          HzSpectBG.Graphics.DrawLine(GridGrayPEN, xx, HzSpect_GridTop, xx, HzSpect_GridBot);
          //HzSpectBG.Graphics.DrawString("-" + Convert.ToString(10 - ii), GridLblFNT, Brushes.Black, xx - 5f, HzSpect_GridBot + 2f);
        }

        xx = HzSpect_GridLeft;
        for (int jj = 1; jj < 100; jj++)
        {
          xx += HzSpect_TicX;
          HzSpectBG.Graphics.DrawLine(GridGrayPEN, xx, HzSpect_GridTop, xx, HzSpect_GridTop + 4f);
          HzSpectBG.Graphics.DrawLine(GridGrayPEN, xx, HzSpect_GridCenterY - 2f, xx, HzSpect_GridCenterY + 2f);
          HzSpectBG.Graphics.DrawLine(GridGrayPEN, xx, HzSpect_GridBot, xx, HzSpect_GridBot - 4f);
        }

        xx = HzSpect_GridLeft;
        for (int jj = 1; jj < 40; jj++)
        {
          xx += HzSpect_MinorX;
          HzSpectBG.Graphics.DrawLine(GridGrayPEN, xx, HzSpect_GridTopHalf - 2f, xx, HzSpect_GridTopHalf + 2f);
          HzSpectBG.Graphics.DrawLine(GridGrayPEN, xx, HzSpect_GridBotHalf - 2f, xx, HzSpect_GridBotHalf + 2f);
        }

        PointF[] pts = new PointF[4];
        pts[0] = new PointF(HzSpect_GridLeft, HzSpect_GridTop);
        pts[1] = new PointF(HzSpect_GridRight, HzSpect_GridTop);
        pts[2] = new PointF(HzSpect_GridRight, HzSpect_GridBot);
        pts[3] = new PointF(HzSpect_GridLeft, HzSpect_GridBot);
        HzSpectBG.Graphics.DrawPolygon(GridBlackPEN, pts);

        // Do the Spect Labels
        HzSpectBG.Graphics.DrawString("0%", TitleFNT, Brushes.Black, HzSpect_GridCenterX - 8f, HzSpect_GridTop - 18f);
        HzSpectBG.Graphics.DrawString(Sensor.Nominal_Hz.ToString() + "Hz", TitleFNT, Brushes.Black, HzSpect_GridCenterX - 16f, HzSpect_GridBot + 4f);
        switch (HzChartScaleDDL.SelectedIndex)
        {
          case 0:  // 1.0 %, +/- 600mHz or +/- 500mHz
            HzSpectBG.Graphics.DrawString("-1.0%", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("+1.0%", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("-" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.01).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridBot + 6f);
            HzSpectBG.Graphics.DrawString("+" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.01).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridBot + 6f);
            break;

          case 1:  // 0.5 %, +/- 300mHz or +/- 250mHz
            HzSpectBG.Graphics.DrawString("-0.50%", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("+0.50%", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("-" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.005).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridBot + 6f);
            HzSpectBG.Graphics.DrawString("+" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.005).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridBot + 6f);
            break;

          case 2:  // 0.2 %, +/- 120mHz or +/- 100mHz
            HzSpectBG.Graphics.DrawString("-0.20%", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("+0.20%", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("-" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.002).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridBot + 6f);
            HzSpectBG.Graphics.DrawString("+" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.002).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridBot + 6f);
            break;

          case 3:  // 0.1 %, +/- 60mHz or +/- 50mHz
            HzSpectBG.Graphics.DrawString("-0.10%", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("+0.10%", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("-" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.001).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridBot + 6f);
            HzSpectBG.Graphics.DrawString("+" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.001).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridBot + 6f);
            break;

          case 4:  // 0.05 %, +/- 30mHz or +/- 25mHz
            HzSpectBG.Graphics.DrawString("-0.05%", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("+0.05%", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("-" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.0005).ToString("0.000") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridBot + 6f);
            HzSpectBG.Graphics.DrawString("+" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.0005).ToString("0.000") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridBot + 6f);
            break;

          case 5:  // 0.02 %, +/- 12mHz or +/- 10mHz
            HzSpectBG.Graphics.DrawString("-0.02%", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("+0.02%", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("-" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.0002).ToString("0.000") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridBot + 6f);
            HzSpectBG.Graphics.DrawString("+" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.0002).ToString("0.000") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridBot + 6f);
            break;

          case 6:  // 0.01 %, +/- 6mHz or +/- 5mHz
            HzSpectBG.Graphics.DrawString("-0.01%", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("+0.01%", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("-" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.001).ToString("0.000") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridBot + 6f);
            HzSpectBG.Graphics.DrawString("+" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.001).ToString("0.000") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridBot + 6f);
            break;

          default:
            HzSpectBG.Graphics.DrawString("-0.10%", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("+0.10%", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridTop - 16f);
            HzSpectBG.Graphics.DrawString("-" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.001).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridLeft - 8f, HzSpect_GridBot + 6f);
            HzSpectBG.Graphics.DrawString("+" + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.001).ToString("0.00") + "Hz", GridLblFNT, Brushes.Black, HzSpect_GridRight - 18f, HzSpect_GridBot + 6f);
            break;
        }

        for (int ii = 0; ii < 5; ii++)
        {
          string sLbl = Convert.ToInt32(ii * HzSpect_GridHeight / HzSpect_Scale).ToString().PadLeft(3, ' ');
          float fY = HzSpect_GridBot - 6f - (ii * HzSpect_GridHeight / 4);
          HzSpectBG.Graphics.DrawString(sLbl, GridLblFNT, Brushes.Black, HzSpect_GridLeft - 24f, fY);
          HzSpectBG.Graphics.DrawString(sLbl, GridLblFNT, Brushes.Black, HzSpect_GridRight + 4f, fY);
        }
      }
      catch (Exception ex1)
      {
        Log.Err(ex1, this.Name, "HzSpect_DrawGrid(1)", Log.LogDevice.LOG);
      }

      HzSpect_Plot(HzSpectBG.Graphics);

      // Dispose of Graphics objects
      try
      {
        HzSpectBG.Render();
        HzSpectBG.Render(HzSpectPAN.CreateGraphics());
        HzSpectBG.Dispose();
      }
      catch (Exception ex)
      {
        Log.Err(ex, this.Name, "HzSpect_DrawGrid(2)", Log.LogDevice.LOG);
      }
      GC.Collect();
    }


    private void HzSpectPAN_Paint(object sender, PaintEventArgs e)
    {
      HzSpect_DrawGrid();
    }

    private void HzSpectPAN_Resize(object sender, EventArgs e)
    {
      HzSpect_Calc();
      HzSpectPAN.Invalidate();
    }


    private void HzSpect_Plot(Graphics g)
    {
      No_Purge = true;
      int iSaveCount = ComPorts.Port1_Data.Count;
      if (iSaveCount > 0)
      {
        Pen pen1 = new Pen(Color.Red, 1.0f);

        // Create an array of integer buckets to a accumulate frequency deviation samples
        int[] iBucket = new int[(int)Math.Ceiling(HzSpect_GridWidth - 2)];

        // Compute a conversion factor to convert frequency deviation to the bucket index values
        // For example:  +/-0.10%
        // fBucketWidth % = 0.20% / (float)HzSpect_GridWidth
        // if HzSpect_GridWidth = 200px then fBucketWidth = 0.20% / 200px = 0.001%/px
        // iBucketIndex = (1px/0.001% * FreqDev%) + (HzSpect_GridWidth / 2)

        // For Example:  +/- 0.06Hz
        // fBucketWidth Hz = 0.12Hz / (float)HzSpect_GridWidth = 0.12Hz / 200px = 0.0006 Hz/px
        // iBucketIndex = (1px/0.0006Hz) * (FreqHz - 60Hz) + MidPointpx

        int iMidPoint = (int)(HzSpect_GridWidth / 2);
        int iBucketIndex = 0;
        for (int ii = 0; ii < iSaveCount; ii++)
        {
          // Increment the frequency's bucket
          if (ii < ComPorts.Port1_Data.Count)
          {
            /// 1.0.9.12 Add HzChart_Adjust to Frequency
            iBucketIndex = (int)((((PHzPacketClass)(ComPorts.Port1_Data[ii])).Frequency - 60f) / 0.0006f) + iMidPoint;
            if ((iBucketIndex > -1) && (iBucketIndex < iBucket.Length))
            {
              iBucket[iBucketIndex]++;
            }
          }
        }

        int xx = 0;
        for (iBucketIndex = 0; iBucketIndex < iBucket.Length; iBucketIndex++)
        {
          xx = iBucketIndex + (int)HzSpect_GridLeft + 1;
          if (iBucket[iBucketIndex] * HzSpect_Scale > HzSpect_GridHeight)
          {
            iBucket[iBucketIndex] = (int)(HzSpect_GridHeight / HzSpect_Scale);
          }
          g.DrawLine(pen1, xx, (int)HzSpect_GridBot, xx, (int)(HzSpect_GridBot) - (int)((float)iBucket[iBucketIndex] * HzSpect_Scale));  // subtract bucket because our drawing system is inverted
        }

        pen1.Dispose();
      }
      No_Purge = false;
    }

    #endregion



    private void DisplayTIMER_Tick(object sender, EventArgs e)
    {
      if (this.WindowState == FormWindowState.Minimized)
      {
        return;
      }
      string sErr = "";
      try
      {
        sErr += "Phasor_Calc,";
        Phasor_Calc();
        sErr += "Phasor_DrawGrid,";
        Phasor_DrawGrid();
        sErr += "PhaseChart_Calc,";
        PhaseChart_Calc();
        sErr += "PhaseChart_DrawGrid,";
        PhaseChart_DrawGrid();
        sErr += "HzChart_Calc,";
        HzChart_Calc();
        sErr += "HzChart_DrawGrid,";
        HzChart_DrawGrid();
        sErr += "HzSpect_Calc,";
        HzSpect_Calc();
        sErr += "HzSpect_DrawGrid";
        HzSpect_DrawGrid();
      }
      catch (Exception ex)
      {
        Log.Err(ex, "Main, Display Timer", "Error: " + sErr, Log.LogDevice.LOG);
      }
    }


    private void NavNextTBTN_Click(object sender, EventArgs e)
    {
      /// Set the Display Page Active and Execute the "Next" Navigation function
      this.MainTABS.SelectedTab = this.DisplayTAB;
      if (this.LiveHistDDL.Text.ToUpper().IndexOf("LIVE") == 0)
      {
        // Live View:  Next = Add current data on a timer event
        Sensor_RunPHzMode();
        this.DisplayTIMER.Start();
      }
      else
      {
        // History view:  Next = Get the Next Minute's Data
        this.DisplayTIMER.Stop();
      }
    }


    private void NavStopTBTN_Click(object sender, EventArgs e)
    {
      /// Set the Display Page Active and Stop the Display
      this.MainTABS.SelectedTab = this.DisplayTAB;
      this.DisplayTIMER.Stop();
    }


    private void LiveHistDDL_Enter(object sender, EventArgs e)
    {
      /// Set the Display Page Active
      this.MainTABS.SelectedTab = this.DisplayTAB;
    }

    private void ScaleLBL_Click(object sender, EventArgs e)
    {
      /// Set the Display Page Active
      this.MainTABS.SelectedTab = this.DisplayTAB;
    }

    private void HzScaleDDL_Enter(object sender, EventArgs e)
    {
      /// Set the Display Page Active
      this.MainTABS.SelectedTab = this.DisplayTAB;
    }



    #region    ////  Configuration Routines  ////

    // TreeView Node Page Controls.  Each displays a user interfaces for setting a group of options.
    private Config_Registration Cfg00 = new Config_Registration();
    private Cfg_RegContact Cfg01 = new Cfg_RegContact();

    private Cfg_Sensor Cfg10 = new Cfg_Sensor();
    private Cfg_SensorIdLoc Cfg11 = new Cfg_SensorIdLoc();
    private Cfg_SensorTime Cfg12 = new Cfg_SensorTime();
    private Cfg_SensorCom Cfg13 = new Cfg_SensorCom();
    private Cfg_SensorStart Cfg14 = new Cfg_SensorStart();
    private Cfg_SensorCalib Cfg15 = new Cfg_SensorCalib();

    private Cfg_Terminal Cfg20 = new Cfg_Terminal();

    private void Config_Save()
    { /// 1.0.9.8
      /// Save Everything
      Cfg.TermLog_Enable = this.TermLogEnableCHK.Checked;
      Cfg.DataLog_Enable = this.DataLogEnableCHK.Checked;
      Cfg.DataLog_FileInterval = Convert.ToInt32(this.DataLogFileIntervalUD.Value);
      Cfg.DataLog_FileNameFormat = this.DataLogFileNameFormatTXT.Text.Trim();
      Cfg.DataLog_Folder = this.DataLogFolderTXT.Text;
      Cfg.Display_LiveHist = this.LiveHistDDL.SelectedIndex;
      Cfg.HzChart_Scale = this.HzChartScaleDDL.SelectedIndex;
      // Cfg.HzSpect_Scale = this.HzSpect_Scale;

      Cfg01.Config_Save();
      Cfg11.Config_Save();
      Cfg12.Config_Save();
      Cfg13.Config_Save();
      Cfg14.Config_Save();
      Cfg15.Config_Save();
      Cfg20.Config_Save();
      this.TermTIMER.Interval = Cfg.Term_TimerInterval;
      this.DisplayTIMER.Interval = Cfg.Display_TimerInterval;
      WrStatus(Cfg.Save());
    }


    private void Config_Load()
    { /// 1.0.9.12
      /// Load Everything
      string sMsg = Cfg.Load();
      this.StatusTIMER.Interval = Cfg.Status_TimerInterval;
      this.TermTIMER.Interval = Cfg.Term_TimerInterval;
      this.DisplayTIMER.Interval = Cfg.Display_TimerInterval;
      this.TermLogEnableCHK.Checked = Cfg.TermLog_Enable;
      this.DataLogEnableCHK.Checked = Cfg.DataLog_Enable;
      this.DataLogFileIntervalUD.Value = Cfg.DataLog_FileInterval;
      this.DataLogFileNameFormatTXT.Text = Cfg.DataLog_FileNameFormat.Trim();
      this.DataLogFolderTXT.Text = Cfg.DataLog_Folder;
      this.LiveHistDDL.SelectedIndex = Cfg.Display_LiveHist;
      
      this.HzSpect_Scale = Cfg.HzSpect_Scale;
      Cfg01.Config_Load();
      Cfg11.Config_Load();
      Cfg12.Config_Load();
      Cfg13.Config_Load();
      Cfg14.Config_Load();
      Cfg15.Config_Load();
      Cfg20.Config_Load();
      SensorList.Load_ListDS();

      this.HzChartScaleDDL.Items.Clear();
      this.HzChartScaleDDL.Items.Add("1.0 %, +/- " + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.01 * 1000).ToString("###") + "mHz");       // 600mHz, 500mHz
      this.HzChartScaleDDL.Items.Add("0.5 %, +/- " + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.005 * 1000).ToString("###") + "mHz");      // 300mHz, 250mHz
      this.HzChartScaleDDL.Items.Add("0.2 %, +/- " + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.002 * 1000).ToString("###") + "mHz");      // 120mHz, 100mHz
      this.HzChartScaleDDL.Items.Add("0.1 %, +/- " + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.001 * 1000).ToString("###") + "mHz");      // 60mHz, 50mHz
      this.HzChartScaleDDL.Items.Add("0.05 %, +/- " + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.0005 * 1000).ToString("###") + "mHz");    // 30mHz, 25mHz
      this.HzChartScaleDDL.Items.Add("0.02 %, +/- " + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.0002 * 1000).ToString("###") + "mHz");    // 12mHz, 10mHz
      this.HzChartScaleDDL.Items.Add("0.01 %, +/- " + (Convert.ToSingle(Sensor.Nominal_Hz) * 0.0001 * 1000).ToString("###") + "mHz");    // 6mHz, 5mHz
      this.HzChartScaleDDL.SelectedIndex = Cfg.HzChart_Scale;

      WrStatus(sMsg);
    }


    private void ConfigTV_AfterSelect(object sender, TreeViewEventArgs e)
    {
      this.ConfigPAN.Controls.Clear();
      if (e.Node.Name.Equals("Node00"))
      {
        // Registration Summary
        this.ConfigPAN.Controls.Add(Cfg00);
        Cfg00.Parent = ConfigPAN;
        Cfg00.Dock = DockStyle.Fill;        
      }
      else if (e.Node.Name.Equals("Node01"))
      {
        // Registration \ Contact
        this.ConfigPAN.Controls.Add(Cfg01);
        Cfg01.Parent = ConfigPAN;
        Cfg01.Dock = DockStyle.Fill;
      }
      else if (e.Node.Name.Equals("Node10"))
      {
        // Sensor Summary
        this.ConfigPAN.Controls.Add(Cfg10);
        Cfg10.Parent = ConfigPAN;
        Cfg10.Dock = DockStyle.Fill;
      }
      else if (e.Node.Name.Equals("Node11"))
      { 
        // Sensor \ Id and Location
        this.ConfigPAN.Controls.Add(Cfg11);
        Cfg11.Parent = ConfigPAN;
        Cfg11.Dock = DockStyle.Fill;
      }
      else if (e.Node.Name.Equals("Node12"))
      {
        // Sensor \ Time Source
        this.ConfigPAN.Controls.Add(Cfg12);
        Cfg12.Parent = ConfigPAN;
        Cfg12.Dock = DockStyle.Fill;
      }
      else if (e.Node.Name.Equals("Node13"))
      {
        // Sensor \ Communications
        this.ConfigPAN.Controls.Add(Cfg13);
        Cfg13.Parent = ConfigPAN;
        Cfg13.Dock = DockStyle.Fill;
      }
      else if (e.Node.Name.Equals("Node14"))
      {
        // Sensor \ Startup
        this.ConfigPAN.Controls.Add(Cfg14);
        Cfg14.Parent = ConfigPAN;
        Cfg14.Dock = DockStyle.Fill;
      }
      else if (e.Node.Name.Equals("Node15"))
      {
        // Sensor \ Startup
        this.ConfigPAN.Controls.Add(Cfg15);
        Cfg15.Parent = ConfigPAN;
        Cfg15.Dock = DockStyle.Fill;
      }
      else if (e.Node.Name.Equals("Node20"))
      {
        // Terminal Settings
        this.ConfigPAN.Controls.Add(Cfg20);
        Cfg20.Parent = ConfigPAN;
        Cfg20.Dock = DockStyle.Fill;
      }
    }

    #endregion


    private void DataLogFolderBrowseBTN_Click(object sender, EventArgs e)
    {
      this.DataLogFolderTXT.Text = this.DataLogFolderTXT.Text.Trim();
      FolderBrowserDialog oDlg = new FolderBrowserDialog();
      oDlg.RootFolder = Environment.SpecialFolder.Desktop;
      oDlg.SelectedPath = this.DataLogFolderTXT.Text;
      oDlg.Description = "Select a Storage Folder for Data Logs";
      if (oDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        Cfg.DataLog_Folder = oDlg.SelectedPath;
        this.DataLogFolderTXT.Text = Cfg.DataLog_Folder;
      }
      oDlg.Dispose();
      GC.Collect();
    }


    private void DataLogFolderTXT_Leave(object sender, EventArgs e)
    {
      this.DataLogFolderTXT.Text = this.DataLogFolderTXT.Text.Trim();
      if (!Directory.Exists(this.DataLogFolderTXT.Text))
      {
        if (MessageBox.Show("The Data Log Folder:" + Cfg.CRLF + this.DataLogFolderTXT.Text + Cfg.CRLF + "does not exist." + Cfg.CRLF
                            + "Do you want to create this folder?", "Create Data Log Folder?",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
        {
          try
          {
            Directory.CreateDirectory(this.DataLogFolderTXT.Text);
          }
          catch (Exception ex)
          {
            Log.Err(ex, "Main, Data Log Folder Dialog", "Error Creating Data Log Folder: " + this.DataLogFolderTXT.Text, Log.LogDevice.LOG_DLG);
          }
        }
      }
    }


    public bool No_Purge = false;
    private void DataPurgeTIMER_Tick(object sender, EventArgs e)
    {
      /// Periodic Purge of Data from the Live DataSet
      if (No_Purge)
      {
        return;
      }
      try
      {
        for (int ii = ComPorts.Port1_Data.Count - 1; ii > -1; ii--)
          {
            if (Sensor.Host_Sync)
            {
              if ((ComPorts.Port1_Data.Count > 0) && (ii < ComPorts.Port1_Data.Count)
                  && ((PHzPacketClass)ComPorts.Port1_Data[ii]).Sample_DT < PHzPacketClass.PreciseDT.UtcNow.AddMinutes(-10d))
              {
                if (ComPorts.Port1_Data[ii] != null)
                {
                  ComPorts.Port1_Data.RemoveAt(ii);
                }
              }
            }
            else
            {
              if ((ComPorts.Port1_Data.Count > 0) && (ii < ComPorts.Port1_Data.Count)
                  && ((PHzPacketClass)ComPorts.Port1_Data[ii]).GPS_Time_DT < PHzPacketClass.PreciseDT.UtcNow.AddMinutes(-10d))
              {
                if (ComPorts.Port1_Data[ii] != null)
                {
                  ComPorts.Port1_Data.RemoveAt(ii);
                }
              }
            }

          }
        ComPorts.Port1_Data.TrimToSize();
      }
      catch (Exception ex)
      {
        Log.Err(ex, "Main, Data Purge Timer", "Error Purging Data: " + ComPorts.Port1_Data.Count.ToString(), Log.LogDevice.LOG);
      }
    }


    private bool m_Is_Recording = false;
    private UInt64 m_Recorded = 0;

    private System.IO.StreamWriter m_RecordSW;
    private string m_DataFilePath = "";

    private void Record_Data(string sData)
    {
      if (this.DataLogFileNameFormatTXT.Text.Trim().Length.Equals(0))
      {
        this.DataLogFileNameFormatTXT.Text = "yyyyMMdd_HHmm";
      }
      DateTime dt0 = PHzPacketClass.PreciseDT.UtcNow;
      DateTime date0 = dt0.Date;
      double min0 = dt0.TimeOfDay.TotalMinutes;
      double mindiv = Math.Floor(min0 / (double)this.DataLogFileIntervalUD.Value);
      date0 = date0.AddMinutes(mindiv * (double)this.DataLogFileIntervalUD.Value);


      string sTest = this.DataLogFolderTXT.Text + @"\" + date0.ToString(this.DataLogFileNameFormatTXT.Text.Trim()) + ".csv";
      try
      {
        // Check for DateTime File Stamp change or first time
        if (m_DataFilePath != sTest)
        {
          // New DateTime File Stamp, Create a New File
          m_DataFilePath = sTest;
          m_RecordSW = System.IO.File.AppendText(m_DataFilePath);
          m_RecordSW.WriteLine("\"DATE\",\"UTC_PPS\",\"FRACSEC\",\"ELAPSED\",\"INTERVAL\",\"SAMPLE_ID\",\"FREQUENCY\",\"PHASE_ANGLE\",\"AMPLITUDE\"");
        }
        else
        {
          m_RecordSW = System.IO.File.AppendText(m_DataFilePath);
        }
        m_Recorded++;
        m_RecordSW.WriteLine(sData);
        try
        {
          m_RecordSW.Flush();
          m_RecordSW.Close();
        }
        catch (Exception ex2)
        {
          Log.Err(ex2, this.Name, "Flush Data Log: " + m_DataFilePath, Log.LogDevice.LOG);
          WrTerm("Flush Data Log [" + m_DataFilePath + "] - " + ex2.Message);
        }
      }
      catch (Exception ex)
      {
        m_Is_Recording = false;
        Log.Err(ex, this.Name, "Record to File: " + m_DataFilePath, Log.LogDevice.LOG);
        string sMsg = m_Recorded.ToString() + "  Samples Recorded to " + m_DataFilePath;
        WrTerm(sMsg + " - " + ex.Message);
      }
    }

    private void DataLogEnableCHK_CheckedChanged(object sender, EventArgs e)
    { /// 1.0.9.8
      m_Is_Recording = DataLogEnableCHK.Checked;
      if (m_Is_Recording)
      {
        WrTerm("Recording Data...");
      }
      if ((!m_Is_Recording) && (m_RecordSW != null))
      {
        // Close Previous File
        try
        {
          m_RecordSW.Flush();
          m_RecordSW.Close();
        }
        catch (Exception ex)
        {
          // Exception that is raised if SW is already closed is safe to ignore
        }
        try
        {
          m_RecordSW.Dispose();
          m_RecordSW = null;
        }
        catch (Exception ex2)
        {
          Log.Err(ex2, "MainWF", "DataLogEnable_CheckedChanged", Log.LogDevice.LOG);
        }
        WrTerm("Records Recorded = " + m_Recorded.ToString());
      }
      m_Recorded = 0;
    }


    private void splitter3_SplitterMoved(object sender, SplitterEventArgs e)
    {
      this.PhaseChartPAN.Width = this.HzChartPAN.Width;
    }

    private void splitter5_SplitterMoved(object sender, SplitterEventArgs e)
    {
      this.HzChartPAN.Width = this.PhaseChartPAN.Width;
    }

    private void splitter2_SplitterMoved(object sender, SplitterEventArgs e)
    {
      this.PhasorPAN.Width = this.HzSpectPAN.Width;
    }

    private void splitter6_SplitterMoved(object sender, SplitterEventArgs e)
    {
      this.HzSpectPAN.Width = this.PhasorPAN.Width;
    }


    private void AboutMNU_Click(object sender, EventArgs e)
    {
      About oDlg = new About();
      oDlg.ShowDialog();
      oDlg.Dispose();
    }


    private void SensorListTBTN_Click(object sender, EventArgs e)
    {
      SensorListWF oDlg = new SensorListWF();
      try
      {
        oDlg.ShowDialog();
      }
      catch (Exception ex)
      {
        Log.Err(ex, this.Name, "Sensor List Error", Log.LogDevice.LOG_DLG);
      }
      oDlg.Dispose();
      GC.Collect();
      HzLegendPAN.Invalidate();
    }


    private void HzLegendPAN_Paint(object sender, PaintEventArgs e)
    {
      // Redraw the Frequency Lengend panel
      Hz_DrawLegend();
    }

    private void HzLegendPAN_Resize(object sender, EventArgs e)
    {
      Hz_DrawLegend();
    }


    private Brush[] m_Brushes = { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.DarkViolet };

    private void Hz_DrawLegend()
    {
      BufferedGraphics drawBG = null;
      try
      {
        drawBG = CurrentBGC.Allocate(this.HzLegendPAN.CreateGraphics(), HzLegendPAN.DisplayRectangle);
        drawBG.Graphics.Clear(Color.White);

        // Title
        drawBG.Graphics.DrawString("Sensors", TitleFNT, Brushes.Black, 4.0f, 4.0f);

        // Sensors List
        if ((SensorList.SensorDS != null) && (SensorList.SensorDS.Tables[0].Rows.Count > 0))
        {
          int bb = 0;
          float yy = 16.0f;
          for (int ii = 0; ii < SensorList.SensorDS.Tables[0].Rows.Count; ii++)
          {
            if ((int)SensorList.SensorDS.Tables[0].Rows[ii][Cfg.C_IS_PICK] == 1)
            {
              yy += 16.0f;
              drawBG.Graphics.DrawString(SensorList.SensorDS.Tables[0].Rows[ii][Sensor.C_SENSOR_ID].ToString(),
                                         LegendFNT, m_Brushes[bb], 4.0f, yy);
              drawBG.Graphics.DrawString(SensorList.SensorDS.Tables[0].Rows[ii][Sensor.C_LOCATION_NAME].ToString(),
                                         LegendFNT, m_Brushes[bb], 44.0f, yy);
              bb++;
              if (bb == m_Brushes.Length)
              {
                bb = 0;
              }
            }
          }
        }

      }
      catch (Exception ex1)
      {
        Log.Err(ex1, this.Name, "Hz_DrawLegend(1)", Log.LogDevice.LOG);
      }

      // Dispose of Graphics objects
      try
      {
        drawBG.Render();
        drawBG.Render(HzLegendPAN.CreateGraphics());
        drawBG.Dispose();
      }
      catch (Exception ex)
      {
        Log.Err(ex, this.Name, "Hz_DrawLegend(2)", Log.LogDevice.LOG);
      }
      GC.Collect();
    }


    private void PhaseLegendPAN_Paint(object sender, PaintEventArgs e)
    {
      // Redraw the Phase Angle Lengend panel
    }




  }
}
