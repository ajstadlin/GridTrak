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

Description | Photo |
----- | ----- |
**Photo - GridTrak Model A, Rev 9.5 kit**<br /> **Operational Open Source SynchroPhasor PMU with GPS Integration.** | <img src="https://github.com/ajstadlin/GridTrak/blob/master/Documentation/Wiki.github/Open_Source_SynchroPhasor_PUM_Project/GridTrak_PMU_Mod_A_9-7_web.jpg" alt="" width="449" /> |
  |   |