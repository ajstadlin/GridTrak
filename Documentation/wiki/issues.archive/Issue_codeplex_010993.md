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
<h1>GTosPMU_App freezes when run in dd/mm/yyyy format regional time</h1>
<h3>DESCRIPTION</h3>
When system date is set to 12/5/2013 or 12/6/2013, on a system running in dd/mm/yyyy format regional date time.<br />
&nbsp;<br />
http://www.youtube.com/watch?v=2h-9DPtNVNk&feature=youtu.be<br />
&nbsp;<br />
The GTosPMU_App configuration and test software was developed using mm/dd/yyyy format regional date time. This is probably one or more problems related to software date conversion routines.<br />
The GridTrak PMU sensor uses SOC and FRAC time so this is probably not firmware related.<br />
&nbsp;<br />
<b>Closed May 12, 2013 at 9:02 PM by ajstadlin</b><br />
The new Timeout break in the Sync_To_PC procedure should prevent blocking the main app thread for more than 1 second. The design and implementation of the Sync_To_PC needs to be improved in a future release.
<h3>COMMENTS</h3>
ajstadlin wrote May 12, 2013 at 8:58 PM 
&nbsp;<br />
GTosPMU_App Release 1.0.9.14 - Failsafe Timeout Break added to Sensor.Sync_To_PC wait loop.<br />
The wait loop will block the main processing thread for up to one second to send a signal to the PMU on the 1 second roll-over. This was originally intended to be performed once. The Sync_To_PC is only required for host synchronized mode in the GTosPMU_App software. The Sync_To_PC procedure is not used when operating in GPS Time source mode. However, various GTosPMU_App activities like CONNECT and PING will call the Sync_To_PC procedure to sync the PMU to the Host. <br />
&nbsp;<br />
This issue does not affect IEEE C37.118 mode used by software like openPDC and PMUConnectionTester. The GridTrak PMU, when operating in Host Sync Mode, synchronizes to the time stamp in the C37.118 protocol messages sent from a IEEE C37.118 compliant PDC host. <br />
&nbsp;<br />
The Sync_To_PC procedure needs to be re-designed to sync the PMU to the host a better way. This will be considered for future releases.
<h3>STATUS</h3>
<ul>
<li>Component:  GridTrak Software</li>
<li>Impact:  High</li>
<li>Status:  Issue, Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  May 12, 2013 at 9:02 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  May 16, 2013 at 2:56 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  May 12, 2013 at 1:10 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/10993">CodePlex Issue #10993</a> Dec 12, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
