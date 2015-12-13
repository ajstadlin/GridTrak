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
<h1>Errors: mscorlib: Index was outside the bounds of the array</h1>
<h3>DESCRIPTION</h3>
Some conditions in the GridTrak software send it out of bounds. The only recovery is to close the program and run it again.<br />
&nbsp;<br />
The cause is under investigation. One suspect cause is that the conditions may be related to operating the software in RDP sessions where the screen scaling changes between sessions. The issue has not been experienced when operating the software while logged into a physical workstation.<br />
&nbsp;<br />  
Typical Errors:<br />
Red "X" is displayed in place of the graphic panels.<br />
&nbsp;<br />
Err - 03/01/12 21:50:08.549[3], MainWF: Phasor_DrawGrid(1)<br />
mscorlib:<br />
Index was outside the bounds of the array.<br />
Err - 03/01/12 21:50:08.553[3], Main, Display Timer: Error: Phasor_Calc,Phasor_DrawGrid,PhaseChart_Calc,PhaseChart_DrawGrid,<br />
mscorlib:<br />
Index was outside the bounds of the array.
<h3>COMMENTS</h3>
ajstadlin wrote Mar 24, 2012 at 10:52 PM <br />
&nbsp;<br />
I have not observed this error when operating in GPS Integrated mode. This is probably specific to host synchronized (non-GPS Time) mode of operation. <br />
&nbsp;<br />
This is still an issue in Release 9.7<br />
&nbsp;<br />

ajstadlin wrote Apr 17, 2013 at 5:46 PM <br />
&nbsp;<br />
This is still an observed problem possibly related to the difference between the data time stamps and the current system time. The problem is less likely to happen when the host's time is synchronized with GPS or NIST standard time and the PMU's time is sychronized with the host. For example, if the PMU is operating in GPS synchronized mode and the host PC's clock time is significantly different from the GPS time, then there may be "X" charting problems. It is also possible that Serial Port buffering may cause delays between the data tiimestamps VS when the host processes the data from the PC's serial port buffer. The best mode of operation is to minimize the Serial Port buffer size or reduce the number of samples per second transmitted by the PMU to assure that the data does not accumulate in the serial port buffers. 
&nbsp;<br />
This is not a problem with PMU Connection Tester or openPDC - both of which have much more sophisticated data I/O processing.
<h3>STATUS</h3>
<ul>
<li>Component:  GridTrak Software</li>
<li>Impact:  Medium</li>
<li>Status:  Issue, Proposed</li>
</ul>
</div>
<hr />
<div class="footer">
Updated Apr 17, 2013 at 5:46 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported Mar 1, 2012 at 10:29 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/9992">CodePlex Issue #9992</a> Dec 12, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
