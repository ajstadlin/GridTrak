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
<h1>GPS Time Data Displayed is Lagging Behind Real Time</h1>
<h3>DESCRIPTION</h3>
The GPS Time displayed in the GTosPMU Measurement Application appears to lag behind the actual system time when the system time is synchronized to NIST time. The Host PC VS GPS time difference, or error, is observed to range from -0.500 to -7.500 seconds.<br />
&nbsp;<br />
The issue can be observed in the screen shot attached to this issue. At the bottom center of the screen shot the Host PC's time, GPS Time, and the Difference of -5.189 seconds are displayed. Also the plotted data in the strip charts shows a 5 seconds gap between the last plotted data point and the "Now" right side edge of the charts.
<h3>FILE ATTACHMENTS</h3>
GPS_TimeLag.png<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/GPS_TimeLag.png" alt="GPS_TimeLag.png" /><br />
GPS_TimePostiveError.png<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/GPS_TimePostiveError.png" alt="GPS_TimePostiveError.png" /><br />
GTPMU_GPS_Time_vs_PMU_ConnTester.png<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/GTPMU_GPS_Time_vs_PMU_ConnTester.png" alt="GTPMU_GPS_Time_vs_PMU_ConnTester.png" /><br />
GTPMU_GPS_Time_vs_openPDC_Manager.png<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/GTPMU_GPS_Time_vs_openPDC_Manager.png" alt="GTPMU_GPS_Time_vs_openPDC_Manager.png" />
<h3>COMMENTS</h3>
ajstadlin wrote Feb 25, 2012 at 12:31 AM <br />
Power off/on PMU reset affects this issue and reduces the error, but does not eliminate the error or fix the problem.<br />
&nbsp;<br />
ajstadlin wrote Feb 25, 2012 at 9:46 PM <br />
Positive time errors are also observed.<br />
&nbsp;<br />
ajstadlin wrote Mar 3, 2012 at 5:26 PM <br />
Verified that this is not a GPS issue. It is either firmware or GTosPMU Application issue. It is still under investigation.<br />
&nbsp;<br />
ajstadlin wrote Mar 24, 2012 at 10:42 PM <br />
The Time Lag problem is specific to the GTosPMU_App configuration application display. Attached is a screenshot of the PMU Connection Tester side by side with the NIST Time web display.<br />
This time lag in the GTosPMU_App live chart is not in the GridTrak PMU firmware but probably in the GTosPMU_App charting program code performance.<br />
&nbsp;<br />
ajstadlin wrote Mar 24, 2012 at 11:07 PM <br />
GridTrak PMU in GPS Mode with openPDC compared to NIST web time screen shot attached.<br />
&nbsp;<br />
Closed Mar 24, 2012 at 10:50 PM by ajstadlin</b><br />
This is a GTosPMU Software performance issue, not a GridTrak PMU firmware or GPS issue. The scope of the GTosPMU software is to configure and verify GridTrak PMU operation.The PMU Connection Tester and openPDC software provides better real-time display performance, more features, and is scalable.
<h3>STATUS</h3>
<ul>
<li>Component:  PMU Firmware</li>
<li>Impact:  High</li>
<li>Status:  Issue, Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  Mar 24, 2012 at 10:50 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  May 16, 2013 at 2:56 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  Feb 24, 2012 at 11:50 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/9965">CodePlex Issue #9965</a> Dec 13, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
