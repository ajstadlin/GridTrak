rem  Resynchronize to time-a.nist.gov
rem  Works for stand alone Windows Vista, 7, and 2008 Server
rem  Note:  For Active Directory, do not use /peers, use the AD Domain Controller
rem  Run As Administrator
w32tm /config /manualpeerlist:time-a.nist.gov /syncfromflags:MANUAL /update
w32tm /query /peers

rem If running on a schedule, use the /nowait parameter
rem w32tm /resync /rediscover /nowait

rem If running manually, wait for results
w32tm /resync /rediscover
pause