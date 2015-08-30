using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace GTosPMU
{

  /// <summary>
  /// Sensor Configuration Class
  /// 02/12/12 1.0.9.10  Host_Sync default set to true
  /// 03/23/12 1.0.9.7  GPS Configuration updated
  /// 02/04/12 1.0.9.3  Firmware Configuration Update for Garmin 15LxW GPS baud rate = 4800
  /// 11/11/11 1.0.9.0  GPS Integrated Implementation update
  /// </summary>
  class Sensor
  {
    /// Sensor Configuration - mostly static because we only support one attached sensor.
    private const string CRLF = "\x000d\x000a";          // Input EOL

    #region  ////  Sensor Configuration Properties ////

    public const string T_SENSOR = "Sensor_TBL";
  
    public  const string C_SENSOR_VER = "Sensor_Ver";
    private const UInt16 DEF_SENSOR_VER = (UInt16)0x0903;
    private static UInt16 m_Sensor_Ver = DEF_SENSOR_VER;
    public static UInt16 Sensor_Ver
    {
      get { return m_Sensor_Ver; }
      set
      {
        m_Sensor_Ver = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SENSOR_VER] = m_Sensor_Ver;
      }
    }

    public const string C_SENSOR_ID = "Sensor_ID";
    private const UInt16 DEF_SENSOR_ID = 0;
    private static UInt16 m_Sensor_ID = DEF_SENSOR_ID;
    public static UInt16 Sensor_ID
    {
      get { return m_Sensor_ID; }
      set 
      {
        m_Sensor_ID = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SENSOR_ID] = m_Sensor_ID; 
      }
    }

    private const string C_SENSOR_PIN = "Sensor_PIN";
    private const UInt16 DEF_SENSOR_PIN = 0;
    private static UInt16 m_Sensor_PIN = DEF_SENSOR_PIN;
    public static UInt16 Sensor_PIN
    {
      get { return m_Sensor_PIN; }
      set
      {
        m_Sensor_PIN = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SENSOR_PIN] = m_Sensor_PIN;
      }
    }

    private const string C_PROG_MODE = "Prog_Mode";
    private const byte DEF_PROG_MODE = PROG_IDLE;
    private static byte m_Prog_Mode = DEF_PROG_MODE;
    public static byte Prog_Mode
    {
      get { return m_Prog_Mode; }
      set
      {
        m_Prog_Mode = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_PROG_MODE] = m_Prog_Mode;
      }
    }

    private const string C_NOMINAL_HZ = "Nominal_Hz";
    private const byte DEF_NOMINAL_HZ = (byte)60;
    private static byte m_Nominal_Hz = DEF_NOMINAL_HZ;
    public static byte Nominal_Hz
    {
      get { return m_Nominal_Hz; }
      set
      {
        m_Nominal_Hz = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_NOMINAL_HZ] = m_Nominal_Hz;
      }
    }

    private const string C_DIAG_MODE = "Diag_Mode";
    private const UInt16 DEF_DIAG_MODE = DIAG_TIMER1 + DIAG_INT0;
    private static UInt16 m_Diag_Mode = DEF_DIAG_MODE;
    public static UInt16 Diag_Mode
    {
      get { return m_Diag_Mode; }
      set
      {
        m_Diag_Mode = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_DIAG_MODE] = m_Diag_Mode;
      }
    }

    private const string C_HZCALOFFSET = "HzCalOffset";
    public const Int16 DEF_HZCALOFFSET = 0;
    private static Int16 m_HzCalOffset = DEF_HZCALOFFSET;
    public static Int16 HzCalOffset
    {
      get { return m_HzCalOffset; }
      set
      {
        m_HzCalOffset = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_HZCALOFFSET] = m_HzCalOffset;
      }
    }

    private const string C_T3CALSTART = "T3CalStart";
    public const Int16 DEF_T3CALSTART = 0;
    private static Int16 m_T3CalStart = DEF_T3CALSTART;
    public static Int16 T3CalStart
    {
      get { return m_T3CalStart; }
      set
      {
        m_T3CalStart = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T3CALSTART] = m_T3CalStart;
      }
    }

    private const string C_ZXCALOFFSET = "ZXCalOffset";
    private const Int16 DEF_ZXCALOFFSET = 0;
    private static Int16 m_ZXCalOffset = DEF_ZXCALOFFSET;    
    public static Int16 ZXCalOffset
    {
      get { return m_ZXCalOffset; }
      set
      {
        m_ZXCalOffset = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_ZXCALOFFSET] = m_ZXCalOffset;
      }
    }

    private const string C_T3CALOFFSET = "T3CalOffset";
    public const Int16 DEF_T3CALOFFSET = 34;
    private static Int16 m_T3CalOffset = DEF_T3CALOFFSET;
    public static Int16 T3CalOffset
    {
      get { return m_T3CalOffset; }
      set
      {
        m_T3CalOffset = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T3CALOFFSET] = m_T3CalOffset;
      }
    }
    
    // 60Hz T3 Offset, Period, and ModZero Constants
    private const UInt16 T3_OFS60 = 0x782E;
    private const UInt16 T3_PRD60 = 0xF063;
    private const UInt32 T3_MOD60 = 0x0005A252;

    // 50Hz T3 Offset, Period, and ModZero Constants
    private const UInt16 T3_OFS50 = 0x7D00;
    private const UInt16 T3_PRD50 = 0xFA00;
    private const UInt32 T3_MOD50 = 0x0006D600;

    private const string C_T3OFFSET = "T3Offset";
    private static UInt16 DEF_T3OFFSET = T3_OFS60;
    private static UInt16 m_T3Offset = DEF_T3OFFSET;
    public static UInt16 T3Offset
    {
      get { return m_T3Offset; }
      set
      {
        m_T3Offset = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T3OFFSET] = m_T3Offset;
      }
    }
    
    private const string C_T3PERIOD = "T3Period";
    private static UInt16 DEF_T3PERIOD = T3_PRD60;
    private static UInt16 m_T3Period = DEF_T3PERIOD;
    public static UInt16 T3Period
    {
      get { return m_T3Period; }
      set
      {
        m_T3Period = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T3PERIOD] = m_T3Period;
      }
    }
    
    private const string C_T3MZMAX = "T3MzMax";
    private static UInt32 DEF_T3MZMAX = T3_MOD60;
    private static UInt32 m_T3MzMax = DEF_T3MZMAX;
    public static UInt32 T3MzMax
    {
      get { return m_T3MzMax; }
      set
      {
        m_T3MzMax = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T3MZMAX] = m_T3MzMax;
      }
    }

    private const string C_EOL = "Eol";
    private const UInt16 DEF_EOL = 0x0d0a;
    private static UInt16 m_Eol = DEF_EOL;
    public static UInt16 Eol
    {
      get { return m_Eol; }
      set
      {
        m_Eol = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_EOL] = m_Eol;
      }
    }

    // dsPIC Baud Rate Generator values
    private const UInt16 BRG_115200 = 12;     // Host PC Interface
    private const UInt16 BRG_4800 = 312;      // GPS, Garmin 15LxW default
    
    private const string C_UART1_BRG = "UART1_BRG";
    private const UInt16 DEF_UART1_BRG = BRG_115200;
    private static UInt16 m_Uart1_Brg = DEF_UART1_BRG;     // 28  // Default = BRG_115200 = 12
    public static UInt16 Uart1_Brg
    {
      get { return m_Uart1_Brg; }
      set
      {
        m_Uart1_Brg = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_UART1_BRG] = m_Uart1_Brg;
      }
    }

    private const string C_UART2_BRG = "UART2_BRG";
    private const UInt16 DEF_UART2_BRG = BRG_4800;
    private static UInt16 m_Uart2_Brg = DEF_UART2_BRG;
    public static UInt16 Uart2_Brg
    {
      get { return m_Uart2_Brg; }
      set
      {
        m_Uart2_Brg = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_UART2_BRG] = m_Uart2_Brg;
      }
    }

    private const string C_CRC0 = "CRC_0";
    private static UInt16 m_Crc0 = 0;
    public static UInt16 Crc0
    {
      get { return m_Crc0; }
      set
      {
        m_Crc0 = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_CRC0] = m_Crc0;
      }
    }

    //// SynchroPhasor Configuration structure
    private const string C_TRANSMODE = "Trans_Mode";
    private static byte m_TransMode = 0;
    public static byte TransMode
    {
      get { return m_TransMode; }
      set
      {
        m_TransMode = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_TRANSMODE] = m_TransMode;
      }
    }

    private const string C_DATAMODE = "Data_Mode";
    private static byte m_DataMode = 0;
    public static byte DataMode
    {
      get { return m_DataMode; }
      set
      {
        m_DataMode = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_DATAMODE] = m_DataMode;
      }
    }

    private const string C_INTERVAL = "Interval";
    public const UInt16 DEF_INTERVAL = 6;
    private static UInt16 m_Interval = DEF_INTERVAL;
    public static UInt16 Interval
    {
      get { return m_Interval; }
      set
      {
        m_Interval = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_INTERVAL] = m_Interval;
      }
    }

    private const string C_T1CALSTART = "T1CalStart";
    private const Int16 DEF_T1CALSTART = 0;
    private static Int16 m_T1CalStart = DEF_T1CALSTART;
    public static Int16 T1CalStart
    {
      get { return m_T1CalStart; }
      set
      {
        m_T1CalStart = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T1CALSTART] = m_T1CalStart;
      }
    }

    private const string C_T1CALPRD = "T1CalPrd";
    private const Int16 DEF_T1CALPRD = 0;
    private static Int16 m_T1CalPrd = DEF_T1CALPRD;
    public static Int16 T1CalPrd
    {
      get { return m_T1CalPrd; }
      set
      {
        m_T1CalPrd = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T1CALPRD] = m_T1CalPrd;
      }
    }


    //// Timer1 Constants
    // 60Hz Nominal Frequency Intervals
    // For Example an Interval of "1" is 60Hz or 1/60 sec, "2" is 30Hz or 2/60 sec
    private const UInt16 T1_PRD60I01 = 49998;  /* 60Hz, Prescale 1:8   */
    private const UInt16 T1_PRD60I02 = 12499;  /* 30Hz, Prescale 1:64  */
    private const UInt16 T1_PRD60I03 = 18749;  /* 20Hz, Prescale 1:64  */
    private const UInt16 T1_PRD60I04 = 24999;  /* 15Hz, Prescale 1:64  */
    private const UInt16 T1_PRD60I05 = 31249;  /* 12Hz, Prescale 1:64  */
    private const UInt16 T1_PRD60I06 = 37499;  /* 10Hz, Prescale 1:64  */
    private const UInt16 T1_PRD60I10 = 15624;  /*  6Hz, Prescale 1:256 */
    private const UInt16 T1_PRD60I12 = 18749;  /*  5Hz, Prescale 1:256 */
    private const UInt16 T1_PRD60I15 = 23436;  /*  4Hz, Prescale 1:256 */
    private const UInt16 T1_PRD60I20 = 31249;  /*  3Hz, Prescale 1:256 */
    private const UInt16 T1_PRD60I30 = 46874;  /*  2Hz, Prescale 1:256 */
    private const UInt16 T1_PRD60I60 = 46874;  /*  1Hz, Prescale 1:256, 2 Overflows */

    // 50Hz Nominal Frequency Intervals
    private const UInt16 T1_PRD50I01 = 59999;  /* 50Hz, Prescale 1:8   */
    private const UInt16 T1_PRD50I02 = 14999;  /* 25Hz, Prescale 1:64  */
    private const UInt16 T1_PRD50I05 = 37599;  /* 10Hz, Prescale 1:64  */
    private const UInt16 T1_PRD50I10 = 18749;  /*  5Hz, Prescale 1:256 */
    private const UInt16 T1_PRD50I25 = 46874;  /*  2Hz, Prescale 1:256 */
    private const UInt16 T1_PRD50I50 = 46874;  /*  1Hz, Prescale 1:256, 2 Overflows */

    private const UInt16 T1_PRESCAL00 = 0x0000;  /* no prescale */
    private const UInt16 T1_PRESCAL01 = 0x0010;  /* 1:8         */
    private const UInt16 T1_PRESCAL10 = 0x0020;  /* 1:64        */
    private const UInt16 T1_PRESCAL11 = 0x0030;  /* 1:256       */


    private const string C_T1PERIOD = "T1Period";
    private static UInt16 DEF_T1PERIOD = T1_PRD60I06;
    private static UInt16 m_T1Period = DEF_T1PERIOD;
    public static UInt16 T1Period
    {
      get { return m_T1Period; }
      set
      {
        m_T1Period = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T1PERIOD] = m_T1Period;
      }
    }


    private const string C_T1PRESCALE = "T1PreScale";
    private static UInt16 DEF_T1PRESCALE = T1_PRESCAL10;
    private static UInt16 m_T1Prescale = DEF_T1PRESCALE; 
    public static UInt16 T1Prescale
    {
      get { return m_T1Prescale; }
      set
      {
        m_T1Prescale = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_T1PRESCALE] = m_T1Prescale;
      }
    }

    private const string C_VREF = "Vref";
    private static UInt16 DEF_VREF = 2500;
    private static UInt16 m_Vref = DEF_VREF;
    public static UInt16 Vref
    {
      get { return m_Vref; }
      set
      {
        m_Vref = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_VREF] = m_Vref;
      }
    }

    private const string C_USE_GPS = "Use GPS";
    public const UInt16 USE_GPS_PPS = 0x01;
    public const UInt16 USE_GPS_TIME = 0x02;
    public static bool Host_Sync = true;

    private const UInt16 DEF_USE_GPS = 0x00;  //USE_GPS_PPS | USE_GPS_TIME;         // Use both GPS PPS and GPS Time
    private static UInt16 m_Use_GPS = DEF_USE_GPS;
    public static UInt16 Use_GPS
    {
      get { return m_Use_GPS; }
      set
      {
        m_Use_GPS = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_USE_GPS] = m_Use_GPS;
        Host_Sync = !((m_Use_GPS & USE_GPS_TIME) == USE_GPS_TIME);
      }
    }

    private const string C_DETECTA = "Detect_A";
    private static UInt16[] m_DetectA = new UInt16[8];
    public static UInt16[] DetectA
    {
      get { return m_DetectA; }
      set
      {
        m_DetectA = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_DETECTA] = m_DetectA;
      }
    }

    private const string C_CRC1 = "CRC_1";
    private static UInt16 m_Crc1 = 0;
    public static UInt16 Crc1
    {
      get { return m_Crc1; }
      set
      {
        m_Crc1 = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_CRC1] = m_Crc1;
      }
    }


    // Application Configuation Structure #2 for PHzMonitor style operation
    private const string C_STEP_MODE = "Step_Mode";
    private static byte m_Step_Mode = 0;
    public static byte Step_Mode
    {
      get { return m_Step_Mode; }
      set
      {
        m_Step_Mode = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_STEP_MODE] = m_Step_Mode;
      }
    }

    private const string C_AVG_MODE = "Avg_Mode";
    private static byte m_Avg_Mode = 0;
    public static byte Avg_Mode
    {
      get { return m_Avg_Mode; }
      set
      {
        m_Avg_Mode = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AVG_MODE] = m_Avg_Mode;
      }
    }

    private const string C_SAMPLEMAX = "Sample_Max";
    private static UInt16 m_SampleMax = 9;
    public static UInt16 SampleMax
    {
      get { return m_SampleMax; }
      set
      {
        m_SampleMax = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SAMPLEMAX] = m_SampleMax;
      }
    }

    private const string C_STEPCOUNT = "Step_Count";
    private static UInt16 m_StepCount = 20;
    public static UInt16 StepCount
    {
      get { return m_StepCount; }
      set
      {
        m_StepCount = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_STEPCOUNT] = m_StepCount;
      }
    }

    private const string C_DETECTB = "Detect_B";
    private static UInt16[] m_DetectB = new UInt16[13];
    public static UInt16[] DetectB
    {
      get { return m_DetectB; }
      set
      {
        m_DetectB = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_DETECTB] = m_DetectB;
      }
    }

    private const string C_CRC2 = "CRC_2";
    private static UInt16 m_Crc2 = 0;
    public static UInt16 Crc2
    {
      get { return m_Crc2; }
      set
      {
        m_Crc2 = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_CRC2] = m_Crc2;
      }
    }


    private const string C_TIME_BASE = "Time_Base";
    private const UInt32 DEF_TIMEBASE = 1000000;   // microseconds
    private static UInt32 m_Time_Base = DEF_TIMEBASE;
    public static UInt32 Time_Base
    {
      get { return m_Time_Base; }
      set
      {
        m_Time_Base = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_TIME_BASE] = m_Time_Base;
      }
    }

    private const string C_PMU_STATION = "PMU_Station";
      //memset(PmuCfg1.PMU_Station, 0x00, 16);
      //strcpy(PmuCfg1.PMU_Station, "GTosPMU Mod_A");
    private static byte[] m_PMU_Station = new byte[16];
    public static byte[] PMU_Station
    {
      get { return m_PMU_Station; }
      set
      {
        m_PMU_Station = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_PMU_STATION] = m_PMU_Station;
      }
    }

    private const string C_GPS_INTERVAL = "GPS_Interval";
    private const UInt16 DEF_GPS_INTERVAL = 10;   // microseconds
    private static UInt16 m_GPS_Interval = DEF_GPS_INTERVAL;
    public static UInt16 GPS_Interval
    {
        get { return m_GPS_Interval; }
        set
        {
            m_GPS_Interval = value;
            Cfg.XDS.Tables[T_SENSOR].Rows[0][C_GPS_INTERVAL] = m_GPS_Interval;
        }
    }

    private const string C_PMU_STUFF = "PMU_Stuff";
    private static byte[] m_PMU_Stuff = new byte[10];     // filler
    public static byte[] PMU_Stuff
    {
      get { return m_PMU_Stuff; }
      set
      {
        m_PMU_Stuff = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_PMU_STUFF] = m_PMU_Stuff;
      }
    }

    ////private const string C_CRC3 = "CRC_3";
    ////public static UInt16 m_Crc3 = 0;
    ////public static UInt16 Crc3
    ////{
    ////  get { return m_Crc3; }
    ////  set
    ////  {
    ////    m_Crc3 = value;
    ////    Cfg.XDS.Tables[T_SENSOR].Rows[0][C_CRC3] = m_Crc3;
    ////  }
    ////}

    //////
    public const string C_SENSOR_NAME = "Sensor_Name";
    public const string DEF_SENSOR_NAME = "";
    private static string m_Sensor_Name = "";
    public static string Sensor_Name
    {
      get { return m_Sensor_Name; }
      set
      {
        m_Sensor_Name = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SENSOR_NAME] = m_Sensor_Name;
      }
    }

    public const string C_LOCATION_NAME = "Location_Name";
    public const string DEF_LOCATION_NAME = "";
    private static string m_Loc_Name = "";
    public static string Location_Name
    {
      get { return m_Loc_Name; }
      set
      {
        m_Loc_Name = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_LOCATION_NAME] = m_Loc_Name;
      }
    }

    public const string C_SYNC_INTERVAL = "Sync_Interval";
    public const Int32 DEF_SYNC_INTERVAL = 3600;
    private static Int32 m_Sync_Interval = DEF_SYNC_INTERVAL;
    public static Int32 Sync_Interval
    {
      get { return m_Sync_Interval; }
      set
      {
        m_Sync_Interval = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SYNC_INTERVAL] = m_Sync_Interval;
      }
    }

    public const string C_AUTO_CONNECT = "Auto_Connect";
    public const bool DEF_AUTO_CONNECT = false;
    private static bool m_Auto_Connect = DEF_AUTO_CONNECT;
    public static bool Auto_Connect
    {
      get { return m_Auto_Connect; }
      set
      {
        m_Auto_Connect = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_CONNECT] = m_Auto_Connect;
      }
    }

    public const string C_CONNECT_ONLY = "Connect_Only";
    public const bool DEF_CONNECT_ONLY = false;
    private static bool m_Connect_Only = DEF_CONNECT_ONLY;
    public static bool Connect_Only
    {
      get { return m_Connect_Only; }
      set
      {
        m_Connect_Only = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_CONNECT_ONLY] = m_Connect_Only;
      }
    }

    public const string C_AUTO_UPDATE = "Auto_Update";
    public const bool DEF_AUTO_UPDATE = false;
    private static bool m_Auto_Update = DEF_AUTO_UPDATE;
    public static bool Auto_Update
    {
      get { return m_Auto_Update; }
      set
      {
        m_Auto_Update = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_UPDATE] = m_Auto_Update;
      }
    }

    public const string C_AUTO_RESET = "Auto_Reset";
    public const bool DEF_AUTO_RESET = false;
    private static bool m_Auto_Reset = DEF_AUTO_RESET;
    public static bool Auto_Reset
    {
      get { return m_Auto_Reset; }
      set
      {
        m_Auto_Reset = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_RESET] = m_Auto_Reset;
      }
    }

    public const string C_AUTO_REBOOT = "Auto_Reboot";
    public const bool DEF_AUTO_REBOOT = false;
    private static bool m_Auto_Reboot = DEF_AUTO_REBOOT;
    public static bool Auto_Reboot
    {
      get { return m_Auto_Reboot; }
      set
      {
        m_Auto_Reboot = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_REBOOT] = m_Auto_Reboot;
      }
    }

    public const string C_AUTO_CONFIG = "Auto_Config";
    public const bool DEF_AUTO_CONFIG = true;
    private static bool m_Auto_Config = DEF_AUTO_CONFIG;
    public static bool Auto_Config
    {
      get { return m_Auto_Config; }
      set
      {
        m_Auto_Config = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_CONFIG] = m_Auto_Config;
      }
    }

    public const string C_FACTORY_RESTORE = "Factory_Restore";
    public const bool DEF_FACTORY_RESTORE = false;
    private static bool m_Factory_Restore = DEF_FACTORY_RESTORE;
    public static bool Factory_Restore
    {
      get { return m_Factory_Restore; }
      set
      {
        m_Factory_Restore = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_FACTORY_RESTORE] = m_Factory_Restore;
      }
    }


    public const string C_LATE_PPS_EXCEPTION_ENABLE = "Late_PPS_Exception_Enable";
    public const bool DEF_LATE_PPS_EXCEPTION_ENABLE = false;
    private static bool m_Late_PPS_Exception_Enable = DEF_LATE_PPS_EXCEPTION_ENABLE;
    public static bool Late_PPS_Exception_Enable
    {
      get { return m_Late_PPS_Exception_Enable; }
      set
      {
        m_Late_PPS_Exception_Enable = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_LATE_PPS_EXCEPTION_ENABLE] = m_Late_PPS_Exception_Enable;
      }
    }

    public const string C_LATE_PPS_EXCEPTION_SECONDS = "Late_PPS_Exception_Seconds";
    public const double DEF_LATE_PPS_EXCEPTION_SECONDS = 1.2;
    private static double m_Late_PPS_Exception_Seconds = DEF_LATE_PPS_EXCEPTION_SECONDS;
    public static double Late_PPS_Exception_Seconds
    {
      get { return m_Late_PPS_Exception_Seconds; }
      set
      {
        m_Late_PPS_Exception_Seconds = value;
        Cfg.XDS.Tables[T_SENSOR].Rows[0][C_LATE_PPS_EXCEPTION_SECONDS] = m_Late_PPS_Exception_Seconds;
      }
    }

    public static DataTable New_CfgTable()
    {
      DataTable t0 = new DataTable(T_SENSOR);
      t0.Columns.Add(new DataColumn(C_SENSOR_VER, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_SENSOR_VER].DefaultValue = Sensor.Sensor_Ver;

      t0.Columns.Add(new DataColumn(C_SENSOR_ID, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_SENSOR_ID].DefaultValue = 0x0000;

      t0.Columns.Add(new DataColumn(C_SENSOR_PIN, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_SENSOR_PIN].DefaultValue = 0x0000;

      t0.Columns.Add(new DataColumn(C_SENSOR_NAME, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_SENSOR_NAME].DefaultValue = "";

      t0.Columns.Add(new DataColumn(C_LOCATION_NAME, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_LOCATION_NAME].DefaultValue = "";

      t0.Columns.Add(new DataColumn(C_SYNC_INTERVAL, Type.GetType(Cfg.SYS_INT32)));
      t0.Columns[C_SYNC_INTERVAL].DefaultValue = DEF_SYNC_INTERVAL;

      t0.Columns.Add(new DataColumn(C_AUTO_CONNECT, Type.GetType(Cfg.SYS_BOOLEAN)));
      t0.Columns[C_AUTO_CONNECT].DefaultValue = DEF_AUTO_CONNECT;

      t0.Columns.Add(new DataColumn(C_CONNECT_ONLY, Type.GetType(Cfg.SYS_BOOLEAN)));
      t0.Columns[C_CONNECT_ONLY].DefaultValue = DEF_CONNECT_ONLY;

      t0.Columns.Add(new DataColumn(C_AUTO_UPDATE, Type.GetType(Cfg.SYS_BOOLEAN)));
      t0.Columns[C_AUTO_UPDATE].DefaultValue = DEF_AUTO_UPDATE;

      t0.Columns.Add(new DataColumn(C_AUTO_RESET, Type.GetType(Cfg.SYS_BOOLEAN)));
      t0.Columns[C_AUTO_RESET].DefaultValue = DEF_AUTO_RESET;

      t0.Columns.Add(new DataColumn(C_AUTO_REBOOT, Type.GetType(Cfg.SYS_BOOLEAN)));
      t0.Columns[C_AUTO_REBOOT].DefaultValue = DEF_AUTO_REBOOT;

      t0.Columns.Add(new DataColumn(C_AUTO_CONFIG, Type.GetType(Cfg.SYS_BOOLEAN)));
      t0.Columns[C_AUTO_CONFIG].DefaultValue = DEF_AUTO_CONFIG;

      t0.Columns.Add(new DataColumn(C_FACTORY_RESTORE, Type.GetType(Cfg.SYS_BOOLEAN)));
      t0.Columns[C_FACTORY_RESTORE].DefaultValue = DEF_FACTORY_RESTORE;

      t0.Columns.Add(new DataColumn(C_LATE_PPS_EXCEPTION_ENABLE, Type.GetType(Cfg.SYS_BOOLEAN)));
      t0.Columns[C_LATE_PPS_EXCEPTION_ENABLE].DefaultValue = DEF_LATE_PPS_EXCEPTION_ENABLE;

      t0.Columns.Add(new DataColumn(C_LATE_PPS_EXCEPTION_SECONDS, typeof(double)));
      t0.Columns[C_LATE_PPS_EXCEPTION_SECONDS].DefaultValue = DEF_LATE_PPS_EXCEPTION_SECONDS;

      // Sensor Stored Properties

      t0.Columns.Add(new DataColumn(C_PROG_MODE, Type.GetType(Cfg.SYS_BYTE)));
      t0.Columns[C_PROG_MODE].DefaultValue = DEF_PROG_MODE;

      t0.Columns.Add(new DataColumn(C_NOMINAL_HZ, Type.GetType(Cfg.SYS_BYTE)));
      t0.Columns[C_NOMINAL_HZ].DefaultValue = DEF_NOMINAL_HZ;

      t0.Columns.Add(new DataColumn(C_DIAG_MODE, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_DIAG_MODE].DefaultValue = DEF_DIAG_MODE;


      t0.Columns.Add(new DataColumn(C_HZCALOFFSET, Type.GetType(Cfg.SYS_INT16)));
      t0.Columns[C_HZCALOFFSET].DefaultValue = DEF_HZCALOFFSET;

      t0.Columns.Add(new DataColumn(C_T3CALSTART, Type.GetType(Cfg.SYS_INT16)));
      t0.Columns[C_T3CALSTART].DefaultValue = DEF_T3CALSTART;

      t0.Columns.Add(new DataColumn(C_ZXCALOFFSET, Type.GetType(Cfg.SYS_INT16)));
      t0.Columns[C_ZXCALOFFSET].DefaultValue = DEF_ZXCALOFFSET;

      t0.Columns.Add(new DataColumn(C_T3CALOFFSET, Type.GetType(Cfg.SYS_INT16)));
      t0.Columns[C_T3CALOFFSET].DefaultValue = DEF_T3CALOFFSET;
    
      t0.Columns.Add(new DataColumn(C_T3OFFSET, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_T3OFFSET].DefaultValue = DEF_T3OFFSET;
    
      t0.Columns.Add(new DataColumn(C_T3PERIOD, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_T3PERIOD].DefaultValue = DEF_T3PERIOD;
    
      t0.Columns.Add(new DataColumn(C_T3MZMAX, Type.GetType(Cfg.SYS_UINT32)));
      t0.Columns[C_T3MZMAX].DefaultValue = DEF_T3MZMAX;

      t0.Columns.Add(new DataColumn(C_EOL, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_EOL].DefaultValue = DEF_EOL;

      t0.Columns.Add(new DataColumn(C_UART1_BRG, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_UART1_BRG].DefaultValue = DEF_UART1_BRG;

      t0.Columns.Add(new DataColumn(C_UART2_BRG, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_UART2_BRG].DefaultValue = DEF_UART2_BRG;

      t0.Columns.Add(new DataColumn(C_CRC0, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_CRC0].DefaultValue = 0;

    //// SynchroPhasor Configuration structure
      t0.Columns.Add(new DataColumn(C_TRANSMODE, Type.GetType(Cfg.SYS_BYTE)));
      t0.Columns[C_TRANSMODE].DefaultValue = 0;

      t0.Columns.Add(new DataColumn(C_DATAMODE, Type.GetType(Cfg.SYS_BYTE)));
      t0.Columns[C_DATAMODE].DefaultValue = 0;

      t0.Columns.Add(new DataColumn(C_INTERVAL, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_INTERVAL].DefaultValue = DEF_INTERVAL;

      t0.Columns.Add(new DataColumn(C_T1CALSTART, Type.GetType(Cfg.SYS_INT16)));
      t0.Columns[C_T1CALSTART].DefaultValue = DEF_T1CALSTART;

      t0.Columns.Add(new DataColumn(C_T1CALPRD, Type.GetType(Cfg.SYS_INT16)));
      t0.Columns[C_T1CALPRD].DefaultValue = DEF_T1CALPRD;

      t0.Columns.Add(new DataColumn(C_T1PERIOD, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_T1PERIOD].DefaultValue = DEF_T1PERIOD;

      t0.Columns.Add(new DataColumn(C_T1PRESCALE, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_T1PRESCALE].DefaultValue = DEF_T1PRESCALE;

      t0.Columns.Add(new DataColumn(C_VREF, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_VREF].DefaultValue = DEF_VREF;

      t0.Columns.Add(new DataColumn(C_USE_GPS, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_USE_GPS].DefaultValue = DEF_USE_GPS;
      Host_Sync = !((DEF_USE_GPS & USE_GPS_TIME) == USE_GPS_TIME);

      t0.Columns.Add(new DataColumn(C_DETECTA, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_DETECTA].DefaultValue = "";

      t0.Columns.Add(new DataColumn(C_CRC1, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_CRC1].DefaultValue = 0;


      // Application Configuation Structure #2 for PHzMonitor style operation
      t0.Columns.Add(new DataColumn(C_STEP_MODE, Type.GetType(Cfg.SYS_BYTE)));
      t0.Columns[C_STEP_MODE].DefaultValue = 0;

      t0.Columns.Add(new DataColumn(C_AVG_MODE, Type.GetType(Cfg.SYS_BYTE)));
      t0.Columns[C_AVG_MODE].DefaultValue = 0;

      t0.Columns.Add(new DataColumn(C_SAMPLEMAX, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_SAMPLEMAX].DefaultValue = 9;

      t0.Columns.Add(new DataColumn(C_STEPCOUNT, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_STEPCOUNT].DefaultValue = 20;

      t0.Columns.Add(new DataColumn(C_DETECTB, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_DETECTB].DefaultValue = "";

      t0.Columns.Add(new DataColumn(C_CRC2, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_CRC2].DefaultValue = 0;


      t0.Columns.Add(new DataColumn(C_TIME_BASE, Type.GetType(Cfg.SYS_UINT32)));
      t0.Columns[C_TIME_BASE].DefaultValue = DEF_TIMEBASE;

      t0.Columns.Add(new DataColumn(C_PMU_STATION, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_PMU_STATION].DefaultValue = "GTosPMU Mod_A";

      t0.Columns.Add(new DataColumn(C_GPS_INTERVAL, Type.GetType(Cfg.SYS_UINT16)));
      t0.Columns[C_GPS_INTERVAL].DefaultValue = 10;

      t0.Columns.Add(new DataColumn(C_PMU_STUFF, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_PMU_STUFF].DefaultValue = "";
  
      ////t0.Columns.Add(new DataColumn(C_CRC3, Type.GetType(Cfg.SYS_UINT16)));
      ////t0.Columns[C_CRC3].DefaultValue = 0;

      t0.Rows.Add(t0.NewRow());
      t0.AcceptChanges();

      return t0;
    }


    public static void Load_Config()
    {
      /// Load the Sensor Configuration
      try
      {
        // Clear the Sensor Version, we will get it from the sensor
        m_Sensor_Ver = 0x0000;   // Convert.ToUInt16(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SENSOR_VER]);

        m_Sensor_ID = Convert.ToUInt16(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SENSOR_ID]);
        m_Sensor_PIN = Convert.ToUInt16(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SENSOR_PIN]);
        m_Sensor_Name = Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SENSOR_NAME].ToString();
        m_Loc_Name = Cfg.XDS.Tables[T_SENSOR].Rows[0][C_LOCATION_NAME].ToString();
        m_Auto_Connect = Convert.ToBoolean(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_CONNECT]);
        m_Connect_Only = Convert.ToBoolean(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_CONNECT_ONLY]);
        m_Auto_Update = Convert.ToBoolean(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_UPDATE]);
        m_Auto_Reset = Convert.ToBoolean(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_RESET]);
        m_Auto_Reboot = Convert.ToBoolean(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_REBOOT]);
        m_Auto_Config = Convert.ToBoolean(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_AUTO_CONFIG]);
        m_Factory_Restore = Convert.ToBoolean(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_FACTORY_RESTORE]);

        m_Late_PPS_Exception_Enable = Convert.ToBoolean(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_LATE_PPS_EXCEPTION_ENABLE]);
        m_Late_PPS_Exception_Seconds = Convert.ToDouble(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_LATE_PPS_EXCEPTION_SECONDS]);

        m_Use_GPS = Convert.ToUInt16(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_USE_GPS]);
        Host_Sync = !((m_Use_GPS & USE_GPS_TIME) == USE_GPS_TIME);

        m_Sync_Interval = Convert.ToInt32(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_SYNC_INTERVAL]);
        m_Nominal_Hz = Convert.ToByte(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_NOMINAL_HZ]);
        m_Interval = Convert.ToByte(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_INTERVAL]);

        // Calibration Values
        m_HzCalOffset = Convert.ToInt16(Cfg.XDS.Tables[T_SENSOR].Rows[0][C_HZCALOFFSET]);

      }
      catch (Exception ex)
      {
        Log.Err(ex, "Sensor, Load_Config", "Error Reading Configuration", Log.LogDevice.LOG_DLG);
      }
    }


    public static string Load_Config(byte[] bCfg)
    {
      /// This procedure verifies and decodes the Sensor Configuration
      string sResult = "OK";
      m_Crc0 = (UInt16)((bCfg[32] << 8) + bCfg[33]);         // CRC Word is Not byte swapped
      m_Crc1 = (UInt16)((bCfg[66] << 8) + bCfg[67]);
      m_Crc2 = (UInt16)((bCfg[100] << 8) + bCfg[101]);

      ////m_Crc3 = (UInt16)((bCfg[134] << 8) + bCfg[135]);

      if ((Crc.Calc_CCITT(bCfg, 0, 32) == m_Crc0)
          && (Crc.Calc_CCITT(bCfg, 34, 32) == m_Crc1)
          && (Crc.Calc_CCITT(bCfg, 68, 32) == m_Crc2))  //// && (Crc.Calc_CCITT(bCfg, 102, 32) == m_Crc3))
      {
        // Word bytes are swapped
        m_Sensor_Ver = (UInt16)((bCfg[1] << 8) + bCfg[0]);        // 0  Firmware Version, burned into the sesnor firmware
        m_Sensor_ID = (UInt16)((bCfg[3] << 8) + bCfg[2]);         // 2  Hose PC Configurable Unique Sensor ID, 0 = not defined yet
        m_Sensor_PIN = (UInt16)((bCfg[5] << 8) + bCfg[4]);        // 4  Sensor's Password for Web Service Authentication
        m_Prog_Mode = (byte)bCfg[6];                              // 6  Running Mode, Default = PROG_STEXT for Streaming Text Mode
        m_Nominal_Hz = (byte)bCfg[7];                             // 7  nominal frequency of 60 or 50 Hz, Default = 60Hz
        m_Diag_Mode = (UInt16)((bCfg[9] << 8) + bCfg[8]);         // 8  Diagnostics Options, Default = DIAG_HZTX
        m_HzCalOffset = (Int16)((bCfg[11] << 8) + bCfg[10]);      // 10  Frequency Calibration Offset
        m_T3CalStart = (Int16)((bCfg[13] << 8) + bCfg[12]);       // 12  Added to Initial Timer Value
        m_ZXCalOffset = (Int16)((bCfg[15] << 8) + bCfg[14]);      // 14  Added to T3 value on IC1 Interrupt
        m_T3CalOffset = (Int16)((bCfg[17] << 8) + bCfg[16]);      // 16  Added to Final Timer Value
        m_T3Offset = (UInt16)((bCfg[19] << 8) + bCfg[18]);        // 18  OFSET60;  // Value of the Measurement Range Mid Point. Default = 60Hz with n Overflows
        m_T3Period = (UInt16)((bCfg[21] << 8) + bCfg[20]);        // 20  PRD60;    // Timer Period.  Default = 60Hz with n Overflows
        m_T3MzMax = (UInt32)((bCfg[25] << 24) + (bCfg[24] << 16)
                           + (bCfg[23] << 8) + bCfg[22]);         // 22  MZMAX60;  // Maximum with n Overflows and Modulus Zero.  Default = 60Hz with n Overflows
        m_Eol = (UInt16)((bCfg[27] << 8) + bCfg[26]);             // 26  Default = CR+LF = 0x0d0a
        m_Uart1_Brg = (UInt16)((bCfg[29] << 8) + bCfg[28]);       // 28  Default = BRG_115200 = 12
        m_Uart2_Brg = (UInt16)((bCfg[31] << 8) + bCfg[30]);       // 30  Default = BRG_9600 = 155
        // Crc0 = (UInt16)((bCfg[32] << 8) + bCfg[33]);           // CRC Word is Not byte swapped

        //// SynchroPhasor Configuration structure
        m_TransMode = bCfg[34];                                   // 0   PHzMonitor Transmit on Sample is TransMode=0, on SynchroPhasor Interval is TransMode=1
        m_DataMode = bCfg[35];                                    // 1   Diagnostic Data Mode
        m_Interval = (UInt16)((bCfg[37] << 8) + bCfg[36]);        // 2   The number of SynchroPhasor Measurement Intervals between PPSs
        m_T1CalStart = (Int16)((bCfg[39] << 8) + bCfg[38]);       // 4   Timer1 Start Offset
        m_T1CalPrd = (Int16)((bCfg[41] << 8) + bCfg[40]);         // 6   Timer1 Period Calibration 
        m_T1Period = (UInt16)((bCfg[43] << 8) + bCfg[42]);        // 8   Timer1 Period
        m_T1Prescale = (UInt16)((bCfg[45] << 8) + bCfg[44]);      // 10  Timer1 Prescale
        m_Vref = (UInt16)((bCfg[47] << 8) + bCfg[46]);            // 12  OpAmp Voltage Reference in milli-volts  Default = 2500 = 2.5V
        
        m_Use_GPS = (UInt16)((bCfg[49] << 8) + bCfg[48]);         // 14  1 = Use GPS PPS on INT0, else use Interval Timer1 on continutiously, 2 = Use GPS Time, 3 = Use Both
        Host_Sync = !((m_Use_GPS & USE_GPS_TIME) == USE_GPS_TIME);

        for (int ii = 0; ii < 8; ii++)
        {
          m_DetectA[ii] = (UInt16)((bCfg[51 + (ii * 2)] << 8) + bCfg[50 + (ii * 2)]);  // 16 to 31
        }
        // Crc1 = (UInt16)((bCfg[66] << 8) + bCfg[67]);       // CRC Word is Not byte swapped

        m_Time_Base = (UInt32)((bCfg[71] << 24) + (bCfg[70] << 16) + (bCfg[69] << 8) + bCfg[68]);
        for (int ii = 0; ii < 16; ii++)
        {
          m_PMU_Station[ii] = bCfg[72 + ii];
        }

        m_GPS_Interval = (UInt16)((bCfg[88] << 8) + bCfg[87]);

        for (int ii = 0; ii < 10; ii++)
        {
          m_PMU_Stuff[ii] = bCfg[89 + ii];     // filler
        }
        // Crc2 = (UInt16)((bCfg[100] << 8) + bCfg[101]);

        //// Application Configuation Structure #3 for PHzMonitor style operation
        ////m_Step_Mode = bCfg[68];                                 // 0
        ////m_Avg_Mode = bCfg[69];                                  // 1
        ////m_SampleMax = (UInt16)((bCfg[71] << 8) + bCfg[70]);     // 2  Maximum Sample Index 0 to 599, Default = 9 (for last 10 samples)
        ////m_StepCount = (UInt16)((bCfg[73] << 8) + bCfg[72]);     // 4
        ////for (int ii = 0; ii < 13; ii++)
        ////{
        ////  m_DetectB[ii] = (UInt16)((bCfg[76 + (ii * 2)] << 8) + bCfg[75 + (ii * 2)]);   // 6 to 31
        ////}
        //// // Crc3 = (UInt16)((bCfg[134] << 8) + bCfg[135]);
      }
      return sResult;
    }


    public static string Stuff_Config(byte[] bCfg)
    {
      // Suff the Configuration Buffer to prepare to send it to the sensor
      // This is basically the revers of Load_Config(byte[])
      string sResult = "OK";

      bCfg[0] = (byte)(m_Sensor_Ver & 0x00ff);                // 0  Firmware Version
      bCfg[1] = (byte)((m_Sensor_Ver & 0xff00) >> 8);  

      bCfg[2] = (byte)(m_Sensor_ID & 0x00ff);                 // 2  Unique Sensor ID, 0 = not defined yet
      bCfg[3] = (byte)((m_Sensor_ID & 0xff00) >> 8);

      bCfg[4] = (byte)(m_Sensor_PIN & 0x00ff);                // 4  Sensor's Password for Web Service Authentication
      bCfg[5] = (byte)((m_Sensor_PIN & 0xff00) >> 8);

      bCfg[6] = m_Prog_Mode;                                  // 6  Running Mode, Default = PROG_STEXT for Streaming Text Mode
      bCfg[7] = m_Nominal_Hz;                                 // 7  nominal frequency of 60 or 50 Hz, Default = 60Hz

      bCfg[8] = (byte)(m_Diag_Mode & 0x00ff);                 // 8  Diagnostics Options, Default = DIAG_HZTX
      bCfg[9] = (byte)((m_Diag_Mode & 0xff00) >> 8);

      bCfg[10] = (byte)(m_HzCalOffset & 0x00ff);              // 10  Frequency Calibration Offset
      bCfg[11] = (byte)((m_HzCalOffset & 0xff00) >> 8);

      bCfg[12] = (byte)(m_T3CalStart & 0x00ff);               // 12  Added to Initial Timer Value
      bCfg[13] = (byte)((m_T3CalStart & 0xff00) >> 8);

      bCfg[14] = (byte)(m_ZXCalOffset & 0x00ff);              // 14  Added to T3 value on IC1 Interrupt
      bCfg[15] = (byte)((m_ZXCalOffset & 0xff00) >> 8);

      bCfg[16] = (byte)(m_T3CalOffset & 0x00ff);              // 16  Added to Final Timer Value
      bCfg[17] = (byte)((m_T3CalOffset & 0xff00) >> 8);

      bCfg[18] = (byte)(m_T3Offset & 0x00ff);                 // 18  OFSET60;  // Value of the Measurement Range Mid Point. Default = 60Hz with n Overflows
      bCfg[19] = (byte)((m_T3Offset & 0xff00) >> 8);
      
      bCfg[20] = (byte)(m_T3Period & 0x00ff);                 // 20  PRD60;    // Timer Period.  Default = 60Hz with n Overflows
      bCfg[21] = (byte)((m_T3Period & 0xff00) >> 8);    

      bCfg[22] = (byte)(m_T3MzMax & 0x000000ff);              // 22  MZMAX60;  // Maximum with n Overflows and Modulus Zero.  Default = 60Hz with n Overflows
      bCfg[23] = (byte)((m_T3MzMax & 0x0000ff00) >> 8);
      bCfg[24] = (byte)((m_T3MzMax & 0x00ff0000) >> 16);
      bCfg[25] = (byte)((m_T3MzMax & 0xff000000) >> 24);

      bCfg[26] = (byte)(m_Eol & 0x00ff);                      // 26  Default = CR+LF = 0x0d0a
      bCfg[27] = (byte)((m_Eol & 0xff00) >> 8);

      bCfg[28] = (byte)(m_Uart1_Brg & 0x00ff);                // 28  Default = BRG_115200 = 12
      bCfg[29] = (byte)((m_Uart1_Brg & 0xff00) >> 8);

      bCfg[30] = (byte)(m_Uart2_Brg & 0x00ff);                // 30  Default = BRG_9600 = 155
      bCfg[31] = (byte)((m_Uart2_Brg & 0xff00) >> 8);

      Crc0 = Crc.Calc_CCITT(bCfg, 0, 32);
      bCfg[32] = (byte)((Crc0 & 0xff00) >> 8);
      bCfg[33] = (byte)(Crc0 & 0x00ff);
      

      //// SynchroPhasor Configuration structure
      bCfg[34] = (byte)m_TransMode;                             // 0   PHzMonitor Transmit on Sample is TransMode=0, on SynchroPhasor Interval is TransMode=1
      bCfg[35] = (byte)m_DataMode;                              // 1   Diagnostic Data MOde
      bCfg[36] = (byte)(m_Interval & 0x00ff);                   // 2   The number of SynchroPhasor Measurement Intervals between PPSs
      bCfg[37] = (byte)((m_Interval & 0xff00) >> 8);

      bCfg[38] = (byte)(m_T1CalStart & 0x00ff);                 // 4   Timer1 Start Offset
      bCfg[39] = (byte)((m_T1CalStart & 0xff00) >> 8);

      bCfg[40] = (byte)(m_T1CalPrd & 0x00ff);                   // 6   Timer1 Period Calibration 
      bCfg[41] = (byte)((m_T1CalPrd & 0xff00) >> 8);

      bCfg[42] = (byte)(m_T1Period & 0x00ff);                   // 8   Timer1 Period
      bCfg[43] = (byte)((m_T1Period & 0xff00) >> 8);

      bCfg[44] = (byte)(m_T1Prescale & 0x00ff);                 // 10  Timer1 Prescale
      bCfg[45] = (byte)((m_T1Prescale & 0xff00) >> 8);

      bCfg[46] = (byte)(m_Vref & 0x00ff);                       // 12  OpAmp Voltage Reference in milli-volts  Default = 2500 = 2.5V
      bCfg[47] = (byte)((m_Vref & 0xff00) >> 8);

      bCfg[48] = (byte)(m_Use_GPS & 0x00ff);                    // 14  1 = Use GPS PPS on INT0, else use Interval Timer1 on continutiously, 2 = Use GPS Time, 3 = Use Both
      bCfg[49] = (byte)((m_Use_GPS & 0xff00) >> 8);

      for (int ii = 0; ii < 8; ii++)
      {
        bCfg[50 + (ii * 2)] = (byte)(m_DetectA[ii] & 0x00ff);
        bCfg[51 + (ii * 2)] = (byte)((m_DetectA[ii] & 0xff00) >> 8);
      }
      Crc1 = Crc.Calc_CCITT(bCfg, 34, 32);
      bCfg[66] = (byte)((Crc1 & 0xff00) >> 8);
      bCfg[67] = (byte)(Crc1 & 0x00ff);

      bCfg[68] = (byte)((Crc2 & 0xff00) >> 8);
      bCfg[69] = (byte)(Crc2 & 0x00ff);

      bCfg[70] = (byte)(m_Time_Base & 0x000000ff);
      bCfg[71] = (byte)((m_Time_Base & 0x0000ff00) >> 8);
      bCfg[72] = (byte)((m_Time_Base & 0x00ff0000) >> 16);
      bCfg[73] = (byte)((m_Time_Base & 0xff000000) >> 24);

      for (int ii = 0; ii < 16; ii++)
      {
        bCfg[74 + ii] = (byte)m_PMU_Station[ii];
      }

      bCfg[89] = (byte)(m_GPS_Interval & 0x00ff);
      bCfg[90] = (byte)((m_GPS_Interval & 0xff00) >> 8);

      for (int ii = 0; ii < 10; ii++)
      {
        bCfg[91 + ii] = (byte)m_PMU_Stuff[ii];
      }
      Crc2 = Crc.Calc_CCITT(bCfg, 68, 32);
      bCfg[100] = (byte)((Crc0 & 0xff00) >> 8);
      bCfg[101] = (byte)(Crc0 & 0x00ff);
      
      return sResult;
    }


    #endregion

    
    public static string Disconnect()
    {
      /// Disconnect from the Sensor
      string sResult = "";
      try
      {
        if (ComPorts.Port1 != null)
        {
          if (ComPorts.Port1.IsOpen)
          {
            // Clear Port Buffers and Close Port
            ComPorts.Port1.DiscardOutBuffer();
            ComPorts.Port1.DiscardInBuffer();
            ComPorts.Port1.Close();
          }
          ComPorts.Port1.Dispose();
          ComPorts.Port1 = null;

          //  Clear Processing Buffers
          ComPorts.Port1_Data.Clear();
          ComPorts.Port1_Buff.Clear();

          GC.Collect();
        }
        sResult = "OK";
      }
      catch (Exception ex)
      {
        sResult = "Sensor Disconnect Error: " + ex.Message;
      }
      return sResult;
    }


    public static string Connect()
    {
      /// Connect to the Sensor
      //  First make sure it is not already connected
      string sResult = Disconnect();
      if ((sResult == "OK") && (ComPorts.Port1 == null))
      {
        // Configure the RS232 Port
        ComPorts.Port1 = new SerialPort(ComPorts.Port1_Name, ComPorts.Port1_Baud, ComPorts.Port1_Parity, ComPorts.Port1_DataBits, ComPorts.Port1_StopBits);
        ComPorts.Port1.Handshake = ComPorts.Port1_Handshake;
        ComPorts.Port1.ReadBufferSize = ComPorts.Port1_ReadBufferSize;
        ComPorts.Port1.ReadTimeout = ComPorts.Port1_ReadTimeout;
        ComPorts.Port1.WriteBufferSize = ComPorts.Port1_WriteBufferSize;
        ComPorts.Port1.WriteTimeout = ComPorts.Port1_WriteTimeout;
        ComPorts.Port1.ReceivedBytesThreshold = ComPorts.Port1_ReceivedBytesThreshold;
        ComPorts.Port1.NewLine = CRLF;
        sResult = "OK";
      }
      else
      {
        sResult =  "Sensor Connection is STUCK.  Consider restarting the program.";
      }
      return sResult;
    }


    //// Program Modes
    // Normal Operation Modes, Persistent when saved in and restored from EEPROM
    // Only One Program Mode is enabled
    public const byte PROG_IDLE = 0x00;      // Run Without Transmitting Data
    public const byte PROG_TEXT = 0x01;     // Streaming ASCII Text Mode
    public const byte PROG_PMU = 0x02;     // PMU Binary Packets Mode

    // User initiated Modes that stay Active until device reset
    // These revert to a Normal Operational Mode read from EEPROM after device reset
    public const byte PROG_EXIT = 0xFF;      // Exit main() commanded

    // Modes that revert to the EEPROM Saved Mode after completion
    public const byte PROG_REBOOT = 0xFE;    // Load Config from EEPROM and Reset
    public const byte PROG_RESET = 0xFD;     // Reset without EEPROM Reload
    public const byte PROG_FACTORY = 0xFC;   // Reload with Factory Defaults

    //// Diagnostics Modes - Used during Normal Program Operation
    //   Typically these modes light the Activity LED for the duration of the operation
    //   Multiple Diagnostics Modes can be enabled concurrently
    
    // No diagnostics, Run optimized
    // 0000 0000 Do not transmit diagnostic data
    public const UInt16 DIAG_NONE = 0x0000;

    // **** **** **** ***1 Main Loop Duration
    public const UInt16 DIAG_MAIN = 0x0001;

    // **** **** **** **1* UART Command Interpreter
    public const UInt16 DIAG_UCMD = 0x0002;

    // Transmit E[0] Elapse Time from IC1 to PPS
    public const UInt16 DIAG_PPS_E0 = 0x0003;

    // **** **** **** *1** Data Transmit
    // Frequency Transmit Duration
    public const UInt16 DIAG_HZTX = 0x0008;

    // Transmit Data on Timer1 Interval
    public const UInt16 DIAG_INTERVAL = 0x0007;

    // Input Capture ISR Duration 
    // **** **** **** 1*** Input Capture Channel 1
    public const UInt16 DIAG_INCAP1 = 0x0010;
    // **** **** ***1 **** Input Capture Channel 1
    public const UInt16 DIAG_INCAP2 = 0x0020;

    // Timer1 and Timer3 ISR Duration (overflow pulses)
    // **** **** **1* **** Timer1 Overflow
    public const UInt16 DIAG_TIMER1 = 0x0040;
    //-- public const UInt16 DIAG_TIMER3 = 0x0040;
   
    // EEPROM Routines Durations
    public const UInt16 DIAG_EEPROM = 0x0080;

    // UART Operation Duration
    // ***1 111* **** **** UART Operation Duration
    public const UInt16 DIAG_UTX1 = 0x0200;
    public const UInt16 DIAG_URX1 = 0x0400;
    public const UInt16 DIAG_URX2 = 0x1000;

    // Zero Crossing Span
    // 001* **** **** **** Zero Crossing Span
    public const UInt16 DIAG_ZXING = 0x2000;

    // 01** **** **** ***** INT0 Duration, GPS PPS Signal
    public const UInt16 DIAG_INT0 = 0x4000;

    // INT1, INT2 Duration 
    public const UInt16 DIAG_INTX = 0x4000;

    // 1000 **** **** **** Togggle Activity LED in Idle Mode   
    public const UInt16 DIAG_IDLE = 0x8000;


    // Sensor Commands
    public static byte[] CX_PING = { (byte)0x40, (byte)0x40 };          // @@ - Ping and then set to IDLE Mode
    public static byte[] CX_PROG_IDLE = { (byte)0x40, (byte)0x30 };     // @0 - No Data Transmition Idle Mode
    public static byte[] CX_PROG_TEXT = { (byte)0x40, (byte)0x31 };     // @1 - Transmit Streaming Text Data Mode
    public static byte[] CX_PROG_PMU = { (byte)0x40, (byte)0x32 };      // @2 - Transmit Binary IEEE C37.118 Data Mode
    public static byte[] CX_PROG_RSPT = { (byte)0x40, (byte)0x33 };     // @3 - RS232 Pass-through Mode

    public static byte[] CX_INTERVAL = { (byte)0x40, (byte)0x39, (byte)0x0A }; // @9 + (byte)Intervals, e.g. 0x0A = 10 samples per second

    public static byte[] CX_FACTORY = { (byte)0x40, (byte)0x21 };       // @! - Reset to Factory Defaults
    public static byte[] CX_REBOOT = { (byte)0x40, (byte)0x23 };        // @# - Reboot; Reloads EEPROM Config
    public static byte[] CX_RESET = { (byte)0x40, (byte)0x2A };         // @* - Reset; Restarts with running configuration, zeros accumulators, etc.

    public static byte[] CX_HOST_SYNC = { (byte)0x40, (byte)0x2E };     // @. - Sync to HOST on 3rd byte (anything; "." is ok)
    public static byte[] CX_HOST_SYNC_GO = { (byte)0x2E };              // .  - 3rd Sync byte

    public static byte[] CX_NOMINAL_HZ = { (byte)0x40, (byte)0x44 };    // @D2 - Set Nominal Hz with next byte
    public static byte[] CX_CALIBRATE = { (byte)0x40, (byte)0x45 };     // @E - Send Calibration Values
    public static byte[] CX_TX_CONFIG = { (byte)0x40, (byte)0x43 };     // @C - Tell Sensor to Transmit its Configuration
    public static byte[] CX_RX_CONFIG = { (byte)0x40, (byte)0x42 };     // @B - Send Configuration to Sensor
    
    
    public static void Port1_Send(byte[] bBuff)
    {
      ComPorts.Port1.Write(bBuff, 0, bBuff.Length);
    }


    public static void Port1_Send(byte b0)
    {
      // Send 2 bytes out the Serial Port
      byte[] bytes1 = new byte[1];
      bytes1[0] = b0;
      ComPorts.Port1.Write(bytes1, 0, 1);
    }


    public static void Port1_Send(byte b0, byte b1)
    {
      // Send 2 bytes out the Serial Port
      byte[] bytes2 = new byte[2];
      bytes2[0] = b0;
      bytes2[1] = b1;
      ComPorts.Port1.Write(bytes2, 0, 2);
    }

    public static void Port1_Send(byte b0, byte b1, byte b2)
    {
      // Send 3 bytes out the Serial Port
      byte[] bytes3 = new byte[3];
      bytes3[0] = b0;
      bytes3[1] = b1;
      bytes3[2] = b2;
      ComPorts.Port1.Write(bytes3, 0, 3);
    }

    public static void Port1_Send(Int16 iVal)
    {
      byte[] bytes2 = new byte[2];
      bytes2[0] = (byte)(((UInt16)iVal & (UInt16)0xFF00) >> 8);
      bytes2[1] = (byte)((UInt16)iVal & (UInt16)0x00FF);
      ComPorts.Port1.Write(bytes2, 0, 2);
    }


    public static string Ping()
    {
      string sResult = "PING!";
      try
      {
        Sync_to_PC();
        Port1_Send(CX_PING);
      }
      catch (Exception ex)
      {
        sResult = "Ping Error:  " + ex.Message;
      }
      return sResult;
    }


    public static void Set_Nominal_Hz(byte iHz)
    {
      Port1_Send(CX_NOMINAL_HZ);
      Port1_Send(iHz);
    }


    public static void Calibrate_Sensor()
    {
      Port1_Send(CX_CALIBRATE);
      Port1_Send(m_HzCalOffset);
    }


    public static void Sync_to_PC()
    { /// 1.0.9.14
      // Send a Synchronize Command to the Sensor on the 1 second roll-over 
      Port1_Send(CX_HOST_SYNC);
      DateTime myDT = PHzPacketClass.PreciseDT.UtcNow.AddSeconds(1);
      while (PHzPacketClass.PreciseDT.UtcNow < myDT)
      {
        // stay here for up to 0.999 seconds - sorry to block the thread!
        if (Math.Abs(PHzPacketClass.PreciseDT.UtcNow.Subtract(myDT).Milliseconds) > 1000) { break; }
      };
      PHzPacketClass.Last_PPS = myDT;
      Port1_Send(CX_HOST_SYNC_GO);
      ComPorts.Port1_Buff.Add("Sync to PC at -> " + PHzPacketClass.PreciseDT.UtcNow.ToString(Cfg.Date_FormatString + " HH:mm:ss"));
    }


    public static string Run_PHzMode(bool bSyncToPC)
    {
      // Run the Sensor in PHz Text Mode with the option to first synchronize with the PC
      string sResult = "";
      try
      {
        if (bSyncToPC)
        {
          Sync_to_PC();
        }
        Port1_Send(CX_PROG_TEXT);
        sResult = "OK";
      }
      catch (Exception ex)
      {
        sResult = "Run PHz Mode Error:  " + ex.Message;
      }
      return sResult;
    }


    public static bool Config_Ready = false;
    public static bool Config_Error = false;
    public static byte[] Config_Buff = new byte[102];  // (32 + 2) * 3 = 102

    public static string Get_ConfigFromSensor()
    { /// 1.0.9.6
      Config_Ready = false;
      Config_Error = false;
      ComPorts.Port1.Write(CX_TX_CONFIG, 0, 2);
      int its = 0;
      while (!Config_Ready)
      {
        System.Threading.Thread.Sleep(100);
        its++;
        if (its > 40)
        {
          Config_Error = true;
          break;
        }
      }
      if (Config_Error)
      {
        if (its > 40)
        {
          return "Sensor Configuration Retrieval Timeout!";
        }
        else
        {
          return "Sensor Configuration Retrieval Error!";
        }
      }
      else
      {
        // We have the Sensor Configuration in the Config_Buff
        // We need to decode the 32 byte sections and do their CRCs
        Load_Config(Config_Buff);
        return "Sensor Configuration Retrieved OK!";
      }
    }


    public static string Send_ConfigToSensor()
    {
      // Send the entire configuration to the sensor
      Config_Ready = false;
      Config_Error = false;
      
      string sResult = Stuff_Config(Config_Buff);

      ComPorts.Port1.Write(CX_RX_CONFIG, 0, 2);
      System.Threading.Thread.Sleep(100);

      for (int ii = 0; ii < Config_Buff.Length; ii++)
      {
        ComPorts.Port1.Write(Config_Buff, ii, 1);
        System.Threading.Thread.Sleep(10);
      }
      System.Threading.Thread.Sleep(100);
      return "Sensor Configuration Sent = " + sResult;
    }


  }
}
