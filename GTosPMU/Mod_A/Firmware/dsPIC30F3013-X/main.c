/* Device header file */
#if defined(__XC16__)
    #include <xc.h>
#elif defined(__C30__)
    #if defined(__dsPIC30F__)
        #include <p30Fxxxx.h>
    #endif
#endif

#include <stdint.h>        /* Includes uint16_t definition                    */
#include <stdbool.h>       /* Includes true/false definition                  */

#include "system.h"        /* System funct/params, like osc/peripheral config */
#include "user.h"          /* User funct/params, such as InitApp              */

//// GTosPMU Sensor, Model A Firmware
// 08/06/14 FW Ver 0A00  Microchip XC16 v1.21 code
// 05/13/13 FW Ver 090C  Debug Suspected PPS Processing Freezing, Error ISRs removed.
// 02/11/13 FW Ver 090A  RB0=1+RB1=1>Text, RB1=0>PMU, RB0=1+(RB2+RB3)=GPS mode select
// 12/16/12 FW Ver 0909  Continous Transmission after Reset by default
// 11/15/12 FW Ver 0908.0 Stand Alone and 0908.g Use GPS
// 09/05/12 FW Ver 0908  Default to Not Use GPS
// 03/24/12 FW Ver 0907  GPS Configuration updated
// 03/03/12 FW Ver 0905  Release
// 02/04/12 FW Ver 0903  Release
// 11/11/11 FW Ver 9  GPS Integrated Implementation, Stable Release
// 11/10/11 FW Ver 8a RS232 OERR handling and GPS Time
// 11/06/11 FW Ver 8  GPS RS232 Integration, GPS Passthu mode
// 04/22/11 FW Ver 7  Release to CodePlex
// 04/08/11 FW Ver 7  Diagnostic Modes Revised, Real GPS implementation started
////
//  dsPIC30F3013 Micro Controller
//               +----------------------------------------+
//    Reset -> 1-| *MCLR                             AVdd |-28
//      RUN -> 2-| AN0/CN2/RB0                       AVss |-27
// TEXT/PMU -> 3-| AN1/CN3/RB1               AN6/OCFA/RB6 |-26
// GPS MODE -> 4-| AN2/CN4/RB2                    AN7/RB7 |-25
// GPS MODE -> 5-| AN3/CN5/RB3                AN8/OC1/RB8 |-24
//             6-| AN4/CN6/RB4                AN9/OC2/RB9 |-23
//             7-| AN5/CN7/RB5              U2RX/CN17/RF4 |-22 <- U2RX <-GPS
//             8-| Vss                      U2TX/CN18/RF5 |-21 -> U2TX ->GPS
//             9-| OSC1/CLKI                          Vdd |-20
//            10-| OSC2/CLKO/RC15                     Vss |-19
//    U1TX <- 11-| U1ATX/CN1/RC13   PGC/U1RX/SDI1/SDA/RF2 |-18
//    U1RX -> 12-| U1ARX/CN0/RC14   PGD/U1TX/SDO1/SCL/RF3 |-17
//            13-| Vdd                      SCK1/INT0/RF6 |-16 <- PPS <-GPS
// OpAmp.B -> 14-| IC2/INT2/RD9              IC1/INT1/RD8 |-15 <- OpAmp.A
//               +----------------------------------------+
///


//// Firmware Version
volatile unsigned int THIS_VER = 0x0A00;
#define GPS_OPT 0x00
//#define GPS_OPT 0x03

//// Absolute Serial Number
// Optional:  Increment this for every MCU for uniqueness.
// The SERIAL_NBR constant is set here and cannot be changed by the PC Host
volatile unsigned int SERIAL_NBR = 0x0A00;

// Note:  The Configuration has a Sensor_ID value that Can be configured by the Host PC for application requirements.

//// Library Inclusions
// Standard Libraries
#include <libpic30.h>     // EEPROM Routines
#include <math.h>
#include <string.h>
#include <time.h>
#include <stdlib.h>

//// Customized Project Includes
#include "RS232.h"        // Serial Port Library


/// I/O External interrupt ISRs Initialization
void IO_Init(void);
void InCap_Init(void);
void INTx_Init(void);

/// GPS/PPS Input Interrupt on INT0
void __attribute__((__interrupt__, no_auto_psv)) _INT0Interrupt(void);

/// Using Input Capture ICx Interrupts instead of INTx
void __attribute__((interrupt, no_auto_psv)) _IC1Interrupt(void);
void __attribute__((interrupt, no_auto_psv)) _IC2Interrupt(void);

/// Interrupt Routines suggested for EEPROM routines
//void _ISR __attribute__((__no_auto_psv__)) _AddressError(void);
//void _ISR __attribute__((__no_auto_psv__)) _StackError(void);
//void _ISR _DefaultInterrupt(void);
//void __attribute__((interrupt, no_auto_psv)) _DefaultInterrupt(void);


/// EEPROM Flash Configuration Procedure
#define CONFIG_NOLOAD 0x00
#define CONFIG_LOAD 0x01
#define CONFIG_FACTORY 0x02

void EGet_Config(unsigned char bFactory);   // Get the Configuration from EEPROM
void ESave_Config(void);                    // Save the Configuration to EEPROM

/// Operational Procedures
void Full_Stop(void);                       // Stop Everything prior to runing Prog_Reset() or exit from main()
void Prog_Reset(unsigned char bLoadCfg);    // Reset with Load Configuration Option
// void Proc_Ctrl(void);                    //-- Used by PHzSensor Model 10 Process Control Outputs

// UART Routines
void UART1_Init(void);
void UART2_Init(void);
void URX1_Proc(void);   // Process Commands Received on the UART
void URX2_Proc(void);
void RS_Passthru(void); // RS232 Passthru Mode Processing

void UTX1_Proc(void);   // Send Data out UART1
void U1TX_Eol(void);    // UART Transmit an End Of Line (EOL) CrLf
void Tx_Ping(void);

void Tx_PmuHeader(void);
void Tx_PmuCfg(unsigned char ucCfgNbr);

unsigned int Calc_CRC(unsigned char* sData, unsigned int iDataLen);
void Send_PmuData(unsigned char uCmd);  // Send PMU Data Message
void Send_PHzData(unsigned char uCmd);  // Send PHz Data as Text

/// Get GPS Information every interval in PPS's.  0= ignore GPS time, 1= Get GPS time for every PPS
//  Note:  The Garmin 15xL only sends the Time once per second.
//         The PPS clears the Fraction of Second.
//         This is only needed for drift of more than 1 second or on reboot.
volatile unsigned int GPS_PPSCount = 0;
volatile char GPS_Info[80];
volatile char GPS_Input[80];
volatile struct tm GPS_tm;
volatile time_t GPS_Time = 0;
void GPS_GetInfo(void);
void GPS_TxInfo(void);

//// Program Modes
// Normal Operation Modes, Persistent when saved in and restored from EEPROM
// Only One Program Mode is enabled
#define PROG_IDLE 0x00      // Run Without Transmitting Data
#define PROG_TEXT 0x01      // Streaming ASCII Text Mode
#define PROG_PMU  0x02      // PMU Binary Packets Mode
#define PROG_RSPT 0x03      // RS232 Pass Through Mode

// User initiated Modes that stay Active until device reset
// These revert to a Normal Operational Mode read from EEPROM after device reset
#define PROG_EXIT 0xFF      // Exit main() commanded

// Modes that revert to the EEPROM Saved Mode after completion
#define PROG_REBOOT 0xFE    // Load Config from EEPROM and Reset
#define PROG_RESET 0xFD     // Reset without EEPROM Reload
#define PROG_FACTORY 0xFC   // Reload with Factory Defaults
// Any Other ProgMode = unknown error condition, exit

/// Program Mode Global Variables
volatile unsigned char Prog_Mode = PROG_IDLE;       // Current Running Program Mode
volatile unsigned char Prog_PrevMode = PROG_TEXT;   // Save Previous ProgMode when changing modes


//// Diagnostics Modes - Used during Normal Program Operation
//   Typically these modes light the Activity LED for the duration of the operation
//   Multiple Diagnostics Modes can be enabled concurrently
// 0000 0000 0000 0000 No diagnostics, Run optimized
#define DIAG_NONE 0x0000

// **** **** **** ***1 Main Loop Duration
#define DIAG_MAIN 0x0001

// **** **** **** **1* UART Command Interpreter
#define DIAG_UCMD 0x0002

// **** **** **** *1** Data Transmit
#define DIAG_TXDATA 0x0008

// **** **** **** 1*** Input Capture Channel 1
#define DIAG_INCAP1 0x0010

// **** **** ***1 **** Input Capture Channel 1
#define DIAG_INCAP2 0x0020

// **** **** **1* **** Timer1 Overflow
#define DIAG_TIMER1 0x0040

// ***1 111* **** **** UART Operation Duration
#define DIAG_UTX1 0x0200
#define DIAG_URX1 0x0400
#define DIAG_URX2 0x1000

// 001* **** **** **** Zero Crossing Span
#define DIAG_ZXING 0x2000

// 01** **** **** ***** INT0 Duration, GPS PPS Signal
#define DIAG_INT0 0x4000

// 1000 **** **** **** Togggle Activity LED in Idle Mode
#define DIAG_IDLE 0x8000

/// Diagnostic Data Modes
// 0000 0000 Do not transmit diagnostic data
#define DATA_NONE 0x00

// Transmit Last GPS Information on PPS
#define DATA_GPS_INFO 0x08

/// Diagnostic Mode Global Variables
volatile unsigned int Diag_PrevMode = DIAG_NONE;    // Previous Diagnostics Modes
volatile unsigned char Data_PrevMode = DATA_NONE;


//// Timer Periods: Magic Numbers
// 12MHz Tosc with PLLx8:  Tcy = Tosc * 2 = 24 MHz
// To use the full clock resolution (no prescaling):
//   Set the Timer Period and let the Timer overflow.
// We want the maximum range of measurement for 16bits.
// Constraints: Period >= Range AND Period <= 65535  (16 bits)
//
// To get our frequency for PLLx8, we use the formula:
//   Frequency Hz = 24,000,000 / ( ModzMax + Offset + Delta)
//   For example:  24,000,000 / (369,234 + 30766 + 6) = 59.9991 Hz
//                 24,000,000 / (369,234 + 30766 - 1) = 60.00015 Hz
//   Resolution is about 0.00015 Hz per Timer tic.  Or better than 6 tics per millihertz.
//
// Overflows 	60Hz 6     50Hz 7
//     Range   61532      64000
//    Offset   30766      32000
//   Mod Max  369234     448000
// ** Period   61539      64000  ** <- the Magic Numbers we want
// Note:  ModzMax + Offset = 12,000,000 / Frequency Hz = 200,000 for 60Hz and 240,000 for 50Hz

// Oscillator Frequency  (PLLx8 / 4) = Tcy = Instruction Frequency
#define OSC_HZ 24000000

//----------
/// PLLx8, 12MHz, 60Hz Magic Numbers
//  Overflows = 6
//  Offset = 30766
//  Period = 61539  Largest 60Hz Range [PREFERED 60HZ PERIOD]
//  ModMax = 369234 = 6 x Period
#define OFS60 0x782E
#define PRD60 0xF063
#define MOD60 0x0005A252

/// PLLx8, 12MHz, 50Hz Magic Numbers
//  Overflows = 7
//  Offset = 32000
//  Period = 64000  Largest 50Hz Range [PREFERRED 50HZ PERIOD]
//  ModMax = 448000 = 7 x Period
#define OFS50 0x7D00
#define PRD50 0xFA00
#define MOD50 0x0006D600

//// Timer1 Constants
// 60Hz Nominal Frequency Intervals
// For Example an Interval of "1" is 60Hz or 1/60 sec, "2" is 30Hz or 2/60 sec
#define PRD60I01 49998  /* 60Hz, Prescale 1:8   */
#define PRD60I02 12499  /* 30Hz, Prescale 1:64  */
#define PRD60I03 18749  /* 20Hz, Prescale 1:64  */
#define PRD60I04 24999  /* 15Hz, Prescale 1:64  */
#define PRD60I05 31249  /* 12Hz, Prescale 1:64  */
#define PRD60I06 37499  /* 10Hz, Prescale 1:64  */
#define PRD60I10 15624  /*  6Hz, Prescale 1:256 */
#define PRD60I12 18749  /*  5Hz, Prescale 1:256 */
#define PRD60I15 23436  /*  4Hz, Prescale 1:256 */
#define PRD60I20 31249  /*  3Hz, Prescale 1:256 */
#define PRD60I30 46874  /*  2Hz, Prescale 1:256 */
#define PRD60I60 46874  /*  1Hz, Prescale 1:256, 2 Overflows */

