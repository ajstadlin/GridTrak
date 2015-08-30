// GridTrak PHzSensor Model 10 Firmware upgrade for the PHzSensor Model 9B
// 03/09/11 0007  Parallel Version with GTosPMU Model A dsPIC30F3013 firmware
// 03/08/11 0005  Complete Configuration Transmit and Receive implemented
// 02/04/11 00A1  IEEE C37.118-2005 Phase Angle must be in Radians and URX1 Interrupt Priority increased to 6
// 10/30/10 00A0  Separate PPS/Timer sample identifiers
// 10/08/10 00A0  PMU_ID added
// 06/17/10 00A0  IEEE C37.118-2005 Communications
// 06/16/10 00A0  Firmware Upgrade to enable PPS and SynchroPhasor Measurement with GridTrak compatible Output
//
//// dsPIC30F3012 Pin Assignments
//
// Model 10 Changes = 24LC1024 EEPROM is removed 
//   1. RB0 and RB1 disconnected
//   2. RB6/INT0 switched from SCK output to PPS Input
//   3. RB4/PGC/SDI/EE_SO is no longer connected to the EEPROM
//   4. RB5/PGD/SDO/EE_SI is no longer connected to the EEPROM
//
//             +----------------------------+
//         1 *-| !MCLR                 AVdd |-* 18
//         2 <-| AN0/RB0               AVss |-* 17
//         3 <-| AN1/RB1   RB6/AN6/INT0/SCK |-< 16 PPS
//   CTRL0 4 <-| AN2/RB2       IC2/RB7/INT2 |-< 15 Hz from OpAmp.B
//   CTRL1 5 <-| AN3/RB3                Vdd |-* 14
//     OSC 6 >-| CLKI                   Vss |-* 13
// ACT LED 7 <-| RC15           RB5/PGC/SDI |-< 12 ICD2/EE_SO
//   RS232 8 <-| UTXA/RC13      RB4/PGD/SDO |-> 11 ICD2/EE_SI
//   RS232 9 >-| URXA/RC14     IC1/INT1/RD0 |-< 10 Hz from OpAmp.A
//             +----------------------------+
//
// Differences From dsPIC30F3013 to 3012 Firmware
//   1.  OpAmp.A INT2/RD9 changes to INT1/RD0
//   2.  OpAmp.B INT1/RD8 changes to INT2/RB7
//   3.  PPS INT0/RF6 changes to INT0/RB6
//
////  dsPIC30F3013 Micro Controller
//               +----------------------------------------+
//             1-| *MCLR                             AVdd |-28
//             2-| AN0/CN2/RB0                       AVss |-27
//             3-| AN1/CN3/RB1               AN6/OCFA/RB6 |-26
//             4-| AN2/CN4/RB2                    AN7/RB7 |-25
//             5-| AN3/CN5/RB3                AN8/OC1/RB8 |-24
//             6-| AN4/CN6/RB4                AN9/OC2/RB9 |-23
//             7-| AN5/CN7/RB5              U2RX/CN17/RF4 |-22 <- U2RX
//             8-| Vss                      U2TX/CN18/RF5 |-21 -> U2TX
//             9-| OSC1/CLKI                          Vdd |-20
//    LED  <- 10-| OSC2/CLKO/RC15                     Vss |-19
//    U1TX <- 11-| U1ATX/CN1/RC13   PGC/U1RX/SDI1/SDA/RF2 |-18
//    U1RX -> 12-| U1ARX/CN0/RC14   PGD/U1TX/SDO1/SCL/RF3 |-17
//            13-| Vdd                      SCK1/INT0/RF6 |-16 <- PPS
// OpAmp.A -> 14-| IC2/INT2/RD9              IC1/INT1/RD8 |-15 <- OpAmp.B
//               +----------------------------------------+
//                                                                                                                                                                                                                            
///


//// Device Library
#include <p30f3012.h>

//// MCU "FUSE" Options
// Not Using Internal Fast RC Oscillator @ 7.37 MHz  _FOSC(CSW_FSCM_OFF & FRC);

// External Clock Oscillator @ 12.000 MHz *PLL4 = 48 MHz (12 MIPS), *PLL8 = 96 MHz (24 MIPS)
_FOSC(CSW_FSCM_OFF & ECIO_PLL8);
//   Watch Dog OFF
_FWDT(WDT_OFF);
//   Enable MCLR reset pin Brown Out ON, Brown Out Voltage 4.5V, Power Reset Timer 64ms
_FBORPOR(MCLR_EN & PBOR_ON & BORV_45 & PWRT_64);  
//   Disable Code Protection 
_FGS(CODE_PROT_OFF);   


//// Firmware Version
volatile unsigned int SENSOR_VER = 0x0007;

//// Absolute Serial Number
// Optional:  Increment this for every MCU programmed for uniqueness.
// The SERIAL_NBR is fixed here in code and cannot be changed by the PC Host
volatile unsigned int SERIAL_NBR = 0x0009;

// Note:  The Configuration has a Sensor_ID that can be configured by the Host PC for application requirements.


//// Library Inclusions
// Standard Libraries
#include <libpic30.h>     // EEPROM Routines
#include <math.h>
#include <string.h>

// Customized Includes
#include "itoa.h"         // Integer to ASCII conversion
#include "RS232.h"        // Serial Port Library

//// Function Prototypes

// I/O External interrupt ISRs Initialization
void IO_Init(void);
void InCap_Init(void);
void INTx_Init(void);

// GPS/PPS Input Interrupt on INT0
void __attribute__((__interrupt__, no_auto_psv)) _INT0Interrupt(void);

// Using Input Capture ICx Interrupts instead of INTx
void __attribute__((interrupt, no_auto_psv)) _IC1Interrupt(void);
void __attribute__((interrupt, no_auto_psv)) _IC2Interrupt(void);
// void __attribute__((__interrupt__, no_auto_psv)) _INT1Interrupt(void);
// void __attribute__((__interrupt__, no_auto_psv)) _INT2Interrupt(void);

// Interrupt Routines suggested for EEPROM routines
void _ISR __attribute__((__no_auto_psv__)) _AddressError(void);
void _ISR __attribute__((__no_auto_psv__)) _StackError(void);
void _ISR _DefaultInterrupt(void);
void __attribute__((interrupt, no_auto_psv)) _DefaultInterrupt(void);


void Proc_Ctrl(void);                     // Process Control Outputs
void Full_Stop(void);                     // Stop Everything prior to runing Prog_Reset() or exit from main()
void Prog_Reset(unsigned char bLoadCfg);  // Reset with Load Configuration Option

#define CONFIG_NOLOAD 0x00
#define CONFIG_LOAD 0x01
#define CONFIG_FACTORY 0x02

void EGet_Config(unsigned char bFactory);    // Get the Configuration from EEPROM
void ESave_Config(void);                     // Save the Configuration to EEPROM

// UART Routines
void UART1_Init(void);
void URX1_Proc(void);                    // Process Commands Received on the UART

// Send Data Routines
void UTX1_Proc(void);                    // Send Data out UART1

