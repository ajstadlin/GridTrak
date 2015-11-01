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
<div class="wikidoc">
<h1><span>GridTrak Open Source PMU Integration with openPDC</span></h1>
<p><span>Part 1 - Setup openPDC and Connect the PMU Sensor to Collect Data</span></p>
<p><span style="font-size:xx-small"><span style="color:#ff0000"><span style="font-size:x-small">Updated Mar 4, 2012, ajs</span></span></span><span style="font-size:x-small">&nbsp;</span></p>
<h2><span>1.&nbsp; openPDC Installation</span></h2>
<h3><span>Configuration Tested Platform <span>(others may also work)</span></span></h3>
<ul>
<li>Applies to openPDC v1.4 Release </li><li>SQL Server Express 2008 </li><li>Microsoft .NET Framework 4, Full Version </li><li>Windows 7 Professional/32, P4 (Hyper Threading enabled) 3 GHz, 1GB RAM, 250GB SATA HD, Ethernet
</li><li>Serial Port (for GridTrak PMU sensor) </li><li>openPDC and PMU Connection Tester will be installed in folders outside of the Windows standard Program Files and Documents paths to avoid encountering issues with User specific privileges and Program Files virtual store.
</li></ul>
<h3><span>Downloading the openPDC Release Software</span></h3>
<ul>
<li>
<div>Navigate to the openPDC project with the link: <a href="http://openpdc.codeplex.com" target="_blank">
openPDC - The Open Source Phasor Data Concentrator</a></div>
</li><li>Select the Downloads tab and download the <strong>openPDCSetup.zip</strong> file to a convenient folder. After you click on the
<strong>openPDCSetup.zip</strong> link, you may be prompted to accept a license agreement. After accepting the license, a Save As style dialog is presented for you to select a folder and rename the file. For this document, we will save the file as
<strong>C:\openPDC\Synchrophasor.Installs.zip</strong> and extract it into the <strong>
C:\openPDC\Synchrophasor.Installs </strong>folder. The following is a screen shot of the results.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/Synchrophasor_Installs_ExtractedFolder_2.png"><img title="Synchrophasor_Installs_ExtractedFolder" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/Synchrophasor_Installs_ExtractedFolder_thumb.png" border="0" alt="Synchrophasor_Installs_ExtractedFolder" width="484" height="248" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Reading the README.txt is a good idea. In this release, this tells us to simply extract the files as we have done and run the
<strong>Setup.exe </strong>to install openPDC. </li></ul>
<h3><span>Running openPDC Setup</span></h3>
<ul>
<li>Right-click on the <strong>Setup.exe</strong> program and select the <strong>
Run as administrator</strong> option from the shortcut menu as illustrated in the following screen shot.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/Synchrophasor_Installs_RunAsAdministrator_2.png"><img title="Synchrophasor_Installs_RunAsAdministrator" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/Synchrophasor_Installs_RunAsAdministrator_thumb.png" border="0" alt="Synchrophasor_Installs_RunAsAdministrator" width="484" height="81" align="left" style="padding-left:0px; padding-right:0px; display:inline; float:left; padding-top:0px; border-width:0px"></a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<ul>
<li>The following is a series of screen shots for a simple openPDC setup installing all the options.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_1_2.png"><img title="openPDC_Setup_1" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_1_thumb.png" border="0" alt="openPDC_Setup_1" width="244" height="196" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_2_2.png"><img title="openPDC_Setup_2" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_2_thumb.png" border="0" alt="openPDC_Setup_2" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_3_2.png"><img title="openPDC_Setup_3" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_3_thumb.png" border="0" alt="openPDC_Setup_3" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_4_2.png"><img title="openPDC_Setup_4" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_4_thumb.png" border="0" alt="openPDC_Setup_4" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Using Windows 7 Professional, 32 bit operating system.&nbsp;&nbsp; Install the software to an easy to find and maintain stand-alone folder in drive C.&nbsp;&nbsp; Installing in &ldquo;Program Files&rdquo; may hide settings in virtual stores or in my &ldquo;Documents&rdquo;
 space and may conflict with running openPDC as a service while not logged in. </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_5_2.png"><img title="openPDC_Setup_5" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_5_thumb.png" border="0" alt="openPDC_Setup_5" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_6_2.png"><img title="openPDC_Setup_6" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_6_thumb.png" border="0" alt="openPDC_Setup_6" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_7_2.png"><img title="openPDC_Setup_7" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_7_thumb.png" border="0" alt="openPDC_Setup_7" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_8_2.png"><img title="openPDC_Setup_8" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Setup_8_thumb.png" border="0" alt="openPDC_Setup_8" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>The <strong>Configuration Setup Utility</strong> automatically runs after the software is installed by
