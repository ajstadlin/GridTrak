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

<h1>Open Source PMU Comparison Chart</h1>
<table border="0" align="left" style="width:100%">
<tbody>
<tr valign="top" style="background-color:#dee1d6">
<td valign="top" style="width:16%"><strong>Properties</strong></td>
<td valign="top" style="width:42%"><strong><a href="http://gtospmu.codeplex.com/wikipage?title=GridTrak%20Open%20Source%20PMU%20%28GTosPMU%29&referringTitle=Home">GridTrak Open Source PMU (GTosPMU)</a></strong></td>
<td><strong><a href="http://openpmu.sourceforge.net">OpenPMU</a></strong></td>
</tr>
<tr>
<td>Technology</td>
<td>Voltage reference crossings converted to digital and&nbsp;directly timed</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>ADC and DSP (FFT)</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Hardware</td>
<td>dsPIC30F3013 Micro Controller based Open Source PMU Sensor Kit</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>National Instruments NI-DAQ, Sound Card, or other DAQ with custom OpenPMU GPS Clock &amp; Sampling Trigger</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Firmware</td>
<td>
<p>Microchip C30, 16bit, open source code provided</p>
</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>GPS Clock &ndash; Microchip C18</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Software</td>
<td>
<p>Open Source C# .NET host PC application for sensor configuraiton and local observation and data recording, compatible with
<strong>openPDC</strong> and IEEE C37.118-2005 compliant software</p>
</td>
<td>Open Source Python code library</td>
</tr>
<tr>
<td>GPS</td>
<td>Optional - Integration testing scheduled for March 2011</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>GPS or other UTC 1PPS source required</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Availability</td>
<td>Available February 2,&nbsp;2011 as Kits and Assembled Sensors</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>Summer 2011 (expected)</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Project Life Cycle</td>
<td>R&amp;D work in progress for at least 3 years from Jan 2011</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>Continuous development</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Cost</td>
<td>Kit = $150, Assembled = $500 (USD)&nbsp; (GPS not included at this time)</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>~ $400 (less if using existing NI-DAQ)</p>
</span></span></span></span></td>
</tr>
<tr>
<td>More Info Contact</td>
<td><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md"><strong>Arnold Stadlin</strong></a></td>
<td><a href="http://www.codeplex.com/site/users/view/clockdoctor"><strong>clockdoctor</strong></a></td>
</tr>
<tr valign="top" style="background-color:#dee1d6">
<td><strong>Performance</strong></td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
<tr>
<td>Signal Input</td>
<td>9-12 V AC/AC Linear Transformer, Adapters work for foreign conversions</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>3-phase &amp; neutral voltages and currents</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Measurements</td>
<td>AC Voltage, single phase per sensor, Voltage Reference Crossings</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>14-bit, &#43;/- 10 V</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Measurements Rate</td>
<td>Measurements are made each AC cycle</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>240 samples / cycle at 50 Hz (200 at 60 Hz)</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Minimum Sample Time</td>
<td>1.2 AC Cycles, consecutive cycles overlap by about 20%</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>FFT uses minimum 2 cycles, sliding window</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Nominal Frequency and Range</td>
<td>50Hz&nbsp;or 60Hz&nbsp; &#43;/- 1.0%</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>50 Hz / 60 Hz &#43;/- 1.0%</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Packet Transmission Rate</td>
<td>Design Maximum = 60,&nbsp; Reliably Tested = 20 packets per second</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>50 / 60 Hz, 25 / 30 Hz, 10 Hz</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Data Connectivity</td>
<td>RS232 115200 bps over Serial Port or Serial/USB adapter</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>UDP/IP using NMEA style plain text</p>
</span></span></span></span></td>
</tr>
<tr>
<td>Power Requirement</td>
<td>9-12V AC/AC Linear, non-Switching,&nbsp;Transformer is shared between&nbsp;input signal and circuitry. Optional &#43;9V DC&nbsp;backup battery.&nbsp;&nbsp;Optional &#43;5V DC.&nbsp;&nbsp;Jumper for isolating the signal input from the circuit power supply.</td>
<td><span style="color:#30332d"><span style="color:#30332d"><span style="color:#30332d"><span>
<p>30 W auxiliary mains supply</p>
</span></span></span></span></td>
</tr>
<tr>
<td>&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
</tbody>
</table>

<hr />
<div class="footer">
Last edited <span class="smartDate" title="3/2/2011 5:00:05 AM" LocalTimeTicks="1299070805">Mar 2, 2011 at 5:00 AM</span> by <a id="wikiEditByLink" href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a>, version 11<br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gtospmu.codeplex.com/wikipage?title=Open%20Source%20PMU%20Comparison">CodePlex</a> Oct 31, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajs</a><!--/HtmlToGmd.Migration-->
</div>

<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->

</body>
</html>