void U1TX_Eol(void);                         // UART Transmit an End Of Line (EOL) CrLf
void Tx_Ping(void);

// PMU Header and Config Output
unsigned int Calc_CRC(unsigned char* sData, unsigned int iDataLen);
void Tx_PmuHeader(void);
void Tx_PmuCfg(unsigned char ucCfgNbr);
void Tx_PmuCfg2(void);

// Data Output
void Send_PmuData(unsigned char uCmd);   // Send PMU Data Message
void Send_PHzData(unsigned char uCmd);   // Send PHz Data as Text

//// Globals

//// Program Modes
// Normal Operation Modes, Persistent when saved in and restored from EEPROM
// Only One Program Mode is enabled
#define PROG_IDLE 0x00      // Run Without Transmitting Data
#define PROG_TEXT 0x01     // Streaming ASCII Text Mode
#define PROG_PMU  0x02     // PMU Binary Packets Mode

// User initiated Modes that stay Active until device reset
// These revert to a Normal Operational Mode read from EEPROM after device reset
#define PROG_EXIT 0xFF      // Exit main() commanded

// Modes that revert to the EEPROM Saved Mode after completion
#define PROG_REBOOT 0xFE    // Load Config from EEPROM and Reset
#define PROG_RESET 0xFD     // Reset without EEPROM Reload
#define PROG_FACTORY 0xFC   // Reload with Factory Defaults

// Any Other ProgMode = unknown error condition, exit

// Program Mode Global Variables
volatile unsigned char Prog_Mode = PROG_IDLE;       // Current Running Program Mode
volatile unsigned char Prog_PrevMode = PROG_TEXT;   // Save Previous ProgMode when changing modes


//// Diagnostics Modes - Used during Normal Program Operation
//   Typically these modes light the Activity LED for the duration of the operation
//   Multiple Diagnostics Modes can be enabled concurrently
// No diagnostics, Run optimized
#define DIAG_NONE 0x0000     

// Main Loop Duration
#define DIAG_MAIN 0x0001

// UART Command Interpreter Duration
#define DIAG_UCMD 0x0002

// Transmit E[0] Elapse Time from IC1 to PPS
#define DIAG_PPS_E0 0x0003

// Frequency Transmit Duration
#define DIAG_HZTX 0x0004     

// Transmit T[0] and Z[0] on IC1 Interrupt
#define DIAG_IC1_T0 0x0005

// Transmit T[1] and Z[1] on IC2 Interrupt
#define DIAG_IC2_T1 0x0006

// Transmit Data on Timer1 Interval
#define DIAG_INTERVAL 0x0007

// Input Capture ISR Duration 
#define DIAG_INCAP1 0x0008   
#define DIAG_INCAP2 0x0010   

// Timer1 and Timer3 ISR Duration (overflow pulses)
#define DIAG_TIMER1 0x0020  
#define DIAG_TIMER3 0x0040  

// EEPROM Routines Durations
#define DIAG_EEPROM 0x0080   

// UART Operation Duration
#define DIAG_UTX1 0x0100   
#define DIAG_URX1 0x0200   
#define DIAG_UTX2 0x0400
#define DIAG_URX2 0x0800

// Zero Crossing Span
#define DIAG_ZXING 0x1000

// INT0 Duration, GPS PPS Signal
#define DIAG_INT0 0x2000

// INT1, INT2 Duration 
#define DIAG_INTX 0x4000

// Togggle Activity LED in Idle Mode   
#define DIAG_IDLE 0x8000

// Diagnostic MOde Global Variables
volatile unsigned int Diag_PrevMode = DIAG_NONE;    // Previous Diagnostics Modes


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
//  BEST:  Overflows = 6
//  Offset = 30766
//  Period = 61539  Largest 60Hz Range [PREFERED 60HZ PERIOD]
//  ModMax = 369234 = 6 x Period
#define OFS60 0x782E
#define PRD60 0xF063        
#define MOD60 0x0005A252

/// PLLx8, 12MHz, 50Hz Magic Numbers
//  BEST:  Overflows = 7
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


// Configuation Structure #0 - Make this structure the same size as _EE_ROW = 32 bytes or 16 words
typedef struct __attribute__((__packed__)) 
{
  unsigned int Sensor_Ver;   // 0
  unsigned int Sensor_ID;    // 2   Unique Sensor ID, 0 = not defined yet
  unsigned int Sensor_PIN;   // 4   Sensor's Password for Web Service Authentication
  unsigned char Prog_Mode;   // 6   Running Mode, Default = PROG_STEXT for Streaming Text Mode
  unsigned char Nominal_Hz;  // 7   nominal frequency of 60 or 50 Hz, Default = 60Hz
  unsigned int Diag_Mode;    // 8   Diagnostics Options, Default = DIAG_HZTX
  int HzCalOffset;           // 10  Frequency Calibration Offset
  int T3CalStart;            // 12  Added to Initial Timer Value
  int ZXCalOffset;           // 14  Added to T3 value on IC1 Interrupt
  int T3CalOffset;           // 16  Added to Final Timer Value
  unsigned int T3Offset;     // 18  OFSET60;  // Value of the Measurement Range Mid Point. Default = 60Hz with n Overflows
  unsigned int T3Period;     // 20  PRD60;    // Timer Period.  Default = 60Hz with n Overflows
  unsigned long T3MzMax;     // 22  MZMAX60;  // Maximum with n Overflows and Modulus Zero.  Default = 60Hz with n Overflows
  unsigned int EOL;          // 26  Default = CR+LF = 0x0d0a
  unsigned int UART1_BRG;    // 28  Default = BRG_115200 = 12
  unsigned int UART2_BRG;    // 30  Default = BRG_9600 = 155
} APP_CFG0;
APP_CFG0 ACfg0;


// Configuation Structure #1 - Make this structure the same size as _EE_ROW = 32 bytes or 16 words
typedef struct __attribute__((__packed__)) 
{
  unsigned char TransMode;   // 0    PHzMonitor Transmit on Sample is TransMode=0, on SynchroPhasor Interval is TransMode=1
  unsigned char extra1;      // 1    unused for now
  unsigned int Intervals;    // 2    The number of SynchroPhasor Measurement Intervals between PPSs
  int T1CalStart;            // 4    Timer1 Start Offset
  int T1CalPrd;              // 6    Timer1 Period Calibration 
  unsigned int T1Period;     // 8    Timer1 Period
  unsigned int T1Prescale;   // 10   Timer1 Prescale
  unsigned int Vref;         // 12   OpAmp Voltage Reference in milli-volts  Default = 2500 = 2.5V
  unsigned char Use_PPS;     // 14   1 = Use GPS PPS on INT0, else use Interval Timer1 on continutiously  
  unsigned char extra2;      // 15   unused for now
  unsigned int Detect[8];    // 16 to 31
} APP_CFG1;
APP_CFG1 ACfg1;


// Configuation Structure #2 - Make this structure the same size as _EE_ROW = 32 bytes or 16 words
typedef struct __attribute__((__packed__))
{
  unsigned long Time_Base;
  char PMU_Station[16];
  char PMU_Stuff[12];        // future use
} PMU_CFG2;
PMU_CFG2 PmuCfg2;


