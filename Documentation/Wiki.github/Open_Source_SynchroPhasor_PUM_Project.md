# Open Source SynchroPhasor PMU Project
#### Project Description
The Open Source SynchroPhasor PMUs Project provides resources that enable you to build your own SynchroPhasor measuring sensors for use with the openPDC project, research, development, or electric grid observation.

* Open source resources enabling you to construct your own PMU for research, experimentation, development, or fun. The information provided here is intended to be reproducible, repeatable, and reusable in your own projects.
* The open source resources hosted here can be used with the openPDC - Open Source Phasor Data Concentrator project software.
* **If you have an open source PMU, please let us know so we can include a description and links to your project.**

## GridTrak Open Source PMU (GTosPMU)
### How GridTrak Open Source PMU (GTosPMU) Works:
The GridTrak PMU is a hardware sensor designed to measure and transmit AC SynchroPhasor Frequency, Phase Angle, and Magnitude digitally by:

* Converts the AC signal to complimentary square waves using a operatonal amplifier and precision voltage reference
* Uses a precision timer to measure square wave cycle (Frequency) and offset from a PPS time reference (Phase angle)
* Calculates Magnitude for the ideal sine wave of the signal
* Optionally uses GPS for precision PPS triggering and SynchroPhasor measurement values
* Transmits Data per IEEE C37.118-2005 specifications or in plain text
* Compatible with openPDC for extended application and systems development

The GridTrak concept filters out the original wave form, harmonics, and distortion by the square wave conversion of the analog input signal to a digital signal. If you need to measure or observe wave form distortion or harmonics, this is not the PMU for the job.

<table style="width: 100%;">
<tbody>
<tr>
<td style="width: 40%;">
<p><strong>Photo - </strong><strong>GridTrak Model A, Rev 9.5 kit</strong></p>
<p><strong>Operational&nbsp;Open Source SynchroPhasor PMU with GPS Integration.</strong></p>
<p>&nbsp;</p>
</td>
<td style="width: 60%;"><strong><img src="http://download.codeplex.com/Download?ProjectName=gridtrak&amp;DownloadId=361556" alt="" width="449" /></strong></td>
</tr>
<tr>
<td style="width: 40%;">
<p><strong>&nbsp;</strong></p>
</td>
<td style="width: 60%;">&nbsp;</td>
</tr>
<tr>
<td style="width: 40%;">
<p><strong>Photo - Testing a GridTrak Model A PMU with Garmin GPS 15xL-W.&nbsp; </strong></p>
<p><strong>A bypass capacitor (upper right)&nbsp;is added to the PPS to reduce transient false signals.</strong></p>
<p><strong>Two serial port connections are shown.&nbsp;&nbsp;A completed&nbsp;Model A PMU will only require 1 serial port connection for operation, however, the second serial port remains available for direct PC to GPS connection for development and diagnostics.&nbsp; </strong></p>
</td>
<td style="width: 60%;"><img src="http://download.codeplex.com/Download?ProjectName=gridtrak&amp;DownloadId=294840" alt="" width="449" height="276" /></td>
</tr>
</tbody>
</table>
