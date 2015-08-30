GridTrak Open Source SynchroPhasor PMU (GTosPMU) Sensor Model A - Design Notes
------------------------------------------------------------------------------
Current Version:  Model A, Rev 9.7 Release, Apr, 2012

Design Files
------------
Uses free PCB Design Software:  ExpressPCB - www.expresspcb.com
Schematics = *.sch
PCB Layout = *.pcb

Project Resources
-----------------
Home Page:  http://gridtrak.codeplex.com

GridTrak Resources
------------------
Web Site:  http://www.gridtrak.com
Resources:  http://wiki.gridtrak.com/wiki


Project Description
-------------------
The GridTrak Open Source SynchroPhasor PMU (GTosPMU) is an open source dsPIC30F3013 micro controller based SynchroPhasor sensor.  The sensor converts 9-12VAC voltage from a simple wall transformer to complimentary square waves and uses a 12MHz oscillator time source and micro controller to measure the frequency and phase angle of the signal.

Design Features
---------------
* Measures Frequency and Phase Angle for 60Hz and 50Hz AC signals
* Calculates the magnitude for an ideal sine wave of the signal
* Maximum sample rate = 30 to 60 samples per second, reliable rate = 10 samples per second
* GPS PPS Input
* Dual RS232 interfaces for communications with Host PC and GPS
* Uses the AC signal source as the circuit power supply.
* Optional jumper to isolate the AC signal from the circuit power supply (requires a separate DC power source for the circuit)
* Schematic, PCB Layout, and Firmware source code are Open Source

License
-------
Codeplex New BSD License
http://gridtrak.codeplex.com/license

===============================
Copyright 2006-2012 GridTrak
Author:  ajstadlin@gridtrak.com
===============================

History
-------
Apr 06, 2012  Rev 9.7 Release - minor adjustments
Mar 21, 2012  Rev 9.6 Release - PCB Power LED Orientation shape corrected to match kit LEDs
Feb 04, 2012  Rev 9.3 Release - Schematic and PCB Review and netlist updates
Sep 22, 2011  GPS Header Pins added
Aug 24, 2011  Schematic to PCB Layout netlist no longer used so schematic can have duplicate components
Aug 19, 2011  Bypass Capacitors added to PPS and INT external input pins, extra ground pins
Feb 10, 2011  Renamed to Grid Trak Open Source PMU (GTosPMU)
Jan 22, 2011  Fixed J6 label
Jan 20, 2011  Open Source PMU Model A, Rev 2:  Derived from GridTrak Model A, Rev 2
Jan 01, 2011  GridTrak Model A, Rev 2:  Minor improvements to the PCB Layout
Dec 19, 2010  GridTrak Model A, Rev 1:  First release of the GridTrak PMU