//// PHzMonitor Control Options
// Not Implemented right now in the GridTrak dsPIC30F3013 sensor
// Configuation Structure #3 - Make this structure the same size as _EE_ROW = 32 bytes or 16 words
// CtrX_HI and LO values are in 6 digit Hz, e.g. 600000
typedef struct __attribute__((__packed__)) 
{
  unsigned char Ctr0_EN;    // 0    Control Output #0 Enabled
  unsigned char Ctr0_HiLo;  // 1    Control Output #0 Active HI or LO, 0 = Active LO
  unsigned char Ctr0_Avg;   // 2    Ctr0_Avg = 1 uses Average, 0 uses actual, Default = 1
  unsigned char Ctr0_Range; // 3    Ctr0_Flag = 0 Below LO, 1 In Range, 2 Above High
  unsigned long Ctr0_LoCut; // 4    Control Output #0 LO Cutoff.  If Hz < Ctrl0_LO, then RBx = !Ctrl0_D, Default = 599940 (-0.001%)
  unsigned long Ctr0_HiCut; // 8    Control Output #0 HI Cutoff.  If Hz > Ctrl0_LO, then RBx = !Ctrl0_D, Default = 600060 (+0.001%)
  unsigned char Ctr1_EN;    // 12   Control Output #1 Enabled
  unsigned char Ctr1_HiLo;  // 13   Control Output #1 Default = !Ctr0_D
  unsigned char Ctr1_Avg;   // 14   Ctr1_Avg = 1 uses Average, 0 uses actual, Default = 1
  unsigned char Ctr1_Range; // 15   Ctr1_Range = 0 Below LO, 1 In Range, 2 Above High
  unsigned long Ctr1_LoCut; // 16   Control Output #1 LO Cutoff.  If Hz < Ctrl0_LO, then RBx = !Ctrl0_D
  unsigned long Ctr1_HiCut; // 20   Control Output #1 HI Cutoff.  If Hz > Ctrl0_LO, then RBx = !Ctrl0_D
  unsigned int MeasMethod;  // 24   Output Options
  unsigned int extra3;      // 26   unused for now
  unsigned char AvgMode;    // 28   0=Sum SampleSet/Qty, 1=Mean of Sample Set
  unsigned char StepMode;   // 29   Transmit-> 0=Normal Continuous Hz and Average, Step Modes: 1=Raw and Average, 2=Raw and Mean, 3 = Average and Mean
  unsigned int StepCount;   // 30   Step Count for StepMode > 0;
} PHz_CFG3;
PHz_CFG3 PHzCfg3;


//// Communications
// UART Baud Rate Generator Values
// Fcy = 12 MHz @ PLLx8 = 96 MHz (24 MIPS)
// 12 = [ (24000000 / (16 * 115200 bps)) ] - 1
#define BRG_115200 12  
// 25.0417 = [ (24000000 / (16 * 57600 bps)) ] - 1
#define BRG_57600 25   
// 38.0625 = [ (24000000 / (16 * 38400 bps)) ] - 1
#define BRG_38400 38   
// 77.125 = [ (24000000 / (16 * 19200 bps)) ] - 1
#define BRG_19200 77   
// 155.25 = [ (24000000 / (16 * 9600 bps)) ] - 1
#define BRG_9600 155   
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

// GridTrak Message Prefix = "@"
#define A_GTOSPMU_AT 0x40  

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


// C30 Exception Handlers
// If your code gets here, you either tried to read or write a NULL pointer, 
//  or your application overflowed the stack by having too many local variables or parameters declared.
void _ISR __attribute__((__no_auto_psv__)) _AddressError(void)
{
  Nop();
  Nop();
}

void _ISR __attribute__((__no_auto_psv__)) _StackError(void)
{
  Nop();
	Nop();
}

void _ISR _DefaultInterrupt(void);
void __attribute__((interrupt, no_auto_psv)) _DefaultInterrupt(void)
{
  while(1) ClrWdt()
}


//// EEPROM Data
int _EEDATA(1024) EE_DataAry[512];

