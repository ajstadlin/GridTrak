# GridTrak Open Source PMU Integration with openPDC
## Part 1 - Setup openPDC and Connect the PMU Sensor to Collect Data
<p><span style="font-size: xx-small;"><span style="color: #ff0000;"><span style="font-size: x-small;">Updated Mar 4, 2012, ajs</span></span></span><span style="font-size: x-small;">&nbsp;</span></p>
### 1.&nbsp; openPDC Installation 
#### Configuration Tested Platform (others may also work)
<ul>
<li>Applies to openPDC v1.4 Release</li>
<li>SQL Server Express 2008</li>
<li>Microsoft .NET Framework 4, Full Version</li>
<li>Windows 7 Professional/32, P4 (Hyper Threading enabled) 3 GHz, 1GB RAM, 250GB SATA HD, Ethernet</li>
<li>Serial Port (for GridTrak PMU sensor)</li>
<li>openPDC and PMU Connection Tester will be installed in folders outside of the Windows standard Program Files and Documents paths to avoid encountering issues with User specific privileges and Program Files virtual store.</li>
</ul>
#### Downloading the openPDC Release Software
<ul>
<li>
<div>Navigate to the openPDC project with the link: <a href="https://github.com/GridProtectionAlliance/openPDC" target="_blank">openPDC - The Open Source Phasor Data Concentrator</a></div>
</li>
<li>Select the Downloads tab and download the <strong>openPDCSetup.zip</strong> file to a convenient folder. After you click on the <strong>openPDCSetup.zip</strong> link, you may be prompted to accept a license agreement. After accepting the license, a Save As style dialog is presented for you to select a folder and rename the file. For this document, we will save the file as <strong>C:\openPDC\Synchrophasor.Installs.zip</strong> and extract it into the <strong>C:\openPDC\Synchrophasor.Installs </strong>folder. The following is a screen shot of the results.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/Synchrophasor_Installs_ExtractedFolder.png"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="Synchrophasor_Installs_ExtractedFolder" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/Synchrophasor_Installs_ExtractedFolder_thumb.png" alt="Synchrophasor_Installs_ExtractedFolder" width="484" height="248" border="0" /></a></p>
<ul>
<li>Reading the README.txt is a good idea. In this release, this tells us to simply extract the files as we have done and run the <strong>Setup.exe </strong>to install openPDC.</li>
</ul>
#### Running openPDC Setup
<ul>
<li>Right-click on the <strong>Setup.exe</strong> program and select the <strong>Run as administrator</strong> option from the shortcut menu as illustrated in the following screen shot.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/Synchrophasor_Installs_RunAsAdministrator.png"><img style="padding-left: 0px; padding-right: 0px; display: inline; float: left; padding-top: 0px; border-width: 0px;" title="Synchrophasor_Installs_RunAsAdministrator" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/Synchrophasor_Installs_RunAsAdministrator_thumb.png" alt="Synchrophasor_Installs_RunAsAdministrator" width="484" height="81" align="left" border="0" /></a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<ul>
<li>The following is a series of screen shots for a simple openPDC setup installing all the options.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217707"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Setup_1" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217708" alt="openPDC_Setup_1" width="244" height="196" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217709"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Setup_2" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217710" alt="openPDC_Setup_2" width="244" height="198" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217711"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Setup_3" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217712" alt="openPDC_Setup_3" width="244" height="198" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217713"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Setup_4" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217714" alt="openPDC_Setup_4" width="244" height="198" border="0" /></a></p>
<ul>
<li>Using Windows 7 Professional, 32 bit operating system.&nbsp;&nbsp; Install the software to an easy to find and maintain stand-alone folder in drive C.&nbsp;&nbsp; Installing in &ldquo;Program Files&rdquo; may hide settings in virtual stores or in my &ldquo;Documents&rdquo; space and may conflict with running openPDC as a service while not logged in.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217715"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Setup_5" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217716" alt="openPDC_Setup_5" width="244" height="198" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217717"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Setup_6" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217718" alt="openPDC_Setup_6" width="244" height="198" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217719"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Setup_7" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217720" alt="openPDC_Setup_7" width="244" height="198" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217721"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Setup_8" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217722" alt="openPDC_Setup_8" width="244" height="198" border="0" /></a></p>
<ul>
<li>The <strong>Configuration Setup Utility</strong> automatically runs after the software is installed by <strong>Setup.exe</strong>.&nbsp;</li>
<li>Install a new &ldquo;<strong>openPDC</strong>&rdquo; database in the local SQL Server Express.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217723"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_9" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217724" alt="openPDC_Config_Setup_9" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217725"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_10" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217726" alt="openPDC_Config_Setup_10" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217727"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_11" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217728" alt="openPDC_Config_Setup_11" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217729"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_12" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217730" alt="openPDC_Config_Setup_12" width="244" height="207" border="0" /></a></p>
<ul>
<li>Using Windows Integrated Authentication to connect to the local SQL Server Express database service.&nbsp; The openPDC v1.4.90.0 release does not completely implement this feature in the Configuration Setup Utility.&nbsp; As a work around, uncheck the &ldquo;Use integrated security for openPDC Manager&rdquo; option, then add &ldquo;; Integrated Security=SSPI&rdquo; to the Advanced Settings Connection String property.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217731"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_13" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217732" alt="openPDC_Config_Setup_13" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217733"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_23f_SSPI" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217734" alt="openPDC_Config_Setup_23f_SSPI" width="417" height="207" border="0" /></a></p>
<ul>
<li>Click the <strong>Test Connection</strong> button and verify that the database connection succeeds before continuing.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217735"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_15" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217736" alt="openPDC_Config_Setup_15" width="211" height="130" border="0" /></a></p>
<ul>
<li>Use Windows Authentication for Account Credentials and optionally allow the credentials to be used by openPDC Manager.&nbsp; Also select running openPDC as a service.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217737"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_16" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217738" alt="openPDC_Config_Setup_16" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217739"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_17" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217740" alt="openPDC_Config_Setup_17" width="244" height="207" border="0" /></a></p>
<ul>
<li>Use the Historian defaults</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217741"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_18" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217742" alt="openPDC_Config_Setup_18" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217743"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_19" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217744" alt="openPDC_Config_Setup_19" width="244" height="207" border="0" /></a></p>
<ul>
<li>The remaining setup process will check for and provide the option to erase a previous database, if there is one.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217745"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_20" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217746" alt="openPDC_Config_Setup_20" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217747"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_23g_SSPI_DatabaseExists" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217748" alt="openPDC_Config_Setup_23g_SSPI_DatabaseExists" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217749"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_21" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217750" alt="openPDC_Config_Setup_21" width="244" height="207" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217751"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Config_Setup_22" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217752" alt="openPDC_Config_Setup_22" width="244" height="207" border="0" /></a></p>
<ul>
<li>Installing PMU Connection Tester is simple.&nbsp; Like openPDC, install PMU Connection Tester in its own folder outside of the scope of the &ldquo;Program Files&rdquo; and &ldquo;Documents&rdquo;.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217753"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_ConnTester_Setup_24" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217754" alt="openPDC_ConnTester_Setup_24" width="244" height="198" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217755"><img style="margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_ConnTester_Setup_25" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217756" alt="openPDC_ConnTester_Setup_25" width="244" height="198" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217757"><img style="margin: 0px; padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_ConnTester_Setup_26" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217758" alt="openPDC_ConnTester_Setup_26" width="244" height="198" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217759"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_ConnTester_Setup_28" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=217760" alt="openPDC_ConnTester_Setup_28" width="244" height="198" border="0" /></a></p>
<hr />
<h2>2. Verify Sensor Communications with PMU Connection Tester</h2>
<ul>
<li>Run PMU Connection Tester</li>
<li>Configure the <strong>Connection Parameters, Serial</strong> tab settings for the COM port, Baud Rate = 11520, Parity = None, Stop Bits = One, Data Bits = 8, DTR and RTS unchecked.</li>
<li>Configure the <strong>Protocol</strong> tab setting for <strong>IEEE C37.118-2005</strong></li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219027"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="PmuConnTester_1" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219028" alt="PmuConnTester_1" width="244" height="219" border="0" /></a></p>
<ul>
<li>Below the <strong>Graph </strong>select the <strong>Settings </strong>tab</li>
<li>In the <strong>Application Settings </strong>section set <strong>MinimumFrameDisplayBytes</strong>&nbsp; to <strong>512</strong></li>
<li>In the <strong>Connection Settings </strong>section set <strong>AutoStartDataParsingSequence </strong>to <strong>False</strong></li>
<li>In the <strong>Phase Angle Graph</strong> section set <strong>PhaseAngleGraphStyle </strong>to <strong>Raw</strong></li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219029"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="PmuConnTester_2" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219030" alt="PmuConnTester_2" width="244" height="219" border="0" /></a><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=347179" alt="PMUConnTester_2a" width="244" height="219" /></p>
<ul>
<li>In the <strong>Protocol</strong> click the <strong>Connect</strong> button</li>
<li>Select the <strong>Command:&nbsp; Send Header Frame </strong>then click the <strong>Send </strong>button</li>
<li>Select the <strong>Command:&nbsp; Send Config Frame 1&nbsp; </strong>then click the <strong>Send </strong>button</li>
<li>Select the <strong>Command:&nbsp; Send Config Frame 2&nbsp; </strong>then click the <strong>Send </strong>button</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219031"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="PmuConnTester_3" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219032" alt="PmuConnTester_3" width="244" height="219" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219033"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="PmuConnTester_4" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219034" alt="PmuConnTester_4" width="244" height="219" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219035"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="PmuConnTester_5" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219036" alt="PmuConnTester_5" width="244" height="219" border="0" /></a></p>
<ul>
<li>Select the <strong>Command:&nbsp; Enable Real-time Data</strong>&nbsp; then click the <strong>Send </strong>button</li>
<li>To increase the sample density in the chart, increase the settings for <strong>FrequencyPointsToPlot </strong>and <strong>PhaseAnglePointsToPlot</strong>&nbsp;</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219037"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="PmuConnTester_6" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219038" alt="PmuConnTester_6" width="244" height="219" border="0" /></a></p>
<hr />
<h2>3.&nbsp; Configure Your Company, Vendor, and Vendor Device in openPDC Manager</h2>
<p>openPDC comes preloaded with Company, Vendor, and Device records.&nbsp; However, if your Company, Vendor, and Device is not included in these, it is a good idea to add your own records to the openPDC.&nbsp; If you are new to openPDC, entering this information is a simple introduction to the look and feel of using the openPDC Manager.</p>
<ul>
<li>Run the openPDC Manager and select <strong>Manage </strong>menu and click the <strong>Companies </strong>option.
<ul>
<li>Click the <strong>Clear </strong>button to setup a new blank <strong>Company</strong> record.</li>
<li>Enter your <strong>Company</strong> information then click the <strong>Save</strong> button.</li>
</ul>
</li>
</ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=350202" alt="" width="404" height="384" />&nbsp;<img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=350203" alt="" width="404" height="384" />&nbsp;</p>
<ul>
<li>Select the <strong>Manager </strong>menu, <strong>Vendors </strong>option
<ul>
<li>Click the <strong>Clear </strong>button to setup a new blank <strong>Vendor </strong>record.</li>
<li>Enter your <strong>Vendor</strong> information then click the <strong>Save</strong> button.</li>
</ul>
</li>
</ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=350204" alt="" width="404" height="384" />&nbsp;</p>
<ul>
<li>Select the <strong>Manager </strong>menu, <strong>Vendor Devices </strong>option
<ul>
<li>Click the <strong>Clear </strong>button to setup a new blank <strong>Vendor Device </strong>record.</li>
<li>Enter your <strong>Vendor Device</strong> information then click the <strong>Save</strong> button.</li>
</ul>
</li>
</ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=350205" alt="" width="404" height="384" />&nbsp;</p>
<hr />
<h2>4.&nbsp; Configure the Sensor as a New Device in openPDC Manager</h2>
<ul>
<li>If PMU Connection Tester is Connected, click the <strong>Disconnect </strong>button in the <strong>Protocol </strong>tab.&nbsp; The PC&rsquo;s Serial Port can only be used by one application at any time.</li>
<li>Run <strong>openPDC Manager </strong>and select the <strong>Add New </strong>option from the <strong>Devices </strong>menu.</li>
</ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=350206" alt="" width="213" height="144" /></p>
<p>&nbsp;</p>
<ul>
<li>Click the <strong>Build </strong>link to the right of the <strong>Connection String </strong>field.</li>
<li>In the <strong>Build Connection String </strong>dialog&rsquo;s <strong>Serial </strong>tab, select the COM Port and set the other parameters to Baud Rate = 11520, Parity = None, Stop Bits = One, Data.&nbsp; Be sure to set the <strong>Stop Bits </strong>to <strong>One</strong> because this is not the default in this dialog.</li>
</ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=350207" alt="" width="404" height="320" /></p>
<p style="padding-left: 60px;"><strong>port=COM7; baudrate=115200; parity=None; stopbits=One; databits=8; dtrenable=false; rtsenable=false; transportprotocol=serial; interface=0.0.0.0;</strong></p>
<p>&nbsp;</p>
<ul>
<li>Click the <strong>Next </strong>button at the bottom right of the screen</li>
<li>Click the <strong>Request Configuration From openPDC </strong>button to get the read the configuration from the sensor using IEEE C37.118-2005 protocol.</li>
<li>Select your <strong>Company</strong></li>
<li>Select the <strong>Interconnection </strong>that the sensor will be recording data for.</li>
</ul>
<p><img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=350210" alt="" width="404" height="320" /></p>
<p>&nbsp;</p>
<ul>
<li>Click the <strong>Modify Configuration </strong>button to review the configuration retrieved from the sensor.&nbsp; Close the dialog by clicking its <strong>Save</strong> button.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219052"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_5" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219053" alt="openPDC_Manager_5" width="404" height="171" border="0" /></a></p>
<ul>
<li>I noticed a <strong>Save Configuration </strong>button near the <strong>Modify Configuration </strong>button in the wizard&rsquo;s Step 2 screen.&nbsp; When I clicked this and saved the configuration to an XML file, I got the following Error.&nbsp; I didn&rsquo;t click this button in previous tests, so this error may not be a problem for this exercise.&nbsp; I will report it in the Issue Tracker.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219054"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_5_OnSaveButton_Error" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219055" alt="openPDC_Manager_5_OnSaveButton_Error" width="404" height="201" border="0" /></a></p>
<ul>
<li>Click the <strong>Next </strong>button at the bottom right of the <strong>Device Configuration Wizard </strong>screen to continue to Step 3.</li>
<li>Select your <strong>Vendor </strong><strong>Device</strong></li>
<li>Enter your <strong>Longitude and Latitude</strong></li>
<li>Review the information and click the <strong>Finish </strong>button.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219073"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_6" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219074" alt="openPDC_Manager_6" width="404" height="326" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219075"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_7" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219076" alt="openPDC_Manager_7" width="404" height="326" border="0" /></a></p>
<ul>
<li>In the <strong>Devices </strong>menu, click the <strong>Browse </strong>option</li>
<li>Click the <strong>Acronym </strong>link and update the <strong>Time Zone </strong>property</li>
<li>Make sure the <strong>ID Code (AccessID) </strong>is set to the Sensor&rsquo;s ID , then click the <strong>Save </strong>button.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219078"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_8" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219079" alt="openPDC_Manager_8" width="404" height="326" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219088"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_11" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219089" alt="openPDC_Manager_11" width="404" height="326" border="0" /></a></p>
<ul>
<li>After the <strong>Save</strong>, you are returned to the devices browser.&nbsp; Click the <strong>Acronym </strong>link for the sensor again.</li>
<li>In the <strong>Manage Devices </strong>screen, click the <strong>Initialize </strong>link, then click <strong>Yes </strong>to initialize the sensor.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219090"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_12" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219091" alt="openPDC_Manager_12" width="404" height="326" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219092"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_12a" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219093" alt="openPDC_Manager_12a" width="404" height="326" border="0" /></a></p>
<ul>
<li>In the <strong>Monitoring </strong>menu, select the <strong>System Console </strong>option and scroll back to verify that the sensor was initialized.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219230"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_14" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219231" alt="openPDC_Manager_14" width="404" height="326" border="0" /></a></p>
<ul>
<li>Try the <strong>Home </strong>and <strong>Monitoring, Input Status &amp; Monitoring</strong></li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219232"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_15" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219233" alt="openPDC_Manager_15" width="404" height="326" border="0" /></a><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219234"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_16" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219235" alt="openPDC_Manager_16" width="404" height="326" border="0" /></a></p>
<ul>
<li>The <strong>Manage </strong>menu, <strong>System Settings </strong>option provides settings for adjusting the chart.</li>
</ul>
<p><a href="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219722"><img style="padding-left: 0px; padding-right: 0px; display: inline; padding-top: 0px; border-width: 0px;" title="openPDC_Manager_SystemSettings" src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/GridTrak_Open_Source_PMU_Integration_with_openPDC.files/=219723" alt="openPDC_Manager_SystemSettings" width="404" height="349" border="0" /></a></p>