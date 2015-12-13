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
<h1>To Do: Automatic Self Reset Feature</h1>
<h3>DESCRIPTION</h3>
Need to enable the sensor and host software to automatically reset itself for transmit modes. The problem is occurs when a sensor goes off-line, the host relay software may not resume data transmission.<br />
&nbsp;<br />  
Applies to PHzMonitor Model 9 Sensors relaying data to the PHzRelay host application.<br />
Need to determine if the problem applies to the GridTrak sensors and software.<br />
&nbsp;<br />  
Current workaround is to execute the procedures that initiate data transmission. For example: Type the "1" command in the PHzRelay host application.<br />
&nbsp;<br />
<b>Closed Mar 25, 2012 at 12:19 AM by ajstadlin</b><br />
This is not an issue with openPDC software. openPDC can be configured to re-initialize the IEEE C37.118-2005 communications. This works very well with GridTrak PMU Release 9.7 and openPDC.
<h3>COMMENTS</h3>
ajstadlin wrote Nov 19, 2011 at 7:15 PM <br />
Use the PIC's built in watch-dog for automatic restart on inoperative failure.
<h3>STATUS</h3>
<ul>
<li>Component:  GridTrak Software</li>
<li>Impact:  High</li>
<li>Status:  Issue, Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  Mar 25, 2012 at 12:19 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  May 16, 2013 at 2:56 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  Aug 28, 2011 at 8:56 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/9112">CodePlex Issue #9112</a> Dec 13, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