// 50Hz Nominal Frequency Intervals
#define PRD50I01 59999  /* 50Hz, Prescale 1:8   */
#define PRD50I02 14999  /* 25Hz, Prescale 1:64  */
#define PRD50I05 37599  /* 10Hz, Prescale 1:64  */
#define PRD50I10 18749  /*  5Hz, Prescale 1:256 */
#define PRD50I25 46874  /*  2Hz, Prescale 1:256 */
#define PRD50I50 46874  /*  1Hz, Prescale 1:256, 2 Overflows */

#define PRESCAL00 0x0000  /* no prescale */
#define PRESCAL01 0x0010  /* 1:8         */
#define PRESCAL10 0x0020  /* 1:64        */
#define PRESCAL11 0x0030  /* 1:256       */

// Application Configuation Structure #0
// Make this structure the same size as _EE_ROW = 32 bytes or 16 words
typedef struct __attribute__((__packed__))
{
  unsigned int ThisVer;      // 0   // Hard Coded Version Number = THIS_VER
  unsigned int Sensor_ID;    // 2   // Host PC Configurable Unique Sensor ID, 0 = not defined yet
  unsigned int Sensor_PIN;   // 4   // Sensor's Password for Web Service Authentication
  unsigned char Prog_Mode;   // 6   // Running Mode, Default = PROG_TEXT for Streaming Text Mode
  unsigned char Nominal_Hz;  // 7   // nominal frequency of 60 or 50 Hz, Default = 60Hz
  unsigned int Diag_Mode;    // 8   // Diagnostics Options, Default = DIAG_TXDATA
  int HzCalOffset;           // 10  // Frequency Calibration Offset
  int T3CalStart;            // 12  // Added to Initial Timer Value
  int ZXCalOffset;           // 14  // Added to T3 value on IC1 Interrupt
  int T3CalOffset;           // 16  // Added to Final Timer Value
  unsigned int T3Offset;     // 18  // OFSET60;  // Value of the Measurement Range Mid Point. Default = 60Hz with n Overflows
  unsigned int T3Period;     // 20  // PRD60;    // Timer Period.  Default = 60Hz with n Overflows
  unsigned long T3MzMax;     // 22  // MZMAX60;  // Maximum with n Overflows and Modulus Zero.  Default = 60Hz with n Overflows
  unsigned int EOL;          // 26  // Default = CR+LF = 0x0d0a
  unsigned int UART1_BRG;    // 28  // Default = BRG_115200 = 12
  unsigned int UART2_BRG;    // 30  // Default = BRG_4800 = 312
} APP_CFG0;
APP_CFG0 ACfg0;


// SynchroPhasor Configuration structure
typedef struct __attribute__((__packed__))
{
  unsigned char TransMode;   // 0        // Transmit Data on Sample is TransMode=0, on SynchroPhasor Interval is TransMode=1
  unsigned char Data_Mode;   // 1        // Diagnostic Data Mode
  unsigned int Intervals;    // 2        // The number of SynchroPhasor Measurement Intervals between PPSs
  int T1CalStart;            // 4        // Timer1 Start Offset
  int T1CalPrd;              // 6        // Timer1 Period Calibration
  unsigned int T1Period;     // 8        // Timer1 Period
  unsigned int T1Prescale;   // 10       // Timer1 Prescale
  unsigned int Vref;         // 12       // OpAmp Voltage Reference in milli-volts  Default = 2500 = 2.5V
  unsigned int Use_GPS;      // 14       // 0x01 = GPS PPS, 0x02 = GPS Time, 0x03 = both, anything else = Not Use GPS
  unsigned int Detect[8];    // 16 to 31
} APP_CFG1;
APP_CFG1 ACfg1;


typedef struct __attribute__((__packed__))
{
  unsigned long Time_Base;    // 0
  char PMU_Station[16];       // 4 to 19
  unsigned int GPS_Interval;  // 20
  char PMU_Stuff[10];         // 22 to 31
} PMU_CFG2;                   // 32
PMU_CFG2 PmuCfg2;


//// Communications
// UART Baud Rate Generator Values
// Fcy = 12 MHz @ PLLx8 = 96 MHz (24 MIPS)

// Host PC RS232 Interface
// 12 = [ (24000000 / (16 * 115200 bps)) ] - 1
#define BRG_115200 12

// 25.0417 = [ (24000000 / (16 * 57600 bps)) ] - 1
//#define BRG_57600 25
// 38.0625 = [ (24000000 / (16 * 38400 bps)) ] - 1
//#define BRG_38400 38
// 77.125 = [ (24000000 / (16 * 19200 bps)) ] - 1
//#define BRG_19200 77
// 155.25 = [ (24000000 / (16 * 9600 bps)) ] - 1
//#define BRG_9600 155

// Garmin 15LxW RS232 Interface
// 311.5 = [ (24000000 / (16 * 4800 bps)) ] - 1
#define BRG_4800 312

//// Protocol
// I/O Default End of Line, EOL sequence
// Windows style EOL = CR+LF default Data Record Terminator
#define A_CRLF 0x0d0a
#define A_CR 0x0d
#define A_LF 0x0a
// "," character default Data Field Delimiter
#define A_COMMA 0x2C

// GridTrak Open Source PMU Protocol Message Prefix = "@"
#define A_SYNC_AT 0x40

/// IEEE Std C37.118-2005 SynchroPhasor Message Prefixes
// 0xAA = Frame Synchronization byte.  Start of Message Character
#define A_SYNC_AA 0xAA
// SYNC[1] Second byte
// bit[7] = Reserved
// bits[6,5,4] = Frame Type, bits[3,2,1,0] = Version Number (IEEE C35.118-2005 = 0001
//      r000 = Data Frame
#define A_SYNC_DATA 0x01
//      r001 = Header Frame
#define A_SYNC_HDR 0x11
//      r010 = Config Frame 1
#define A_SYNC_CFG1 0x21
//      r011 = Config Frame 2
#define A_SYNC_CFG2 0x31
//      r100 = Command Frame
#define A_SYNC_CMD 0x41

// Data Record Terminator and Field Delimiter
volatile unsigned int Data_Eol = A_CRLF;
volatile unsigned char Field_Delimit = A_COMMA;


/// C30 Exception Handlers
/// If your code gets here, you either tried to read or write a NULL pointer,
///  or your application overflowed the stack by having too many local variables or parameters declared.
//void _ISR __attribute__((__no_auto_psv__)) _AddressError(void)
//{
//  Nop();
//  Nop();
//}
//
//void _ISR __attribute__((__no_auto_psv__)) _StackError(void)
//{
//  Nop();
//  Nop();
//}
//
//void _ISR _DefaultInterrupt(void);
//void __attribute__((interrupt, no_auto_psv)) _DefaultInterrupt(void)
//{
//  while(1) ClrWdt()
//}


//// EEPROM Data
int _EEDATA(1024) EE_DataAry[512];

void EGet_Config(unsigned char bFactory)
{
  /// Read Configuration from the EEPROM
  int ii = 0;  // loop counter

  LATCbits.LATC15 = 1;

  // Set the EEPROM Address in
  _prog_addressT EEAdr;
  _init_prog_address(EEAdr, EE_DataAry);

  _memcpy_p2d16((void*)&ACfg0, EEAdr, _EE_ROW);
  _memcpy_p2d16((void*)&ACfg1, EEAdr + sizeof(ACfg0), _EE_ROW);
  _memcpy_p2d16((void*)&PmuCfg2, EEAdr + sizeof(ACfg0) + sizeof(ACfg1), _EE_ROW);

  if ((THIS_VER != ACfg0.ThisVer) || (bFactory == CONFIG_FACTORY))
  {
    // New MCU or Version, Reset values to the Defaults
    // After release, this will only happen if we change the THIS_VER or EEPROM location for ACfg0.This_Ver to differing values.
    ACfg0.ThisVer = (unsigned int)THIS_VER;
    ACfg0.Sensor_ID = SERIAL_NBR;
    ACfg0.Sensor_PIN = 0x0000u;
    ACfg0.Prog_Mode = (unsigned int)PROG_IDLE;
    ACfg0.Nominal_Hz = (unsigned int)60;
    ACfg0.Diag_Mode = (unsigned int)DIAG_TXDATA;  // (unsigned int)DIAG_TIMER1 + (unsigned int)DIAG_INT0;
    ACfg0.HzCalOffset = (int)0;
    ACfg0.T3CalStart = (int)0;
    ACfg0.ZXCalOffset = (int)0;
    ACfg0.T3CalOffset = (int)34;
    ACfg0.T3Offset = (unsigned int)OFS60;
    ACfg0.T3Period = (unsigned int)PRD60;
    ACfg0.T3MzMax = (unsigned long)MOD60;
    ACfg0.EOL = (unsigned int)A_CRLF;
    ACfg0.UART1_BRG = (unsigned int)BRG_115200;  // Network
    ACfg0.UART2_BRG = (unsigned int)BRG_4800;    // Garmin GPS Default

     // Erase a row in Data EEPROM at array EE_DataAry
    _erase_eedata(EEAdr, _EE_ROW);
      _wait_eedata();

    // Write a row to Data EEPROM from array CfgData
    _write_eedata_row(EEAdr, (void*)&ACfg0);
      _wait_eedata();

    ACfg1.TransMode = (unsigned char)0x00;        // PHzMonitor Mode
    ACfg1.Data_Mode = (unsigned char)DATA_NONE;
    ACfg1.Intervals = (unsigned int)0x0006;       // 6/60 seconds per Interval
    ACfg1.T1CalStart = (int)0;
    ACfg1.T1CalPrd = (int)0;
    ACfg1.T1Period = (unsigned int)PRD60I06;      // Period for 60Hz @ 6 Intervals per Second
    ACfg1.T1Prescale = (unsigned int)PRESCAL10;   // Prescale 1:64
    ACfg1.Vref = (unsigned int)3200;              // 2.5Vref + 0.7V for diode in milli-volts
    ACfg1.Use_GPS = (unsigned int)GPS_OPT;        // Default = Do Not Use GPS PPS and Time
    for (ii = 0; ii < 8; ii++)
    {
      ACfg1.Detect[ii] = 0x0000;
    }

     // Erase a row in Data EEPROM at array EE_DataAry
    _erase_eedata(EEAdr + sizeof(ACfg0), _EE_ROW);
      _wait_eedata();

    // Write a row to Data EEPROM from array CfgData
    _write_eedata_row(EEAdr + sizeof(ACfg0), (void*)&ACfg1);
      _wait_eedata();

    PmuCfg2.Time_Base = 1000000;            // Microseconds
    memset(PmuCfg2.PMU_Station, 0x00, 16);
    strcpy(PmuCfg2.PMU_Station, "GTosPMU A");
    PmuCfg2.GPS_Interval = 10;
    memset(PmuCfg2.PMU_Stuff, 0x00, 10);

    // Erase a row in Data EEPROM at array EE_DataAry
    _erase_eedata(EEAdr + sizeof(ACfg0) + sizeof(ACfg1), _EE_ROW);
      _wait_eedata();

    // Write a row to Data EEPROM from array CfgData
    _write_eedata_row(EEAdr + sizeof(ACfg0) + sizeof(ACfg1), (void*)&PmuCfg2);
      _wait_eedata();

  } // end if Load Factory Defaults

  // Set the EEPROM Address and Read the EEPROM Again
  _memcpy_p2d16((void*)&ACfg0, EEAdr, _EE_ROW);
  _memcpy_p2d16((void*)&ACfg1, EEAdr + sizeof(ACfg0), _EE_ROW);
  _memcpy_p2d16((void*)&PmuCfg2, EEAdr + sizeof(ACfg0) + sizeof(ACfg1), _EE_ROW);

  LATCbits.LATC15 = 0;
}


void ESave_Config(void)
{
  /// Read Configuration from the EEPROM
  LATCbits.LATC15 = 1;

  ACfg0.ThisVer = THIS_VER;
  if (ACfg0.Prog_Mode > PROG_PMU)
  {
    // Do not save Program modes higher than PROG_PMU
    // Reset to Text Mode as Default
    ACfg0.Prog_Mode = (unsigned char)PROG_TEXT;
  }

  _prog_addressT EEAdr;
  _init_prog_address(EEAdr, EE_DataAry);

  // Erase a row in Data EEPROM at array EE_DataAry
  _erase_eedata(EEAdr, _EE_ROW);
  _wait_eedata();

  // Write a row to Data EEPROM from array CfgData
  _write_eedata_row(EEAdr, (void*)&ACfg0);
  _wait_eedata();

  // Erase a row in Data EEPROM at array EE_DataAry
  _erase_eedata(EEAdr + sizeof(ACfg0), _EE_ROW);
  _wait_eedata();

  // Write a row to Data EEPROM from array CfgData
  _write_eedata_row(EEAdr + sizeof(ACfg0), (void*)&ACfg1);
  _wait_eedata();

  // Erase a row in Data EEPROM at array EE_DataAry
  _erase_eedata(EEAdr + sizeof(ACfg0) + sizeof(ACfg1), _EE_ROW);
  _wait_eedata();

  // Write a row to Data EEPROM from array CfgData
  _write_eedata_row(EEAdr + sizeof(ACfg0) + sizeof(ACfg1), (void*)&PmuCfg2);
  _wait_eedata();

  LATCbits.LATC15 = 0;
}