<strong>Setup.exe</strong>.&nbsp; </li><li>Install a new &ldquo;<strong>openPDC</strong>&rdquo; database in the local SQL Server Express.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_9_2.png"><img title="openPDC_Config_Setup_9" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_9_thumb.png" border="0" alt="openPDC_Config_Setup_9" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_10_2.png"><img title="openPDC_Config_Setup_10" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_10_thumb.png" border="0" alt="openPDC_Config_Setup_10" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_11_2.png"><img title="openPDC_Config_Setup_11" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_11_thumb.png" border="0" alt="openPDC_Config_Setup_11" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_12_2.png"><img title="openPDC_Config_Setup_12" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_12_thumb.png" border="0" alt="openPDC_Config_Setup_12" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Using Windows Integrated Authentication to connect to the local SQL Server Express database service.&nbsp; The openPDC v1.4.90.0 release does not completely implement this feature in the Configuration Setup Utility.&nbsp; As a work around, uncheck the &ldquo;Use
 integrated security for openPDC Manager&rdquo; option, then add &ldquo;; Integrated Security=SSPI&rdquo; to the Advanced Settings Connection String property.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_13_4.png"><img title="openPDC_Config_Setup_13" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_13_thumb_1.png" border="0" alt="openPDC_Config_Setup_13" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_23f_SSPI_2.png"><img title="openPDC_Config_Setup_23f_SSPI" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_23f_SSPI_thumb.png" border="0" alt="openPDC_Config_Setup_23f_SSPI" width="417" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Click the <strong>Test Connection</strong> button and verify that the database connection succeeds before continuing.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_15_2.png"><img title="openPDC_Config_Setup_15" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_15_thumb.png" border="0" alt="openPDC_Config_Setup_15" width="211" height="130" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Use Windows Authentication for Account Credentials and optionally allow the credentials to be used by openPDC Manager.&nbsp; Also select running openPDC as a service.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_16_2.png"><img title="openPDC_Config_Setup_16" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_16_thumb.png" border="0" alt="openPDC_Config_Setup_16" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_17_2.png"><img title="openPDC_Config_Setup_17" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_17_thumb.png" border="0" alt="openPDC_Config_Setup_17" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Use the Historian defaults </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_18_2.png"><img title="openPDC_Config_Setup_18" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_18_thumb.png" border="0" alt="openPDC_Config_Setup_18" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_19_2.png"><img title="openPDC_Config_Setup_19" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_19_thumb.png" border="0" alt="openPDC_Config_Setup_19" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>The remaining setup process will check for and provide the option to erase a previous database, if there is one.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_20_2.png"><img title="openPDC_Config_Setup_20" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_20_thumb.png" border="0" alt="openPDC_Config_Setup_20" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_23g_SSPI_DatabaseExists_2.png"><img title="openPDC_Config_Setup_23g_SSPI_DatabaseExists" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_23g_SSPI_DatabaseExists_thumb.png" border="0" alt="openPDC_Config_Setup_23g_SSPI_DatabaseExists" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_21_2.png"><img title="openPDC_Config_Setup_21" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_21_thumb.png" border="0" alt="openPDC_Config_Setup_21" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_22_2.png"><img title="openPDC_Config_Setup_22" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Config_Setup_22_thumb.png" border="0" alt="openPDC_Config_Setup_22" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Installing PMU Connection Tester is simple.&nbsp; Like openPDC, install PMU Connection Tester in its own folder outside of the scope of the &ldquo;Program Files&rdquo; and &ldquo;Documents&rdquo;.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ConnTester_Setup_24_2.png"><img title="openPDC_ConnTester_Setup_24" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ConnTester_Setup_24_thumb.png" border="0" alt="openPDC_ConnTester_Setup_24" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ConnTester_Setup_25_2.png"><img title="openPDC_ConnTester_Setup_25" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ConnTester_Setup_25_thumb.png" border="0" alt="openPDC_ConnTester_Setup_25" width="244" height="198" style="margin:0px; padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ConnTester_Setup_26_2.png"><img title="openPDC_ConnTester_Setup_26" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ConnTester_Setup_26_thumb.png" border="0" alt="openPDC_ConnTester_Setup_26" width="244" height="198" style="margin:0px; padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ConnTester_Setup_28_2.png"><img title="openPDC_ConnTester_Setup_28" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ConnTester_Setup_28_thumb.png" border="0" alt="openPDC_ConnTester_Setup_28" width="244" height="198" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<hr>
