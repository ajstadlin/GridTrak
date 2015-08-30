using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO.Ports;
using System.Text;

namespace GTosPMU
{
  public class ComPorts
  {
    //  using System.IO.Ports;
    public static SerialPort Port1 = null;
    public static ArrayList Port1_Buff = new ArrayList();   // Received Text Buffer
    public static ArrayList Port1_Data = new ArrayList();   // Received Data Packets; array of PHzPacketClass objects
    public static string Port1_Message = "";                // Message to Display in the Terminal

    private const string CRLF = "\x000d\x000a";          // Input EOL


    // Serial Port 1 Configuration Properties
    public const string T_PORT1 = "Port1_TBL";

    public const string C_PORT_NAME = "Port_Name";
    public const string DEF_PORT1_NAME = "COM1";
    private static string m_Port1_Name = DEF_PORT1_NAME;
    public static string Port1_Name
    {
      get { return m_Port1_Name; }
      set
      {
        m_Port1_Name = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_PORT_NAME] = m_Port1_Name;
      }
    }

    public const string C_BAUD = "Baud";
    public const Int32 DEF_PORT1_BAUD = 115200;
    private static Int32 m_Port1_Baud = DEF_PORT1_BAUD;
    public static Int32 Port1_Baud
    {
      get { return m_Port1_Baud; }
      set
      {
        m_Port1_Baud = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_BAUD] = m_Port1_Baud;
      }
    }

    public const string C_HANDSHAKE = "Handshake";
    public const Handshake DEF_PORT1_HANDSHAKE = Handshake.None;
    private static Handshake m_Port1_Handshake = DEF_PORT1_HANDSHAKE;
    public static Handshake Port1_Handshake
    {
      get { return m_Port1_Handshake; }
      set
      {
        m_Port1_Handshake = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_HANDSHAKE] = m_Port1_Handshake.ToString();
      }
    }

    public const string C_DATABITS = "DataBits";
    public const Int32 DEF_PORT1_DATABITS = 8;
    private static Int32 m_Port1_DataBits = DEF_PORT1_DATABITS;
    public static Int32 Port1_DataBits
    {
      get { return m_Port1_DataBits; }
      set
      {
        m_Port1_DataBits = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_DATABITS] = m_Port1_DataBits;
      }
    }

    public const string C_PARITY = "Parity";
    public const Parity DEF_PORT1_PARITY = Parity.None;
    private static Parity m_Port1_Parity = DEF_PORT1_PARITY;
    public static Parity Port1_Parity
    {
      get { return m_Port1_Parity; }
      set
      {
        m_Port1_Parity = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_PARITY] = m_Port1_Parity.ToString();
      }
    }

    public const string C_STOPBITS = "StopBits";
    public const StopBits DEF_PORT1_STOPBITS = StopBits.One;
    private static StopBits m_Port1_StopBits = DEF_PORT1_STOPBITS;
    public static StopBits Port1_StopBits
    {
      get { return m_Port1_StopBits; }
      set
      {
        m_Port1_StopBits = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_STOPBITS] = m_Port1_StopBits.ToString();
      }
    }

    public const string C_READBUFFERSIZE = "ReadBufferSize";
    public const Int32 DEF_PORT1_READBUFFERSIZE = 128;
    private static Int32 m_Port1_ReadBufferSize = DEF_PORT1_READBUFFERSIZE;
    public static Int32 Port1_ReadBufferSize
    {
      get { return m_Port1_ReadBufferSize; }
      set
      {
        m_Port1_ReadBufferSize = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_READBUFFERSIZE] = m_Port1_ReadBufferSize;
      }
    }

    public const string C_READTIMEOUT = "ReadTimeOut";
    public const Int32 DEF_PORT1_READTIMEOUT = 100;
    private static Int32 m_Port1_ReadTimeout = DEF_PORT1_READTIMEOUT;
    public static Int32 Port1_ReadTimeout
    {
      get { return m_Port1_ReadTimeout; }
      set
      {
        m_Port1_ReadTimeout = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_READTIMEOUT] = m_Port1_ReadTimeout;
      }
    }

    public const string C_WRITEBUFFERSIZE = "WriteBufferSize";
    public const Int32 DEF_PORT1_WRITEBUFFERSIZE = 128;
    private static Int32 m_Port1_WriteBufferSize = DEF_PORT1_WRITEBUFFERSIZE;
    public static Int32 Port1_WriteBufferSize
    {
      get { return m_Port1_WriteBufferSize; }
      set
      {
        m_Port1_WriteBufferSize = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_WRITEBUFFERSIZE] = m_Port1_WriteBufferSize;
      }
    }

    public const string C_WRITETIMEOUT = "WriteTimeOut";
    public const Int32 DEF_PORT1_WRITETIMEOUT = 100;
    private static Int32 m_Port1_WriteTimeout = DEF_PORT1_WRITETIMEOUT;
    public static Int32 Port1_WriteTimeout
    {
      get { return m_Port1_WriteTimeout; }
      set
      {
        m_Port1_WriteTimeout = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_WRITETIMEOUT] = m_Port1_WriteTimeout;
      }
    }

    public const string C_RECEIVEDBYTESTHRESHOLD = "ReceivedBytesThreshold";
    public const Int32 DEF_PORT1_RECEIVEDBYTESTHRESHOLD = 16;
    private static Int32 m_Port1_ReceivedBytesThreshold = DEF_PORT1_RECEIVEDBYTESTHRESHOLD;
    public static Int32 Port1_ReceivedBytesThreshold
    {
      get { return m_Port1_ReceivedBytesThreshold; }
      set
      {
        m_Port1_ReceivedBytesThreshold = value;
        Cfg.XDS.Tables[T_PORT1].Rows[0][C_RECEIVEDBYTESTHRESHOLD] = m_Port1_ReceivedBytesThreshold;
      }
    }


    public static DataTable New_CfgTable()
    {
      DataTable t0 = new DataTable(T_PORT1);

      t0.Columns.Add(new DataColumn(C_PORT_NAME, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_PORT_NAME].DefaultValue = DEF_PORT1_NAME;

      t0.Columns.Add(new DataColumn(C_BAUD, Type.GetType(Cfg.SYS_INT32)));
      t0.Columns[C_BAUD].DefaultValue = DEF_PORT1_BAUD;

      t0.Columns.Add(new DataColumn(C_HANDSHAKE, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_HANDSHAKE].DefaultValue = DEF_PORT1_HANDSHAKE.ToString();

      t0.Columns.Add(new DataColumn(C_DATABITS, Type.GetType(Cfg.SYS_INT32)));
      t0.Columns[C_DATABITS].DefaultValue = DEF_PORT1_DATABITS;

      t0.Columns.Add(new DataColumn(C_PARITY, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_PARITY].DefaultValue = DEF_PORT1_PARITY.ToString();

      t0.Columns.Add(new DataColumn(C_STOPBITS, Type.GetType(Cfg.SYS_STRING)));
      t0.Columns[C_STOPBITS].DefaultValue = DEF_PORT1_STOPBITS.ToString();

      t0.Columns.Add(new DataColumn(C_READBUFFERSIZE, Type.GetType(Cfg.SYS_INT32)));
      t0.Columns[C_READBUFFERSIZE].DefaultValue = DEF_PORT1_READBUFFERSIZE;

      t0.Columns.Add(new DataColumn(C_READTIMEOUT, Type.GetType(Cfg.SYS_INT32)));
      t0.Columns[C_READTIMEOUT].DefaultValue = DEF_PORT1_READTIMEOUT;

      t0.Columns.Add(new DataColumn(C_WRITEBUFFERSIZE, Type.GetType(Cfg.SYS_INT32)));
      t0.Columns[C_WRITEBUFFERSIZE].DefaultValue = DEF_PORT1_WRITEBUFFERSIZE;

      t0.Columns.Add(new DataColumn(C_WRITETIMEOUT, Type.GetType(Cfg.SYS_INT32)));
      t0.Columns[C_WRITETIMEOUT].DefaultValue = DEF_PORT1_WRITETIMEOUT;
  
      t0.Columns.Add(new DataColumn(C_RECEIVEDBYTESTHRESHOLD, Type.GetType(Cfg.SYS_INT32)));
      t0.Columns[C_RECEIVEDBYTESTHRESHOLD].DefaultValue = DEF_PORT1_RECEIVEDBYTESTHRESHOLD;
      
      t0.Rows.Add(t0.NewRow());
      t0.AcceptChanges();

      return t0;
    }


    public static void Load_Config()
    {
      /// Load the Sensor Configuration
      try
      {
        // Load Sensor Serial Port 1 Properties
        m_Port1_Name = Cfg.XDS.Tables[T_PORT1].Rows[0][C_PORT_NAME].ToString();
        m_Port1_Baud = Convert.ToInt32(Cfg.XDS.Tables[T_PORT1].Rows[0][C_BAUD]);
        m_Port1_DataBits = Convert.ToInt32(Cfg.XDS.Tables[T_PORT1].Rows[0][C_DATABITS]);

        switch (Cfg.XDS.Tables[T_PORT1].Rows[0][C_HANDSHAKE].ToString().ToUpper())
        {
          case "REQUESTTOSEND":
            m_Port1_Handshake = Handshake.RequestToSend;
            break;
          case "REQUESTTOSENDXONXOFF":
            m_Port1_Handshake = Handshake.RequestToSendXOnXOff;
            break;
          case "XONXOFF":
            m_Port1_Handshake = Handshake.XOnXOff;
            break;
          default:
            m_Port1_Handshake = Handshake.None;
            break;
        }

        switch (Cfg.XDS.Tables[T_PORT1].Rows[0][C_PARITY].ToString().ToUpper())
        {
          case "EVEN":
            m_Port1_Parity = Parity.Even;
            break;
          case "MARK":
            m_Port1_Parity = Parity.Mark;
            break;
          case "ODD":
            m_Port1_Parity = Parity.Odd;
            break;
          case "SPACE":
            m_Port1_Parity = Parity.Space;
            break;
          default:
            m_Port1_Parity = Parity.None;
            break;
        }

        switch (Cfg.XDS.Tables[T_PORT1].Rows[0][C_STOPBITS].ToString().ToUpper())
        {
          case "NONE":
            m_Port1_StopBits = StopBits.None;
            break;
          case "ONEPOINTFIVE":
            m_Port1_StopBits = StopBits.OnePointFive;
            break;
          case "TWO":
            m_Port1_StopBits = StopBits.Two;
            break;
          default:
            m_Port1_StopBits = StopBits.One;
            break;
        }

        m_Port1_ReadBufferSize = Convert.ToInt32(Cfg.XDS.Tables[T_PORT1].Rows[0][C_READBUFFERSIZE]);
        m_Port1_ReadTimeout = Convert.ToInt32(Cfg.XDS.Tables[T_PORT1].Rows[0][C_READTIMEOUT]);
        m_Port1_WriteBufferSize = Convert.ToInt32(Cfg.XDS.Tables[T_PORT1].Rows[0][C_WRITEBUFFERSIZE]);
        m_Port1_WriteTimeout = Convert.ToInt32(Cfg.XDS.Tables[T_PORT1].Rows[0][C_WRITETIMEOUT]);
        m_Port1_ReceivedBytesThreshold = Convert.ToInt32(Cfg.XDS.Tables[T_PORT1].Rows[0][C_RECEIVEDBYTESTHRESHOLD]);

      }
      catch (Exception ex)
      {
        Log.Err(ex, "Sensor, Load_Config", "Error Reading Configuration", Log.LogDevice.LOG_DLG);
      }
    }


  }
}
