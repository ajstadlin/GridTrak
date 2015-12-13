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
<h1>BSOD when using USB to Serial Converter, IOGEAR GUC232A</h1>
<h3>DESCRIPTION</h3>
It appears that not all USB to Serial Converters work. Tests have determined that IOGEAR GUC232A converters, even with most current driver updates, cause Blue Screen crashing when receiving data. The BSOD is not related to the GTosPMU or other PDC Software. The BSOD occurs even when using PuTTY connected to a Cisco Router's console cable. The BSOD appears to be related more to Receiving data than Transmitting and is unpredictable and unrepeatable - but it is definitely UNRELIABLE.<br />
&nbsp;<br />
This issue may or may not be specific to Windows 7 / 64 running on DELL Optiplex workstations, however we don't have the time and alternative resources to isolate the issue with the GUC232A. IOGEAR has closed the issue as "Resolved" automatically after 72 hours. I sincerely hope they notify me when they discover and release a fix for this issue.<br />
&nbsp;<br />
As a work around we are no longer using the GUC232A devices and can no longer recommend them for use with the GTosPMU. We are testing FTDI chipset adapters by GearMO and Keyspan to determine their reliability. The fact that these other devices are currently working more reliably indicates that the problem was specific to the GUC232A. However, this issue is open until we experience a longer test period with the new devices.<br />
&nbsp;<br />  
Feedback from anyone currently successfully using USB to RS232 Serial adapters with Windows 7/64, Linux/64, or Windows Server 2008, would be greatly appreciated. We know we are not the only ones experiencing BSOD problems with USB to RS232 converters.<br />
&nbsp;<br />
<b>Closed Feb 4, 2012 at 12:14 PM by ajstadlin</b><br />
The problem was the USB-RS232 Converter. We are now using the GearMO USA-FTDI-A12 in Windows 7/64 and Windows Server 2008 R2. The Windows 7 drivers can be automatically installed without the CD. In Windows Server 2008 the drivers need to be obtained from FTDI directly at the URL:http://www.ftdichip.com/FTDrivers.htm
<h3>COMMENTS</h3>
ajstadlin wrote Feb 4, 2012 at 12:08 PM <br />
The only USB-RS232 Converter that we are able to reliably use is the GearMO Model USA-FTDI-A12. <br />
&nbsp;<br />
The GearMO USA-FTDI-A12 converter can be purchased from either of the following two URLs. There are probably other suppliers as well. <br />
&nbsp;<br />
http://www.serialstuff.com/products/Windows-7-Compatible-USB-Serial-Adapter-FTDI-Chip-RS232-DB%252d9-920K-with-TX%7B47%7DRX-LED.html <br />
&nbsp;<br />
http://www.amazon.com/Windows-Compatible-Serial-Adapter-RS232/dp/B004WLA4P4/ref=sr_1_1?ie=UTF8&qid=1328375012&sr=8-1<br />
&nbsp;<br />
ajstadlin wrote Nov 4, 2014 at 11:37 AM <br />
I sure hope all the GearMO USA-FTDI-A12 converters with the GritTrak PMUs I deployed use genuine FTDI chips!! <br />
&nbsp;<br />
http://www.eevblog.com/forum/reviews/ftdi-driver-kills-fake-ftdi-ft232<br />
&nbsp;<br />
ajstadlin wrote Nov 4, 2014 at 11:48 AM <br />
Related EEVBLOG Video Link<br />
http://www.eevblog.com/2014/10/27/eevblog-676-rant-ftdi-bricking-counterfeit-chips/
<h3>STATUS</h3>
<ul>
<li>Component:  RS232 to USB Adapter</li>
<li>Impact:  High</li>
<li>Status:  Issue, Closed</li>
</ul>
</div>
<hr />
<div class="footer">
Closed  Feb 4, 2012 at 12:14 PM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Updated  Nov 4, 2014 at 11:48 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
Reported  Nov 19, 2011 at 11:01 AM by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/workitem/9604">CodePlex Issue #9604</a> Dec 12, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajstadlin</a><!--/HtmlToGmd.Migration-->
</div>
<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>