void IO_Init(void)
{
  /// Initialize I/O PINS and Associated Peripherals
  //  dsPIC30F3013 Micro Controller
  //               +----------------------------------------+
  //    Reset -> 1-| *MCLR                             AVdd |-28
  //             2-| AN0/CN2/RB0                       AVss |-27
  //             3-| AN1/CN3/RB1               AN6/OCFA/RB6 |-26
  //             4-| AN2/CN4/RB2                    AN7/RB7 |-25
  //             5-| AN3/CN5/RB3                AN8/OC1/RB8 |-24
  //             6-| AN4/CN6/RB4                AN9/OC2/RB9 |-23
  //             7-| AN5/CN7/RB5              U2RX/CN17/RF4 |-22 <- U2RX
  //             8-| Vss                      U2TX/CN18/RF5 |-21 -> U2TX
  //             9-| OSC1/CLKI                          Vdd |-20
  //            10-| OSC2/CLKO/RC15                     Vss |-19
  //    U1TX <- 11-| U1ATX/CN1/RC13   PGC/U1RX/SDI1/SDA/RF2 |-18
  //    U1RX -> 12-| U1ARX/CN0/RC14   PGD/U1TX/SDO1/SCL/RF3 |-17
  //            13-| Vdd                      SCK1/INT0/RF6 |-16 <- PPS
  // OpAmp.B -> 14-| IC2/INT2/RD9              IC1/INT1/RD8 |-15 <- OpAmp.A
  //               +----------------------------------------+

  // Turn OFF A/D Peripheral
  ADCON1bits.ADON = 0;

  // A/D ANx Pins Config:  1=Digital, 0=Analog
  ADPCFG = 0xFFFF;    // Set to All Digital

  // Tri-State Pins Config:  1=Input, 0=Output
  TRISFbits.TRISF6 = 1;   // INT0
  PORTFbits.RF6 = 0;
  TRISDbits.TRISD8 = 1;   // INT1
  PORTDbits.RD8 = 0;
  TRISDbits.TRISD9 = 1;   // INT2
  PORTDbits.RD9 = 0;

  // General Purpose Digital I/O.  Set to Default = Input
  // AN0..7/RB0..7
  TRISBbits.TRISB0 = 1;   // CN2
  PORTBbits.RB0 = 0;
  TRISBbits.TRISB1 = 1;   // CN3
  PORTBbits.RB1 = 0;
  TRISBbits.TRISB2 = 1;   // CN4
  PORTBbits.RB2 = 0;
  TRISBbits.TRISB3 = 1;   // CN5
  PORTBbits.RB3 = 0;
  TRISBbits.TRISB4 = 1;   // CN6
  PORTBbits.RB4 = 0;
  TRISBbits.TRISB5 = 1;   // CN7
  PORTBbits.RB5 = 0;
  TRISBbits.TRISB6 = 1;
  PORTBbits.RB6 = 0;
  TRISBbits.TRISB7 = 1;
  PORTBbits.RB7 = 0;

  // PWM Output Pins
  TRISBbits.TRISB8 = 0;   // OC1
  LATBbits.LATB8 = 0;
  TRISBbits.TRISB9 = 0;   // OC2
  LATBbits.LATB9 = 0;

  // UART Pins
  TRISFbits.TRISF4 = 1;   // U2RX
  TRISFbits.TRISF5 = 0;   // U2TX

  TRISCbits.TRISC13 = 0;  // U1ATX
  TRISCbits.TRISC14 = 1;  // U1ARX

  // Programmer/SPI Pins
  TRISFbits.TRISF2 = 1;     // SDI1/SDA/U1RX/PGC
  TRISFbits.TRISF3 = 0;     // SDO1/SCL/U1TX/PGD
  //TRISFbits.TRISF6 = 1;  // SCK1/INT0

  TRISCbits.TRISC15 = 0;  // CLKO or Activity LED
  LATCbits.LATC15 = 0;
}


void INTx_Init(void)
{
  /// PPS:  GPS Pulse Per Second Input
  // Disable INTx
  IEC0bits.INT0IE = 0;
  IEC1bits.INT1IE = 0;
  IEC1bits.INT2IE = 0;

  // Set INT0 Interrupt on Leading Edge Transition
  INTCON2bits.INT0EP = 0;

  // Clear INT0 Interrupt Status Flag
  IFS0bits.INT0IF = 0;

  // Set INT0 to Highest Priority
  IPC0bits.INT0IP = 6;
}


//// Timers
// Timer 1 times the Sample Intervals after the PPS
// Timer 3 records the elappsed time since the square wave input capture on OpAmp Channel A

// Overflow Counters
volatile unsigned int T1Overflow = 0;
volatile unsigned int T2Overflow = 0;
volatile unsigned int T3Overflow = 0;

// Measurements
volatile unsigned long E[2];  // Elapsed TMR3 between IC1 and INT0 (PPS)
volatile unsigned long T[2];  // Timer3 check points
volatile unsigned int M[2];   // Zero Crossing + to - Vref slope time

volatile unsigned long Msg_Id = 0;

// IEEE C37.118-2005 Time Fields
unsigned long PmuSoc = 0x00000000;      // SOC - Second of Century
unsigned long PmuFracSec = 0x00000000;  // Faction of Second


void Timer_Init(void)
{
  // Timer1 Configuration
  // Timer1 is zero synchronized with the PPS (INT0)
  //    The Timer1 interrupt is raised at configured intervals following the PPS
  T1CONbits.TON = 0;
  IEC0bits.T1IE = 0;
  TMR1 = ACfg1.T1CalStart;   // Calibratable Start Time, Default = 0
  IFS0bits.T1IF = 0;

  // Set Interval Timer Interrupt to 3rd Highest Priority
  IPC0bits.T1IP = 5;

  T1CON = T1CON & 0xFFCF;  // Clear the TCKPS[6,5] bits for adding the Prescaler value

  if (ACfg0.Nominal_Hz == 60)
  {
    // 60 Hz Timer1 Settings
    switch (ACfg1.Intervals)
    {
      case 1:
        ACfg1.T1Period = (unsigned int)PRD60I01;
        ACfg1.T1Prescale = (unsigned int)PRESCAL01;
        break;
      case 2:
        ACfg1.T1Period = (unsigned int)PRD60I02;
        ACfg1.T1Prescale = (unsigned int)PRESCAL10;
        break;
      case 3:
        ACfg1.T1Period = (unsigned int)PRD60I03;
        ACfg1.T1Prescale = (unsigned int)PRESCAL10;
        break;
      case 4:
        ACfg1.T1Period = (unsigned int)PRD60I04;
        ACfg1.T1Prescale = (unsigned int)PRESCAL10;
        break;
      case 5:
        ACfg1.T1Period = (unsigned int)PRD60I05;
        ACfg1.T1Prescale = (unsigned int)PRESCAL10;
        break;
      case 6:
        ACfg1.T1Period = (unsigned int)PRD60I06;
        ACfg1.T1Prescale = (unsigned int)PRESCAL10;
        break;
      case 10:
        ACfg1.T1Period = (unsigned int)PRD60I10;
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;
        break;
      case 12:
        ACfg1.T1Period = (unsigned int)PRD60I12;
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;
        break;
      case 15:
        ACfg1.T1Period = (unsigned int)PRD60I15;
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;
        break;
      case 20:
        ACfg1.T1Period = (unsigned int)PRD60I20;
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;
        break;
      case 30:
        ACfg1.T1Period = (unsigned int)PRD60I30;
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;
        break;
      default: // 60
        // Actually we don't need the timer because we are just waiting for the next PPS
        // If we set this up, we can use it to calibrate our own clock using the PPS
        ACfg1.T1Period = (unsigned int)PRD60I60;     // 2 Overflows required
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;  // TCKPS = 01 for 1:8 prescale
        break;
    }
  }
  else if (ACfg0.Nominal_Hz == 50)
  {
    // 50 Hz Timer1 Settings
    switch (ACfg1.Intervals)
    {
      case 1:
        ACfg1.T1Period = (unsigned int)PRD50I01;
        ACfg1.T1Prescale = (unsigned int)PRESCAL01;
        break;
      case 2:
        ACfg1.T1Period = (unsigned int)PRD50I02;
        ACfg1.T1Prescale = (unsigned int)PRESCAL10;
        break;
      case 5:
        ACfg1.T1Period = (unsigned int)PRD50I05;
        ACfg1.T1Prescale = (unsigned int)PRESCAL10;
        break;
      case 10:
        ACfg1.T1Period = (unsigned int)PRD50I10;
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;
        break;
      case 25:
        ACfg1.T1Period = (unsigned int)PRD50I25;
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;
        break;
      default: // 50
        // Actually we don't need the timer because we are just waiting for the next PPS
        // If we set this up, we can use it to calibrate our own clock using the PPS
        ACfg1.T1Period = (unsigned int)PRD50I50;     // 2 Overflows Required
        ACfg1.T1Prescale = (unsigned int)PRESCAL11;  // TCKPS = 01 for 1:8 prescale
        break;
    }
  }

  PR1 = ACfg1.T1Period;
  T1CON = T1CON | ACfg1.T1Prescale;  // T1CON | TCKPS
  T1Overflow = 0;

  // Enable Timer1 Interrupt
  IEC0bits.T1IE = 1;
  // Turn on Timer 1
  T1CONbits.TON = 1;

  // Note:  Time3 Configuration is done in the InCap_Init() procedure
}


//// Input Capture Variables
// IC2 is our "First Half" cycle's input.
volatile unsigned int IC2Data[4];
volatile unsigned short IC2Count = 0;

// IC1 is our "Second Half" cycle's input
volatile unsigned int IC1Data[4];
volatile unsigned short IC1Count = 0;

void InCap_Init()
{
  /// Initialize the Input Capture and Timer3
  // Turn off Timer 3 while we configure stuff
  T3CON = 0x0000;

  // Turn off IC1 and IC2 Input Capture
  IC1CON = 0x0000;
  IC2CON = 0x0000;

  // Disable IC Interrupts and Reset Flags
  IEC0bits.IC1IE = 0;  // Disable IC1 Interrupt
  IFS0bits.IC1IF = 0;  // Clear IC1 Interrupt Flag
  IEC0bits.IC2IE = 0;  // Disable IC2 Interrupt
  IFS0bits.IC2IF = 0;  // Clear IC2 Interrupt Flag

  // Set IC1 and IC2 Interrupts to priority to highest priority.
  // We can set both to the same priority because they occur sequentially in normal conditions
  _IC2IP = 6;
  _IC1IP = 6;

  // Input Capture Channel 2 Configuration
  // IC2CON.ICSDL[13]  = 0:    Continue in CPU Idle
  // IC2CON.ICTMR[7]   = 0:    Use Timer 3
  // IC2CON.ICI[6,5]   = 00:   Interrupt on Every Event
  // IC2CON.ICM[2,1,0] = 010:  Capture Edge; 010 = Falling Edge
  //   111 = Input Capture functions as interrupt pin only, when device is in Sleep or Idle mode
  //   (Rising edge detect only, all other control bits are not applicable.)
  //   110 = Unused (module disabled)
  //   101 = Capture mode, every 16th rising edge
  //   100 = Capture mode, every 4th rising edge
  //   011 = Capture mode, every rising edge
  //   010 = Capture mode, every falling edge
  //   001 = Capture mode, every edge (rising and falling)
  //   (ICI<1:0> does not control interrupt generation for this mode.)
  //   000 = Input capture module turned off
  IC2CON = 0x0002;

  // IC2Data is the clock cycles count since the previous trailing edge.
  // Point the FIFO Buffer Pointer to the IC2Data variable address
  IC2Data[0] = 0x0000;
  IC2Data[1] = 0x0000;
  IC2Data[2] = 0x0000;
  IC2Data[3] = 0x0000;

  // Input Capture Channel 1 Configuration
  // IC1CON.ICSDL[13]  = 0:    Continue in CPU Idle
  // IC1CON.ICTMR[7]   = 0:    Use Timer 3
  // IC1CON.ICI[6,5]   = 00:   Interrupt on Every Event
  // IC1CON.ICM[2,1,0] = 011:  Capture Edge; 011 = Rising Edge
  IC1CON = 0x0003;

  // IC1Data is the clock cycles count since the previous trailing edge.
  // Point the FIFO Buffer Pointer to the IC1Data variable address
  IC1Data[0] = 0x0000;
  IC1Data[1] = 0x0000;
  IC1Data[2] = 0x0000;
  IC1Data[3] = 0x0000;

  // Timer3 Configuration
  // T3CON.TON[15] = 1:  Turn On Timer
  // T3CON.TSIDL[13] = 0:  Continue in Idle Mode
  // T3CON.TGATE[6] = 0
  // T3CON.TCKPS[5,4] = 01: Prescaler = 1:8 = 1,500,000 ticks/second = 1.5 MHz
  // 01 = 1:8 Prescaled:  (1500000 ticks) / (60 Hz) =  25000 ticks/Hz = 25 ticks/mHz
  //      With a 1:8 Prescale, 60 Hz @ 25000 is in the 16bit range
  //
  // 00 = 1:1 Full Reso: (12000000 ticks) / (60 Hz) = 200000 ticks/Hz = 200 ticks/mHz
  //      With 1:1 resolution, we can set the Timer Period to 45000,
  //      Then 60 Hz will be 4 overflows + 20000
  //
  PR3 = ACfg0.T3Period;      // Set the Timer 3 Period to Configured value
  TMR3 = ACfg0.T3CalStart;	 // Calibratable Start Time, Default = 0
  IFS0bits.T3IF = 0;         // bcf INTCON,TMR0IF;  Reset Timer3 Flag
  T3Overflow = 0;            // Reset our Overflow Counter

  /// Timer3 Interrupt Priority Level is relatively low.
  //  We actually do not need the Timer3 interrupt for our measurement.
  //  We can use the overflow count to verify that our measurement is in acceptable range.
  IPC1bits.T3IP = 7;

  // Enable Timer3 Interrupt and Start the Timer
  IEC0bits.T3IE = 1;      // Enable the Timer Interrupt
  T3CONbits.TON = 1;      // Enable the Timer

  // Enable the Input Capture Interrupts
  IEC0bits.IC2IE = 1;
  IEC0bits.IC1IE = 1;
}


