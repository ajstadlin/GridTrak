using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GTosPMU
{
  /// <summary>
  /// Sensor configuration Display
  /// 02/12/13 1.0.9.10  Scroll bar added
  /// 03/23/12 1.0.9.7  GPS Configuration Updated
  /// </summary>
  public partial class Cfg_Sensor : UserControl
  {
    public Cfg_Sensor()
    {
      InitializeComponent();
    }


    public void Config_Load()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("Sensor ID# " + Sensor.Sensor_ID.ToString() + Cfg.CRLF);
      sb.Append("Sensor Name:  " + Sensor.Sensor_Name + Cfg.CRLF);
      sb.Append("Location Name:  " + Sensor.Location_Name + Cfg.CRLF);

      sb.Append("** Frequency and Time Source **" + Cfg.CRLF);
      sb.Append("Nominal Frequency = " + Sensor.Nominal_Hz.ToString("##") + " Hz" + Cfg.CRLF);
      sb.Append("Interval = " + Sensor.Interval + " Nominal Frequency Cycles per Sample" + Cfg.CRLF);
      sb.Append("Time Synchronization Interval = ");
      if (Sensor.Sync_Interval == 0)
      {
        sb.Append("Disabled" + Cfg.CRLF);
      }
      else
      {
        sb.Append(Sensor.Sync_Interval.ToString() + " seconds" + Cfg.CRLF);
      }
      if ((Sensor.Use_GPS & Sensor.USE_GPS_PPS) == Sensor.USE_GPS_PPS)
      {
        sb.Append("GPS PPS Enabled" + Cfg.CRLF);
      }
      else
      {
        sb.Append("GPS PPS Disabled" + Cfg.CRLF);
      }
      if ((Sensor.Use_GPS & Sensor.USE_GPS_TIME) == Sensor.USE_GPS_TIME)
      {
        sb.Append("GPS Time Enabled" + Cfg.CRLF);
      }
      else
      {
        sb.Append("GPS Time Disabled" + Cfg.CRLF);
      }
      sb.Append("Host Synchronized = " + Sensor.Host_Sync.ToString());
      
      sb.Append(Cfg.CRLF);
      sb.Append("** Communications **" + Cfg.CRLF);
      sb.Append("Data Serial Port = " + ComPorts.Port1_Name + Cfg.CRLF);
      sb.Append("      Parameters = " + ComPorts.Port1_Baud.ToString() + " bps, " + ComPorts.Port1_DataBits.ToString() + " data bits, " + ComPorts.Port1_StopBits.ToString() + " stop bit, Parity " + ComPorts.Port1_Parity.ToString() + Cfg.CRLF);
      sb.Append("       Handshake = " + ComPorts.Port1_Handshake.ToString() + Cfg.CRLF);
      sb.Append(Cfg.CRLF);
      sb.Append("        Read Buffer Size = " + ComPorts.Port1_ReadBufferSize.ToString() + Cfg.CRLF);
      sb.Append("            Read Timeout = " + ComPorts.Port1_ReadTimeout.ToString() + " ms" + Cfg.CRLF);
      sb.Append("Received Bytes Threshold = " + ComPorts.Port1_ReceivedBytesThreshold + Cfg.CRLF);
      sb.Append("       Write Buffer Size = " + ComPorts.Port1_WriteBufferSize.ToString() + Cfg.CRLF);
      sb.Append("           Write Timeout = " + ComPorts.Port1_WriteTimeout.ToString() + Cfg.CRLF);
      
      sb.Append("==========" + Cfg.CRLF);

      sb.Append("** Sensor Configuration - Stored in the Sensor **" + Cfg.CRLF);
      sb.Append("Firmware Version:  " + Sensor.Sensor_Ver.ToString("X4") + Cfg.CRLF);
      sb.Append("Sensor ID:  " + Sensor.Sensor_ID.ToString() + Cfg.CRLF);
      sb.Append("Sensor PIN:  " + Sensor.Sensor_PIN.ToString() + Cfg.CRLF);
      sb.Append("Program Mode:  " + Sensor.Prog_Mode.ToString("X") + Cfg.CRLF);
      sb.Append("Nominal Hz:  " + Sensor.Nominal_Hz.ToString() + Cfg.CRLF);
      sb.Append("Diagnostic Mode:  " + Sensor.Diag_Mode.ToString("X4") + Cfg.CRLF);
      sb.Append("Hz Calibration Offset:  " + Sensor.HzCalOffset.ToString() + Cfg.CRLF);
      sb.Append("T3 Calibration Start:  " + Sensor.T3CalStart.ToString() + Cfg.CRLF);
      sb.Append("ZX Calibration Offset:  " + Sensor.ZXCalOffset.ToString() + Cfg.CRLF);
      sb.Append("T3 Calibration Offset: " + Sensor.T3CalOffset.ToString() + Cfg.CRLF);
      sb.Append("T3 Range Midpoint:  " + Sensor.T3Offset.ToString() + Cfg.CRLF);
      sb.Append("T3 Timer Period:  " + Sensor.T3Period.ToString() + Cfg.CRLF);
      sb.Append("T3 Mod 0 Max:  " + Sensor.T3MzMax.ToString() + Cfg.CRLF);
      sb.Append("Text EOL:  " + Sensor.Eol.ToString("X4") + Cfg.CRLF);
      sb.Append("UART1 Baud Rate Generator:  " + Sensor.Uart1_Brg.ToString("X4") + Cfg.CRLF);
      sb.Append("UART2 Baud Rate Generator:  " + Sensor.Uart2_Brg.ToString("X4") + Cfg.CRLF);
      sb.Append("Config Block 0 CRC = " + Sensor.Crc0.ToString("X4") + Cfg.CRLF);
      sb.Append("=" + Cfg.CRLF);

      //// SynchroPhasor Configuration structure
      sb.Append("Transmit Mode:  " + Sensor.TransMode.ToString("x") + Cfg.CRLF);
      sb.Append("Data Mode:  " + Sensor.DataMode.ToString("x") + Cfg.CRLF);
      sb.Append("Measurement Intervals per Second:  " + Sensor.Interval.ToString() + Cfg.CRLF);
      sb.Append("T1 Calibration Start:  " + Sensor.T1CalStart.ToString() + Cfg.CRLF);
      sb.Append("T1 Period Calibration:  " + Sensor.T1CalPrd.ToString() + Cfg.CRLF);
      sb.Append("T1 Period:  " + Sensor.T1Period.ToString() + Cfg.CRLF);
      sb.Append("T1 Prescale:  " + Sensor.T1Prescale.ToString() + Cfg.CRLF);
      sb.Append("Voltage Reference (mV):  " + Sensor.Vref.ToString() + Cfg.CRLF);
      sb.Append("Use GPS:  " + Sensor.Use_GPS.ToString("X") + Cfg.CRLF);
      sb.Append("Data:  ");
      for (int ii = 0; ii < 8; ii++)
      {
        sb.Append(Sensor.DetectA[ii].ToString("X4") + " ");
      }
      sb.Append(Cfg.CRLF);
      sb.Append("Config Block 1 CRC = " + Sensor.Crc1.ToString("X4") + Cfg.CRLF);
      sb.Append("=" + Cfg.CRLF);

      sb.Append("Time Base:  " + Sensor.Time_Base.ToString() + Cfg.CRLF);
      sb.Append("Data:  ");
      for (int ii = 0; ii < 16; ii++)
      {
        sb.Append(Sensor.PMU_Station[ii].ToString("X"));
      }
      sb.Append(Cfg.CRLF);
      sb.Append("GPS Interval = " + Sensor.GPS_Interval.ToString() + Cfg.CRLF);
      sb.Append("Data:  ");
      for (int ii = 0; ii < 10; ii++)
      {
        sb.Append(Sensor.PMU_Stuff[ii].ToString("X"));
      }
      sb.Append(Cfg.CRLF);
      sb.Append("Config Block 2 CRC = " + Sensor.Crc2.ToString("X4") + Cfg.CRLF);
      sb.Append("=" + Cfg.CRLF);

      this.SummaryTXT.Text = sb.ToString();
    }



    private void Config_Sensor_ParentChanged(object sender, EventArgs e)
    {
      if (this.Parent != null)
      {
        // Load our Summary
        Config_Load();
      }
    }


  }
}