<h2><span>2. Verify Sensor Communications with PMU Connection Tester</span></h2>
<ul>
<li>Run PMU Connection Tester </li><li>Configure the <strong>Connection Parameters, Serial</strong> tab settings for the COM port, Baud Rate = 11520, Parity = None, Stop Bits = One, Data Bits = 8, DTR and RTS unchecked.
</li><li>Configure the <strong>Protocol</strong> tab setting for <strong>IEEE C37.118-2005</strong>
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_1_2.png"><img title="PmuConnTester_1" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_1_thumb.png" border="0" alt="PmuConnTester_1" width="244" height="219" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Below the <strong>Graph </strong>select the <strong>Settings </strong>tab </li><li>In the <strong>Application Settings </strong>section set <strong>MinimumFrameDisplayBytes</strong>&nbsp; to
<strong>512</strong> </li><li>In the <strong>Connection Settings </strong>section set <strong>AutoStartDataParsingSequence
</strong>to <strong>False</strong> </li><li>In the <strong>Phase Angle Graph</strong> section set <strong>PhaseAngleGraphStyle
</strong>to <strong>Raw</strong> </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_2_4.png"><img title="PmuConnTester_2" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_2_thumb_1.png" border="0" alt="PmuConnTester_2" width="244" height="219" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PMUConnTester_2a.png" alt="PMUConnTester_2a" width="244" height="219"></p>
<ul>
<li>In the <strong>Protocol</strong> click the <strong>Connect</strong> button </li><li>Select the <strong>Command:&nbsp; Send Header Frame </strong>then click the <strong>
Send </strong>button </li><li>Select the <strong>Command:&nbsp; Send Config Frame 1&nbsp; </strong>then click the
<strong>Send </strong>button </li><li>Select the <strong>Command:&nbsp; Send Config Frame 2&nbsp; </strong>then click the
<strong>Send </strong>button </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_3_2.png"><img title="PmuConnTester_3" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_3_thumb.png" border="0" alt="PmuConnTester_3" width="244" height="219" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_4_2.png"><img title="PmuConnTester_4" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_4_thumb.png" border="0" alt="PmuConnTester_4" width="244" height="219" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_5_2.png"><img title="PmuConnTester_5" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_5_thumb.png" border="0" alt="PmuConnTester_5" width="244" height="219" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Select the <strong>Command:&nbsp; Enable Real-time Data</strong>&nbsp; then click the
<strong>Send </strong>button </li><li>To increase the sample density in the chart, increase the settings for <strong>
FrequencyPointsToPlot </strong>and <strong>PhaseAnglePointsToPlot</strong>&nbsp; </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_6_2.png"><img title="PmuConnTester_6" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/PmuConnTester_6_thumb.png" border="0" alt="PmuConnTester_6" width="244" height="219" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<hr>
<h2><span>3.&nbsp; Configure Your Company, Vendor, and Vendor Device in openPDC Manager</span></h2>
<p>openPDC comes preloaded with Company, Vendor, and Device records.&nbsp; However, if your Company, Vendor, and Device is not included in these, it is a good idea to add your own records to the openPDC.&nbsp; If you are new to openPDC, entering this information
 is a simple introduction to the look and feel of using the openPDC Manager.</p>
