<html lang="en">
<body>
<!--HtmlToGmd.Body-->
<div id="NavigationMenu">
<h1><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Home.md">
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Logo.png" alt="Open Source SynchroPhasor PMU" /></a></h1>
<hr />
<table style="width: 100%; border-collapse: collapse; border: 0px solid gray;">
<tr>
<td style="width: 25%; text-align:center;"><b><a href="http://www.gridtrak.com">GridTrak</a></b></td>
<td style="width: 25%; text-align:center;"><b><a href="https://github.com/ajstadlin/GridTrak">GridTrak on GitHub</a></b></td>
<td style="width: 25%; text-align:center;"><b><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Home.md">GridTrak Wiki Home</a></b></td>
<td style="width: 25%; text-align:center;"><b><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Documentation_Home.md">Documentation</a></b></td>
</tr>
</table>
</div>
<hr />
<!--/HtmlToGmd.Body-->

<div class="WikiContent">
<div class="wikidoc">
<h1>GridTrak Open Source PMU (GTosPMU) to openPDC Data in 5 Easy Steps</h1>
<p>This document is derived from the <a href="http://openpdc.codeplex.com"><strong>openPDC</strong></a> project's document: &nbsp;<a href="http://openpdc.codeplex.com/wikipage?title=Device to Data"><strong>Device to Data in 5 Easy Steps</strong></a><br>
The procedure is the same. There are only a few minor code changes.</p>
<ol>
<li><a href="#step1">Create a project</a> </li><li><a href="#step2">Add references</a> </li><li><a href="#step3">Copy in the code snippet</a> </li><li><a href="#step4">Set up your data source</a> </li><li><a href="#step5">Run the application</a> </li></ol>
<h2><a name="step1"></a>Step 1: Create a project</h2>
<p>The first thing you need to do is create a console application in Microsoft Visual Studio 2008. The following are detailed steps to guide you through the process.</p>
<ol>
<li>Launch Microsoft Visual Studio </li><li>In the toolbar, go to &quot;File &gt; New &gt; Project...&quot; </li><li>Under &quot;Project Types&quot;, click on &quot;Windows&quot;. </li><li>Under &quot;Templates&quot;, click on &quot;Console Application&quot;. </li><li>In the text box labeled &quot;Name&quot;, enter the name of your application (e.g. &quot;DeviceToData&quot;).
</li><li>Click the button labeled &quot;Browse...&quot; and select a directory to store the project.
</li><li>Click the &quot;OK&quot; button </li><li>Right Click on your new project and select &quot;Properties&quot; </li><li>In the Application settings, change the Target Framework to &quot;.NET Framework 4&quot;
</li></ol>
<h2><a name="step2"></a>Step 2: Add references</h2>
<p>In order to get the code to run, you will need to add references to the openPDC assemblies. The following are detailed steps to guide you through the process.<br>
<strong>Note</strong>: In order to complete this step, you will need to <a href="/wikipage?title=Getting Started&referringTitle=Device to Data&ANCHOR#build_source_code">
build openPDC</a>.</p>
<ol>
<li>In your project's Solution Explorer on the right, right-click &quot;References&quot;, select &quot;Add Reference...&quot;, then click the Browse button
</li><li>Navigate to &quot;SOURCEDIR\Main\Build\Output\Debug\Libraries&quot; (SOURCEDIR is the directory where you extracted and built the openPDC source code files).
</li><li>Select &quot;TVA.Communication.dll&quot;, &quot;TVA.Core.dll&quot;, &quot;TVA.PhasorProtocols.dll&quot;, and &quot;TimeSeriesFramework.dll&quot; then click the &quot;OK&quot; button
</li></ol>
<h2><a name="step3"></a>Step 3: Copy in the code snippet</h2>
<p>Now you are ready to copy the source code that will interface with your device. Remove everything in Program.cs and replace it with the following code snippet.<br>
<br>
</p>
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
      Console.WriteLine(&quot;GTosPMU to Data for openPDC Test&quot;);
      // Create a new protocol parser
      parser = new MultiProtocolFrameParser();

      // Attach to desired events
      parser.ConnectionAttempt &#43;= parser_ConnectionAttempt;
      parser.ConnectionEstablished &#43;= parser_ConnectionEstablished;
      parser.ConnectionException &#43;= parser_ConnectionException;
      parser.ParsingException &#43;= parser_ParsingException;
      parser.ReceivedConfigurationFrame &#43;= parser_ReceivedConfigurationFrame;
      parser.ReceivedDataFrame &#43;= parser_ReceivedDataFrame;

      // Define the connection string
      parser.ConnectionString = &quot;phasorProtocol=IeeeC37_118V1; transportProtocol=Serial; accessID=2; &quot;
                                &#43; &quot;port=COM1; baudrate=115200; parity=None; stopbits=One; databits=8; &quot;
                                &#43; &quot;dtrenable=False; rtsenable=False; autoStartDataParsingSequence = false;&quot;;

      // Start frame parser
      parser.AutoStartDataParsingSequence = true;
      parser.Start();

      // Hold the console open until the operator hits the &lt;Enter&gt; key
      Console.ReadLine();
    }


    static void parser_ReceivedDataFrame(object sender, EventArgs&lt;IDataFrame&gt; e)
    {
      // Increase the frame count each time a frame is received
      frameCount&#43;&#43;;

      // Print information each time we receive 60 frames (every 2 seconds @ 30 fps)
      // Also check to make sure we have at least one protocol cell in the DataFrame; else ignore processing it.
      if ((frameCount % 60 == 0) &amp;&amp; (e.Argument.Cells.Count &gt; 0))
      {
        IDataCell device = e.Argument.Cells[0];
        Console.WriteLine(&quot;Received data frames so far =  &quot; &#43; frameCount.ToString());
        Console.WriteLine(&quot;  Last frequency =  &quot;, device.FrequencyValue.Frequency.ToString(&quot;00.0000&quot;) &#43; &quot; Hz&quot;);
        for (int ii= 0; ii &lt; device.PhasorValues.Count; ii&#43;&#43;)
        {
          Console.WriteLine(&quot;  Last phase angle[&quot; &#43; ii.ToString() &#43; &quot;] =  &quot; 
                            &#43; device.PhasorValues[ii].Angle.ToString(&quot;##0.0000&quot;) &#43; &quot; rad&quot;);

          Console.WriteLine(&quot;  Last magnitude[&quot; &#43; ii.ToString() &#43; &quot;] =  &quot; 
                            &#43; device.PhasorValues[ii].Magnitude.ToString(&quot;##0.000&quot;) &#43; &quot; {device units}&quot;);
        }
        Console.WriteLine(&quot;    Last Timestamp =  &quot; &#43;((DateTime)device.Timestamp).ToString(&quot;yyyy-MM-dd HH:mm:ss.fff&quot;));
      }
    }


    static void parser_ReceivedConfigurationFrame(object sender, EventArgs&lt;IConfigurationFrame&gt; e)
    {
      // Notify the user when a configuration frame is received
      Console.WriteLine(&quot;Received configuration frame with {0} device(s)&quot;, e.Argument.Cells.Count);
    }


    static void parser_ParsingException(object sender, EventArgs&lt;Exception&gt; e)
    {
      // Output the exception to the user
      Console.WriteLine(&quot;Parsing exception: {0}&quot;, e.Argument);
    }


    static void parser_ConnectionException(object sender, EventArgs&lt;Exception, int&gt; e)
    {
      // Display which connection attempt failed and the exception that occurred
      Console.WriteLine(&quot;Connection attempt {0} failed due to exception: {1}&quot;, e.Argument2, e.Argument1);
    }

    static void parser_ConnectionEstablished(object sender, EventArgs e)
    {
      // Notify the user when the connection is established
      Console.WriteLine(&quot;Initiating {0} {1} based connection...&quot;,
                        parser.PhasorProtocol.GetFormattedProtocolName(),
                        parser.TransportProtocol.ToString().ToUpper());
    }

    static void parser_ConnectionAttempt(object sender, EventArgs e)
    {
      // Let the user know we are attempting to connect
      Console.WriteLine(&quot;Attempting connection...&quot;);
    }

  }
}
</pre>
<h2>Step 4: Set up your PMU Sensor</h2>
<p>Just connect the GTosPMU sensor to the host PC serial or USB port and and plug in the transformer.</p>
<h2>Step 5: Run the application</h2>
<p>If you followed all the other steps correctly, you should be able to run the project by pressing &quot;F5&quot; from within Microsoft Visual Studio. The result should look something like the example image below.<br>
<br>
<img src="http://download.codeplex.com/download?ProjectName=gridtrak&DownloadId=222058" alt="Device_To_Data_Example" width="668" height="511"></p>
</div>
</div>

<hr />
<div class="wikiComments">
<div id="comment31477">
<div class="SubText">
<a name="C31477" />
<a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md" >ajstadlin</a>
<span class="smartDate" title="3/21/2015 3:49:15 AM" LocalTimeTicks="1426934955">Mar 21 at 3:49 AM</span>&nbsp;
</div>
Thank you jramirez for the above comments and corrections&#33;<p></p>
</div>

<div id="comment19088">
<div class="SubText">
<a name="C19088" />
<a href="http://www.codeplex.com/site/users/view/jramirez" >jramirez</a>
<span class="smartDate" title="3/29/2011 8:42:19 PM" LocalTimeTicks="1301456539">Mar 29, 2011 at 8:42 PM</span>&nbsp;
</div>
About the &#34;ToGrads&#34; method&#58;<br /><br />This method gives mistakes in the measurements, I have done conversions to degrees with it and its mistakes are around of 11&#37;.<br /><br />The error was calculated with E&#61;&#124;Vref-Vdata&#124;&#47;Vref .... where Vref is the data given by the PhasorPoint developed by Psymetrix.<br /><br />If I do the procedure with the explicit equation &#40; X&#91;&#176;&#93;&#61;180&#91;&#176;&#93; &#42; X&#91;Rad&#93; &#47; Pi &#41;, it gives the same value that the PhasorPoint.<p></p>
</div>

<div id="comment19083">
<div class="SubText">
<a name="C19083" />
<a href="http://www.codeplex.com/site/users/view/jramirez" >jramirez</a>
<span class="smartDate" title="3/29/2011 3:14:24 PM" LocalTimeTicks="1301436864">Mar 29, 2011 at 3:14 PM</span>&nbsp;
</div>
you should update the version Visual Studio 2008 to Visual Studio 2010 in Step 1.<br /><br />The frequency value does not appear due to you forgot write &#123;0&#125; before quotes after equal sign ...........<br />Console.WriteLine&#40;&#34; Last frequency &#61;&#123;0&#125; &#34;, device.FrequencyValue.Frequenc<wbr></wbr>y.ToString&#40;&#34;00.0000&#34;&#41; &#43; &#34; Hz&#34;&#41;&#59;<p></p>
</div>    
</div>

<hr />
<div class="footer">
Last edited <span class="smartDate" title="1/15/2012 2:32:09 AM" LocalTimeTicks="1326623529">Jan 15, 2012 at 2:32 AM</span> by <a id="wikiEditByLink" href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a>, version 7<br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/wikipage?title=GTosPMU%20to%20openPDC%20Data%20in%205%20Easy%20Steps">CodePlex</a> Oct 31, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajs</a><!--/HtmlToGmd.Migration-->
</div>

<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->

</body>
</html>