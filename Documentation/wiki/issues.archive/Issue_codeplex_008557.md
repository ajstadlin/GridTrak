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
<h1>Errors in Calculations when Measure at 30 Samples per Second</h1>
<h3>DESCRIPTION</h3>
There are calculation errors in the algorithm when making measurements at rates of 30 samples per second or faster. These appear mostly around the +/- 180 degrees phase angles. There are errors in the Phase and Frequency measurements at 30 fps or faster.<br />  
Until this issue is resolved, the GTosPMU sensor is only reliable at 20 samples per second or slower.
<h3>FILE ATTACHMENTS</h3>
Calc Errors at 30 Samples per Second displayed in GridTrak PMU Software<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/CalcErrors_30sps_Test_09.png" alt="CalcErrors_30sps_Test_09.png" /><br />
&nbsp;<br />
Calc Errors at 30 Samples per Second displayed in openPDC<br />
<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/issues/CalcErrors_30sps_OpenPDC.png" alt="CalcErrors_30sps_OpenPDC.png" /><br />
<h3>STATUS</h3>
<ul>
<li>Component:  PMU Firmware</li>
<li>Impact:  Low</li>
<li>Status:  Issue, Proposed</li>
</ul>
</div>
<hr />
<div class="footer">
Updated Feb 13, 2013 at 8:07 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported Apr 29, 2011 at 8:32 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/8557">CodePlex Issue #8557</a> Dec 11, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