void EGet_Config(unsigned char bFactory)
{
  /// Read Configuration from the EEPROM
  int ii = 0;  // loop counter

  if ((DIAG_EEPROM & ACfg0.Diag_Mode) == DIAG_EEPROM)  { LATCbits.LATC15 = 1; }
  
  // Set the EEPROM Address in 
  _prog_addressT EEAdr;
  _init_prog_address(EEAdr, EE_DataAry);
	
  _memcpy_p2d16((void*)&ACfg0, EEAdr, _EE_ROW); 
  _memcpy_p2d16((void*)&ACfg1, EEAdr + sizeof(ACfg0), _EE_ROW);
  _memcpy_p2d16((void*)&PmuCfg2, EEAdr + sizeof(ACfg0) + sizeof(ACfg1), _EE_ROW);
  _memcpy_p2d16((void*)&PHzCfg3, EEAdr + sizeof(ACfg0) + sizeof(ACfg1) + sizeof(PmuCfg2), _EE_ROW);

  if ((SENSOR_VER != ACfg0.Sensor_Ver) || (bFactory == CONFIG_FACTORY))
  {
    // New MCU or Version, Reset values to the Defaults
    // After release, this will only happen if we change the THIS_VER or EEPROM location for ACfg0.This_Ver to differing values.
    ACfg0.Sensor_Ver = (unsigned int)SENSOR_VER;
    ACfg0.Sensor_ID = SERIAL_NBR;
    ACfg0.Sensor_PIN = 0x0000u;
    ACfg0.Prog_Mode = (unsigned int)PROG_IDLE;
    ACfg0.Nominal_Hz = (unsigned int)60;
    ACfg0.Diag_Mode = (unsigned int)DIAG_HZTX;
    ACfg0.HzCalOffset = (int)0;
    ACfg0.T3CalStart = (int)0;
    ACfg0.ZXCalOffset = (int)0;
    ACfg0.T3CalOffset = (int)34;
    ACfg0.T3Offset = (unsigned int)OFS60;
    ACfg0.T3Period = (unsigned int)PRD60;
    ACfg0.T3MzMax = (unsigned long)MOD60;
    ACfg0.EOL = (unsigned int)A_CRLF;
    ACfg0.UART1_BRG = (unsigned int)BRG_115200;  // Network
    ACfg0.UART2_BRG = (unsigned int)BRG_4800;    // Garmin GPS Default (unused in dsPIC30F3012

     // Erase a row in Data EEPROM at array EE_DataAry
    _erase_eedata(EEAdr, _EE_ROW);
      _wait_eedata();  

    // Write a row to Data EEPROM from array CfgData
    _write_eedata_row(EEAdr, (void*)&ACfg0);
      _wait_eedata();  

    ACfg1.TransMode = (unsigned char)0x00;        // PHzMonitor Mode
    ACfg1.extra1 = (unsigned char)0x00;
    ACfg1.Intervals = (unsigned int)0x0006;       // 6 Intervals per Second
    ACfg1.T1CalStart = (int)0;
    ACfg1.T1CalPrd = (int)0;
    ACfg1.T1Period = (unsigned int)PRD60I06;      // Period for 60Hz @ 6 Intervals per Second
    ACfg1.T1Prescale = (unsigned int)PRESCAL10;   // Prescale 1:64
    ACfg1.Vref = (unsigned int)3200;              // 2.5Vref + 0.7V for diode in milli-volts
    ACfg1.Use_PPS = (unsigned int)0x00;           // 1 = use GPS PPS on INT0, else ignore and use Timer1
    ACfg1.extra2 = (unsigned char)0x00;
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
    strcpy(PmuCfg2.PMU_Station, "GridTrak PHz10");  
    memset(PmuCfg2.PMU_Stuff, 0x00, 12);

    // Erase a row in Data EEPROM at array EE_DataAry
    _erase_eedata(EEAdr + sizeof(ACfg0) + sizeof(ACfg1), _EE_ROW);
      _wait_eedata();  

    // Write a row to Data EEPROM from array CfgData
    _write_eedata_row(EEAdr + sizeof(ACfg0) + sizeof(ACfg1), (void*)&PmuCfg2);
      _wait_eedata();  

    PHzCfg3.Ctr0_EN = (unsigned char)1;  
    PHzCfg3.Ctr0_HiLo = (unsigned char)0;   
    PHzCfg3.Ctr0_Avg= (unsigned char)1;  
    PHzCfg3.Ctr0_Range = (unsigned char)1; 
    PHzCfg3.Ctr0_LoCut = (unsigned long)5997000;
    PHzCfg3.Ctr0_HiCut = (unsigned long)6003000;
    PHzCfg3.Ctr1_EN = (unsigned char)1;   
    PHzCfg3.Ctr1_HiLo = (unsigned char)1;    
    PHzCfg3.Ctr1_Avg = (unsigned char)1;  
    PHzCfg3.Ctr1_Range = (unsigned char)1; 
    PHzCfg3.Ctr1_LoCut = (unsigned long)5997000;
    PHzCfg3.Ctr1_HiCut = (unsigned long)6003000;
    PHzCfg3.MeasMethod = (unsigned int)0x0000;
    PHzCfg3.extra3 = (unsigned int)0x0000;
    PHzCfg3.AvgMode = (unsigned char)0x00;
    PHzCfg3.StepMode = (unsigned char)0x03;
    PHzCfg3.StepCount = 20;

    // Erase a row in Data EEPROM at array EE_DataAry
    _erase_eedata(EEAdr + sizeof(ACfg0) + sizeof(ACfg1) + sizeof(PmuCfg2), _EE_ROW);
      _wait_eedata();  

    // Write a row to Data EEPROM from array CfgData
    _write_eedata_row(EEAdr + sizeof(ACfg0) + sizeof(ACfg1) + sizeof(PmuCfg2), (void*)&PHzCfg3);
      _wait_eedata();  

  } // end if Load Factory Defaults

  // Set the EEPROM Address and Read the EEPROM Again
	_memcpy_p2d16((void*)&ACfg0, EEAdr, _EE_ROW);
	_memcpy_p2d16((void*)&ACfg1, EEAdr + sizeof(ACfg0), _EE_ROW);
  _memcpy_p2d16((void*)&PmuCfg2, EEAdr + sizeof(ACfg0) + sizeof(ACfg1), _EE_ROW);
  _memcpy_p2d16((void*)&PHzCfg3, EEAdr + sizeof(ACfg0) + sizeof(ACfg1) + sizeof(PmuCfg2), _EE_ROW);

  if ((DIAG_EEPROM & ACfg0.Diag_Mode) == DIAG_EEPROM) { LATCbits.LATC15 = 0; }  
}


