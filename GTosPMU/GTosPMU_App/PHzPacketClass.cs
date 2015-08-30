using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GTosPMU
{
  /// <summary>
  /// PHzPacket Class
  /// 04/17/13 1.0.9.11  Late PPS Exception and Time value configuration paramenters added.  Late PPS exception disabled by default
  /// 11/11/11 1.0.0.9  GPS Integration Implementation
  /// </summary>
  class PHzPacketClass
  {
    public static DateTimePrecise PreciseDT = new DateTimePrecise(10);

    // IEEE C37.118-2005 Time is "Unix Time" measured by the number of seconds after 1/1/1970
    // -- So, why does IEEE C37.118-2005 call it "SOC" - Second of Century
    private static DateTime UnixTimeOffsetDT = new DateTime(1970, 1, 1, 0, 0, 0);

    public static DateTime Last_PPS = PreciseDT.UtcNow;
    public static DateTime Last_Sample = PreciseDT.UtcNow;

    public DateTime Sample_PPS;                     // Sample PPS DateTime; Copied from Last_PPS
    public DateTime Sample_DT;                      // Sample Time Stamp

    public DateTime GPS_Time_PPS;                   // GPS PPS DateTime received from the PMU's GPS
    public DateTime GPS_Time_DT;                    // Sample's GPS Time Stamp after the GPS Time PPS

    public TimeSpan Elapsed;                        // Time elapsed since last sample
    public TimeSpan PPS_Offset;                     // In Seconds
    
    public static Int64 Interval_Idx = 0;
    public Int64 Interval_Id = 0;
    public static Int64 Last_Sample_ID = 0;

    public const int FIELD_COUNT = 8;

    public byte Is_PPS = (byte)0;
    public Int64 Sample_ID = 0;
    public float Frequency = 60.00000f;
    public float Amplitude = 10.000f;
    public float PhaseAngle = 0.000f;
    public UInt32 PmuSocTime = 0;
    public UInt32 PmuFracSec = 0;
    public UInt16 CRC = 0x0000;
    public int Status = 0;

    // Typical Data Stream
    // 0) Start of Packet = "PHz"
    // 1) PPS Indicator:  "*" = PPS, "-" = Interval after PPS, "+" = Roll-over Without PPS (or missed PPS)
    // 2) Sample ID from Sensor
    // 3) Frequency * 100,000
    // 4) Amplitude * 1000
    // 5) Phase Angle * 1000
    // 7) PMU SOC (Second of Century) Time
    // 6) PMU FRACSEC (microseconds)
    // 7) CRC
    // For Example:  10 Samples per Second
    //GTosPMU!, Sensor ID# 0, SN# 0, FW Rev 9, dsPIC30F3013, 60 Hz
    //PHz*,284,6001620,9473,121696,1321064038,0,CC22
    //PHz-,285,6000179,9477,121696,1321064038,100000,1884
    //PHz-,286,5999954,9467,121748,1321064038,200000,BD79
    //PHz-,287,6000284,9437,121848,1321064038,300000,B574
    //PHz-,288,6000480,9457,121982,1321064038,400000,C5FF
    //PHz-,289,6000855,9473,122068,1321064038,500000,BFBC
    //PHz-,290,6000705,9474,122118,1321064038,600000,E50D
    //PHz-,291,6000209,9480,122150,1321064038,700000,A487
    //PHz-,292,6000044,9489,122185,1321064038,800000,D4E5
    //PHz-,293,6000359,9478,122251,1321064038,900000,BE2B
    //PHz*,294,6000705,9469,121039,1321064039,0,EF92
    
    //PHz*,121,6000239,6849,-155101,1352672840,0,8AB9
    //PHz-,122,5999220,6823,-155200,1352672840,500000,9354
    //PHz*,123,6000585,6804,-154755,1352672841,0,C5D7
    //PHz-,124,6000750,6814,-154428,1352672841,500000,BAA3
    //PHz*,125,6000359,6835,-153791,1352672842,0,BEA
    //PHz-,126,6000525,6823,-153353,1352672842,500000,6828

    public static string[] PktFields;

    public PHzPacketClass(string sPkt)
    {
      /// Constructor
      //  Check for PPS Indicator
      Sample_DT = PreciseDT.UtcNow;

      Is_PPS = (byte)sPkt[3];
      if ((Is_PPS == (byte)'+') || (Is_PPS == (byte)'*'))
      {
        // PPS on Sensor Input PIN (i.e. GPS PPS) or internal timer
        if (Sensor.Host_Sync)
        {
          Last_PPS = Sample_DT;
        }
        Interval_Idx = 0;
      }
      
      // Assign the Last PPS to this sample
      if (Sensor.Host_Sync)
      {
        Sample_PPS = Last_PPS;
        PPS_Offset = Sample_DT.Subtract(Last_PPS);
      }
      else
      {
        PPS_Offset = GPS_Time_DT.Subtract(Last_PPS);
      }

      // 
      if (Sensor.Late_PPS_Exception_Enable && (PPS_Offset.TotalSeconds > Sensor.Late_PPS_Exception_Seconds))
      {
        // We missed a PPS!
        Status = -1;
        return;
      }

      // Parse the string packet
      PktFields = sPkt.Split(",".ToCharArray());
      if (PktFields.Length == FIELD_COUNT)
      {
        // Check the CRC
        // Trim the CRC off the end of the input string
        string sInData = sPkt.Substring(0, sPkt.LastIndexOf(",") + 1);
        if (Convert.ToUInt16(PktFields[7], 16) == Crc.Calc_CCITT(sInData, (UInt16)sInData.Length))
        {
          Sample_ID = Convert.ToUInt32(PktFields[1]);
          if (Is_PPS == (byte)'-')
          {
            Interval_Idx += Sample_ID - Last_Sample_ID;
            Interval_Id = Interval_Idx;
          }
          Last_Sample_ID = Sample_ID;
          Frequency = Convert.ToSingle(PktFields[2]) / 100000.0f;
          Amplitude = Convert.ToSingle(PktFields[3]) / 1000.0f;
          PhaseAngle = Convert.ToSingle(PktFields[4]) / 1000.0f;
          PmuSocTime = Convert.ToUInt32(PktFields[5]);
          PmuFracSec = Convert.ToUInt32(PktFields[6]);

          GPS_Time_PPS = UnixTimeOffsetDT.AddSeconds(PmuSocTime);
          if (!Sensor.Host_Sync)
          {
            Last_PPS = GPS_Time_PPS;
          }
          GPS_Time_DT = GPS_Time_PPS.AddMilliseconds((double)PmuFracSec / 1000d);  // To Do:  use Ticks for higher resolution

          if (Sensor.Host_Sync)
          {
            Elapsed = Sample_DT.Subtract(Last_Sample);
            Last_Sample = Sample_DT;
          }
          else
          {
            Elapsed = GPS_Time_DT.Subtract(Last_Sample);
            Last_Sample = GPS_Time_DT;
          }        
        }
        else
        {
          // Bad CRC, mark the packet as invalid
          Status = -3;
          return;
        }
      }
      else
      {
        // Set Invalid Packet Identification
        Status = -4;
        return;
      }
    }


  }
}
