using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTosPMU
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Log.Initialize();
      try
      {
        Application.Run(new MainWF());
      }
      catch (Exception exApp)
      {
        Log.Err(exApp, "Application, Program", "Outer Error, Unhandled Exception", Log.LogDevice.LOG_DLG);
      }
    }
  }
}