volatile float Frequency = (float)0.0;
volatile float Magnitude = (float)0.0;
volatile float PhaseAng = (float)0.0;
volatile unsigned int Crc16 = 0x0000;               // 16 bits CRC word
volatile char szSendxxx[17] = "0123456789abcdef";   // Scratch space for Send routines

void Send_PHzData(unsigned char uCmd)
{
  /// Transmit Data as comma delimited text
  if ((DIAG_TXDATA & ACfg0.Diag_Mode) == DIAG_TXDATA)  { LATCbits.LATC15 = 1; }

  char szData[40];     // working data string

  memset(szData, 0x00, sizeof(szData));

  strcat(szData, "PHz");
  if (uCmd == 0x00)
  {
    // Interval Sample
    strcat(szData, "-,");
  }
  else if (uCmd == 0x01)
  {
    // PPS Sample
    strcat(szData, "*,");
  }
  else
  {
    // Timer/PC Sync simulated PPS Sample
    strcat(szData, "+,");
  }
  strcat(szData, ltoa((char *)szSendxxx, (long)Msg_Id++, 10));

  // Compute the Frequency to 7 significant digits: ##.#####
  Frequency = (float)100000 * (float)ACfg0.Nominal_Hz * (float)400000 / (float)T[0];
  Frequency = (float)Frequency + (float)ACfg0.HzCalOffset;
  strcat(szData, ",");
  strcat(szData, ltoa((char *)szSendxxx, (long)Frequency, 10));

  // Compute and Transmit the Magnitude
  // Vp = Vref / sine( pi * M / T )
  Magnitude = (float)ACfg1.Vref / (float)sin((float)3.14159265359 * (float)M[0] / (float)T[0] );
  strcat(szData, ",");
  strcat(szData, ltoa((char *)szSendxxx, (long)Magnitude, 10));

  // Compute the Phase Angle, GridTrak/PHzSensor Text mode uses Degrees, IEEE C37.118-2005 uses Radians
  // Phase Angle = [(E - M/2)/T * 2pi] - 3pi/4 radians
  //PhaseAng = (long)((double)1000 * ( ( ((double)E[0] - (double)M[0]/(double)2) / (double)T[0] ) * (double)2 * (double)3.14159265359 )); // - ( (double)0.75 * (double)3.14159265359 ));
  PhaseAng = (float)360 * ( ((float)E[0] - (float)M[0]/(float)2) / (float)T[0] );
  PhaseAng = PhaseAng - (float)90;
  if (PhaseAng > (float)180)
  {
    PhaseAng = PhaseAng - (float)360;
  }
  PhaseAng = PhaseAng * (float)1000;
  strcat(szData, ",");
  strcat(szData, ltoa((char *)szSendxxx, (long)PhaseAng, 10));
  strcat(szData, ",");

  // Send the Time Stamp
  strcat(szData, ltoa((char *)szSendxxx, (long)PmuSoc, 10));
  strcat(szData, ",");
  strcat(szData, ltoa((char *)szSendxxx, (long)PmuFracSec, 10));
  strcat(szData, ",");

  // Send the Text Data
  PutsUART1((unsigned int *)szData);

  // Calculate the CRC and send it followed by EOL
  Crc16 = Calc_CRC((unsigned char *)szData, strlen(szData));
  PutsUART1((unsigned int *)ltoa((char *)szSendxxx, (long)Crc16, 16));
  U1TX_Eol();

  // Process Control Options
  //Proc_Ctrl();

  if ((DIAG_TXDATA & ACfg0.Diag_Mode) == DIAG_TXDATA)  { LATCbits.LATC15 = 0; }
}


/// IEEE C37.118-2005 stuff
unsigned int IFrameSz = 0x0000;       // IEEE Frame Size (bytes in the message packet)
unsigned int IDCode = 0x0000;         // ID Code
unsigned int ICmd = 0x0000;           // IEEE Command
unsigned char IExtFrame = 0x00;       // Extended Frame Input Byte

void Send_PmuData(unsigned char uCmd)
{
  // Transmit SynchroPhasor PMU Data Message in IEEE C37.118-2005 Format
  if ((DIAG_TXDATA & ACfg0.Diag_Mode) == DIAG_TXDATA)  { LATCbits.LATC15 = 1; }

  /// Transmit PMU Data IEEE C37.118-2005 format

  unsigned char ucData[36];
  memset(ucData, 0x00, sizeof(ucData));

  int ii = 0;  // loop counter
  unsigned char ucFloat[4];

  // 1. Data Message Sync Byte and Header Frame Type
  ucData[0] = A_SYNC_AA;
  ucData[1] = A_SYNC_DATA;

  // 2. Frame Size = 16 overhead bytes + 48 bytes PMU Header Data
  ucData[2] = 0x00;
  ucData[3] = 38;

  // 3. PMU Sensor ID UInt16
  ucData[4] = (unsigned char)((ACfg0.Sensor_ID & 0xFF00) >> 8);
  ucData[5] = (unsigned char)(ACfg0.Sensor_ID & 0x00FF);

  // 4. SOC = Our maintained copy
  ucData[6] = (unsigned char)((PmuSoc & 0xFF000000) >> 24);
  ucData[7] = (unsigned char)((PmuSoc & 0x00FF0000) >> 16);
  ucData[8] = (unsigned char)((PmuSoc & 0x0000FF00) >> 8);
  ucData[9] = (unsigned char)(PmuSoc & 0x000000FF);

  // 5. FRACSEC
  ucData[10] = (unsigned char)((PmuFracSec & 0xFF000000) >> 24);
  ucData[11] = (unsigned char)((PmuFracSec & 0x00FF0000) >> 16);
  ucData[12] = (unsigned char)((PmuFracSec & 0x0000FF00) >> 8);
  ucData[13] = (unsigned char)(PmuFracSec & 0x000000FF);

  // 6. STAT
  ucData[14] = 0x00;
  ucData[15] = 0x00;

  // 7. Phasor
  // Compute the Magnitude of ideal sine wave
  // Vp = Vref / sine( pi * M / T )
  Magnitude = (float)( (float)ACfg1.Vref / (float)sin((float)3.14159265359 * (float)M[0] / (float)T[0] ));

  // Compute the Phase Angle
  // Phase Angle = [(E - M/2)/T * 2pi] - 3pi/4 radians
  // PhaseAng = (long)((double)1000 * ( ( ((double)E[0] - (double)M[0]/(double)2) / (double)T[0] ) * (double)2 * (double)3.14159265359 )); // - ( (double)0.75 * (double)3.14159265359 ));

  // IEEE Specifies Radians; be sure to convert to Radians before transmission
  PhaseAng = (float)360 * ( ((float)E[0] - (float)M[0]/(float)2) / (float)T[0] );
  PhaseAng = PhaseAng - (float)90.0;
  if (PhaseAng > (float)180.)
  {
    PhaseAng = PhaseAng - (float)360;
  }
  // Convert Phase Angle to Radians
  PhaseAng = PhaseAng * (float)3.14159265359 / (float)180.0;

  memcpy(ucFloat, (void*)&Magnitude, 4);
  ucData[16] = ucFloat[3];
  ucData[17] = ucFloat[2];
  ucData[18] = ucFloat[1];
  ucData[19] = ucFloat[0];

  memcpy(ucFloat, (void*)&PhaseAng, 4);
  ucData[20] = ucFloat[3];
  ucData[21] = ucFloat[2];
  ucData[22] = ucFloat[1];
  ucData[23] = ucFloat[0];

  Msg_Id++;

  // 8. Frequency
  // Compute the Frequency
  Frequency = (float)((float)ACfg0.Nominal_Hz * (float)400000 / (float)T[0] );
  Frequency = Frequency + ((float)ACfg0.HzCalOffset / 10000);

  memcpy(ucFloat, (void*)&Frequency, 4);
  ucData[24] = ucFloat[3];
  ucData[25] = ucFloat[2];
  ucData[26] = ucFloat[1];
  ucData[27] = ucFloat[0];

  // 9. Rate of Change in Frequency
  // TODO!
  ucData[28] = 0x00;
  ucData[29] = 0x00;
  ucData[30] = 0x00;
  ucData[31] = 0x00;

  // 10.  Analog
  // TODO! - Rate of Change in Phase Angle
  ucData[32] = 0x00;
  ucData[33] = 0x00;
  ucData[34] = 0x00;
  ucData[35] = 0x00;

  // 11.  Digital
  // No Digital Data

  for (ii = 0; ii < 36; ii++)
  {
    while(U1STAbits.UTXBF);
      U1TXREG = (unsigned char)ucData[ii];
  }

  Crc16 = Calc_CRC(ucData, 36);

  // 12.  Calculate and Transmit the CRC UInt16
  while(U1STAbits.UTXBF);
    U1TXREG = (unsigned char)((Crc16 & 0xFF00) >> 8);
  while(U1STAbits.UTXBF);
    U1TXREG = (unsigned char)(Crc16 & 0x00FF);

  if ((DIAG_TXDATA & ACfg0.Diag_Mode) == DIAG_TXDATA)  { LATCbits.LATC15 = 0; }
}


volatile char szInt0xxx[17] = "0123456789abcdef";   // static scratch space for the INT0

void __attribute__((__interrupt__, no_auto_psv)) _INT0Interrupt(void)
{
  /// GPS PPS Input Interrupt
  if ((ACfg1.Use_GPS & 0x01) == 0x01)
  {
    // Save Elapsed TMR3 time ASAP
    E[0] = (unsigned long)TMR3 + ((unsigned long)T3Overflow * (unsigned long)ACfg0.T3Period);

    if ((DIAG_INT0 & ACfg0.Diag_Mode) == DIAG_INT0) { LATCbits.LATC15 = 1; }

    // Restart Timer1
    T1Overflow = 0;
    TMR1 = ACfg1.T1CalStart;    // Calibratable Start Time, Default = 0
    IFS0bits.T1IF = 0;

    /// Increment our copy of the SOC = Second of Century
    PmuSoc++;

    // Increment the last time received from the GPS before this PPS
    GPS_Time++;
    GPS_PPSCount++;

    if ((ACfg1.Use_GPS & 0x02) == 0x02)
    {
      if (GPS_Time > PmuSoc)
      {
        // Update our PmuSoc time, because our GPS Time is better
        PmuSoc = GPS_Time;

        // Reset the GPS PPS Counter
        GPS_PPSCount = 1;
      }
      else if ((PmuSoc != GPS_Time) || (GPS_PPSCount >= PmuCfg2.GPS_Interval))
      {
        // One or more of the GPS samples failed, or our PmuSoc is invalid
        PmuSoc = GPS_Time;

        // Reset the GPS PPS Counter
        GPS_PPSCount = 1;
      }
    }

    // Clear the Fraction of Second IEEE Time Field
    // 0x00** **** = "Normal Operation, Clock Locked" = Synchronized by PPS Signal
    PmuFracSec = 0x00000000;

    if (Prog_Mode == PROG_PMU)
    {
      Send_PmuData(0x01);
    }
    else if (Prog_Mode == PROG_TEXT)
    {
      Send_PHzData(0x01);
    }

    if ((DATA_GPS_INFO & ACfg1.Data_Mode) == DATA_GPS_INFO)
    {
      GPS_TxInfo();
    }
  }
  else
  {
    if ((DIAG_INT0 & ACfg0.Diag_Mode) == DIAG_INT0) { LATCbits.LATC15 = 1; }
  }

  // Clear INT0 Interrupt Status Flag
  IFS0bits.INT0IF = 0;

  if ((DIAG_INT0 & ACfg0.Diag_Mode) == DIAG_INT0)  { LATCbits.LATC15 = 0; }
}



