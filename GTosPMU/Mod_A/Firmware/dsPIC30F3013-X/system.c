/* Device header file */
#if defined(__XC16__)
    #include <xc.h>
#elif defined(__C30__)
    #if defined(__dsPIC30F__)
        #include <p30Fxxxx.h>
    #endif
#endif

#include <stdint.h>          /* For uint32_t definition                       */
#include <stdbool.h>         /* For true/false definition                     */

#include "system.h"          /* variables/params used by system.c             */


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


/******************************************************************************/
/* System Level Functions                                                     */
/*                                                                            */
/* Custom oscillator configuration funtions, reset source evaluation          */
/* functions, and other non-peripheral microcontroller initialization         */
/* functions get placed in system.c                                           */
/*                                                                            */
/******************************************************************************/

/* Refer to the device Family Reference Manual Oscillator section for
information about available oscillator configurations.  Typically
this would involve configuring the oscillator tuning register or clock
switching useing the compiler's __builtin_write_OSCCON functions.
Refer to the C Compiler for PIC24F MCUs and dsPIC DSCs User Guide in the
compiler installation directory /doc folder for documentation on the
__builtin functions.  Refer to the XC16 C Compiler User's Guide appendix G
 for a list of the XC16 compiler __builtin functions */

void ConfigureOscillator(void)
{
  /* Disable Watch Dog Timer */
  RCONbits.SWDTEN = 0;

  /* When clock switch occurs switch to Pri Osc controlled by FPR<4:0> */
  __builtin_write_OSCCONH(0x03);  /* Set OSCCONH for clock switch */
  __builtin_write_OSCCONL(0x01);  /* Start clock switching */

  /* Wait for Clock switch to occur */
  //while(OSCCONbits.COSC != 0b011);

  /* Wait for PLL to lock, if PLL is used */
  while(OSCCONbits.LOCK != 1);
}

