# Open Source SynchroPhasor PMU Project
#### Project Description
The Open Source SynchroPhasor PMU Project provides resources that enable you to construct your own SynchroPhasor PMU sensor.  The resources in this project are intended to be open source, reproducible, repeatable, and reusable in your own projects.
* Open Source PMU sensors can be used with the openPDC - the Open Source Phasor Data Concentrator project software.

## GridTrak Open Source PMU (GTosPMU)
The *GridTrak* Open Source PMU or *GTosPMU* is a specific implementation of an Open Source SynchroPhasor PMU created using the resources hosted in this project.

### How the GridTrak Open Source PMU (GTosPMU) Works
The GridTrak PMU is a hardware sensor designed to measure and transmit AC SynchroPhasor Frequency, Phase Angle, and Magnitude digitally using the following technology:

* Converts the AC signal to complimentary square waves using an operatonal amplifier and precision voltage reference
* Uses a precision timer to measure the Frequency of square wave cycles and Phase Angle from a Pulse per Second (PPS) time reference
* Calculates the Voltage Magnitude for the ideal sine wave of the signal
* Optionally uses GPS for precision PPS triggering and SynchroPhasor measurement values
* Transmits Data per IEEE C37.118-2005 specifications or in plain text
* Compatible with openPDC and PMUConnectionTester for extended application and systems development

The GridTrak PMU's square wave conversion of the analog input signal to a digital signal filters out the original wave form, harmonics, and distortion.  If you need to measure or observe wave form distortion or harmonics, you need a different PMU.

Description | Photo |
----- | ----- |
**Photo - GridTrak Model A, Rev 9.5 kit**<br /> **Operational Open Source SynchroPhasor PMU with GPS Integration.** | <img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/Open_Source_SynchroPhasor_PUM_Project/GridTrak_PMU_Mod_A_9-7_web.jpg" alt="" width="449" /> |
  |   |