unsigned long m_E0 = 0;   // variable to temporarily hold E[0]

// Timer1 Interrupt Service Routine on Period Match
// This is the Measurement Interval timer.
void __attribute__((interrupt, no_auto_psv)) _T1Interrupt(void)
{
  // Save Elapsed TMR3 time ASAP
  m_E0 = (unsigned long)TMR3 + ((unsigned long)T3Overflow * (unsigned long)ACfg0.T3Period);

  if ((DIAG_TIMER1 & ACfg0.Diag_Mode) == DIAG_TIMER1) {  LATCbits.LATC15 = 1;  }

  // Reset the Timer Interrupt Flag
  IFS0bits.T1IF = 0;

  // Increment our Overflow Counter
  T1Overflow++;

  if (T1Overflow < (ACfg0.Nominal_Hz / ACfg1.Intervals))
  {
    // Interval Time Interrupt
    // All Interval Timer events are after the INT0 PPS

    // Update the Fraction of Second IEEE Time Field
    E[0] = m_E0;
    ///PmuFracSec = m_E0;
    PmuFracSec = (unsigned long)((double)PmuCfg2.Time_Base * (double)T1Overflow * (double)ACfg1.Intervals / (double)ACfg0.Nominal_Hz);

    if (Prog_Mode == PROG_PMU)
    {
      Send_PmuData(0x00);
    }
    else if (Prog_Mode == PROG_TEXT)
    {
      Send_PHzData(0x00);
    }
  }
  else if ((ACfg1.Use_GPS & 0x01) != 0x01)
  {
    // No PPS on INT0, so we simulate it every set number of Intervals.
    E[0] = m_E0;

    // Increment our copy of the SOC = Second of Century
    PmuSoc++;
    GPS_Time = PmuSoc;   // Just for fun, because without PPS, the GPS_Time won't be used.

    // Clear the Fraction of Second IEEE Time Field
    PmuFracSec = 0x00000000;

    // Note: If we are not using the PPS on INT0, then we can leave Timer1 Running
    // Reset the T1 Overflow
    T1Overflow = 0;

    if (Prog_Mode == PROG_PMU)
    {
      Send_PmuData(0x02);
    }
    else if (Prog_Mode == PROG_TEXT)
    {
      Send_PHzData(0x02);
    }
  }

  if ( ((ACfg1.Use_GPS & 0x01) == 0x01) && (T1Overflow == ((ACfg0.Nominal_Hz / ACfg1.Intervals) - 1)) )
  {
    // Prevent this Interrupt from colliding with the Last Interval
    //-- Do Not Set E[0] Here
  }

  if ((DIAG_TIMER1 & ACfg0.Diag_Mode) == DIAG_TIMER1) {  LATCbits.LATC15 = 0;  }
}


// Timer3 Interrupt Service Routine on Period Match
void __attribute__((interrupt, no_auto_psv)) _T3Interrupt(void)
{
  // Increment our Overflow Counter
  T3Overflow++;

  // Reset the Timer3 Interrupt Flag
  IFS0bits.T3IF = 0;
}



/// Input Capture Interrupt Service Routine
// _ICxInterrupt() is the INTx interrupt service routine (ISR).
// The routine must have global scope in order to be an ISR.
// The ISR name is defined in the the device linker script (the .gld file).

volatile int icidx = 0;         // Input Capture loop counter variable
volatile char szIC1xxx[17] = "0123456789abcdef";   // scratch space for IC1

void __attribute__((interrupt, no_auto_psv)) _IC1Interrupt(void)
{
  // INT1/IC1 Interrupt is triggered on each trailing edge of OpAmp Channel A on Vref

  // Get the Timer Value and Reset before diagnostics
  T[0] = TMR3 + ACfg0.T3MzMax;

  // Reset the Timer before diagnostics to reduce latency
  TMR3 = ACfg0.T3CalStart;

  if ((DIAG_ZXING & ACfg0.Diag_Mode) == DIAG_ZXING)
  {
    // Zero Crossing Diagnostic, turn LED ON
    LATCbits.LATC15 = 1;
  }

  if ((DIAG_INCAP1 & ACfg0.Diag_Mode) == DIAG_INCAP1) { LATCbits.LATC15 = 1; }

  // Clear Overflow Counter
  T3Overflow = 0;

  // Clear the Interrupt Flag
  IFS0bits.IC1IF = 0;

  // If Diagnostic mode, Clear diagnostic LED
  if ((DIAG_INCAP1 & ACfg0.Diag_Mode) == DIAG_INCAP1)  { LATCbits.LATC15 = 0; }
}

volatile char szIC2xxx[17] = "0123456789abcdef";  // scratch space for IC2

void __attribute__((interrupt, no_auto_psv)) _IC2Interrupt(void)
{
  //// To Do:  Verify this comment referring to Channel A
  // INT2/IC2 Interrupt is triggered on each trailing edge of OpAmp Channel A on Vref
  ////
  // INTx/ICx Interrupt is triggered on each trailing edge of
  // the square wave generated by the OpAmp on Vref Comparison.
  // Record our Zero Crossing Time
  // Copy our current Z1 to be our next Z0
  M[0] = TMR3;

  if ((DIAG_ZXING & ACfg0.Diag_Mode) == DIAG_ZXING)
  {
    // Zero Crossing Completed, turn diagnostic LED OFF
    LATCbits.LATC15 = 0;
    PutsUART1((unsigned int *)"M1,OF,");
    PutsUART1((unsigned int *)ltoa((char *)szIC2xxx, (long)M[1], 10));
    PutsUART1((unsigned int *)",");
    PutsUART1((unsigned int *)ltoa((char *)szIC2xxx, (long)T3Overflow, 10));
    U1TX_Eol();
  }

  if ((DIAG_INCAP2 & ACfg0.Diag_Mode) == DIAG_INCAP2)  {  LATCbits.LATC15 = 1;  }

  // Calculate our T1 based on the Zero Crossing Times and IC1 T0 time span
  T[1] = T[0] - M[0] + M[1];
  M[1] = M[0];

  // Clear the Interrupt Flag
  IFS0bits.IC2IF = 0;
  if ((DIAG_INCAP2 & ACfg0.Diag_Mode) == DIAG_INCAP2)  {  LATCbits.LATC15 = 0;  }
}


void UART1_Init(void)
{
  /// Initialize the UART for RS232 Communications
  // UART1 Mode Register
  U1MODE = 0x0000;
  U1MODEbits.LPBACK = 0;    // 1=Enable Loop Back
  U1MODEbits.ABAUD = 0;  	  // 1=Enable AutoBAUD
  // PDSEL<1:0>: Parity and Data Selection bits
  //  11 = 9-bit data, no parity
  //  10 = 8-bit data, odd parity
  //  01 = 8-bit data, even parity
  //* 00 = 8-bit data, no parity
  // STSEL: Stop Selection bit
  //  1 = 2 Stop bits
  //* 0 = 1 Stop bit

  U1MODEbits.ALTIO = 1;

  // UART1 Status and Control Register
  U1STA = 0x0000;
  U1STAbits.UTXISEL = 0;
  U1STAbits.UTXBRK = 0;		// 1=Transmit Break, TX Pin Low
  // URXISEL<1:0>: Receive Interrupt Mode Selection bit
  //  11 =Interrupt flag bit is set when Receive Buffer is full (i.e., has 4 data characters)
  //  10 =Interrupt flag bit is set when Receive Buffer is 3/4 full (i.e., has 3 data characters)
  //  0x =Interrupt flag bit is set when a character is received
  //  U2STAbits.URISEL1 = 0;
  //  U2STAbits.URISEL0 = 0;
  U1STAbits.OERR = 0;

  // UxBRG: UART Baud Rate Generator Register
  U1BRG = ACfg0.UART1_BRG;

  // #define CLOSEST_UBRG_VALUE ((GetPeripheralClock()+8ul*BAUD_RATE)/16/BAUD_RATE-1)
  // #define BAUD_ACTUAL (GetPeripheralClock()/16/(CLOSEST_UBRG_VALUE+1))
  // #define BAUD_ERROR ((BAUD_ACTUAL > BAUD_RATE) ? BAUD_ACTUAL-BAUD_RATE : BAUD_RATE-BAUD_ACTUAL)
  // #define BAUD_ERROR_PRECENT	((BAUD_ERROR*100+BAUD_RATE/2)/BAUD_RATE)
  // #if (BAUD_ERROR_PRECENT > 3)
  //   #warning UART frequency error is worse than 3%
  // #elif (BAUD_ERROR_PRECENT > 2)
  //   #warning UART frequency error is worse than 2%
  // #endif
  // UBRG = CLOSEST_UBRG_VALUE;

  // Assign UART Interrupts
  // Receive gets high priority because these are control commands that must be processed
  _U1RXIP = 7;

  // Transmit can wait
  _U1TXIP = 1;

  IEC0bits.U1RXIE = 1;      // Receive Interrupt Enabled
  U1MODEbits.UARTEN = 1;
  U1STAbits.UTXEN = 1;      // Transmit Enabled by UARTEN
}


void UART2_Init(void)
{
  /// dsPIC30F3013 Only - GPS RS232 Communications
  // Initialize the GPS Information string buffers
  GPS_PPSCount = PmuCfg2.GPS_Interval;    // Set it so we use the GPS Time at the next PPS
  memset((unsigned int *)GPS_Info, 0x00, sizeof(GPS_Info));
  memset((unsigned int *)GPS_Input, 0x00, sizeof(GPS_Input));

  /// Initialize the UART
  // UART2 Mode Register
  U2MODE = 0x0000;
  U2MODEbits.LPBACK = 0;      // Disable Loop Back
  U2MODEbits.ABAUD = 0;  	  // Disable AutoBAUD
  // PDSEL<1:0>: Parity and Data Selection bits
  //  11 = 9-bit data, no parity
  //  10 = 8-bit data, odd parity
  //  01 = 8-bit data, even parity
  //* 00 = 8-bit data, no parity
  // STSEL: Stop Selection bit
  //  1 = 2 Stop bits
  //* 0 = 1 Stop bit
  
  // UART Status and Control Register
  U2STA = 0x0000;
  U2STAbits.UTXISEL = 0;    // Word transfered to Receive Buffer
  U2STAbits.UTXBRK = 0;		// 1=Transmit Break, TX Pin Low
  // URXISEL<1:0>: Receive Interrupt Mode Selection bit
  //  11 =Interrupt flag bit is set when Receive Buffer is full (i.e., has 4 data characters)
  //  10 =Interrupt flag bit is set when Receive Buffer is 3/4 full (i.e., has 3 data characters)
  //  0x =Interrupt flag bit is set when a character is received
  //  U2STAbits.URISEL1 = 0;
  //  U2STAbits.URISEL0 = 0;
  U2STAbits.OERR = 0;

  // UxBRG: UART Baud Rate Generator Register
  U2BRG = ACfg0.UART2_BRG;

  // Assign UART 2 Interrupts priority
  // Receive TOD and position info from GPS with moderate priority
  _U2RXIP = 4;
  // Config commands to GPS have low priority
  _U2TXIP = 1;

  IEC1bits.U2RXIE = 1;      // Receive Interrupt Enabled
  U2MODEbits.UARTEN = 1;
  U2STAbits.UTXEN = 1;      // Transmit Enabled by UARTEN
}


void Wait4Me(int iNops)
{
  int rr;
  for (rr = 0; rr < iNops; rr++)
  {
    Nop();
  }
}