<ul>
<li>Run the openPDC Manager and select <strong>Manage </strong>menu and click the
<strong>Companies </strong>option.
<ul>
<li>Click the <strong>Clear </strong>button to setup a new blank <strong>Company</strong> record.
</li><li>Enter your <strong>Company</strong> information then click the <strong>Save</strong> button.
</li></ul>
</li></ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Mgr_100.png" alt="" width="404" height="384">&nbsp;<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Mgr_Manage_Company_101.png" alt="" width="404" height="384">&nbsp;</p>
<ul>
<li>Select the <strong>Manager </strong>menu, <strong>Vendors </strong>option
<ul>
<li>Click the <strong>Clear </strong>button to setup a new blank <strong>Vendor </strong>
record. </li><li>Enter your <strong>Vendor</strong> information then click the <strong>Save</strong> button.
</li></ul>
</li></ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Mgr_Manage_Vendor_102.png" alt="" width="404" height="384">&nbsp;</p>
<ul>
<li>Select the <strong>Manager </strong>menu, <strong>Vendor Devices </strong>option
<ul>
<li>Click the <strong>Clear </strong>button to setup a new blank <strong>Vendor Device
</strong>record. </li><li>Enter your <strong>Vendor Device</strong> information then click the <strong>
Save</strong> button. </li></ul>
</li></ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Mgr_Manage_Vendor_Device_103.png" alt="" width="404" height="384">&nbsp;</p>
<hr>
<h2><span>4.&nbsp; Configure the Sensor as a New Device in openPDC Manager</span></h2>
<ul>
<li>If PMU Connection Tester is Connected, click the <strong>Disconnect </strong>
button in the <strong>Protocol </strong>tab.&nbsp; The PC&rsquo;s Serial Port can only be used by one application at any time.
</li><li>Run <strong>openPDC Manager </strong>and select the <strong>Add New </strong>
option from the <strong>Devices </strong>menu. </li></ul>
<p><img src="http://download.codeplex.com/Download?ProjectName=gridtrak&DownloadId=350206" alt="" width="213" height="144"></p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Mgr_Devices_AddNew_Menu_104.png" target="_blank"></a></p>
<ul>
<li>Click the <strong>Build </strong>link to the right of the <strong>Connection String
</strong>field. </li><li>In the <strong>Build Connection String </strong>dialog&rsquo;s <strong>Serial
</strong>tab, select the COM Port and set the other parameters to Baud Rate = 11520, Parity = None, Stop Bits = One, Data.&nbsp; Be sure to set the
<strong>Stop Bits </strong>to <strong>One</strong> because this is not the default in this dialog.
</li></ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Mgr_Devices_BuildConnectionString_105.png" alt="" width="404" height="320"></p>
<p style="padding-left:60px"><strong>port=COM7; baudrate=115200; parity=None; stopbits=One; databits=8; dtrenable=false; rtsenable=false; transportprotocol=serial; interface=0.0.0.0;</strong></p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_2_2.png"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_3_2.png"></a></p>
<ul>
<li>Click the <strong>Next </strong>button at the bottom right of the screen </li><li>Click the <strong>Request Configuration From openPDC </strong>button to get the read the configuration from the sensor using IEEE C37.118-2005 protocol.
</li><li>Select your <strong>Company</strong> </li><li>Select the <strong>Interconnection </strong>that the sensor will be recording data for.
</li></ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Mgr_Device_Config_106.png" alt="" width="404" height="320"></p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_4a_2.png"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_5c_2.png"></a></p>
<ul>
<li>Click the <strong>Modify Configuration </strong>button to review the configuration retrieved from the sensor.&nbsp; Close the dialog by clicking its
<strong>Save</strong> button. </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_5_2.png"><img title="openPDC_Manager_5" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_5_thumb.png" border="0" alt="openPDC_Manager_5" width="404" height="171" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>I noticed a <strong>Save Configuration </strong>button near the <strong>Modify Configuration
</strong>button in the wizard&rsquo;s Step 2 screen.&nbsp; When I clicked this and saved the configuration to an XML file, I got the following Error.&nbsp; I didn&rsquo;t click this button in previous tests, so this error may not be a problem for this exercise.&nbsp;
 I will report it in the Issue Tracker. </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_5_OnSaveButton_Error_2.png"><img title="openPDC_Manager_5_OnSaveButton_Error" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_5_OnSaveButton_Error_thumb.png" border="0" alt="openPDC_Manager_5_OnSaveButton_Error" width="404" height="201" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Click the <strong>Next </strong>button at the bottom right of the <strong>Device Configuration Wizard
