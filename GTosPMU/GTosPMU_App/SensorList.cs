using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace GTosPMU
{
  public class SensorList
  {
    private const string THIS_NAME = "SensorList";

    public static DataSet SensorDS = null;

    private static string m_MySensors_FilePath = Application.StartupPath + @"\MySensors.xml";


    public static void Load_ListDS()
    {
      if (File.Exists(m_MySensors_FilePath))
      {
        SensorDS = new DataSet("SensorDS");
        SensorDS.ReadXml(m_MySensors_FilePath, XmlReadMode.ReadSchema);
      }
      else
      {
        if (SensorDS != null)
        {
          SensorDS.Dispose();
          GC.Collect();
        }
        DataTable dt0 = new DataTable("SensorTBL");
        dt0.Columns.Add(new DataColumn(Cfg.C_IS_PICK, Type.GetType(Cfg.SYS_INT32)));
        dt0.Columns[Cfg.C_IS_PICK].DefaultValue = 1;

        dt0.Columns.Add(new DataColumn(Sensor.C_SENSOR_ID, Type.GetType(Cfg.SYS_INT64)));
        dt0.Columns[Sensor.C_SENSOR_ID].DefaultValue = Sensor.Sensor_ID;

        dt0.Columns.Add(new DataColumn(Sensor.C_LOCATION_NAME, Type.GetType(Cfg.SYS_STRING)));
        dt0.Columns[Sensor.C_LOCATION_NAME].DefaultValue = Sensor.Location_Name;

        dt0.Rows.Add(dt0.NewRow());
        dt0.AcceptChanges();
        SensorDS = new DataSet("SensorDS");
        SensorDS.Tables.Add(dt0);
        SensorDS.AcceptChanges();
      }
      SensorDS.Tables[0].DefaultView.Sort = Sensor.C_SENSOR_ID;
    }


    public static void Refresh_ListDS()
    {
      // Refresh the Sensor List from the Web Service
      string sResult = "";
      if (SensorDS != null)
      {
        SensorDS.Dispose();
        GC.Collect();
      }
      SensorWS.SensorWSSoapClient wsSensor = null;
      try
      {
        wsSensor = new SensorWS.SensorWSSoapClient();
        SensorDS = wsSensor.Get_SensorList(Cfg.Account_Id, Cfg.Account_EMail, Cfg.Account_Pswd);
        sResult = SensorDS.Tables[0].Rows.Count.ToString();
        SensorDS.Tables[0].Columns.Add(new DataColumn(Cfg.C_IS_PICK, Type.GetType(Cfg.SYS_INT32)));
        SensorDS.Tables[0].Columns[Cfg.C_IS_PICK].DefaultValue = 0;
        SensorDS.Tables[0].AcceptChanges();
        bool bFound = false;
        for (int ii = 0; ii < SensorDS.Tables[0].Rows.Count; ii++)
        {
          if (SensorDS.Tables[0].Rows[ii][Sensor.C_SENSOR_ID] == (object)Sensor.Sensor_ID)
          {
            bFound = true;
            SensorDS.Tables[0].Rows[ii].BeginEdit();
            SensorDS.Tables[0].Rows[ii][Cfg.C_IS_PICK] = 1;
            SensorDS.Tables[0].Rows[ii].EndEdit();
            SensorDS.Tables[0].Rows[ii].AcceptChanges();
          }
          else
          {
            SensorDS.Tables[0].Rows[ii].BeginEdit();
            SensorDS.Tables[0].Rows[ii][Cfg.C_IS_PICK] = 0;
            SensorDS.Tables[0].Rows[ii].EndEdit();
            SensorDS.Tables[0].Rows[ii].AcceptChanges();
          }
        }
        if (!bFound)
        {
          DataRow oDR = SensorDS.Tables[0].NewRow();
          oDR[Cfg.C_IS_PICK] = 1;
          oDR[Sensor.C_SENSOR_ID] = Sensor.Sensor_ID;
          oDR[Sensor.C_LOCATION_NAME] = Sensor.Location_Name;
          SensorDS.Tables[0].Rows.Add(oDR);
          SensorDS.Tables[0].AcceptChanges();
        }

        SensorDS.Tables[0].DefaultView.Sort = Sensor.C_SENSOR_ID;
      }
      catch (Exception ex)
      {
        sResult = "Get Sensor List from Web Service Failed [" + sResult + "]";
        Log.Err(ex, THIS_NAME + ".Refresh_listDS", sResult, Log.LogDevice.LOG_DLG);
      }
      wsSensor = null;
      GC.Collect();
    }


    public static void Save_ListDS()
    {
      // Save a local copy of the Sensor List and Sensor Selections
      if (SensorDS != null)
      {
        try
        {
          if (File.Exists(m_MySensors_FilePath))
          {
            File.Delete(m_MySensors_FilePath);
          }
          SensorDS.WriteXml(m_MySensors_FilePath, XmlWriteMode.WriteSchema);
        }
        catch (Exception ex)
        {
          Log.Err(ex, THIS_NAME, "Save Error", Log.LogDevice.LOG_DLG);
        }
      }
    }

  }
}
