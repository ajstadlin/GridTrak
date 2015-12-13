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
<h1>GTosPMU_App - PPS Enabled, Stops Running after a few seconds</h1>
<h3>DESCRIPTION</h3>
The GTosPMU_App software stops running (receiving data) after a few seconds when PPS mode is enabled.<br />
&nbsp;<br />
http://www.youtube.com/watch?v=lYY68KGMYJ4&feature=youtu.be<br />
&nbsp;<br />
Possibly Related: GPS sync mode with PPS disabled does not work.<br />
Host sync mode without PPS appears to work reliably.
<h3>FILE ATTACHMENTS</h3>
ajstadlin wrote May 13, 2013 at 1:01 PM <br />
PPS Test using Oscilloscope photo attached
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/PPS_Test_OK.jpg" alt="PPS_Test_OK.jpg" /><br />
<h3>COMMENTS</h3>
ajstadlin wrote May 13, 2013 at 12:37 PM <br />
&nbsp;<br />
Tested:<br />
 Conditions = PPS Enabled, GTosPMU_App connected but not displaying data.<br />
 Observations =<br />
 1) PPS signal continues to be transmitted by the GPS and measures properly by oscilloscope on the PMU's PPS pin.<br />
 2) PMU Activity LED is steady ON, not blinking. <br />
&nbsp;<br />
 This problem is most likely a firmware bug.<br />
&nbsp;<br />
<b>Closed Dec 6, 2014 at 10:07 PM by ajstadlin</b><br />
Fixed by May 14, 2013 FW Ver 090C.0 - Error Handler ISRs removed.<br />
<h3>STATUS</h3>
<ul>
<li>Component:  PMU Firmware</li>
<li>Impact:  High</li>
<li>Status:  Issue, Fixed Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  Dec 6, 2014 at 10:07 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  Dec 6, 2014 at 10:07 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  May 13, 2013 at 12:02 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/10994">CodePlex Issue #10994</a> Dec 12, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