void ESave_Config()
{  
  /// Read Configuration from the EEPROM
  if ((DIAG_EEPROM & ACfg0.Diag_Mode) == DIAG_EEPROM)  {  LATCbits.LATC15 = 1;  }  

  ACfg0.Sensor_Ver = SENSOR_VER;
  if (ACfg0.Prog_Mode > PROG_PMU)
  {
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

  // Erase a row in Data EEPROM at array EE_DataAry
  _erase_eedata(EEAdr + sizeof(ACfg0) + sizeof(ACfg1) + sizeof(PmuCfg2), _EE_ROW);
  _wait_eedata();  

  // Write a row to Data EEPROM from array CfgData
  _write_eedata_row(EEAdr + sizeof(ACfg0) + sizeof(ACfg1) + sizeof(PmuCfg2), (void*)&PHzCfg3);
  _wait_eedata();  

  if ((DIAG_EEPROM & ACfg0.Diag_Mode) == DIAG_EEPROM)  {  LATCbits.LATC15 = 0;  }  
}


void IO_Init(void)
{
  /// Initialize I/O PINS and Associated Peripherals
  //             +----------------------------+
  //         1 *-| !MCLR                 AVdd |-* 18
  //   EX_#1 2 >-| AN0/RB0               AVss |-* 17
  //   EX_#7 3 >-| AN1/RB1   RB6/AN6/INT0/SCK |-< 16 PPS/EX_#6
  //   CTRL0 4 <-| AN2/RB2       IC2/RB7/INT2 |-< 15 Hz from OpAmp B
  //   CTRL1 5 <-| AN3/RB3                Vdd |-* 14
  //     OSC 6 >-| CLKI                   Vss |-* 13
  // ACT LED 7 <-| RC15           RB5/PGC/SDI |-< 12 ICD2/EX_#2
  //         8 <-| UTXA/RC13      RB4/PGD/SDO |-> 11 ICD2/EX_#5
  //         9 >-| URXA/RC14     IC1/INT1/RD0 |-< 10 Hz from OpAmp A
  //             +----------------------------+

  // Turn OFF A/D Peripheral
  ADCON1bits.ADON = 0;

  // A/D ANx Pins Config:  1=Digital, 0=Analog
  ADPCFG = 0xFFFF;    // Set to All Digital

  // Differences From dsPIC30F3013 to 3012 Firmware
  //   1.  PPS INT0/RF6 changes to INT0/RB6
  //   2.  OpAmp.A INT2/RD9 changes to INT1/RD0
  //   3.  OpAmp.B INT1/RD8 changes to INT2/RB7
  //   4.  RB0,1,2, and 3 are Output for PHzSensor 10
  //   5.  UX2 Pins are not available

  // Tri-State Pins Config:  1=Input, 0=Output

  // UART Pins
  TRISCbits.TRISC13 = 0;  // U1ATX
  TRISCbits.TRISC14 = 1;  // U1ARX

  // SPI Pins
  TRISBbits.TRISB5 = 1;     // SDI1/SDA/U1RX/PGC
  TRISBbits.TRISB4 = 0;     // SDO1/SCL/U1TX/PGD

  // Setup Output Pins
  TRISCbits.TRISC15 = 0;  // Activity LED
  LATCbits.LATC15 = 0;    // Default = LO

  TRISBbits.TRISB0 = 1;   // Input - External Socket Pin #1
  LATBbits.LATB0 = 0;

  TRISBbits.TRISB1 = 1;   // Input - External Socket Pin #7
  LATBbits.LATB1 = 0;

  TRISBbits.TRISB2 = 0;   // -> Control Output 0, LED
  LATBbits.LATB2 = 0;

  TRISBbits.TRISB3 = 0;   // -> Control Output 1, LED
  LATBbits.LATB3 = 0;

  TRISBbits.TRISB4 = 0;   // -> PGD - External Socket Pin #5
  LATBbits.LATB4 = 0;    

  TRISBbits.TRISB5 = 1;   // <- PGC - External Socket Pin #2
  PORTBbits.RB5 = 0;

  // Input Pins
  TRISBbits.TRISB6 = 1;   // INT0  <- Input PPS
  PORTBbits.RB6 = 0;

  // Input Capture Pins
  TRISDbits.TRISD0 = 1;   // <- Channel A Input OpAmp HZ, IC1/INT1
  PORTDbits.RD0 = 0;

  TRISBbits.TRISB7 = 1;   // <- Channel B Input OpAmp Hz Signal, IC2/INT2
  PORTBbits.RB7 = 0;
}


void INTx_Init()
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

  // Set INT0 to Third Highest Priority
  IPC0bits.INT0IP = 5;
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

void __attribute__((__interrupt__, no_auto_psv)) _INT0Interrupt(void)
{
  /// GPS PPS Input Interrupt
  // Save Elapsed TMR3 time ASAP
  E[0] = (unsigned long)TMR3 + ((unsigned long)T3Overflow * (unsigned long)ACfg0.T3Period);

  if ((DIAG_INT0 & ACfg0.Diag_Mode) == DIAG_INT0) { LATCbits.LATC15 = 1; }

  // Reset Timer1
  T1CONbits.TON = 0;
  T1Overflow = 0;
  TMR1 = 0;
  T1CONbits.TON = 1;

  /// Increment our copy of the SOC = Second of Century
  //  We can safely assume that our copy of the SOC was received earlier than Now.
  //  We are in the UTC Seconds Roll Over Interrupt, so we can't get an updated SOC.
  PmuSoc++;

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

  if ((DIAG_PPS_E0 & ACfg0.Diag_Mode) == DIAG_PPS_E0)
  {
    // PPS Timer3 Diagnostics; Transmit the E[0] Elapsed Time
    PutsUART1((unsigned int *)"E0=");
    PutsUART1((unsigned int *)itoa((long)E[0], 10));
    U1TX_Eol();
  }

  // Clear INT0 Interrupt Status Flag 
  IFS0bits.INT0IF = 0;

  if ((DIAG_INT0 & ACfg0.Diag_Mode) == DIAG_INT0)  { LATCbits.LATC15 = 0; }
}


void Timer_Init()
{
  // Timer1 Configuration
  // Timer1 is zero synchronized with the PPS (INT0)
  //    The Timer1 interrupt is raised at configured intervals following the PPS
  T1CONbits.TON = 0;
  IEC0bits.T1IE = 0;
  TMR1 = 0;
  IFS0bits.T1IF = 0;
  IPC0bits.T1IP = 6;

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
  // T1CONbits.TON = 1;

  // Timer2 Configuration
  // T2CON.TON[15] = 1:  Turn On Timer
  // T2CON.TSIDL[13] = 0:  Continue in Idle Mode
  // T2CON.TGATE[6] = 0
  // T2CON.TCKPS[5,4] = 01: Prescaler = 1:8 = 1,500,000 ticks/second = 1.5 MHz
  // 01 = 1:8 Prescaled:  (1500000 ticks) / (60 Hz) =  25000 ticks/Hz = 25 ticks/mHz
  //      With a 1:8 Prescale, 60 Hz @ 25000 is in the 16bit range
  //
  // 00 = 1:1 Full Reso: (12000000 ticks) / (60 Hz) = 200000 ticks/Hz = 200 ticks/mHz
  //      With 1:1 resolution, we can set the Timer Period to 45000, 
  //      Then 60 Hz will be 4 overflows + 20000
  // PR2 = ACfg0.T2Period;   // Set the Timer Period to Configured value
  /// 
  //TMR2 = 0;			          // Clear the Timer
  //IFS0bits.T2IF = 0;   // bcf INTCON,TMR0IF;  Reset Timer2 Flag
  //IPC1bits.T2IP = 7;   // Timer2 Interrupt Priority level
  //// Disable for now, enable in Set_Int()
  //IEC0bits.T2IE = 0;    // Disable the Timer Interrupt
  //T2CONbits.TON = 0;    // Disable the Timer

  //// Note:  Time3 Configuration is done in the InCap_Init() procedure
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
  // PR3 = ACfg0.T3Period;   // Set the Timer Period to Configured value
  ///
  //TMR3 = 0;				        // Clear the Timer
  //IFS0bits.T3IF = 0;   // bcf INTCON,TMR0IF;  Reset Timer3 Flag
  //IPC1bits.T3IP = 7;   // Timer3 Interrupt Priority level
  //// Disable for now, enable in Set_Int()
  //IEC0bits.T3IE = 0;    // Disable the Timer Interrupt
  //T3CONbits.TON = 0;    // Disable the Timer
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
  // We can set both to the same priority because they occure sequentially in normal conditions
  _IC2IP = 7;
  _IC1IP = 7;
  
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
  PR3 = ACfg0.T3Period;   // Set the Timer 3 Period to Configured value
  TMR3 = 0;		    // Clear the Timer
  IFS0bits.T3IF = 0;      // bcf INTCON,TMR0IF;  Reset Timer3 Flag
  T3Overflow = 0;         // Reset our Overflow Counter

  /// Timer3 Interrupt Priority Level is relatively low.
  //  We actually do not need the Timer3 interrupt for our measurement.
  //  We can use the overflow count to verify that our measurement is in acceptable range.
  IPC1bits.T3IP = 3;
  
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
unsigned int Crc16 = 0x0000;           // 16 bits CRC word

void Send_PHzData(unsigned char uCmd)
{
  /// Transmit PHzMonitor Style Text Data, comma delimited
  if (Prog_Mode == PROG_IDLE) { return; }
  if ((DIAG_HZTX & ACfg0.Diag_Mode) == DIAG_HZTX)  { LATCbits.LATC15 = 1; }

  char szData[40];
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
  strcat(szData, itoa((long)Msg_Id++, 10));

  // Compute the Frequency to 7 significant digits
  Frequency = (float)100000 * (float)ACfg0.Nominal_Hz * (float)400000 / (float)T[0];
  Frequency = (float)Frequency + (float)ACfg0.HzCalOffset;
  strcat(szData, ",");
  strcat(szData, itoa((long)Frequency, 10));

  // Compute and Transmit the Magnitude
  // Vp = Vref / sine( pi * M / T )
  Magnitude = (float)ACfg1.Vref / (float)sin((float)3.14159265359 * (float)M[0] / (float)T[0] );
  strcat(szData, ",");
  strcat(szData, itoa((long)Magnitude, 10));

  // Compute and Transmit the Phase Angle
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
  strcat(szData, itoa((long)PhaseAng, 10));
  strcat(szData, ",");

  // Send the Text Data
  PutsUART1((unsigned int *)szData);

  // Calculate the CRC and send it followed by EOL
  Crc16 = Calc_CRC((unsigned char *)szData, strlen(szData));
  PutsUART1((unsigned int *)itoa((long)Crc16, 16));
  U1TX_Eol();

  // Process Control Options
  Proc_Ctrl();

  if ((DIAG_HZTX & ACfg0.Diag_Mode) == DIAG_HZTX)  { LATCbits.LATC15 = 0; }
}


/// IEEE C37.118-2005 stuff
unsigned int IFrameSz = 0x0000;       // IEEE Frame Size (bytes in the message packet)
unsigned int IDCode = 0x0000;         // ID Code
unsigned int ICmd = 0x0000;           // IEEE Command
unsigned char IExtFrame = 0x00;       // Extended Frame Input Byte

void Send_PmuData(unsigned char uCmd)
{
  // Transmit SynchroPhasor PMU Data Message in IEEE C37.118-2005 Format
  if (Prog_Mode == PROG_IDLE) { return; }
  if ((DIAG_HZTX & ACfg0.Diag_Mode) == DIAG_HZTX)  { LATCbits.LATC15 = 0; }

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

  // 5. FRACSEC  Note: we don't know this, so use what was sent by the Host CMD Message
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

  // 9. Change in Frequency
  // TODO!
  ucData[28] = 0x00;
  ucData[29] = 0x00;
  ucData[30] = 0x00;
  ucData[31] = 0x00;

  // 10.  Analog
  ucData[32] = 0x00;
  ucData[33] = 0x00;
  ucData[34] = 0x00;
  ucData[35] = 0x00;
  
  // 11.  Digital
  // None

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

  if ((DIAG_HZTX & ACfg0.Diag_Mode) == DIAG_HZTX)  { LATCbits.LATC15 = 0; }
}


// Timer1 Interrupt Service Routine on Period Match = Interval Timer Interrupt
void __attribute__((interrupt, no_auto_psv)) _T1Interrupt(void)
{
  // Save Elapsed TMR3 time ASAP
  E[0] = (unsigned long)TMR3 + ((unsigned long)T3Overflow * (unsigned long)ACfg0.T3Period);

  if ((DIAG_TIMER1 & ACfg0.Diag_Mode) == DIAG_TIMER1) {  LATCbits.LATC15 = 1;  }

  // Increment our Overflow Counter
  T1Overflow++;

  if (T1Overflow < (ACfg0.Nominal_Hz / ACfg1.Intervals))
  {
    // Interval Time Interrupt
    // All Interval Timer events are after the INT0 PPS

    // Update the Fraction of Second IEEE Time Field
    PmuFracSec = (unsigned long)E[0]; 

    if (Prog_Mode == PROG_PMU)
    {
      Send_PmuData(0x00);
    }
    else if (Prog_Mode == PROG_TEXT)
    {
      Send_PHzData(0x00);
    }
  }
  else
  {
    // PPS on INT0, so we simulate it every set number of Intervals.

    // Increment our copy of the SOC = Second of Century
    PmuSoc++;      
  
    // Clear the Fraction of Second IEEE Time Field
    PmuFracSec = 0x00000000;

    if (Prog_Mode == PROG_PMU)
    {
      Send_PmuData(0x02);
    }
    else if (Prog_Mode == PROG_TEXT)
    {
      Send_PHzData(0x02);
    }
    // Note: If we are not using the PPS on INT0, then we can leave Timer1 Running
    // Reset the T1 Overflow
    T1Overflow = 0;
  }  

  if ( (ACfg1.Use_PPS == 1) && (T1Overflow == ((ACfg0.Nominal_Hz / ACfg1.Intervals) - 1)) )
  {
    // Prevent this Interrupt from colliding with the Last Interval
    // Turn Timer1 OFF on the Interval Roll-Over
    T1CONbits.TON = 0;
  }

  // Reset the Timer Interrupt Flag
  IFS0bits.T1IF = 0;

  if ((DIAG_TIMER1 & ACfg0.Diag_Mode) == DIAG_TIMER1) {  LATCbits.LATC15 = 0;  }
}


// Timer3 Interrupt Service Routine on Period Match
void __attribute__((interrupt, no_auto_psv)) _T3Interrupt(void)
{
  if ((DIAG_TIMER3 & ACfg0.Diag_Mode) == DIAG_TIMER3) {  LATCbits.LATC15 = 1;  }

  // Increment our Overflow Counter
  T3Overflow++;

  // Reset the Timer3 Interrupt Flag
  IFS0bits.T3IF = 0;

  if ((DIAG_TIMER3 & ACfg0.Diag_Mode) == DIAG_TIMER3) {  LATCbits.LATC15 = 0;  }
}


/*  // Input Caputure Interrupts used instead of these //
void __attribute__((__interrupt__, no_auto_psv)) _INT1Interrupt(void)
{
  if ((DIAG_INTX & ACfg0.Diag_Mode) == DIAG_INTX) { LATCbits.LATC15 = 1; }
  IFS1bits.INT1IF = 0;
  if ((DIAG_INTX & ACfg0.Diag_Mode) == DIAG_INTX) { LATCbits.LATC15 = 0; }
}

void __attribute__((__interrupt__, no_auto_psv)) _INT2Interrupt(void)
{
  if ((DIAG_INTX & ACfg0.Diag_Mode) == DIAG_INTX) { LATCbits.LATC15 = 1; }
  IFS1bits.INT2IF = 0;
  if ((DIAG_INTX & ACfg0.Diag_Mode) == DIAG_INTX) { LATCbits.LATC15 = 0; }
}
*/


/// Input Capture Interrupt Service Routine
// _ICxInterrupt() is the INTx interrupt service routine (ISR).
// The routine must have global scope in order to be an ISR.
// The ISR name is defined in the the device linker script (the .gld file).

volatile int icidx = 0;         // Input Capture loop counter variable

void __attribute__((interrupt, no_auto_psv)) _IC1Interrupt(void)
{
  // INT1/IC1 Interrupt is triggered on each trailing edge of OpAmp Channel B on Vref

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

  if ((DIAG_IC1_T0 & ACfg0.Diag_Mode) == DIAG_IC1_T0)
  {
    // Input Capture Timer3 Diagnostics; Transmit the T[0] value
    PutsUART1((unsigned int *)"T0,M0,OF,");
    PutsUART1((unsigned int *)itoa((long)T[0], 10));
    PutsUART1((unsigned int *)",");
    PutsUART1((unsigned int *)itoa((long)M[0], 10));
    PutsUART1((unsigned int *)",");
    PutsUART1((unsigned int *)itoa((long)T3Overflow, 10));
    U1TX_Eol();
  }

  // Clear the Interrupt Flag
  IFS0bits.IC1IF = 0;
  
  if ((DIAG_INCAP1 & ACfg0.Diag_Mode) == DIAG_INCAP1)  { LATCbits.LATC15 = 0; }
}


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
    PutsUART1((unsigned int *)itoa((long)M[1], 10));
    PutsUART1((unsigned int *)",");
    PutsUART1((unsigned int *)itoa((long)T3Overflow, 10));
    U1TX_Eol();
  }

  if ((DIAG_INCAP2 & ACfg0.Diag_Mode) == DIAG_INCAP2)  {  LATCbits.LATC15 = 1;  }

  // Calculate our T1 based on the Zero Crossing Times and IC1 T0 time span
  T[1] = T[0] - M[0] + M[1];
  M[1] = M[0];

  if ((DIAG_IC2_T1 & ACfg0.Diag_Mode) == DIAG_IC2_T1)
  {
    // Input Capture Timer3 Diagnostics; Transmit the T[1] value
    PutsUART1((unsigned int *)"T1,M1,OF,");
    PutsUART1((unsigned int *)itoa((long)T[1], 10));
    PutsUART1((unsigned int *)",");
    PutsUART1((unsigned int *)itoa((long)M[1], 10));
    PutsUART1((unsigned int *)",");
    PutsUART1((unsigned int *)itoa((long)T3Overflow, 10));
    U1TX_Eol();
  }

  // Clear the Interrupt Flag
  IFS0bits.IC2IF = 0;  
  if ((DIAG_INCAP2 & ACfg0.Diag_Mode) == DIAG_INCAP2)  {  LATCbits.LATC15 = 0;  }
}


