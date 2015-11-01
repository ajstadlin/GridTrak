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
<p><span style="font-size:xx-small">updated May 11, 2011</span></p>
<h1>Scenario</h1>
<ul>
<li>Collect PMU measurement data using a GridTrak Open Source PMU Sensor and openPDC as described in:
<ul>
<li>&nbsp;&nbsp; <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Open_Source_PMU_Integration_with_openPDC.md">
<strong>Part 1:&nbsp;&nbsp; GridTrak Open Source PMU Integration with openPDC</strong></a>
</li></ul>
</li><li>Relay the collected data to a remote openPDC server. </li></ul>
<h1>1.&nbsp; openPDC Server Installation</h1>
<p>1.1.&nbsp; Download and Install openPDC per section 1 in&nbsp; <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/GridTrak_Open_Source_PMU_Integration_with_openPDC.md">
<strong>Part 1:&nbsp;&nbsp; GridTrak Open Source PMU Integration with openPDC</strong></a></p>
<p>For this scenario, openPDC version 1.5.2, 64 bit from the May 5/6, 2011 nightly build and PMU Connection Tester 4.2.7 will be installed.</p>
<p>The platform for this scenario&rsquo;s openPDC server is Windows 2008 Enterprise R2/64, 2GB RAM, AMD Phenom II X4-955 (3.21 GHz) running in a Hyper-V virtual machine with allocation for 2GB RAM and 4 virtual CPUs and 127GB VHD.&nbsp; Also installed in the
 server are SQL Server 2008, Visual Studio 2010 Professional, and IIS web server.</p>
<p>We will use Windows Integrated Security for SQL Server and use pass through user credentials for the openPDC service.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_SqlSetup_2.png"><img title="openPDC_SqlSetup" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_SqlSetup_thumb.png" border="0" alt="openPDC_SqlSetup" width="244" height="207" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a>&nbsp;&nbsp;
<a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_UserCredentials_2.png">
<img title="openPDC_UserCredentials" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_UserCredentials_thumb.png" border="0" alt="openPDC_UserCredentials" width="244" height="206" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p>Add Company and Vendor for GridTrak, add gridtrak Vendor Device, and update the Node information.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Node_2.png"><img title="openPDC_Node" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Node_thumb.png" border="0" alt="openPDC_Node" width="704" height="304" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p>1.2.&nbsp; The openPDC server is configured with a static IP4 Address:&nbsp; 192.168.0.83.&nbsp; For this scenario, the openPDC Server and openPDC PMU Client are on the same LAN network segment.</p>
<h1>2.&nbsp; openPDC PMU Client Setup</h1>
<p>The openPDC PMU Client is running on a Windows 7 Professional PC with a gridtrak Sensor connected by serial port.&nbsp; The following is a screen shot of the gridtrak Device configuration in the openPDC Client.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_PMUClientDevice_2.png"><img title="openPDC_PMUClientDevice" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_PMUClientDevice_thumb.png" border="0" alt="openPDC_PMUClientDevice" width="704" height="380" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p>2.1.&nbsp; Select the Manage Menu, Configure an Output Stream.&nbsp; Set the configuration to transmit UDP to the server (192.168.0.83) on port 4712.&nbsp; Enable Auto Publish Config Frame and Auto Start Data Channel.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ClientOutputStream_2.png"><img title="openPDC_ClientOutputStream" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ClientOutputStream_thumb.png" border="0" alt="openPDC_ClientOutputStream" width="704" height="380" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p>2.2.&nbsp; While configuring the Output Stream, click the Launch Device Wizard link (in the browse list row for the selected Output Stream) and add the PMU device.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ClientOutputStream_AddDeviceWiz_2.png"><img title="openPDC_ClientOutputStream_AddDeviceWiz" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_ClientOutputStream_AddDeviceWiz_thumb.png" border="0" alt="openPDC_ClientOutputStream_AddDeviceWiz" width="604" height="322" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p>2.3.&nbsp; Open the Windows Control Panel, open the Windows Firewall applet and select the Advanced options.&nbsp;</p>
<p>Create and Enable an Outbound Firewall Rule for UDP Port 4712.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Client_FirewallOut1_UDP-4712_2.png"><img title="openPDC_Client_FirewallOut1_UDP-4712" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Client_FirewallOut1_UDP-4712_thumb.png" border="0" alt="openPDC_Client_FirewallOut1_UDP-4712" width="324" height="292" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a>&nbsp;&nbsp;&nbsp;
<a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Client_FirewallOut2_UDP-4712_2.png">
<img title="openPDC_Client_FirewallOut2_UDP-4712" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Client_FirewallOut2_UDP-4712_thumb.png" border="0" alt="openPDC_Client_FirewallOut2_UDP-4712" width="324" height="288" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Client_FirewallOut3_UDP-4712_4.png"><img title="openPDC_Client_FirewallOut3_UDP-4712" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Client_FirewallOut3_UDP-4712_thumb_1.png" border="0" alt="openPDC_Client_FirewallOut3_UDP-4712" width="704" height="147" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p>2.4.&nbsp; We can run Wireshark and filter for udp.dstport == 4712 to verify that the configured openPDC Output Stream is transmitting.</p>
<p><strong>I&rsquo;ll need to ask the openPDC developers why there are no UDP checksums in the UDP packet.</strong>&nbsp; The UDP packet contains IEEE C37.118 data as indicated by the &ldquo;aa01&rdquo; prefix.&nbsp; The IEEE C37.118 data packet will have its
 own CRC check.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Client_Wireshark-UDP=4712_2.png"><img title="openPDC_Client_Wireshark-UDP=4712" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Client_Wireshark-UDP=4712_thumb.png" border="0" alt="openPDC_Client_Wireshark-UDP=4712" width="804" height="577" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<h1>3.&nbsp; Windows Server Configuration to Receive Data</h1>
