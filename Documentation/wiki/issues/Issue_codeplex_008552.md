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
<h1>GPS Synchronized Phase Measurement Noise</h1>
<h3>DESCRIPTION</h3>
When operating the GTosPMU Sensor in GPS synchronized mode, the Phase Measurements are noisy.<br />
Fixing this is a high priority.
<h3>FILE ATTACHMENTS</h3>
Without By-Pass Capacitors<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/GPS_Synchronized_Phase_Noise.png" alt="GPS_Synchronized_Phase_Noise.png" /><br />
With By-Pass Capacitors<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/Phase_Noise_Test_07_GPS_1d0uF_Bypass.png" alt="Phase_Noise_Test_07_GPS_1d0uF_Bypass.png" />
<h3>COMMENTS</h3>
<b>Closed Apr 29, 2011 at 7:52 PM by ajstadlin</b><br />
&nbsp;<br />
ajstadlin wrote Apr 29, 2011 at 7:52 PM <br />
The noise can be filtered out using a 1.0uF bypass capacitor between the PPS and ground when using the Garmin 15xL. This correction may be different for other GPS units. The next screenshot shows the improved signal.<br />
&nbsp;<br />
Note the spikes that are similar to the spikes observed when oscilloscope probes are attached to the PPS. The random spikes are preferable to the noise without the bypass capacitor. Since spikes are not desireable, the noise issue can be closed, but the spikes in a different issue still needs work.
&nbsp;<br />
ajstadlin wrote Apr 29, 2011 at 7:57 PM <br />
Correction to last comment. The spikes occur when probes are Not Attached. <br />
https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/Issue_codeplex_008423.md
<h3>STATUS</h3>
<ul>
<li>Component:  PMU Firmware</li>
<li>Impact:  High</li>
<li>Status:  Issue, Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  Apr 29, 2011 at 7:52 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  May 16, 2013 at 2:56 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  Apr 28, 2011 at 9:44 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/8552">CodePlex Issue #8552</a> Dec 12, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
