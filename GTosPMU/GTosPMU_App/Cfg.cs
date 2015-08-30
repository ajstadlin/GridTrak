using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GTosPMU
{
  /// GTosPMU Configuration 
  /// 05/12/13 1.0.9.14  Date_FormatString config option added
  /// 04/18/13 1.0.9.12  HzChart_Adjust configuration value added for shifting the Frequency Chart displayed data
  /// 04/17/13 1.0.9.11  Late PPS Exception and Time value configuration paramenters added.  Late PPS exception disabled by default
  /// 
  class Cfg
  {
    public const string APP_VER = "1.0.9.14";

    // Data Type Identifiers
    public const string SYS_BYTE = "System.Byte";
    public const string SYS_STRING = "System.String";
    public const string SYS_BOOLEAN = "System.Boolean";
    public const string SYS_INT32 = "System.Int32";
    public const string SYS_INT16 = "System.Int16";
    public const string SYS_UINT16 = "System.UInt16";
    public const string SYS_UINT32 = "System.UInt32";
    public const string SYS_INT64 = "System.Int64";
    public const string SYS_SINGLE = "System.Single";

    public const string CRLF = "\x000d\x000a";          // Input EOL

    public static string File_Path = Application.StartupPath + @"\Config.xml";


    /// Configuration DataSet Schema
    public static DataSet XDS = null;
    public const string DS_CONFIG = "Config_DS";

    /// User Preferences
    public const string T_PREFERENCES = "Preferences_TBL";
    public const Int32 DEF_STATUS_TIMERINTERVAL = 10000;
    public const string C_STATUS_TIMERINTERVAL = "Status_TimerInterval";

    public const string C_APP_VER = "App_Ver";
    public const string C_IS_PICK = "Is_Pick";

    public const string DEF_DATE_FORMATSTRING = "MM/dd/yy";
    public const string C_DATE_FORMATSTRING = "Date_FormatString";

    /// Terminal Properties
    public const string T_TERMINAL = "Terminal_TBL";
    public const Int32 DEF_TERM_MAXLINES = 200;
    public const string C_TERM_MAXLINES = "Term_MaxLines";

    public const Int32 DEF_TERM_TIMERINTERVAL = 20;
    public const string C_TERM_TIMERINTERVAL = "Term_TimerInterval";

    public const bool DEF_TERMLOG_ENABLE = false;
    public const string C_TERMLOG_ENABLE = "TermLog_Enable";

    public const Int32 DEF_DATALOG_FILEINTERVAL = 10;
    public const string C_DATALOG_FILEINTERVAL = "DataLog_FileInterval";

    public const string DEF_DATALOG_FILENAMEFORMAT = "yyyyMMdd_HHmm";
    public const string C_DATALOG_FILENAMEFORMAT = "DataLog_FileFormat";

    public const bool DEF_DATALOG_ENABLE = false;
    public const string C_DATALOG_ENABLE = "DataLog_Enable";

    public static string DEF_DATALOG_FOLDER = Application.StartupPath + @"\Data";
    public const string C_DATALOG_FOLDER = "DataLog_Folder";

    public const Int32 DEF_DATAPURGE_TIMERINTERVAL = 1000;
    public const string C_DATAPURGE_TIMERINTERVAL = "DataPurge_TimerInterval";

    /// Graphics Display Properties
    public const string T_DISPLAY = "Display_TBL";
    public const Int32 DEF_DISPLAY_TIMERINTERVAL = 1000;
    public const string C_DISPLAY_TIMERINTERVAL = "Display_TimerInterval";

    public const Int32 DEF_DISPLAY_LIVEHIST = 0;
    public const string C_DISPLAY_LIVEHIST = "Live_History";

    public const Int32 DEF_HZCHART_SCALE = 2;
    public const string C_HZCHART_SCALE = "HzChart_Scale";

    public const float DEF_HZCHART_ADJUST = 0.0f;
    public const string C_HZCHART_ADJUST = "HzChart_Adjust";

    public const float DEF_HZSPECT_SCALE = 0.5f;
    public const string C_HZSPECT_SCALE = "HzSpect_Scale";

    public const string T_ACCOUNT = "Account_TBL";
    public const Int64 DEF_ACCOUNT_ID = 0;
    public const string C_ACCOUNT_ID = "Account_Id";
    public const string C_ACCOUNT_EMAIL = "Account_EMail";
    public const string C_ACCOUNT_PSWD = "Account_Pswd";


    public static void New_Config()
    { /// 1.0.9.14
      if (XDS != null)
      {
        XDS.Dispose();
        XDS = null;
        GC.Collect();
      }
      XDS = new DataSet(DS_CONFIG);

      XDS.Tables.Add(Sensor.New_CfgTable());
      XDS.AcceptChanges();

      XDS.Tables.Add(ComPorts.New_CfgTable());
      XDS.AcceptChanges();

      DataTable t2 = new DataTable(T_TERMINAL);
      t2.Columns.Add(new DataColumn(C_TERM_MAXLINES, Type.GetType(SYS_INT32)));
      t2.Columns[C_TERM_MAXLINES].DefaultValue = DEF_TERM_MAXLINES;

      t2.Columns.Add(new DataColumn(C_TERM_TIMERINTERVAL, Type.GetType(SYS_INT32)));
      t2.Columns[C_TERM_TIMERINTERVAL].DefaultValue = DEF_TERM_TIMERINTERVAL;

      t2.Columns.Add(new DataColumn(C_TERMLOG_ENABLE, Type.GetType(SYS_BOOLEAN)));
      t2.Columns[C_TERMLOG_ENABLE].DefaultValue = DEF_TERMLOG_ENABLE;

      t2.Columns.Add(new DataColumn(C_DATALOG_ENABLE, Type.GetType(SYS_BOOLEAN)));
      t2.Columns[C_DATALOG_ENABLE].DefaultValue = DEF_DATALOG_ENABLE;

      t2.Columns.Add(new DataColumn(C_DATALOG_FOLDER, Type.GetType(SYS_STRING)));
      t2.Columns[C_DATALOG_FOLDER].DefaultValue = DEF_DATALOG_FOLDER;

      t2.Columns.Add(new DataColumn(C_DATALOG_FILEINTERVAL, Type.GetType(SYS_INT32)));
      t2.Columns[C_DATALOG_FILEINTERVAL].DefaultValue = DEF_DATALOG_FILEINTERVAL;

      t2.Columns.Add(new DataColumn(C_DATALOG_FILENAMEFORMAT, Type.GetType(SYS_STRING)));
      t2.Columns[C_DATALOG_FILENAMEFORMAT].DefaultValue = DEF_DATALOG_FILENAMEFORMAT;

      t2.Columns.Add(new DataColumn(C_DATAPURGE_TIMERINTERVAL, Type.GetType(SYS_INT32)));
      t2.Columns[C_DATAPURGE_TIMERINTERVAL].DefaultValue = DEF_DATAPURGE_TIMERINTERVAL;

      t2.Rows.Add(t2.NewRow());
      t2.AcceptChanges();
      XDS.Tables.Add(t2);
      XDS.AcceptChanges();

      DataTable t3 = new DataTable(T_DISPLAY);
      t3.Columns.Add(new DataColumn(C_DISPLAY_TIMERINTERVAL, Type.GetType(SYS_INT32)));
      t3.Columns[C_DISPLAY_TIMERINTERVAL].DefaultValue = DEF_DISPLAY_TIMERINTERVAL;

      t3.Columns.Add(new DataColumn(C_DISPLAY_LIVEHIST, Type.GetType(SYS_INT32)));
      t3.Columns[C_DISPLAY_LIVEHIST].DefaultValue = DEF_DISPLAY_LIVEHIST;

      t3.Columns.Add(new DataColumn(C_HZCHART_SCALE, Type.GetType(SYS_INT32)));
      t3.Columns[C_HZCHART_SCALE].DefaultValue = DEF_HZCHART_SCALE;

      t3.Columns.Add(new DataColumn(C_HZCHART_ADJUST, Type.GetType(SYS_SINGLE)));
      t3.Columns[C_HZCHART_ADJUST].DefaultValue = DEF_HZCHART_ADJUST;

      t3.Columns.Add(new DataColumn(C_HZSPECT_SCALE, Type.GetType(SYS_SINGLE)));
      t3.Columns[C_HZSPECT_SCALE].DefaultValue = DEF_HZSPECT_SCALE;

      t3.Rows.Add(t3.NewRow());
      t3.AcceptChanges();
      XDS.Tables.Add(t3);
      XDS.AcceptChanges();

      DataTable t4 = new DataTable(T_PREFERENCES);
      t4.Columns.Add(new DataColumn(C_APP_VER, Type.GetType(SYS_STRING)));
      t4.Columns[C_APP_VER].DefaultValue = APP_VER;

      t4.Columns.Add(new DataColumn(C_STATUS_TIMERINTERVAL, Type.GetType(SYS_INT32)));
      t4.Columns[C_STATUS_TIMERINTERVAL].DefaultValue = DEF_STATUS_TIMERINTERVAL;

      t4.Columns.Add(new DataColumn(C_DATE_FORMATSTRING, Type.GetType(SYS_STRING)));
      t4.Columns[C_DATE_FORMATSTRING].DefaultValue = DEF_DATE_FORMATSTRING;

      t4.Rows.Add(t4.NewRow());
      t4.AcceptChanges();
      XDS.Tables.Add(t4);
      XDS.AcceptChanges();

      DataTable t5 = new DataTable(T_ACCOUNT);
      t5.Columns.Add(new DataColumn(C_ACCOUNT_ID, Type.GetType(SYS_INT64)));
      t5.Columns[C_ACCOUNT_ID].DefaultValue = DEF_ACCOUNT_ID;
      t5.Columns.Add(new DataColumn(C_ACCOUNT_EMAIL, Type.GetType(SYS_STRING)));
      t5.Columns[C_ACCOUNT_EMAIL].DefaultValue = "";
      t5.Columns.Add(new DataColumn(C_ACCOUNT_PSWD, Type.GetType(SYS_STRING)));
      t5.Columns[C_ACCOUNT_PSWD].DefaultValue = "";

      t5.Rows.Add(t5.NewRow());
      t5.AcceptChanges();
      XDS.Tables.Add(t5);
      XDS.AcceptChanges();


      // Remove the old Configuration file
      if (File.Exists(File_Path))
      {
        try
        {
          File.Delete(File_Path);
        }
        catch (Exception ex)
        {
          Log.Err(ex, "Sensor, New_Config", "Error Deleting Old Config File: " + File_Path, Log.LogDevice.LOG_DLG);
        }
      }

      // Create a new Default Configuration file
      try
      {
        XDS.WriteXml(File_Path, XmlWriteMode.WriteSchema);
      }
      catch (Exception ex)
      {
        Log.Err(ex, "Sensor, New_Config", "Error Creating New Config File: " + File_Path, Log.LogDevice.LOG_DLG);
      }

      // Note:  If we fail to Delete the Old Config or Save a New Config, we now have the Default Configuration anyway.
    }


    public static string Load()
    {
      /// Load the Sensor Configuration
      string sResult = "";
      try
      {
        // Clear any existing configuration
        if (XDS != null)
        {
          XDS.Dispose();
          XDS = null;
          GC.Collect();
        }

        if (File.Exists(File_Path))
        {
          // Read the configuration DataSet from disk
          XDS = new DataSet(DS_CONFIG);
          XDS.ReadXml(File_Path, XmlReadMode.Auto);
        }
      }
      catch (Exception ex)
      {
        if (XDS != null)
        {
          XDS.Dispose();
          XDS = null;
          GC.Collect();
        }
        sResult = "Error Reading Config File: " + File_Path;
        Log.Err(ex, "Sensor, Load_Config", sResult, Log.LogDevice.LOG_DLG);
      }

      /// Check the Version
      try
      {
        if (XDS == null)
        {
          New_Config();
          sResult = "New Configuration Loaded";
        }
        else
        {
          try
          {
            if ((XDS.Tables.Count > 1)
                && (XDS.Tables[T_PREFERENCES].Rows.Count == 1)
                && (XDS.Tables[T_PREFERENCES].Rows[0][C_APP_VER].ToString() == APP_VER)
                && (XDS.Tables[Sensor.T_SENSOR].Rows.Count == 1)
                && (XDS.Tables[ComPorts.T_PORT1].Rows.Count == 1)
                && (Convert.ToUInt16(XDS.Tables[Sensor.T_SENSOR].Rows[0][Sensor.C_SENSOR_VER]) == Sensor.Sensor_Ver))
            {
              // Probably a good current Configuration!
              sResult = "Configuration Loaded";
              Log.Info("Sensor, Load_Config", sResult + ": " + File_Path, Log.LogDevice.LOG);
            }
            else
            {
              // Invalid Configuration
              sResult = "Unrecognized Configuration or Upgrade Required.  Creating a New Configuration Version " + APP_VER;
              Log.Info("Sensor, Load_Config", sResult + ": " + File_Path, Log.LogDevice.LOG);
              New_Config();
            }
          }
          catch (Exception ex)
          {
            Log.Err(ex, "Cfg.", "Load()", Log.LogDevice.LOG_DLG);
            New_Config();
          }
        }

        Sensor.Load_Config();
        ComPorts.Load_Config();

        m_Date_FormatString = XDS.Tables[T_PREFERENCES].Rows[0][C_DATE_FORMATSTRING].ToString();
        m_Status_TimerInterval = Convert.ToInt32(XDS.Tables[T_PREFERENCES].Rows[0][C_STATUS_TIMERINTERVAL]);
        m_Term_MaxLines = Convert.ToInt32(XDS.Tables[T_TERMINAL].Rows[0][C_TERM_MAXLINES]);
        m_Term_TimerInterval = Convert.ToInt32(XDS.Tables[T_TERMINAL].Rows[0][C_TERM_TIMERINTERVAL]);
        m_TermLog_Enable = Convert.ToBoolean(XDS.Tables[T_TERMINAL].Rows[0][C_TERMLOG_ENABLE]);
        m_DataLog_Enable = Convert.ToBoolean(XDS.Tables[T_TERMINAL].Rows[0][C_DATALOG_ENABLE]);
        m_DataLog_FileInterval = Convert.ToInt32(XDS.Tables[T_TERMINAL].Rows[0][C_DATALOG_FILEINTERVAL]);
        m_DataLog_FileNameFormat = XDS.Tables[T_TERMINAL].Rows[0][C_DATALOG_FILENAMEFORMAT].ToString();
        m_DataPurge_TimerInterval = Convert.ToInt32(XDS.Tables[T_TERMINAL].Rows[0][C_DATAPURGE_TIMERINTERVAL]);
        m_Display_TimerInterval = Convert.ToInt32(XDS.Tables[T_DISPLAY].Rows[0][C_DISPLAY_TIMERINTERVAL]);
        m_Display_LiveHist = Convert.ToInt32(XDS.Tables[T_DISPLAY].Rows[0][C_DISPLAY_LIVEHIST]);
        m_HzChart_Scale = Convert.ToInt32(XDS.Tables[T_DISPLAY].Rows[0][C_HZCHART_SCALE]);
        m_HzChart_Adjust = Convert.ToSingle(XDS.Tables[T_DISPLAY].Rows[0][C_HZCHART_ADJUST]);
        m_HzSpect_Scale = Convert.ToSingle(XDS.Tables[T_DISPLAY].Rows[0][C_HZSPECT_SCALE]);
        m_DataLog_Folder = XDS.Tables[T_TERMINAL].Rows[0][C_DATALOG_FOLDER].ToString();
        if (!Directory.Exists(m_DataLog_Folder))
        {
          try
          {
            Directory.CreateDirectory(m_DataLog_Folder);
          }
          catch (Exception ex)
          {
            Log.Err(ex, "Cfg, New_Config", "Error Creating Data Logging Folder: " + m_DataLog_Folder, Log.LogDevice.LOG_DLG);
          }
        }

        m_Account_Id = Convert.ToInt64(XDS.Tables[T_ACCOUNT].Rows[0][C_ACCOUNT_ID]);
        m_Account_EMail = XDS.Tables[T_ACCOUNT].Rows[0][C_ACCOUNT_EMAIL].ToString();
        m_Account_Pswd = XDS.Tables[T_ACCOUNT].Rows[0][C_ACCOUNT_PSWD].ToString();


      
      }
      catch (Exception ex)
      {
        sResult = "Error Loading Configuration File: " + File_Path;
        Log.Err(ex, "Cfg, Load_Config", sResult, Log.LogDevice.LOG_DLG);
      }
      return sResult;
    }


    public static string Save()
    {
      string sResult = "";
      // Remove the old Configuration file
      if (File.Exists(File_Path))
      {
        try
        {
          File.Delete(File_Path);
          XDS.AcceptChanges();
          XDS.WriteXml(File_Path, XmlWriteMode.WriteSchema);
          sResult = "Configuration Saved!";
        }
        catch (Exception ex)
        {
          sResult = "Error Saving Sensor Config File: " + File_Path;
          Log.Err(ex, "Config, Save_Config", sResult, Log.LogDevice.LOG_DLG);
        }
      }
      return sResult;
    }


    /// Misc Configuration Properties
    private static string m_Date_FormatString = DEF_DATE_FORMATSTRING;
    public static string Date_FormatString
    {
      get { return m_Date_FormatString; }
      set
      {
        m_Date_FormatString = value;
        XDS.Tables[T_PREFERENCES].Rows[0][C_DATE_FORMATSTRING] = m_Date_FormatString;
      }
    }

    private static Int32 m_Status_TimerInterval = DEF_STATUS_TIMERINTERVAL;
    public static Int32 Status_TimerInterval
    {
      get { return m_Status_TimerInterval; }
      set
      {
        m_Status_TimerInterval = value;
        XDS.Tables[T_PREFERENCES].Rows[0][C_STATUS_TIMERINTERVAL] = m_Status_TimerInterval;
      }
    }


    private static Int32 m_Term_MaxLines = DEF_TERM_MAXLINES;
    public static Int32 Term_MaxLines
    {
      get { return m_Term_MaxLines; }
      set
      {
        m_Term_MaxLines = value;
        XDS.Tables[T_TERMINAL].Rows[0][C_TERM_MAXLINES] = m_Term_MaxLines;
      }
    }

    private static Int32 m_Term_TimerInterval = DEF_TERM_TIMERINTERVAL;
    public static Int32 Term_TimerInterval
    {
      get { return m_Term_TimerInterval; }
      set
      {
        m_Term_TimerInterval = value;
        XDS.Tables[T_TERMINAL].Rows[0][C_TERM_TIMERINTERVAL] = m_Term_TimerInterval;
      }
    }


    private static bool m_TermLog_Enable = DEF_TERMLOG_ENABLE;
    public static bool TermLog_Enable
    {
      get { return m_TermLog_Enable; }
      set
      {
        m_TermLog_Enable = value;
        XDS.Tables[T_TERMINAL].Rows[0][C_TERMLOG_ENABLE] = m_TermLog_Enable;
      }
    }

    private static bool m_DataLog_Enable = DEF_DATALOG_ENABLE;
    public static bool DataLog_Enable
    {
      get { return m_DataLog_Enable; }
      set
      {
        m_DataLog_Enable = value;
        XDS.Tables[T_TERMINAL].Rows[0][C_DATALOG_ENABLE] = m_DataLog_Enable;
      }
    }


    private static Int32 m_DataLog_FileInterval = DEF_DATALOG_FILEINTERVAL;
    public static Int32 DataLog_FileInterval
    {
      get { return m_DataLog_FileInterval; }
      set
      {
        m_DataLog_FileInterval = value;
        XDS.Tables[T_TERMINAL].Rows[0][C_DATALOG_FILEINTERVAL] = m_DataLog_FileInterval;
      }
    }

    private static string m_DataLog_FileNameFormat = DEF_DATALOG_FILENAMEFORMAT;
    public static string DataLog_FileNameFormat
    {
      get { return m_DataLog_FileNameFormat; }
      set
      {
        m_DataLog_FileNameFormat = value.Trim();
        XDS.Tables[T_TERMINAL].Rows[0][C_DATALOG_FILENAMEFORMAT] = m_DataLog_FileNameFormat;
      }
    }

    private static string m_DataLog_Folder = DEF_DATALOG_FOLDER;
    public static string DataLog_Folder
    {
      get { return m_DataLog_Folder; }
      set
      {
        m_DataLog_Folder = value.Trim();
        XDS.Tables[T_TERMINAL].Rows[0][C_DATALOG_FOLDER] = m_DataLog_Folder;
      }
    }

    private static Int32 m_DataPurge_TimerInterval = DEF_DATAPURGE_TIMERINTERVAL;
    public static Int32 DataPurge_TimerInterval
    {
      get { return m_DataPurge_TimerInterval; }
      set
      {
        m_DataPurge_TimerInterval = value;
        XDS.Tables[T_TERMINAL].Rows[0][C_DATAPURGE_TIMERINTERVAL] = m_DataPurge_TimerInterval;
      }
    }



    private static Int32 m_Display_TimerInterval = DEF_DISPLAY_TIMERINTERVAL;
    public static Int32 Display_TimerInterval
    {
      get { return m_Display_TimerInterval; }
      set
      {
        m_Display_TimerInterval = value;
        XDS.Tables[T_DISPLAY].Rows[0][C_DISPLAY_TIMERINTERVAL] = m_Display_TimerInterval;
      }
    }

    private static Int32 m_Display_LiveHist = DEF_DISPLAY_LIVEHIST;
    public static Int32 Display_LiveHist
    {
      get { return m_Display_LiveHist; }
      set
      {
        m_Display_LiveHist = value;
        XDS.Tables[T_DISPLAY].Rows[0][C_DISPLAY_LIVEHIST] = m_Display_LiveHist;
      }
    }

    private static Int32 m_HzChart_Scale = DEF_HZCHART_SCALE;
    public static Int32 HzChart_Scale
    {
      get { return m_HzChart_Scale; }
      set
      {
        m_HzChart_Scale = value;
        XDS.Tables[T_DISPLAY].Rows[0][C_HZCHART_SCALE] = m_HzChart_Scale;
      }
    }

    private static Single m_HzChart_Adjust = DEF_HZCHART_ADJUST;
    public static Single HzChart_Adjust
    {
      get { return m_HzChart_Adjust; }
      set
      {
        m_HzChart_Adjust = value;
        XDS.Tables[T_DISPLAY].Rows[0][C_HZCHART_ADJUST] = m_HzChart_Adjust;
      }
    }

    private static float m_HzSpect_Scale = DEF_HZSPECT_SCALE;
    public static float HzSpect_Scale
    {
      get { return m_HzSpect_Scale; }
      set
      {
        m_HzSpect_Scale = value;
        XDS.Tables[T_DISPLAY].Rows[0][C_HZSPECT_SCALE] = m_HzSpect_Scale;
      }
    }


    private static Int64 m_Account_Id = DEF_ACCOUNT_ID;
    public static Int64 Account_Id
    {
      get { return m_Account_Id; }
      set
      {
        m_Account_Id = value;
        XDS.Tables[T_ACCOUNT].Rows[0][C_ACCOUNT_ID] = m_Account_Id;
      }
    }


    private static string m_Account_EMail = "";
    public static string Account_EMail
    {
      get { return m_Account_EMail; }
      set
      {
        m_Account_EMail = value;
        XDS.Tables[T_ACCOUNT].Rows[0][C_ACCOUNT_EMAIL] = m_Account_EMail;
      }
    }

    private static string m_Account_Pswd = "";
    public static string Account_Pswd
    {
      get { return m_Account_Pswd; }
      set
      {
        m_Account_Pswd = value;
        XDS.Tables[T_ACCOUNT].Rows[0][C_ACCOUNT_PSWD] = m_Account_Pswd;
      }
    }


  }
}