<p>3.1.&nbsp; Open the Windows Control Panel, open the Windows Firewall applet and select the Advanced options.&nbsp;</p>
<p>Review the Inbound Rules to determine if there are any conflicts for UDP Port 4712.&nbsp; In our server, there 2 Rules for Windows Media Player that allow UDP on
<strong>any</strong> <strong>local port</strong>.&nbsp; These 2 rules are disabled.&nbsp; I suspect that running Windows Media Player on the server could potentially cause problems with receiving UDP, so we won&rsquo;t do that!</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_Firewall_UDP-4712_2.png"><img title="openPDC_Server_Firewall_UDP-4712" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_Firewall_UDP-4712_thumb.png" border="0" alt="openPDC_Server_Firewall_UDP-4712" width="704" height="406" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p>3.2.&nbsp; Run the PMU Connection Tester and configure it to listen or UDP Port 4712.&nbsp; In the Settings tab, set
<strong>Force IP4</strong>&nbsp; to True.&nbsp; Enter the Device ID Code for the PMU and click the Connect button.&nbsp; The Real Time Frame Detail at the bottom of the window should show incoming data.&nbsp; After about 1 minute (or less) the PMU Connection
 Tester should receive a Configuration Frame.&nbsp; After receiving the Configuration Frame, the program will start plotting the data on the graph.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_PmuConnTester_UDP-4712_2.png"><img title="openPDC_Server_PmuConnTester_UDP-4712" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_PmuConnTester_UDP-4712_thumb.png" border="0" alt="openPDC_Server_PmuConnTester_UDP-4712" width="704" height="590" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border-width:0px"></a></p>
