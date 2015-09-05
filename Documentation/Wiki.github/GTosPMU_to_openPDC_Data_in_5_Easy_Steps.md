<h1>GridTrak Open Source PMU (GTosPMU) to openPDC Data in 5 Easy Steps</h1>
<p>This document is derived from the <a href="http://openpdc.codeplex.com"><strong>openPDC</strong></a> project's document: &nbsp;<a href="http://openpdc.codeplex.com/wikipage?title=Device to Data"><strong>Device to Data in 5 Easy Steps</strong></a><br />The procedure is the same. There are only a few minor code changes.</p>
<ol>
<li><a href="#x_x_x_x_x_x_x_x_x_x_x_x_x_step1">Create a project</a></li>
<li><a href="#x_x_x_x_x_x_x_x_x_x_x_x_x_step2">Add references</a></li>
<li><a href="#x_x_x_x_x_x_x_x_x_x_x_x_x_step3">Copy in the code snippet</a></li>
<li><a href="#x_x_x_x_x_x_x_x_x_x_x_x_x_step4">Set up your data source</a></li>
<li><a href="#x_x_x_x_x_x_x_x_x_x_x_x_x_step5">Run the application</a></li>
</ol>
<h2><a name="step1"></a>Step 1: Create a project</h2>
<p>The first thing you need to do is create a console application in Microsoft Visual Studio 2008. The following are detailed steps to guide you through the process.</p>
<ol>
<li>Launch Microsoft Visual Studio</li>
<li>In the toolbar, go to "File &gt; New &gt; Project..."</li>
<li>Under "Project Types", click on "Windows".</li>
<li>Under "Templates", click on "Console Application".</li>
<li>In the text box labeled "Name", enter the name of your application (e.g. "DeviceToData").</li>
<li>Click the button labeled "Browse..." and select a directory to store the project.</li>
<li>Click the "OK" button</li>
<li>Right Click on your new project and select "Properties"</li>
<li>In the Application settings, change the Target Framework to ".NET Framework 4"</li>
</ol>
<h2><a name="step2"></a>Step 2: Add references</h2>
<p>In order to get the code to run, you will need to add references to the openPDC assemblies. The following are detailed steps to guide you through the process.<br /><strong>Note</strong>: In order to complete this step, you will need to <a href="/wikipage?title=Getting Started&amp;referringTitle=Device to Data&amp;ANCHOR#build_source_code">build openPDC</a>.</p>
<ol>
<li>In your project's Solution Explorer on the right, right-click "References", select "Add Reference...", then click the Browse button</li>
<li>Navigate to "SOURCEDIR\Main\Build\Output\Debug\Libraries" (SOURCEDIR is the directory where you extracted and built the openPDC source code files).</li>
<li>Select "TVA.Communication.dll", "TVA.Core.dll", "TVA.PhasorProtocols.dll", and "TimeSeriesFramework.dll" then click the "OK" button</li>
</ol>
<h2><a name="step3"></a>Step 3: Copy in the code snippet</h2>
<p>Now you are ready to copy the source code that will interface with your device. Remove everything in Program.cs and replace it with the following code snippet.<br /><br /></p>
<pre>using System;
using System.Collections.Generic;
using System.Text;
using TVA;
using TVA.PhasorProtocols;

namespace GTosPMUtoData
{
  /// &lt;summary&gt;
  /// Sample Program adapted for the GridTrak Open Source SynchroPhasor PMU
  /// from openPDC sample: Device to Data in 5 Easy Steps.
  /// References:  http://openpdc.codeplex.com and http://gtospmu.codeplex.com
  /// 
  /// Mar 28, 2011  Updated
  /// &lt;/summary&gt;
  class Program
  {
    static MultiProtocolFrameParser parser;
    static long frameCount;