int16_t main(void)
{
  /* Configure the oscillator for the device */
  ConfigureOscillator();

  // Soft boot
  Prog_Reset(CONFIG_LOAD);

  LATBbits.LATB0 = 0;
  LATBbits.LATB1 = 0;
  LATBbits.LATB2 = 0;
  LATBbits.LATB3 = 0;

  while (Prog_Mode != PROG_EXIT)
  {
    // If RB0 is HI, then set then reset running mode
    // This will restart the running mode if the sensor is reset
    if (PORTBbits.RB0 == 1)
    {
      if (PORTBbits.RB1 == 1)
      {
        Prog_Mode = PROG_TEXT;
      }
      else
      {
        Prog_Mode = PROG_PMU;
      }
      ACfg1.Use_GPS = (((unsigned int)PORTBbits.RB2) << 1) + (unsigned int)PORTBbits.RB3;
    }


    if ((ACfg0.Diag_Mode & DIAG_MAIN) == DIAG_MAIN) { LATCbits.LATC15 = 1; }
    if (ACfg0.Diag_Mode == DIAG_NONE)  { LATCbits.LATC15 = 0; }

    // Process Received Commands from the Host
    URX1_Proc();

    switch (Prog_Mode)
    {
      case PROG_PMU:
        // Interrupt Driven Process
        break;

      case PROG_TEXT:
        // Interrupt Driven Process
        break;

      case PROG_RESET:
        Prog_Reset(CONFIG_NOLOAD);
        PutsUART1((unsigned int *)"Reset!");
        U1TX_Eol();
        Wait4Me(1000);
        break;

      case PROG_REBOOT:
        Prog_Reset(CONFIG_LOAD);
        PutsUART1((unsigned int *)"Rebooted!");
        U1TX_Eol();
        Wait4Me(1000);
        break;

      case PROG_FACTORY:
        EGet_Config(CONFIG_FACTORY);
        PutsUART1((unsigned int *)"Factory Defaults!");
        U1TX_Eol();
        Wait4Me(1000);
        Prog_Mode = PROG_REBOOT;
        break;

      default:
        // PROG_IDLE = no action
        break;
    }

    if ((DIAG_MAIN & ACfg0.Diag_Mode) == DIAG_MAIN) { LATCbits.LATC15 = 0; }
  }
  Full_Stop();
  return 0;
}


void Prog_Reset(unsigned char bLoadCfg)
{
  /// Initialize Everthing for Complete "Soft" Reset
  // Initialize I/O pins to defaults first for "safety"
  IO_Init();

  if (bLoadCfg == 0x01)
  {
    // Load the Configuration from Internal EEPROM
    EGet_Config(CONFIG_NOLOAD);
  }

  // Re-initialize the I/O pins with the restored configuration options
  IO_Init();

  // Initialize everything else
  UART1_Init();
  UART2_Init();			// dsPIC30F3013 only

  Timer_Init();
  InCap_Init();
  INTx_Init();

  Prog_Mode = ACfg0.Prog_Mode;

  // GPS PPS Interrupt Enable
  IEC0bits.INT0IE = 1;

  if ((ACfg1.Use_GPS & 0x01) != 0x01)
  {
    // If we don't have a GPS PPS, then we can use Timer1 Intervals continuously
    T1CONbits.TON = 1;
  }

  // Reset the messages counter
  Msg_Id = 0;
  Wait4Me(2000);
  PutsUART1((unsigned int *)"Sensor Reset!");

  Prog_Mode = PROG_TEXT;
  LATBbits.LATB8 = ACfg1.Use_GPS & 0x01;
  LATBbits.LATB9 = ACfg1.Use_GPS & 0x02;
}


void Full_Stop(void)
{
  INTx_Init();
  IO_Init();
  TRISBbits.TRISB8 = 1;   // OC1
  TRISBbits.TRISB9 = 1;   // OC2
  TRISFbits.TRISF3 = 1;   // SDO1/SCL/U1TX/PGD
  TRISFbits.TRISF5 = 1;
  TRISFbits.TRISF6 = 1;   // SCK1/INT0
  TRISCbits.TRISC14 = 1;
  TRISCbits.TRISC15 = 1;
}


void U1TX_Eol(void)
{
  // UART Transmit a End of Line Record Terminator
  // Default = 0x0D0A or CR+LF
  while(U1STAbits.UTXBF);
    U1TXREG = (unsigned char)((Data_Eol & 0xFF00) >> 8);
  while(U1STAbits.UTXBF);
    U1TXREG = (unsigned char)(Data_Eol & 0x00FF);
}


//// IEEE C37.118-2005 Implementation Routines
unsigned int Calc_CRC(unsigned char* sData, unsigned int iDataLen)
{
  //// CRC-CCITT Calculation
  // f(x) = x^16 + x^12 + x^5 + 1
  //
  // Derived from IEEE Std C37.118-2005 sample code
  // Example:  cout << "CRC of " << "Arnold" << " = " << Calc_CRC((unsigned char*)"Arnold") << endl;
  unsigned int iCrc = 0xFFFF;   // 0xFFFF is specific for SynchroPhasor Data CRC
  unsigned int iCalc1;
  unsigned int iCalc2;
  unsigned int ii;
  for (ii = 0; ii < iDataLen; ii++)
  {
    iCalc1 = (iCrc >> 8) ^ sData[ii];
    iCrc <<= 8;
    iCalc2 = iCalc1 ^ (iCalc1 >> 4);
    iCrc ^= iCalc2;
    iCalc2 <<= 5;
    iCrc ^= iCalc2;
    iCalc2 <<= 7;
    iCrc ^= iCalc2;
  }
  return iCrc;
}


volatile char szUTXxxx[17] = "0123456789abcdef";  // scratch space for UART Transmit

void Tx_PmuHeader(void)
{
  //// Transmit the PMU's Header Record
  unsigned char ucData[92];
  char szData[48];
  int ii = 0;  // loop counter

  // Message Sync Byte and Header Frame Type
  ucData[0] = A_SYNC_AA;
  ucData[1] = A_SYNC_HDR;

  // Frame Size = 16 overhead bytes + 48 bytes PMU Header Data
  ucData[2] = 0x00;
  ucData[3] = 64;

  // PMU Sensor ID UInt16
  ucData[4] = (unsigned char)((ACfg0.Sensor_ID & 0xFF00) >> 8);
  ucData[5] = (unsigned char)(ACfg0.Sensor_ID & 0x00FF);

  // SOC.  Note: we don't know this, so use what was sent by the Host CMD Message
  ucData[6] = (unsigned char)((PmuSoc & 0xFF000000) >> 24);
  ucData[7] = (unsigned char)((PmuSoc & 0x00FF0000) >> 16);
  ucData[8] = (unsigned char)((PmuSoc & 0x0000FF00) >> 8);
  ucData[9] = (unsigned char)(PmuSoc & 0x000000FF);

  // FRACSEC.  Note: we don't know this, so use what was sent by the Host CMD Message
  ucData[10] = (unsigned char)((PmuFracSec & 0xFF000000) >> 24);
  ucData[11] = (unsigned char)((PmuFracSec & 0x00FF0000) >> 16);
  ucData[12] = (unsigned char)((PmuFracSec & 0x0000FF00) >> 8);
  ucData[13] = (unsigned char)(PmuFracSec & 0x000000FF);

  memset(szData, 0x00, 48);
  strcat(szData, "GTosPMU! Sensor ID# ");
  strcat(szData, ltoa((char *)szUTXxxx, (long)ACfg0.Sensor_ID, 10));
  strcat(szData, ", SN# ");
  strcat(szData, ltoa((char *)szUTXxxx, (long)SERIAL_NBR, 16));
  strcat(szData, ", Rev ");
  strcat(szData, ltoa((char *)szUTXxxx, (long)THIS_VER, 16));
  strcat(szData, ".");

  memcpy(ucData + 14, szData, 48);
  for (ii = 0; ii < 62; ii++)
  {
    while(U1STAbits.UTXBF);
      U1TXREG = (unsigned char)ucData[ii];
  }

  // Calculate and Transmit the CRC UInt16
  Crc16 = Calc_CRC(ucData, 62);
  while(U1STAbits.UTXBF);
    U1TXREG = (unsigned char)((Crc16 & 0xFF00) >> 8);
  while(U1STAbits.UTXBF);
    U1TXREG = (unsigned char)(Crc16 & 0x00FF);
}


void Tx_PmuCfg(unsigned char ucCfgNbr)
{
  /// Transmit PMU Config Record 1 or 2 (both are the same except for byte[1]
  //// Transmit the PMU's Header Record
  unsigned char ucData[92];
  char cCHName[16];
  unsigned int iDataRate = 0x0000;
  int ii = 0;  // loop counter

  memset(ucData, 0x00, sizeof(ucData));

  // 1. Message Sync Byte and Header Frame Type
  ucData[0] = A_SYNC_AA;
  ucData[1] = ucCfgNbr;

  // 2. Frame Size = 16 overhead bytes + 48 bytes PMU Header Data
  ucData[2] = 0x00;
  ucData[3] = 94;

  // 3. PMU Sensor ID UInt16
  ucData[4] = (unsigned char)((ACfg0.Sensor_ID & 0xFF00) >> 8);
  ucData[5] = (unsigned char)(ACfg0.Sensor_ID & 0x00FF);

  // 4. SOC  Note: we don't know this, so use what was sent by the Host CMD Message
  ucData[6] = (unsigned char)((PmuSoc & 0xFF000000) >> 24);
  ucData[7] = (unsigned char)((PmuSoc & 0x00FF0000) >> 16);
  ucData[8] = (unsigned char)((PmuSoc & 0x0000FF00) >> 8);
  ucData[9] = (unsigned char)(PmuSoc & 0x000000FF);

  // 5. FRACSEC
  ucData[10] = (unsigned char)((PmuFracSec & 0xFF000000) >> 24);
  ucData[11] = (unsigned char)((PmuFracSec & 0x00FF0000) >> 16);
  ucData[12] = (unsigned char)((PmuFracSec & 0x0000FF00) >> 8);
  ucData[13] = (unsigned char)(PmuFracSec & 0x000000FF);

  // 6. TIME_BASE = Tcy = OSC_HZ = 24,000,000 = 0x016E3600 = 1/(instruction time)
  ucData[14] = (unsigned char)((PmuCfg2.Time_Base & 0xFF000000) >> 24);
  ucData[15] = (unsigned char)((PmuCfg2.Time_Base & 0x00FF0000) >> 16);
  ucData[16] = (unsigned char)((PmuCfg2.Time_Base & 0x0000FF00) >> 8);
  ucData[17] = (unsigned char)(PmuCfg2.Time_Base & 0x000000FF);

  // 7. NUM_PMU = 1 PMU for this configuration (Repeats of fields 8 to 19)
  ucData[18] = 0x00;
  ucData[19] = 0x01;

  // 8. STN = Station Name
  memcpy(ucData + 20, PmuCfg2.PMU_Station, 16);

  // 9. PMU Sensor ID UInt16, for us, this is the same as above
  ucData[36] = (unsigned char)((ACfg0.Sensor_ID & 0xFF00) >> 8);
  ucData[37] = (unsigned char)(ACfg0.Sensor_ID & 0x00FF);

  // 10. Data Format = 16 bit integers
  ucData[38] = 0x00;
  ucData[39] = 0x0F;  // 1111b Freq/DFreq, Analogs, Phasors = Single Prec.  Phasor = Polar Mag,Ang

  // 11. Number of Phasors
  ucData[40] = 0x00;
  ucData[41] = 0x01;  // only one phasor for our sensor

  // 12. Number of Analog Values
  ucData[42] = 0x00;
  ucData[43] = 0x01;  // 1 analog value =

  // 13. Number of Digital Status Words
  ucData[44] = 0x00;
  ucData[45] = 0x00;

  // 14. Channel Names
  memset(cCHName, 0x00, 16);
  strcpy(cCHName, "PHASOR-V1");
  memcpy(ucData + 46, cCHName, 16);

  memset(cCHName, 0x00, 16);
  strcpy(cCHName, "ANALOG1");
  memcpy(ucData + 62, cCHName, 16);

  // 15.1  Phasor Conversion Factor = actual Single Precision Angle
  ucData[78] = 0x00;  // 0 = Voltage, 1 = Current
  ucData[79] = 0x00;  // ignore
  ucData[80] = 0x00;  // ignore
  ucData[81] = 0x00;  // ignore

  // 16.1  Magnitude Conversion Factor
  ucData[82] = 0x02;  // 0 = Point Data, 1 = RMS, 2 = Peak, 3 to 64 = reserved, 65 to 255 = user defined
  ucData[83] = 0x00;  // 24 bits user defined scaling
  ucData[84] = 0x00;
  ucData[85] = 0x00;

  // 18.  Nominal Frequency:  0x00 = 60Hz,  0x01 = 50Hz
  ucData[86] = 0x00;
  if (ACfg0.Nominal_Hz != 50)
  {
    ucData[87] = 0x00;  // 60Hz
  }
  else
  {
    ucData[87] = 0x01;  // 50Hz
  }

  // 19.  Configuration Change Count
  ucData[88] = 0x00;
  ucData[89] = 0x00;

  // 20.  Data_Rate = Samples per Second = Intervals
  iDataRate = (unsigned int)(((unsigned int)ACfg0.Nominal_Hz) / ACfg1.Intervals);
  ucData[90] = (unsigned char)((iDataRate & 0xFF00) >> 8);
  ucData[91] = (unsigned char)(iDataRate & 0x00FF);

  // Transmit the Data
  for (ii = 0; ii < 92; ii++)
  {
    while(U1STAbits.UTXBF);
      U1TXREG = (unsigned char)ucData[ii];
  }

  // Calculate and Transmit the CRC UInt16
  Crc16 = Calc_CRC(ucData, 92);
  while(U1STAbits.UTXBF);
    U1TXREG = (unsigned char)((Crc16 & 0xFF00) >> 8);
  while(U1STAbits.UTXBF);
    U1TXREG = (unsigned char)(Crc16 & 0x00FF);
}