<p>3.3.&nbsp; In the PMU Connection Tester File menu, Save the Connection and the Configuration in files.</p>
<h1>4.&nbsp; Configure the openPDC Server to Receive the Data Stream</h1>
<p>On the Server, do this:</p>
<p>4.1.&nbsp; If the PMU Connection Tester is Connected, Disconnect it and optionally close it.</p>
<p>4.2.&nbsp; Run the openPDC Manager and in the Devices menu, select <strong>Add New (Wizard)</strong></p>
<p>4.3.&nbsp; In the Device Configuration Wizard, Step 1, click <strong>Browse </strong>
next to the<strong> Connection File </strong>field and load the file saved from the PMU Connection Tester session.</p>
<p>4.4.&nbsp; After loading the Connection File, the Device ID Code and Device Protocol fields should be updated.&nbsp; Click
<strong>Next </strong>to continue to Step 2.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceStep1_2.png"><img title="openPDC_Server_NewDeviceStep1" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceStep1_thumb.png" border="0" alt="openPDC_Server_NewDeviceStep1" width="604" height="392" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border:0px"></a></p>
<p>4.5.&nbsp; In Step 2, click <strong>Browse</strong> next to the <strong>Configuration File
</strong>field and load the file saved from the PMU Connection Tester Session.</p>
<p>4.6.&nbsp; After loading the Configuration File, check the <strong>Connection is to Concentrator
</strong>checkbox and fill in the <strong>PDC</strong> fields.&nbsp;</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceStep2_2.png"><img title="openPDC_Server_NewDeviceStep2" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceStep2_thumb.png" border="0" alt="openPDC_Server_NewDeviceStep2" width="604" height="396" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border:0px"></a></p>
<p>4.6.A)&nbsp; You may optionally review and change the configuration by clicking the Modify Configuration button.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceStep2_ModifyCfg_2.png"><img title="openPDC_Server_NewDeviceStep2_ModifyCfg" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceStep2_ModifyCfg_thumb.png" border="0" alt="openPDC_Server_NewDeviceStep2_ModifyCfg" width="504" height="204" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border:0px"></a></p>
<p>4.7.&nbsp; Click <strong>Next </strong>to move on to Step 3.&nbsp; The New Device is shown.&nbsp; Click
<strong>Finish</strong>, then click OK to close the Success dialog.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceStep3_2.png"><img title="openPDC_Server_NewDeviceStep3" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceStep3_thumb.png" border="0" alt="openPDC_Server_NewDeviceStep3" width="504" height="310" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border:0px"></a>&nbsp;
<a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceSuccess_2.png">
<img title="openPDC_Server_NewDeviceSuccess" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_NewDeviceSuccess_thumb.png" border="0" alt="openPDC_Server_NewDeviceSuccess" width="324" height="163" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border:0px"></a></p>
<p>4.8.&nbsp; The Browse Devices screen now displays two Devices.&nbsp; One is the remote PMU and the other is the remote openPDC Concentrator.&nbsp; The remote openPDC Concentrator can be identified by the checked box in the
<strong>Concentrator</strong> column.</p>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_BrowseDevices_2.png"><img title="openPDC_Server_BrowseDevices" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/files/openPDC_Server_BrowseDevices_thumb.png" border="0" alt="openPDC_Server_BrowseDevices" width="704" height="141" style="padding-left:0px; padding-right:0px; display:inline; padding-top:0px; border:0px"></a></p>
</div>
</div>

<hr />
<div class="footer">
Last edited <span class="smartDate" title="1/15/2012 2:48:12 AM" LocalTimeTicks="1326624492">Jan 15, 2012 at 2:48 AM</span> by <a id="wikiEditByLink" href="http://www.codeplex.com/site/users/view/ajstadlin">ajstadlin</a>, version 8<br />
<!--HtmlToGmd.Migration-->Migrated from <a href="http://gridtrak.codeplex.com/wikipage?title=Transmit%20Data%20to%20an%20openPDC%20Server">CodePlex</a> Oct 31, 2015 by <a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/wiki/Contributors/ajstadlin.md">ajs</a><!--/HtmlToGmd.Migration-->
</div>

<!--HtmlToGmd.Foot-->
<div id="copyright">
<hr />
Copyright 2015 <a href="http://www.gridtrak.com">GridTrak</a>
</div>
<!--/HtmlToGmd.Foot-->
</body>
</html>