    static void Main(string[] args)
    {
      Console.WriteLine("GTosPMU to Data for openPDC Test");
      // Create a new protocol parser
      parser = new MultiProtocolFrameParser();

      // Attach to desired events
      parser.ConnectionAttempt += parser_ConnectionAttempt;
      parser.ConnectionEstablished += parser_ConnectionEstablished;
      parser.ConnectionException += parser_ConnectionException;
      parser.ParsingException += parser_ParsingException;
      parser.ReceivedConfigurationFrame += parser_ReceivedConfigurationFrame;
      parser.ReceivedDataFrame += parser_ReceivedDataFrame;

      // Define the connection string
      parser.ConnectionString = "phasorProtocol=IeeeC37_118V1; transportProtocol=Serial; accessID=2; "
                                + "port=COM1; baudrate=115200; parity=None; stopbits=One; databits=8; "
                                + "dtrenable=False; rtsenable=False; autoStartDataParsingSequence = false;";

      // Start frame parser
      parser.AutoStartDataParsingSequence = true;
      parser.Start();

      // Hold the console open until the operator hits the &lt;Enter&gt; key
      Console.ReadLine();
    }


    static void parser_ReceivedDataFrame(object sender, EventArgs&lt;IDataFrame&gt; e)
    {
      // Increase the frame count each time a frame is received
      frameCount++;

      // Print information each time we receive 60 frames (every 2 seconds @ 30 fps)
      // Also check to make sure we have at least one protocol cell in the DataFrame; else ignore processing it.
      if ((frameCount % 60 == 0) &amp;&amp; (e.Argument.Cells.Count &gt; 0))
      {
        IDataCell device = e.Argument.Cells[0];
        Console.WriteLine("Received data frames so far =  " + frameCount.ToString());
        Console.WriteLine("  Last frequency =  ", device.FrequencyValue.Frequency.ToString("00.0000") + " Hz");
        for (int ii= 0; ii &lt; device.PhasorValues.Count; ii++)
        {
          Console.WriteLine("  Last phase angle[" + ii.ToString() + "] =  " 
                            + device.PhasorValues[ii].Angle.ToString("##0.0000") + " rad");

          Console.WriteLine("  Last magnitude[" + ii.ToString() + "] =  " 
                            + device.PhasorValues[ii].Magnitude.ToString("##0.000") + " {device units}");
        }
        Console.WriteLine("    Last Timestamp =  " +((DateTime)device.Timestamp).ToString("yyyy-MM-dd HH:mm:ss.fff"));
      }
    }


    static void parser_ReceivedConfigurationFrame(object sender, EventArgs&lt;IConfigurationFrame&gt; e)
    {
      // Notify the user when a configuration frame is received
      Console.WriteLine("Received configuration frame with {0} device(s)", e.Argument.Cells.Count);
    }


    static void parser_ParsingException(object sender, EventArgs&lt;Exception&gt; e)
    {
      // Output the exception to the user
      Console.WriteLine("Parsing exception: {0}", e.Argument);
    }


    static void parser_ConnectionException(object sender, EventArgs&lt;Exception, int&gt; e)
    {
      // Display which connection attempt failed and the exception that occurred
      Console.WriteLine("Connection attempt {0} failed due to exception: {1}", e.Argument2, e.Argument1);
    }

    static void parser_ConnectionEstablished(object sender, EventArgs e)
    {
      // Notify the user when the connection is established
      Console.WriteLine("Initiating {0} {1} based connection...",
                        parser.PhasorProtocol.GetFormattedProtocolName(),
                        parser.TransportProtocol.ToString().ToUpper());
    }

    static void parser_ConnectionAttempt(object sender, EventArgs e)
    {
      // Let the user know we are attempting to connect
      Console.WriteLine("Attempting connection...");
    }

  }
}
</pre>
<h2>Step 4: Set up your PMU Sensor</h2>
<p>Just connect the GTosPMU sensor to the host PC serial or USB port and and plug in the transformer.</p>
<h2>Step 5: Run the application</h2>
<p>If you followed all the other steps correctly, you should be able to run the project by pressing "F5" from within Microsoft Visual Studio. The result should look something like the example image below.<br /><br /><img src="http://download.codeplex.com/download?ProjectName=gridtrak&amp;DownloadId=222058" alt="Device_To_Data_Example" width="668" height="511" /></p>