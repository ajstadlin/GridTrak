GridTrak Open Source PMU (GTosPMU) Sensor Firmware Release Notes
----------------------------------------------------------------
Firmware Ver 090C, May 14, 2013 - Stable Release
Copyright 2006-2013, GridTrak

Description
-----------
dsPIC30F3013 Micro Controller firmware for the GTosPMU Sensor, Model A

Features
--------
* Dual UART for host PC and GPS communications
* GPS PPS input on INT0 external interrupt pin
* Dual channel INT1 and INT2 inputs from OpAmp for frequency and phase measurement
* PLLx8 x 12MHz for 24MIPS (96MHz) performance
* Target peak performance: 60 samples per second
* Desired optimal performance range 10 to 30 samples per second
* Design frequencies 60Hz and 50Hz modes, +/- 1.000Hz (actual will be more)
* Activity LED output pin
* All other I/O pins available for future use
  
Project Build Platform
----------------------
  MicroChip dsPIC30F3013 Micro Controller
  MicroChip MPLAB 8.76 and C30 Toolsuite 3.12, Academic Version
  Platform Tools Sources are found at the following URL:
  http://www.microchip.com

Author Contact Information
--------------------------
GridTrak Project
http://www.gridtrak.com
http://gridtrak.codeplex.com
Arnold Stadlin
321 Tufton Circle
Fallston, MD 21047
ajstadlin@gridtrak.com

License
-------
CodePlex New BSD License, Source Code is Provided "as is", may be used and/or embedded as is, or modified.
Simplified:
  A.  If un-modified, the Author "GridTrak" should be credited
  B.  The Author does not assume any responsibility or liability
  C.  There is no warranty or support, however, feel free to contact the author.

Revision Notes
--------------
** Stable Release ** May 14, 2013, FW Ver 090C
--------------
May 14, 2013, FW Ver 090C.0  Error Handler ISRs removed
Feb 12, 2013, FW Ver 090A.0  RB0=1+RB1=1>Text, RB1=0>PMU, RB0=1+(RB2+RB3)=GPS mode select
Nov 15, 2012, FW Ver 0908.0  Stand Alone factory default, 0908.g = GPS enabled factory default
Mar 23, 2012, FW ver 0907  GPS Mode Commands updated
Nov 11, 2011, FW Ver 9  GPS Integrated Implementation
Nov 10, 2011, FW Ver 8a GPS Time Data Mode, GPS to UNIX Time verified
Nov 06, 2011, FW Ver 8  RS232 Passthru mode for direct GPS-Host communications
Apr 22, 2011, FW Ver 7  GPS/PPS Signal Synchronization debugged
Feb 26, 2011, FW Ver 6  EPROM Configuration Block transfer routines added.
Feb 10, 2011, FW Ver 4  GridTrak Open Source PMU (GTosPMU) Version
Jan 22, 2011, FW Ver 1  Contributed to Open Source PMU projects
Mar 21, 2010, FW Ver 0  PPS and Interval Synchronized Frequency, Amplitude, and Phase output
Feb 14, 2010, FW Ver 0  PPS Signal Input Test on INT0

=========================

Important Operation Notes
-------------------------
1.  When a PPS is not connected, connect the PPS header pin to ground.
2.  When RB0 is high, the PMU will continously transmit data after resets using the RB1, RB2, and RB3 pin settings
    When RB0 is low (grounded), the RB1, RB2, and RB3 pin settings are ignored.
  A.  Set RB1 high for Text mode.  Set RB1 low for IEEE C37.118 mode
  B.  Set RB2 high for PPS mode, set RB3 high for GPS Time mode.  Set RB2 and RB3 low for GPS Not Used mode.
  C.  You can set these high impedence input pins high by jumper wire to any +5Vdc pin 
  D.  You can set these high impedence input pins low by jumper to any ground pin. 
  
3.  Probes connected directly to the INT1 and INT2 header pins may have adverse affects on phase measurements.
  A.  0.1uF bypass capacitors may help