////////

void Tx_Ping(void)
{
  Prog_Mode = PROG_IDLE;
  U1TX_Eol();
  PutsUART1((unsigned int *)"GridTrakPMU!");
  PutsUART1((unsigned int *)", Sensor ID# ");
  PutsUART1((unsigned int *)ltoa((char *)szUTXxxx, (long)ACfg0.Sensor_ID, 10));
  PutsUART1((unsigned int *)", SN# ");
  PutsUART1((unsigned int *)ltoa((char *)szUTXxxx, (long)SERIAL_NBR, 16));
  PutsUART1((unsigned int *)", FW Rev ");
  PutsUART1((unsigned int *)ltoa((char *)szUTXxxx, (long)THIS_VER, 16));
  if (GPS_OPT == 0x00)
  {
  	PutsUART1((unsigned int *)".0");
  }
  else
  {
    PutsUART1((unsigned int *)".g");
  }
  PutsUART1((unsigned int *)", dsPIC30F3013, ");
  PutsUART1((unsigned int *)ltoa((char *)szUTXxxx, (long)ACfg0.Nominal_Hz, 10));
  PutsUART1((unsigned int *)" Hz");
  U1TX_Eol();
}


void Tx_Config(void)
{
  // Transmit a copy of the EEPROM Configuration to the Host
  // Application Configuation Structure #0
  // Make th ucData structure the same size as _EE_ROW = 32 bytes or 16 words
  int ii = 0;
  int jj = 0;

  unsigned char ucData[32];

  U1TX_Eol();
  PutsUART1((unsigned int *)"PHzCfg");
  U1TX_Eol();

  for (jj = 0; jj < 3; jj++)
  {
    switch (jj)
    {
      case 0:
        memset(ucData, 0x00, sizeof(ucData));
        memcpy(ucData, &ACfg0, sizeof(ucData));
        break;

      case 1:
        memset(ucData, 0x00, sizeof(ucData));
        memcpy(ucData, &ACfg1, sizeof(ucData));
        break;

      case 2:
        memset(ucData, 0x00, sizeof(ucData));
        memcpy(ucData, &PmuCfg2, sizeof(ucData));
        break;
    }

    // Transmit the Data
    for (ii = 0; ii < sizeof(ucData); ii++)
    {
      while(U1STAbits.UTXBF);
        U1TXREG = (unsigned char)ucData[ii];
    }

    // Calculate and Transmit the CRC UInt16
    Crc16 = Calc_CRC(ucData, sizeof(ucData));
    while(U1STAbits.UTXBF);
      U1TXREG = (unsigned char)((Crc16 & 0xFF00) >> 8);
    while(U1STAbits.UTXBF);
      U1TXREG = (unsigned char)(Crc16 & 0x00FF);
  }
}


void Rx_Config(void)
{
  // Receive and load a copy of the Configuration from the Host

  // Configuation Structure
  // Make th ucData structure the same size as _EE_ROW = 32 bytes or 16 words
  int ii = 0;
  int jj = 0;
  int s = 0;
  unsigned char ucData[32];

  for (jj = 0; jj < 3; jj++)
  {
    // Receive the Configuration Data Record
    for (ii = 0; ii < 32; ii++)
    {
      // Get the FRACSEC = next 4 bytes, UInt32
      while(!U1STAbits.URXDA);
        ucData[ii] = (unsigned char)(U1RXREG & 0xFF);
    }

    while(!U1STAbits.URXDA);
      Crc16 = ((unsigned int)(U1RXREG & 0xFF) << 8);
    while(!U1STAbits.URXDA);
      Crc16 = Crc16 + (unsigned int)(U1RXREG & 0xFF);

    if (Crc16 == Calc_CRC(ucData, sizeof(ucData)))
    {
      switch (jj)
      {
        case 0:
          memcpy(&ACfg0, ucData, sizeof(ucData));
          break;

        case 1:
          memcpy(&ACfg1, ucData, sizeof(ucData));
          break;

        case 2:
          memcpy(&PmuCfg2, ucData, sizeof(ucData));
          break;
      }
      s++;
    }
  }
  U1TX_Eol();
  if (s == 2)
  {
    ESave_Config();
    PutsUART1((unsigned int *)"Config Saved to PMU!");
  }
  else
  {
    PutsUART1((unsigned int *)"Config Failed!");
  }
  U1TX_Eol();
  Wait4Me(2000);
  Prog_Reset(CONFIG_LOAD);
}


void Set_DiagMode(unsigned char uCmd)
{
  switch (uCmd)
  {
    case 0x61:  // "a"
      // Turn off Diagnostic and Data Modes
      ACfg0.Diag_Mode = DIAG_NONE;
      ACfg1.Data_Mode = DATA_NONE;
      break;

    case 0x62:  // "b"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_MAIN;
      break;

    case 0x63:  // "c"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_UCMD;
      break;

    case 0x64:  // "d"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_TXDATA;
      break;

    case 0x65:  // "e"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_INCAP1;
      break;

    case 0x66:  // "f"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_INCAP2;
      break;

    case 0x67:  // "g"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_TIMER1;
      break;

    case 0x6A:  // "j"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_UTX1;
      break;

    case 0x6B:  // "k"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_URX1;
      break;

    case 0x6D:  // "m"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_URX2;
      break;

    case 0x6E:  // "n"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_ZXING;
      break;

    case 0x6F:  // "o"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_INT0;
      break;

    case 0x70:  // "p"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_IDLE;
      break;

    case 0x74:  // "t"
      // Transmit Last GPS Information on PPS for Diagnostics
      ACfg1.Data_Mode = DATA_GPS_INFO;
      break;

    default:
      break;
  }
}


void URX1_Proc(void)
{
  /// Process Command Received on the UART
  // Command Format : ASCII Character
  //   RxData[0] = "0".."9"
  // Check for Command Received by UART ISR

  unsigned int rr = 0x0000;             // read incrementer
  unsigned char UCmd0 = 0x00;           // First Input Command Byte
  unsigned char UCmd1 = 0x00;           // Second Input Command Byte
  unsigned char AMsg = 0x00;            // Message Type Byte
  unsigned long ISoc = 0x00000000;      // SOC - Second of Century
  unsigned long IFracSec = 0x00000000;  // Faction of Second

  if (Prog_Mode == PROG_RSPT)
  {
    RS_Passthru();
  }
  else
  {
    GPS_GetInfo();
  }

  if (R1xRdy > 0)
  {
    if ((ACfg0.Diag_Mode == DIAG_NONE) || (DIAG_URX1 & ACfg0.Diag_Mode) == DIAG_URX1) {  LATCbits.LATC15 = 1;  }

    // Disable UART Interrupt.  We will be reading synchronized additional data manually
    IEC0bits.U1RXIE = 0;

    // Copy the command to a working variable
    UCmd0 = R1xData[0];

    // Clear the command register
    R1xData[0] = 0;

    // Clear Comand Received flag
    R1xRdy = 0;

    // Preserve Program Mode
    Prog_PrevMode = ACfg0.Prog_Mode;

    if (UCmd0 == A_SYNC_AA)
    {
      // IEEE C37.118 Protocol Command = 0xAA
      // Get the Message Frame Type
      while(!U1STAbits.URXDA);
        AMsg = (unsigned int)(U1RXREG & 0xFF);

      // Get the Frame Size = next 2 bytes, UInt16
      while(!U1STAbits.URXDA);
        IFrameSz = ((unsigned int)(U1RXREG & 0xFF) << 8);
      while(!U1STAbits.URXDA);
        IFrameSz = IFrameSz + (unsigned int)(U1RXREG & 0xFF);

      // Get the ID CODE = next 2 bytes, UInt16
      while(!U1STAbits.URXDA);
        IDCode = ((unsigned int)(U1RXREG & 0xFF) << 8);
      while(!U1STAbits.URXDA);
        IDCode = IDCode + (unsigned int)(U1RXREG & 0xFF);

      // Get the SOC = next 4 bytes, UInt32
      while(!U1STAbits.URXDA);
        ISoc = ((unsigned long)(U1RXREG & 0xFF) << 24);
      while(!U1STAbits.URXDA);
        ISoc = ISoc + ((unsigned long)(U1RXREG & 0xFF) << 16);
      while(!U1STAbits.URXDA);
        ISoc = ISoc + ((unsigned long)(U1RXREG & 0xFF) << 8);
      while(!U1STAbits.URXDA);
        ISoc = ISoc + (unsigned long)(U1RXREG & 0xFF);

      if (((ACfg1.Use_GPS & 0x01) != 0x01) || ((ACfg1.Use_GPS & 0x02) != 0x02))
      {
        PmuSoc = ISoc;
      }

      // Get the FRACSEC = next 4 bytes, UInt32
      while(!U1STAbits.URXDA);
        IFracSec = ((unsigned long)(U1RXREG & 0xFF) << 24);
      while(!U1STAbits.URXDA);
        IFracSec = IFracSec + ((unsigned long)(U1RXREG & 0xFF) << 16);
      while(!U1STAbits.URXDA);
        IFracSec = IFracSec + ((unsigned long)(U1RXREG & 0xFF) << 8);
      while(!U1STAbits.URXDA);
        IFracSec = IFracSec + (unsigned long)(U1RXREG & 0xFF);

      PmuFracSec = IFracSec;

      // Get the CMD = next 2 bytes, UInt16
      while(!U1STAbits.URXDA);
        ICmd = ((unsigned int)(U1RXREG & 0xFF) << 8);
      while(!U1STAbits.URXDA);
        ICmd = ICmd + (unsigned int)(U1RXREG & 0xFF);

      // Read the Rest of the Frame Bytes up to the CRC UInt16
      for (rr = 16; rr < IFrameSz - 2; rr++)
      {
        // Read the rest of the EXTFRAME
        while(!U1STAbits.URXDA);
          IExtFrame = (unsigned int)(U1RXREG & 0xFF);
        // * TO DO * //
      }

      // Read the CRC UInt16 2 bytes
      while(!U1STAbits.URXDA);
        Crc16 = ((unsigned int)(U1RXREG & 0xFF) << 8);
      while(!U1STAbits.URXDA);
        Crc16 = Crc16 + (unsigned int)(U1RXREG & 0xFF);

      // If CRC IS Good, then process the IEEE command
      // [TO DO] - Handle Bad CRC //

      if (AMsg == A_SYNC_CMD)
      {
        // Sync Command from Host Command + Version = 0x40 + 0x01 = 0x41
        // Ignore the SOC[4] and FRACSEC[4] bytes
        // Decode the CMD 2 bytes
        switch (ICmd)
        {
          case 0x01:  // Disable Data Output
            Prog_Mode = PROG_IDLE;
            break;

          case 0x02:  // Enable Data Output
            Prog_Mode = PROG_PMU;
            break;

          case 0x03:  // Transmit Header Record Frame
            Tx_PmuHeader();
            break;

          case 0x04:  // Transmit Configuration #1 Record Frame
            Tx_PmuCfg(A_SYNC_CFG1);
            break;

          case 0x05:  // Transmit Configuration #2 Record Frame
            Tx_PmuCfg(A_SYNC_CFG2);
            break;

          default:
            break;
        }
      }
    }

    else if (UCmd0 == A_SYNC_AT)
    {
      // GTosPMU Protocol Command ="@"
      while(!U1STAbits.URXDA);
        AMsg = (unsigned int)(U1RXREG & 0xFF);

      // Execute the GTosPMU Command
      switch (AMsg)
      {
        case 0x40:  // "@" PING
          Tx_Ping();
          break;

        case 0x21:  // "!"  Reset to Factory Default Config
          Prog_PrevMode = Prog_Mode;
          Prog_Mode = PROG_FACTORY;
          break;

        case 0x23:  // "#"  Reload EEPROM Config and Reboot
          Prog_PrevMode = Prog_Mode;
          Prog_Mode = PROG_REBOOT;
          break;

        case 0x2A:  // "*" Soft Reset, Fast, Reset Timers and Counters, No Config EEPROM Reload
          Prog_PrevMode = Prog_Mode;
          Prog_Mode = PROG_RESET;
          break;

        case 0x2E:  // "." PC Time Synchronize on Next Byte
          // Turn off PPS Mode
          // ACfg1.Use_PPS = (unsigned char)(0x00);
          // Turn off and reset Timer1
          //--T1CONbits.TON = 0;
          T1Overflow = 0;
          TMR1 = 0;
          // Wait for next byte from PC
          while(!U1STAbits.URXDA);
          // Turn the Timer1 on
          //--T1CONbits.TON = 1;
          // Clear INT0 Interrupt Status Flag
          IFS0bits.INT0IF = 0;
          break;

        case 0x30:  // "0" Run Without Transmitting Data
          Prog_Mode = PROG_IDLE;
          break;

        case 0x31:  // "1" Set Stream Text, PHzMonitor Style Text Protocol
          Prog_Mode = PROG_TEXT;
          break;

        case 0x32:  // "2" Set PMU Data Messages Mode, SynchroPhasor Binary Protocol
          Prog_Mode = PROG_PMU;
          break;

        case 0x33:  // "3" Set RS232 Passthru Mode for direct GPS-Host communications
          Prog_PrevMode = Prog_Mode;
          Prog_Mode = PROG_RSPT;
          break;

        case 0x39:  // "9" Set Intervals
          while(!U1STAbits.URXDA);
            ACfg1.Intervals = (U1RXREG & 0xFF);
          ESave_Config();
          Prog_PrevMode = Prog_Mode;
          Prog_Mode = PROG_REBOOT;
          break;

        case 0x41:  // "A"  Set the Sensor ID
          // Read the next two bytes for the Sensor ID Hi and Lo bytes
          while(!U1STAbits.URXDA);
            ACfg0.Sensor_ID = ((unsigned int)(U1RXREG & 0xFF) << 8);
          while(!U1STAbits.URXDA);
            ACfg0.Sensor_ID = ACfg0.Sensor_ID + (U1RXREG & 0xFF);

          // Read the next two bytes for the Sensor PWD Hi and Lo bytes
          while(!U1STAbits.URXDA);
            ACfg0.Sensor_PIN = ((unsigned int)(U1RXREG & 0xFF) << 8);
          while(!U1STAbits.URXDA);
            ACfg0.Sensor_PIN = ACfg0.Sensor_PIN + (U1RXREG & 0xFF);
          break;

        case 0x42:  // "B"  Set Configuration
          Prog_Mode = PROG_IDLE;
          Rx_Config();
          break;

        case 0x43:  // "C"  Get Configuration
          Prog_Mode = PROG_IDLE;
          Tx_Config();
          break;

        case 0x44:  // "D"  Set the Nominal Hz
          // Read the next bytes for Nominal Frequency
          while(!U1STAbits.URXDA);
            ACfg0.Nominal_Hz = (U1RXREG & 0xFF);
          ESave_Config();
          Prog_Mode = PROG_REBOOT;
          break;

        case 0x45:  // "E"  Set Calibration Values
          // Read the next two bytes for the HzCalOffset
          while(!U1STAbits.URXDA);
            ACfg0.HzCalOffset = (int)((unsigned int)(U1RXREG & 0xFF) << 8);
          while(!U1STAbits.URXDA);
            ACfg0.HzCalOffset = (int)((unsigned int)ACfg0.HzCalOffset + (U1RXREG & 0xFF));
          ESave_Config();
          Prog_Mode = PROG_REBOOT;
          break;

        case 0x47:  // "G" Get GPS Information
          GPS_TxInfo();
          break;

        case 0x53:  // "S" Save Configuration to EEPROM
          ESave_Config();
          break;


        case 0x57:  // "W" Wait HERE, Pause
          // Listen for a byte before continuing
          while(!U1STAbits.URXDA);
          break;

        case 0x58:  // "X" Halt the Micro Controller; requires Physical *MCLR to start again
          ACfg0.Prog_Mode = PROG_EXIT;
          break;

        case 0x59:  // "Y" Set Use_PPS option.
          // 0x#1 = Use GPS PPS on INT0, else use Timer1 intervals and Simulate PPS
          // 0x#2 = Use GPS Time
          // 0x#3 = Use both GPS PPS and GPS Time
          while(!U1STAbits.URXDA);
            ACfg1.Use_GPS = (unsigned char)(U1RXREG & 0x03);
          break;

        case 0x61:  // "a"
          // Read the Diagnostic Mode byte
          while(!U1STAbits.URXDA);
            UCmd1 = (unsigned char)(U1RXREG & 0xFF);
          Set_DiagMode(UCmd1);
          break;

        default:  break;
      }
    }

    if ((ACfg0.Diag_Mode == DIAG_NONE) || (DIAG_URX1 & ACfg0.Diag_Mode) == DIAG_URX1) {  LATCbits.LATC15 = 0;  }
  }

  // Enable the UART Interrupts to automatically receive more data
  // Let's do this all the time to ensure we can receive RS232 Data.
  if (IEC0bits.U1RXIE == 0)
  {
    IEC0bits.U1RXIE = 1;
  }
  if (IEC1bits.U2RXIE == 0)
  {
    IEC1bits.U2RXIE = 1;
  }

  // Clear UART Errors
  if (U1STAbits.OERR ==1)
  {
    U1STAbits.OERR = 0;
  }
  if (U2STAbits.OERR ==1)
  {
    U2STAbits.OERR = 0;
  }
}