</strong>screen to continue to Step 3. </li><li>Select your <strong>Vendor </strong><strong>Device</strong> </li><li>Enter your <strong>Longitude and Latitude</strong> </li><li>Review the information and click the <strong>Finish </strong>button. </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_6_2.png"><img title="openPDC_Manager_6" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_6_thumb.png" border="0" alt="openPDC_Manager_6" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_7_2.png"><img title="openPDC_Manager_7" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_7_thumb.png" border="0" alt="openPDC_Manager_7" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>In the <strong>Devices </strong>menu, click the <strong>Browse </strong>option
</li><li>Click the <strong>Acronym </strong>link and update the <strong>Time Zone </strong>
property </li><li>Make sure the <strong>ID Code (AccessID) </strong>is set to the Sensor&rsquo;s ID , then click the
<strong>Save </strong>button. </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_8_2.png"><img title="openPDC_Manager_8" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_8_thumb.png" border="0" alt="openPDC_Manager_8" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_11_2.png"><img title="openPDC_Manager_11" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_11_thumb.png" border="0" alt="openPDC_Manager_11" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>After the <strong>Save</strong>, you are returned to the devices browser.&nbsp; Click the
<strong>Acronym </strong>link for the sensor again. </li><li>In the <strong>Manage Devices </strong>screen, click the <strong>Initialize </strong>
link, then click <strong>Yes </strong>to initialize the sensor. </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_12_2.png"><img title="openPDC_Manager_12" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_12_thumb.png" border="0" alt="openPDC_Manager_12" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_12a_2.png"><img title="openPDC_Manager_12a" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_12a_thumb.png" border="0" alt="openPDC_Manager_12a" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>In the <strong>Monitoring </strong>menu, select the <strong>System Console </strong>
option and scroll back to verify that the sensor was initialized. </li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_14_2.png"><img title="openPDC_Manager_14" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_14_thumb.png" border="0" alt="openPDC_Manager_14" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>Try the <strong>Home </strong>and <strong>Monitoring, Input Status &amp; Monitoring</strong>
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_15_2.png"><img title="openPDC_Manager_15" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_15_thumb.png" border="0" alt="openPDC_Manager_15" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_16_2.png"><img title="openPDC_Manager_16" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_16_thumb.png" border="0" alt="openPDC_Manager_16" width="404" height="326" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<ul>
<li>The <strong>Manage </strong>menu, <strong>System Settings </strong>option provides settings for adjusting the chart.
</li></ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_SystemSettings_2.png"><img title="openPDC_Manager_SystemSettings" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Manager_SystemSettings_thumb.png" border="0" alt="openPDC_Manager_SystemSettings" width="404" height="349" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
</div>
</div>

<hr />
<div class="footer">
Last edited <span class="smartDate" title="3/5/2012 2:12:29 AM" LocalTimeTicks="1330942349">Mar 5, 2012 at 2:12 AM</span> by <a id="wikiEditByLink" href="http://www.codeplex.com/site/users/view/ajstadlin">ajstadlin</a>, version 32<br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/wikipage?title=%20GridTrak%20Open%20Source%20PMU%20Integration%20with%20openPDC">CodePlex</a> Oct 31, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajs</a><!--/HtmlToGmd.Migration-->
</div>

<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>