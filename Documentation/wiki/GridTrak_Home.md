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
<p><strong>Project Description</strong> <br>
The Open Source SynchroPhasor PMU Project provides resources that enable you to build your own SynchroPhasor measuring sensors for use with the openPDC project, research, development, or electric grid observation.</p>
<h1>Open Source SynchroPhasor PMU Project</h1>
<ul>
<li>Open source resources enable you to construct your own PMU for research, experimentation, development, or fun. The information provided here is intended to be reproducible, repeatable, and reusable in your own projects.
</li><li>The open source resources hosted here can be used with the openPDC - Open Source Phasor Data Concentrator project software.
</li></ul>
<hr>
<h1>GridTrak - <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Open_Source_PMU_-GTosPMU-.md">Open Source PMU (GTosPMU)</a></h1>
<h3>How GridTrak Open Source PMU (GTosPMU) Works:</h3>
<p>The GridTrak PMU is a hardware sensor designed to measure and transmit AC SynchroPhasor Frequency, Phase Angle, and Magnitude digitally by:</p>
<ul>
<li>Converts the AC signal to complimentary square waves using a operatonal amplifier and precision voltage reference
</li><li>Uses a precision timer to measure square wave cycle (Frequency) and offset from a PPS time reference (Phase angle)
</li><li>Calculates Magnitude for the ideal sine wave of the signal </li><li>Optionally uses GPS for precision PPS triggering and SynchroPhasor measurement values
</li><li>Transmits Data per IEEE C37.118-2005 specifications or in plain text </li><li>Compatible with openPDC for extended application and systems development </li></ul>
<p>The GridTrak concept filters out the original wave form, harmonics, and distortion by the square wave conversion of the analog input signal to a digital signal. If you need to measure or observe wave form distortion or harmonics, this is not the PMU for
 the job.</p>
<table style="width:100%">
<tbody>
<tr>
<td style="width:40%">
<p><strong>Photo - </strong><strong>GridTrak Model A, Rev 9.5 kit</strong></p>
<p><strong>Operational&nbsp;Open Source SynchroPhasor PMU with GPS Integration.</strong></p>
<p>&nbsp;</p>
</td>
<td style="width:60%"><strong><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/GridTrak_PMU_Mod_A_9-7_web.jpg" alt="" width="449"></strong></td>
</tr>
<tr>
<td style="width:40%">
<p><strong>&nbsp;</strong></p>
</td>
<td style="width:60%">&nbsp;</td>
</tr>
<tr>
<td style="width:40%">
<p><strong>Photo - Testing a GridTrak Model A PMU with Garmin GPS 15xL-W.&nbsp; </strong>
</p>
<p><strong>A bypass capacitor (upper right)&nbsp;is added to the PPS to reduce transient false signals.</strong></p>
<p><strong>Two serial port connections are shown.&nbsp;&nbsp;A completed&nbsp;Model A PMU will only require 1 serial port connection for operation, however, the second serial port remains available for direct PC to GPS connection for development and diagnostics.&nbsp;
</strong></p>
</td>
<td style="width:60%"><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/GTosPMU_Mod_A_GPS.jpg" alt="" width="449" height="276"></td>
</tr>
</tbody>
</table>
</div>
</div>
<hr />
<div class="footer">
Revised Nov 1, 2015 at 11:00 AM (UTC-5) by <a id="wikiEditByLink" href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a> - Information in the GridTrak project regarding the <i>other</i> <a href="http://openpmu.sourceforge.net/">OpenPMU Project</a> is now deprecated.  Artifacts related to that project found here remain for archival reference only.<br />
Last CodePlex edit <span class="smartDate" title="4/25/2012 3:57:56 AM" LocalTimeTicks="1335351476">Apr 25, 2012 at 3:57 AM</span> by <a id="wikiEditByLink" href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a>, version 46<br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com">CodePlex</a> Oct 30, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajs</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
