GridTrak Open Source PMU (GTosPMU) - SynchroPhasor Measurement Application
--------------------------------------------------------------------------
Release Notes - Read Me
GTosPMU Software version 1.0.9.14, May 12, 2013

* Host PC software for the GridTrak GTosPMU Model A Open Source SyncrhoPhasor PMU sensor
* Firmware Version 090A required, has not been tested with earlier versions
* PHzSensor Model 10 is not supported by this Release.

Copyright 2010-2013, GridTrak
  
GTosPMU Software Features
-------------------------
1. Measures and Displays SynchroPhasor Phase Angle as defined by IEEE C37.118-2005
2. Measures Frequency
3. Calculates Amplitude
4. Records Data to CSV Log files

The latest updates and documentation can be found at:
  http://gridtrak.codeplex.com

Related resources can be found at:
  http://www.gridtrak.com
  http://wiki.gridtrak.net/wiki/index.php/GridTrak
  
Project Build Platform
----------------------
  Software 
    Microsoft .NET Framework 4 (Full Version)

  Firmware Version 0907 (earlier versions have not been tested with this software)
    GTosPMU Model A - MicroChip dsPIC30F3013 Micro Controller
    MicroChip MPLAB and C30 Toolsuite, Academic Version

Author Contact Information
--------------------------
GridTrak
Arnold Stadlin
321 Tufton Circle
Fallston, MD 21047
ajstadlin@GridTrak.com

License
-------
GridTrak License Summary
  Source Code is Provided "as is" and may be used and/or embedded as is or modified.
  A.  If un-modified "GridTrak" should be credited
  B.  The Author does not assume any responsibility or liability
  C.  There is no warranty or support
    C.1) Feel free to contact the author to provide feedback for improvements.
	C.2) Feel free to ask the author questions about the GridTrak PMU design or software.

Change Log
----------
May 12, 2013 - 1.0.9.14:  Sensor.Sync_To_PC failsafe timeout added to prevent freezing.  Date_FormatString config option added
Apr 18, 2013 - 1.0.9.12:  HzChart_Adjust configuraiton value added to shift the frequency displayed in the chart's X-Axis
                          Selectable 60Hz and 50Hz Chart Labels 
Apr 17, 2013 - 1.0.9.11:  Late PPS exception and time value configuration added.  Changed Late PPS exception to disabled by default
Feb 13, 2013 - 1.0.9.10:  If RB0=1 then (RB2 + RB3) = GPS Mode
Dec 16, 2012 - 1.0.9.9:  RB0=1 automatically resumes data transmit on reset.  RB1=0 TEXT mode, RB1=1 IEEE mode
Nov 11, 2012 - 1.0.9.8:  Data Logging Closed File exceptions handled.  Log File DateTime Format and Time Interval added
Mar 23, 2012 - 1.0.9.7:  PMU GPS Configuration updates
Mar 21, 2012 - 1.0.9.6:  Read Timeout error bug fixed
Mar 07, 2012 - 1.0.9.5:  Release
Feb 04, 2012 - 1.0.9.4:  Minor fixups
Feb 04, 2012 - 1.0.9.3:  Configuration Updates per Model A changes, Release Version
Nov 19, 2011 - 1.0.9.1:  Display both GPS and Host Time and difference for last sample
Nov 11, 2011 - 1.0.9.0:  GPS Integrated Implementation, compatible with Firmware version 9
Feb 10, 2011 - 1.0.0.5:  Renamed to GTosPMU
Jan 27, 2011 - 1.0.0.4:  Released to Open Source PMU Project
Jan 09, 2011 - 1.0.0.3:  Flush per Sample Data Logging.
Nov 25, 2010 - 1.0.0.2:  Terminal Logging Enable option added
Nov 23, 2010 - 1.0.0.1:  Error Dialogs removed from timed refresh routines.  Errors are still written to log files.
Nov 15, 2010 - 1.0:  Proof of Concepts Version.  Single Sensor, Local Measurements, and Logging