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
<h1>-1 ERR in GTosPMU_App Streaming Data</h1>
<h3>DESCRIPTION</h3>
The -1 ERR in the GTosPMU_App software is an exception raised when the PPS is more than 20 milliseconds late. This error was observed with the GridTrak PMU configured for 50Hz range.<br />
&nbsp;<br />
In the next GridTrak release, this exception will be modified to have a configurable value and option to disable (ignore missed PPS).<br />
&nbsp;<br />
<b>Closed Apr 17, 2013 at 5:24 PM by ajstadlin</b><br />
<b>GTosPMU_App 1.0.9.11, source code changeset 84109:</b><br />
&nbsp;<br />
Default Configuraiton for Late PPS set to disable raising the exception in the GTosPMU_App software.<br />
The configuration parameters to enable the Late PPS exception and change its time threshold may be modified in the application's configuration XML file.
<h3>STATUS</h3>
<ul>
<li>Component:  GridTrak Software</li>
<li>Impact:  High</li>
<li>Status:  Task, Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  Apr 17, 2013 at 5:24 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  May 16, 2013 at 2:56 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  Apr 16, 2013 at 8:55 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/10930">CodePlex Issue #10930
</a> Dec 12, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