/// GPS Information Receiving
//  Typical GPS Time String:  $GPRMC,HHmmss,A,3905.5370,N,07634.5399,W,000.0,324.4,ddMMyy,011.1,W*70E
#define GPS_INFO_MAX 69

// Allocate some static scratch space outside the function so we don't have to repeatedly declare and allocate.
volatile char szTimexxx[17] = "0123456789abcdef";   // Time decodeing scratch space

void GPS_GetInfo(void)
{
  /// Process Input from UART 2
  // Check for Command Received by UART2 ISR

  char inR2 = 0x00;           // UART2 input byte
  unsigned int iIdxMax = 0;
  unsigned int ii = 0;

  if (R2xRdy > 0)
  {
    if ((DIAG_URX2 & ACfg0.Diag_Mode) == DIAG_URX2) {  LATCbits.LATC15 = 1;  }

    // Copy the input byte to a working variable
    inR2 = R2xData[0];

    // Left Shift the buffer
    iIdxMax = R2xRdy;
    for (ii = 0; ii < iIdxMax; ii++)
    {
	  R2xData[ii] = R2xData[ii+1];
    }
    // Decrement the waiting bytes count
    R2xRdy--;


    iIdxMax = strlen((char *)GPS_Input);
    if (inR2 == '$')
    {
      // "$" Character Received
      // Preserve the Last GPS Information string received if it was good

      // Check for the $GPRMC prefix
      if ((iIdxMax > 11)
          && (strncmp((char *)GPS_Input, "$GPRMC,", 7) == 0)
          && (iIdxMax >= GPS_INFO_MAX)
          && (GPS_Input[67] == '*'))
      {
        // We have enough to get the GPS Time
        szTimexxx[0] = GPS_Input[7];
        szTimexxx[1] = GPS_Input[8];
        szTimexxx[2] = 0x00;
        GPS_tm.tm_hour = atoi((char *)szTimexxx);

        szTimexxx[0] = GPS_Input[9];
        szTimexxx[1] = GPS_Input[10];
        szTimexxx[2] = 0x00;
        GPS_tm.tm_min = atoi((char *)szTimexxx);

        szTimexxx[0] = GPS_Input[11];
        szTimexxx[1] = GPS_Input[12];
        szTimexxx[2] = 0x00;
        GPS_tm.tm_sec = atoi((char *)szTimexxx);

        szTimexxx[0] = GPS_Input[53];
        szTimexxx[1] = GPS_Input[54];
        szTimexxx[2] = 0x00;
        GPS_tm.tm_mday = atoi((char *)szTimexxx);

        szTimexxx[0] = GPS_Input[55];
        szTimexxx[1] = GPS_Input[56];
        szTimexxx[2] = 0x00;
        GPS_tm.tm_mon = atoi((char *)szTimexxx) - 1;

        szTimexxx[0] = GPS_Input[57];
        szTimexxx[1] = GPS_Input[58];
        szTimexxx[2] = 0x00;
        GPS_tm.tm_year = 100 + atoi((char *)szTimexxx);

        GPS_Time = mktime((struct tm *)&GPS_tm);

        // We have a full GPS Information string.  It's a shame it doesn't have a CRC.
        memcpy((unsigned int *)GPS_Info, (unsigned int *)GPS_Input, sizeof(GPS_Input));
      }

      // Clear the GPS_Info buffer
      memset((unsigned int *)GPS_Input, 0x00, sizeof(GPS_Input));
      strncat((char *)GPS_Input, &inR2, 1);
    }
    else if (iIdxMax < GPS_INFO_MAX + 2)
    {
      if (inR2 > 0x24) // text
      {
        // Append character to the GPS_Intput
        strncat((char *)GPS_Input, &inR2, 1);
      }
    }
    else
    {
      // Overflowed, Clear the Input Buffer and start over
      memset((unsigned int *)GPS_Input, 0x00, sizeof(GPS_Input));
    }

    if ((DIAG_URX2 & ACfg0.Diag_Mode) == DIAG_URX2) {  LATCbits.LATC15 = 0;  }
  }
}


void GPS_TxInfo(void)
{
  // Transmit the $GPRMC message and PMU SOC for diagnostics
  LATCbits.LATC15 = 1;
  PutsUART1((unsigned int *)GPS_Info);
  PutsUART1((unsigned int *)" - GPS Time: ");
  PutsUART1((unsigned int *)ltoa((char *)szInt0xxx, (long)(GPS_Time), 10));
  PutsUART1((unsigned int *)" - PMU SOC: ");
  PutsUART1((unsigned int *)ltoa((char *)szInt0xxx, (long)(PmuSoc), 10));
  if (GPS_PPSCount == 1)
  {
    // Correction was made
    PutsUART1((unsigned int *)" +");
  }
  U1TX_Eol();
  LATCbits.LATC15 = 0;
}


volatile unsigned char PTxBreak = 0x00;       // Break Flag to switch off RS232 Passthru Mode

void RS_Passthru(void)
{
  /// Process Command Received on the UART
  //   RxData[0] = "0".."9"
  unsigned char inR1 = 0x00;           // UART1 input byte
  unsigned char inR2 = 0x00;           // UART2 input byte
  unsigned int iIdxMax = 0;
  unsigned int ii = 0;

  // Check for Command Received by UART2 ISR
  while (R2xRdy > 0)
  {
    LATCbits.LATC15 = 1;

    // Copy the input byte to a working variable
    inR2 = R2xData[0];

    // Left Shift the buffer
    iIdxMax = R2xRdy;
    for (ii = 0; ii < iIdxMax; ii++)
    {
	  R2xData[ii] = R2xData[ii+1];
    }
    // Decrement the waiting bytes count
    R2xRdy--;

	// Relay the UART1 input (inR1) to UART2 output
    while(U1STAbits.UTXBF) { Nop(); }
      U1TXREG = inR2;
  }


  // Check for Command Received by UART1 ISR
  while (R1xRdy > 0)
  {
    LATCbits.LATC15 = 1;

    // Copy the input byte to a working variable
    inR1 = R1xData[0];

    // Left Shift the buffer
    iIdxMax = R1xRdy;
    for (ii = 0; ii < iIdxMax; ii++)
    {
	  R1xData[ii] = R1xData[ii+1];
    }
    // Decrement the waiting bytes count
    R1xRdy--;

	// Check for Passthru Mode Break Request
    if (inR1 == A_SYNC_AT)
    {
      PTxBreak++;
      if (PTxBreak > 3)
      {
        // @@@@ Break Requested, Set to IDLE running mode
        Prog_Mode = PROG_IDLE;
        return;
      }
    }
    else
    {
      // Not a break character, reset break flag counter
      PTxBreak = 0x00;
    }

	// Relay the UART1 input (inR1) to UART2 output
    while(U2STAbits.UTXBF) { Nop(); }
      U2TXREG = inR1;
  }

  LATCbits.LATC15 = 0;
}