void UART1_Init()
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

  // Assign UART Receive Interrupt need high priority - used for configuration, so other interrupts can wait
  _U1RXIP = 7;
  _U1TXIP = 1;

  IEC0bits.U1RXIE = 1;      // Receive Interrupt Enabled
  U1MODEbits.UARTEN = 1;
  U1STAbits.UTXEN = 1;      // Transmit Enabled by UARTEN 
}

/*
void UART2_Init()
{ 
  /// dsPIC30F3013 Only
  /// Initialize the UART for RS232 Communications
  // UART2 Mode Register
  U2MODE = 0x0000;
  U2MODEbits.LPBACK = 0;    // 1=Enable Loop Back
  U2MODEbits.ABAUD = 0;  	  // 1=Enable AutoBAUD
  // PDSEL<1:0>: Parity and Data Selection bits
  //  11 = 9-bit data, no parity
  //  10 = 8-bit data, odd parity
  //  01 = 8-bit data, even parity
  //  00 = 8-bit data, no parity
  // STSEL: Stop Selection bit
  //  1 = 2 Stop bits
  //  0 = 1 Stop bit
  U2MODEbits.ALTIO = 1;

  // UART Status and Control Register
  U2STA = 0x0000;
  U2STAbits.UTXISEL = 0;
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
*/


