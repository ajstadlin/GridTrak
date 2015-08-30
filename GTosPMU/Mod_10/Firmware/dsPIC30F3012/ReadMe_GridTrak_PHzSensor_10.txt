GridTrak PHzSensor Model 10 - Firmware Release Notes
----------------------------------------------------
NOTE:  This firmware is work in progress and not ready for production release.

PHzSensor_3012 Firmware Version 7,  March 9, 2011

* Requires 10.0 or higher Software.  Not compatible with 9.x or earlier software.
* Uses 115200 bps RS232 baud rate
* GPS PPS Synchronization Input Enabled on INT0

Copyright 2010-2011, GridTrak, PHzMonitor, MultiAxisMotion, openPMU
  
GridTrak PHzSensor Model 10 Hardware and Software Features
----------------------------------------------------------
1. Measures SynchroPhasor, Frequency, and calculates Amplitude
2. Integrates with the GridTrak Application Software for real time and historic displays
  * The GridTrak Application Software is under construction at this time.
3. Records Data to CSV Log files
4. Transmits Data, contact Author for Sensor_ID and Sensor_PIN assignments
  A.  Web Service transmission to www.GridTrak.com using assigned Sensor_ID and Sensor_PIN
  B.  URL Encoded transmission to the www.GridTrak.com HTTP receiver using assigned Sensor_ID and Sensor_PIN
  * The GridTrak Web Service is under construction at this time.

The latest updates and documentation can be found at the following URL:
  http://wiki.gridtrak.net/wiki/index.php/GridTrak
  
Project Build Platform
----------------------
  MicroChip dsPIC30F3012 Micro Controller
  MicroChip MPLAB 8.46 and C30 Toolsuite, Academic Version
  Platform Tools Sources are found at the following URL:

  http://www.microchip.com

Author Contact Information
--------------------------
GridTrak Project
Arnold Stadlin
121 W. Earleigh Heights Rd.
Severna Park, MD 21146
ajstadlin@GridTrak.com

License
-------
LGPL, Source Code is Provided, may be used and/or embedded as is or modified.
  A.  If un-modified, the Author "GridTrak" should be credited
  B.  The Author does not assume any responsibility or liability
  C.  There is no warranty or support, however, feel free to contact the author.

Change Log
----------
Mar 09, 2011 0007  Model 10/dsPIC30F3012 Firmware is now Parallel with the 
                   GridTrak Model A/dsPIC30F3013

Feb 04, 2011 00A1  IEEE C37.118-2005 Phase Angle Output in Radians,  URX1 Interrupt
                   Priority increased to 6
Jun 16, 2010 0A00  24LC1024 EEPROM Removed from design.  PPS Sync on INT0 pin.
                   SynchoPhasor Measurement Capability added.
Mar 15, 2010 0911  Factory Default Config = Step Mode[3] Average,Mean, and Step Count = 20.  
                   Makes smoother auto start for Host Software
Jan 15, 2010 0910  PLLx8 24 MIPs implementation.  Not compatible with 9.09x and earlier software
                   RS232 Baud Rate Increased from 57600 to 115200 bps
Jan 14, 2010 0909  50Hz and 60Hz range select implemented