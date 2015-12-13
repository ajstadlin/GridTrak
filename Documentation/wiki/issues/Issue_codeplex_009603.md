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
<h1>Time Shift in Display Charts in GPS Synchronized Mode</h1>
<h3>DESCRIPTION</h3>
Applies to GridTrak Local Application version 9.0 configured for GPS synchronization mode.<br />
&nbsp;<br />
The local application charts are shifted left by 2 minutes on the time axis. The time data is correct, but the position of the charted data is not plotted correctly.
<h3>FILE ATTACHMENTS</h3>
Screenshot<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/TimeShift_on_Charts_Bug.png" alt="TimeShift_on_Charts_Bug.png" /><br />
<h3>COMMENTS</h3>
<b>Closed Nov 19, 2011 at 6:06 PM by ajstadlin</b><br />
This is not a problem with the PMU or data. The shift appears when the PC’s clock is not synchronized to the PMU’s GPS time. The chart windows display a window of time from PC Time Now -10 minutes to PC Time Now. If the PC’s clock is running “fast” then the data will be shifted left.A fix is to pin the Chart’s time axis to the GPS time instead of the PC Time. However, it is a good indicator for when the PC’s clock needs to be resynchronized to an accurate time source.
<h3>STATUS</h3>
<ul>
<li>Component:  GridTrak Software</li>
<li>Impact:  High</li>
<li>Status:  Issue, Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  Nov 19, 2011 at 6:06 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  May 16, 2013 at 2:56 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  Nov 19, 2011 at 10:36 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/9603">CodePlex Issue #9603</a> Dec 13, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
