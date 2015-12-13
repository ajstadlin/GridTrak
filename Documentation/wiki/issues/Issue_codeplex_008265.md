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
<h1>openPDC Console: WARNING: Encountered unknown device...</h1>
<h3>DESCRIPTION</h3>
Setup: GTosPMU FW v0007 (not yet released) and openPDC 1.4.90.0<br />
Steps to Reproduce in openPDC<br />
1.Create a Device using the wizard <br />
2.In the Devices Manager, click the Initialize link and send the device initialize to the sensor <br />
3.Switch to the openPDC system console <br />
4.After connection, the console displays the message:<br />
WARNING: Encountered unknown device "GTOSPMU A"... 
<h3>FILE ATTACHMENTS</h3>
PMU Connection Tester showing GridTrak PMU ID# 0<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/openPDC_Manager_13.png" alt="openPDC_Manager_13.png" />
<h3>COMMENTS</h3>
<b>Closed Mar 20, 2011 at 7:52 PM by ajstadlin</b><br />
This problem is resolved by assuring that the Device's ID Code (AccessID) matches the Sensor ID.
<h3>STATUS</h3>
<ul>
<li>Component:  PMU Firmware</li>
<li>Impact:  High</li>
<li>Status:  Issue, Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  Mar 20, 2011 at 7:52 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  May 16, 2013 at 2:56 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  Mar 20, 2011 at 1:20 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/8265">CodePlex Issue #8265</a> Dec 12, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
