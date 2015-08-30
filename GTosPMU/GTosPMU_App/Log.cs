using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GTosPMU
{
  /// <summary>
  /// 05/12/13 1.0.9.14  Date_FormatString config option added
  /// </summary>
  class Log
  {
    public const string THIS_NAME = "Log";

    public static UInt64 Session_Nbr = 0;
    public static string SessionFile = @"\Session.txt";

    public static string Log_Folder = Application.StartupPath + @"\Logs";
    private static int DaysToKeepLogs = 366;

    public const string CRLF = "\x000d\x000a";
    public const string MDY = "MM/dd/yy";
    public const string HMS = "HH:mm:ss";
    public const string HMSF = "HH:mm:ss.fff";
    public const string MDYHMS = "MM/dd/yy HH:mm:ss";
    public const string MDYHMSF = "MM/dd/yy HH:mm:ss.fff";
    public const string YMD = "yyyyMMdd";
    public const string YMD_H = "yyyyMMdd_HH";
    public const string YMD_HM = "yyyyMMdd_HHmm";
    public const string DATETIME_STAMP = "yyyyMMdd_HHmmssfff";


    // Std Output Types
    public enum LogDevice
    {
      LOG = 0x01,
      CON = 0x02,
      LOG_CON = 0x03,
      DLG = 0x04,
      LOG_DLG = 0x05,
      CON_DLG = 0x06,
      LOG_CON_DLG = 0x07
    }


    public static void Initialize()
    {
      Session_Nbr = New_SessionNbr();
    }


    public static UInt64 New_SessionNbr()
    {
      Session_Nbr = 0;
      try
      {
        if (!Directory.Exists(Log_Folder))
        {
          Directory.CreateDirectory(Log_Folder);
        }
        if (File.Exists(Log_Folder + SessionFile))
        {
          Session_Nbr = Convert.ToUInt64(File.ReadAllLines(Log_Folder + SessionFile)[0]);
          File.Delete(Log_Folder + SessionFile);
        }
      }
      catch (Exception ex)
      {
        Err(ex, THIS_NAME, "New_SessionNbr", LogDevice.LOG_CON_DLG);
        Session_Nbr = 0;
      }

      Session_Nbr++;
      try
      {
        System.IO.StreamWriter ioSW = System.IO.File.AppendText(Log_Folder + SessionFile);
        ioSW.WriteLine(Session_Nbr.ToString());
        ioSW.Flush();
        ioSW.Close();
        ioSW = null;
      }
      catch (Exception ex)
      {
        Err(ex, THIS_NAME, "New_SessionNbr.Save[" + Log_Folder + SessionFile + "]", LogDevice.LOG_CON_DLG);
      }
      return Session_Nbr;
    }


    public static string FullPath()
    {
      // Full File Path for the Trace Log File
      if (!Directory.Exists(Log_Folder))
      {
        Directory.CreateDirectory(Log_Folder);
      }
      return Log_Folder + @"\Log_" + DateTime.Now.Date.ToString(YMD) + ".txt";
    }


    public static void Info(string sContext, string sMsg, LogDevice devLog)
    { /// 1.0.9.14
      // Info with Ellapsed Time instead of Date Time
      string sMsg1 = "Info - " + DateTime.Now.ToString(Cfg.Date_FormatString + " " + HMSF).ToString() + " [" + Session_Nbr.ToString() + "], " + sMsg;
      if ((devLog & LogDevice.LOG) == LogDevice.LOG)
      {
        AppendLog(sMsg1);
      }
      if ((devLog & LogDevice.CON) == LogDevice.CON)
      {
        Console.WriteLine(sMsg1);
      }
      if ((devLog & LogDevice.DLG) == LogDevice.DLG)
      {
        MessageBox.Show(sMsg1, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }


    public static void Err(Exception ex, string sContext, string sMsg, LogDevice devLog)
    { /// 1.0.9.14
      string sMsg1 = "Err  - " + DateTime.Now.ToString(Cfg.Date_FormatString + " " + HMSF).ToString() + "[" + Session_Nbr.ToString() + "], "
             + sContext + ":  " + sMsg + CRLF + ex.Source + ":" + CRLF + ex.Message;
      if (ex.InnerException != null)
      {
        sMsg1 += sMsg1 + CRLF + ex.InnerException.Message;
      }
      if ((devLog & LogDevice.LOG) == LogDevice.LOG)
      {
        AppendLog(sMsg1);
      }
      if ((devLog & LogDevice.CON) == LogDevice.CON)
      {
        Console.WriteLine(sMsg1);
      }
      if ((devLog & LogDevice.DLG) == LogDevice.DLG)
      {
        MessageBox.Show(sMsg1, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }


    public static void AppendLog(string sText)
    {
      // Append a Text String to a File with CrLf
      try
      {
        System.IO.StreamWriter ioSW = System.IO.File.AppendText(FullPath());
        ioSW.WriteLine(sText);
        ioSW.Flush();
        ioSW.Close();
        ioSW = null;
      }
      catch (Exception ex)
      {
        Err(ex, THIS_NAME, "AppendLog[" + FullPath() + "]" + "\n" + sText, LogDevice.LOG_CON_DLG);
      }
    }


    public static void Close()
    {
      Info(THIS_NAME, "Log Closed", LogDevice.LOG);
    }


    public static void PurgeLogs()
    {
      try
      {
        string[] aryFileNames = System.IO.Directory.GetFiles(System.IO.Path.GetDirectoryName(FullPath()), "*.txt");
        for (int ii = 0; ii <= aryFileNames.GetUpperBound(0); ii++)
        {
          if (System.DateTime.Compare(System.IO.File.GetLastWriteTime(aryFileNames[ii]).AddDays(DaysToKeepLogs), System.DateTime.Now) < 0)
          {
            Info(THIS_NAME, "Purging Log:  " + System.IO.Path.GetFileName(aryFileNames[ii]), LogDevice.LOG_CON);
            System.IO.File.Delete(aryFileNames[ii]);
          }
        }
      }
      catch (Exception ex)
      {
        Err(ex, THIS_NAME, "PurgeLogs", LogDevice.LOG_CON_DLG);
      }
    }

  }
}