void Wait4Me(int iNops)
{
  int rr;
  for (rr = 0; rr < iNops; rr++)
  {
    Nop();
  }
}


int main (void)
{
  // Soft boot
  Prog_Reset(CONFIG_LOAD);
  
  while (Prog_Mode != PROG_EXIT)
  {
    if ((ACfg0.Diag_Mode & DIAG_MAIN) == DIAG_MAIN) { LATCbits.LATC15 = 1; }
    if (ACfg0.Diag_Mode == DIAG_NONE)  { LATCbits.LATC15 = 0; }

    URX1_Proc();
    switch (Prog_Mode)
    {
      case PROG_TEXT:
        break;

      case PROG_PMU:
        break;

      case PROG_RESET:
        Prog_Reset(CONFIG_NOLOAD);
        PutsUART1((unsigned int *)"GTosPMU Reset!");
        U1TX_Eol();
        Wait4Me(1000);
        break;

      case PROG_REBOOT:
        Prog_Reset(CONFIG_LOAD);
        PutsUART1((unsigned int *)"GTosPMU Rebooted!");
        U1TX_Eol();
        Wait4Me(1000);
        break;

      case PROG_FACTORY:
        EGet_Config(CONFIG_FACTORY);
        PutsUART1((unsigned int *)"Factory Defaults Reloaded!");
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

  if (bLoadCfg == CONFIG_LOAD)
  {
    // Get the Configuration from Internal EEPROM
    EGet_Config(CONFIG_LOAD);
  }

  // Re-initialize the I/O pins with the restored configuration options
  IO_Init();

  // Initialize everything else
  UART1_Init();
  // UART2_Init();   // dsPIC30F3013 only


  Timer_Init();
  InCap_Init();
  INTx_Init();

  Prog_Mode = ACfg0.Prog_Mode;

  // GPS PPS Interrupt Enable
  IEC0bits.INT0IE = 1;  

  if (ACfg1.Use_PPS != 0x01)
  {
    // If we don't have a GPS PPS, then we can use Timer1 Intervals continuously
    T1CONbits.TON = 1;
  }
  Msg_Id = 0;
}


void Full_Stop()
{
  INTx_Init();
  IO_Init();
  LATCbits.LATC15 = 0;	// Activity LED
  LATBbits.LATB0 = 0;		
  LATBbits.LATB1 = 0;		
  LATBbits.LATB2 = 0;
  LATBbits.LATB3 = 0;
  LATBbits.LATB4 = 0;
  LATBbits.LATB6 = 0;
}


void U1TX_Eol()
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
  // Example:  cout << “CRC of “ << “Arnold” << “ = “ << Calc_CRC((unsigned char*)"Arnold") << endl;
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


void Tx_PmuHeader()
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
  strcat(szData, "GridTrak! PHzSensor ID# ");
  strcat(szData, itoa((long)ACfg0.Sensor_ID, 10));
  strcat(szData, ", SN# ");
  strcat(szData, itoa((long)SERIAL_NBR, 16));
  strcat(szData, ", Rev ");
  strcat(szData, itoa((long)SENSOR_VER, 16));
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

  // 5. FRACSEC  Note: we don't know this, so use what was sent by the Host CMD Message
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
  ucData[43] = 0x01;  // 1 analog value = [future use?]  

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

void Tx_Ping()
{
  Prog_Mode = PROG_IDLE;
  U1TX_Eol();
  PutsUART1((unsigned int *)"GridTrak!");
  PutsUART1((unsigned int *)", PHzSensor ID# ");
  PutsUART1((unsigned int *)itoa((long)ACfg0.Sensor_ID, 10));
  PutsUART1((unsigned int *)", SN# ");      
  PutsUART1((unsigned int *)itoa((long)SERIAL_NBR, 16));   
  PutsUART1((unsigned int *)", FW Rev ");
  PutsUART1((unsigned int *)itoa((long)SENSOR_VER, 16));
  PutsUART1((unsigned int *)", dsPIC30F3012, ");
  PutsUART1((unsigned int *)itoa((long)ACfg0.Nominal_Hz, 10));
  PutsUART1((unsigned int *)" Hz");
  U1TX_Eol();
}


void Tx_Config()
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

  for (jj = 0; jj < 4; jj++)
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

      case 3:  
        memset(ucData, 0x00, sizeof(ucData));
        memcpy(ucData, &PHzCfg3, sizeof(ucData));
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


void Rx_Config()
{
  // Receive and load a copy of the Configuration from the Host

  // Configuation Structure
  // Make th ucData structure the same size as _EE_ROW = 32 bytes or 16 words
  int ii = 0;
  int jj = 0;
  int s = 0;
  unsigned char ucData[32];

  for (jj = 0; jj < 4; jj++)
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
  
        case 3:  
          memcpy(&PHzCfg3, ucData, sizeof(ucData));
          break;
      }
      s++;
    } 
  }
  U1TX_Eol();
  if (s == 4)
  {
    ESave_Config();
    PutsUART1((unsigned int *)"GTosPMU Config Saved!");
  }
  else
  {
    PutsUART1((unsigned int *)"GTosPMU Config Failed!");
  }
  U1TX_Eol();
  Wait4Me(1000);
  Prog_Mode = PROG_REBOOT;
}


void Set_DiagMode(unsigned char uCmd)
{
  switch (uCmd)
  {
    case 0x61:  // "a"
      ACfg0.Diag_Mode = DIAG_NONE;
      break;

    case 0x62:  // "b"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_MAIN;
      break;

    case 0x63:  // "c"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_UCMD;
      break;

    case 0x64:  // "d"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_HZTX;
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

    case 0x68:  // "h"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_TIMER3;
      break;

    case 0x69:  // "i"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_EEPROM;
      break;

    case 0x6A:  // "j"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_UTX1;
      break;

    case 0x6B:  // "k"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_URX1;
      break;

    case 0x6C:  // "l"
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_UTX2;
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
      ACfg0.Diag_Mode = ACfg0.Diag_Mode | DIAG_INTX;
      break;

    case 0x71:  // "q"
      ACfg0.Diag_Mode = DIAG_PPS_E0;
      break;

    case 0x72:  // "r"
      ACfg0.Diag_Mode = DIAG_IC1_T0;
      break;

    case 0x73:  // "s"
      ACfg0.Diag_Mode = DIAG_IC2_T1;
      break;

    case 0x74:  // "t"
      ACfg0.Diag_Mode = DIAG_INTERVAL;
      break;

    default: 
      ACfg0.Diag_Mode = DIAG_NONE;
      break;
  }
}


void URX1_Proc(void)
{
  /// Process Command Received on the UART
  // Command Format : ASCII Character
  //   RxData[0] = "0".."9"

  unsigned int rr = 0x0000;             // read incrementer
  unsigned char UCmd0 = 0x00;           // First Input Command Byte
  unsigned char UCmd1 = 0x00;           // Second Input Command Byte
  unsigned char AMsg = 0x00;            // Message Type Byte
  unsigned long ISoc = 0x00000000;      // SOC - Second of Century
  unsigned long IFracSec = 0x00000000;  // Faction of Second
  
  // Check for Command Received by UART ISR
  if (R1xRdy == 1)
  {
    // We have a byte in the URX1 transfer buffer
    if ((ACfg0.Diag_Mode == DIAG_NONE) || (DIAG_URX1 & ACfg0.Diag_Mode) == DIAG_URX1) {  LATCbits.LATC15 = 1;  }  

    // Disable UART Interrupt.  We will be reading additional data manually
    IEC0bits.U1RXIE = 0;

    // Copy the command to a working variable
    UCmd0 = R1xData[0];

    // Clear the transfer buffer
    R1xData[0] = 0;

    // Clear Received Command Ready flag
    R1xRdy = 0;

    // Preserve Program Mode
    Prog_PrevMode = ACfg0.Prog_Mode;

    // Check Commands
    if (UCmd0 == A_SYNC_AA)
    {
      /// IEEE C37.118 Protocol Command = 0xAA

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

      PmuSoc = ISoc;

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
        // [TO DO] - Load a frame buffer to compute CRC and Parse for Processing //
      }
      // [TO DO] - Compute the CRC on the buffer up to here //

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

    else if (UCmd0 == A_GTOSPMU_AT)
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
          ACfg1.Use_PPS = (unsigned char)(0x00);
          // Turn off and reset Timer1
          T1CONbits.TON = 0;
          T1Overflow = 0;
          TMR1 = 0;
          // Wait for next byte from PC
          while(!U1STAbits.URXDA);
          // Turn the Timer1 on
          T1CONbits.TON = 1;
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

        case 0x33:  // "3" Set Intervals
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
          // 0x01 = Use GPS PPS on INT0, else use Timer1 intervals and Simulate PPS
          while(!U1STAbits.URXDA);
            ACfg1.Use_PPS = (unsigned char)(U1RXREG & 0xFF);
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

  // Enable the UART1 Interrupt to automatically receive more data
  // Let's do this all the time to ensure we can receive RS232 Data.
  IEC0bits.U1RXIE = 1;
}


void Proc_Ctrl()
{ 
  // The Frequency Calculation is performed in the Input Capture ISR so we don't need to do it here.
  // This routine is called by the transmission procedures, 
  // not by the ISR so it is safer for longer calculations.

  // Perform your calculations here.  Hopefully in less than 1/120 of a second please.  Save time for transmission.
  // If the calculation takes more than 1/100 of a second, consider using a copy of the the data to prevent it from
  // being  changed by the ISR.
  
  if (PHzCfg3.Ctr0_EN == 1)
  {
    //if (ACfg1.Ctr0_Avg == 1)
    //{
    //  lHz = HzAvg;
    //}
    //else
    //{
    //  lHz = HzNow;
    //}
    if (Frequency < PHzCfg3.Ctr0_LoCut)
    {
      LATBbits.LATB2 = !PHzCfg3.Ctr0_HiLo;
      PHzCfg3.Ctr0_Range = 0;
    }
    else if (Frequency > PHzCfg3.Ctr0_HiCut)
    {
      LATBbits.LATB2 = !PHzCfg3.Ctr0_HiLo;
      PHzCfg3.Ctr0_Range = 2;
    }
    else 
    {
      LATBbits.LATB2 = PHzCfg3.Ctr0_HiLo;
      PHzCfg3.Ctr0_Range = 1;
    }  
  }

  if (PHzCfg3.Ctr1_EN == 1)
  {
    //if (ACfg1.Ctr1_Avg == 1)
    //{
    //  lHz = HzAvg;
    //}
    //else
    //{
    //  lHz = HzNow;
    //}
    if (Frequency < PHzCfg3.Ctr1_LoCut)
    {
      LATBbits.LATB3 = !PHzCfg3.Ctr1_HiLo;
      PHzCfg3.Ctr1_Range = 0;
    }
    else if (Frequency > PHzCfg3.Ctr1_HiCut)
    {
      LATBbits.LATB3 = !PHzCfg3.Ctr1_HiLo;
      PHzCfg3.Ctr1_Range = 2;
    }
    else 
    {
      LATBbits.LATB3 = PHzCfg3.Ctr1_HiLo;
      PHzCfg3.Ctr1_Range = 1;
    }  
  }    
   
  // Reset the Counter Flag so we don't do this again with the same data
  IC2Count = 0;
  IC1Count = 0;
}  
