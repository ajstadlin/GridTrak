/* Device header file */
#if defined(__XC16__)
    #include <xc.h>
#elif defined(__C30__)
    #if defined(__dsPIC30F__)
        #include <p30Fxxxx.h>
    #endif
#endif

#include "RS232.h"        // Serial Port Library

//// UART Receiving Notes
// When a byte is received by a UART, it is stored in the R#xData buffer and the R#xRdy counter is incremented.
// These are command bytes.
// When the process loop detects (R#xRdy > 0) it will retrieve the bytes form the R#xData buffer.
// The process loop then executes appropriate procedures perscribed by the command.
// The process loop procedures are responsible for receiving additional data from the UART.

/// UART1 Received Data Variables
volatile unsigned char R1xRdy = 0;       // Data Bytes Received Counter; Clear this when we process the Received Data
volatile unsigned int R1xIndex = 0;          // Data Array Buffer Index pointer
volatile unsigned int R1xBufMax = 4;
volatile unsigned char R1xData[5];      // Received Data Buffer
volatile unsigned int R1xByte = 0x0000;  // Data Byte working variable

/// UART2 Received Data Variables
volatile unsigned char R2xRdy = 0;        // Data Bytes Received Counter; Clear this when we process the Received Data
volatile unsigned int R2xIndex = 0;           // Data Array Buffer Index pointer
volatile unsigned int R2xBufMax = 4;
volatile unsigned char R2xData[5];       // Received Data Buffer
volatile unsigned int R2xByte = 0x0000;   // Data Byte working variable


void __attribute__((interrupt, no_auto_psv)) _U1RXInterrupt(void)
{
  /// UART1 Receive Interrupt Service Routine
  IFS0bits.U1RXIF = 0;
  if (U1STAbits.URXDA)
  {
    if (R1xRdy <= R1xBufMax)
    {
      R1xByte = U1RXREG & 0x00FF;
      R1xData[R1xRdy] = R1xByte;
    }
    else
    {
      // Buffer Overflow, Left shift the buffer
      R1xRdy = R1xBufMax;
    }
    R1xRdy++;
  }
}


void __attribute__((interrupt, no_auto_psv)) _U2RXInterrupt(void)
{
  /// UART2 Receive Interrupt Service Routine
  IFS1bits.U2RXIF = 0;
  if (U2STAbits.URXDA)
  {
    if (R2xRdy <= R2xBufMax)
    {
      R2xByte = U2RXREG & 0x00FF;
      R2xData[R2xRdy] = R2xByte;
    }
    else
    {
      // Buffer Overflow, Left shift the buffer
      R2xRdy = R2xBufMax;
    }
    R2xRdy++;
  }
}


//// Standard UART Routines
//   Note:  The remaing routines below are from the MicroChip library.

char BusyUART1(void)
{
  // Return TRUE if UART1 is busy Transmitting
  return(!U1STAbits.TRMT);
}


void CloseUART1(void)
{
  /// Disable UART1 and Interrupt;  Clear Flags
  U1MODEbits.UARTEN = 0;    // Disable UART1
  U1STAbits.UTXEN = 0;      // Disable UART1 TX
  IEC0bits.U1RXIE = 0;      // Disable UART1 RX Interrupt
  IEC0bits.U1TXIE = 0;
  IFS0bits.U1RXIF = 0;      // Clear UART1 RX Flag
  IFS0bits.U1TXIF = 0;
}


void ConfigIntUART1(unsigned int config)
{
  /// UART1 Received Data Variables
  R1xRdy = 0;       // Data Bytes Received Counter; Clear this when we process the Received Data
  R1xIndex;          // Data Array Buffer Index pointer
  R1xBufMax = 4;
  R1xData[5];      // Received Data Buffer
  R1xByte = 0x0000;  // Data Byte working variable


  // clear IF flags
  IFS0bits.U1RXIF = 0;
  IFS0bits.U1TXIF = 0;
  // set priority
  IPC2bits.U1RXIP = 0x0007 & config;
  IPC2bits.U1TXIP = (0x0070 & config) >> 4;
  // enable/disable interrupt
  IEC0bits.U1RXIE = (0x0008 & config) >> 3;
  IEC0bits.U1TXIE = (0x0080 & config) >> 7;
}



char DataRdyUART1(void)
{
  // Return true if Data is in the Read buffer
  return(U1STAbits.URXDA);
}


/******************************************************************************
* Description       : This function gets a string of data of specified length *
*                     if available in the UxRXREG buffer into the buffer      *
*                     specified.                                              *
* Parameters        : unsigned int length the length expected                 *
*                     unsigned int *buffer  the received data to be           *
*                                  recorded to this array                     *
*                     unsigned int uart_data_wait timeout value               *
* Return Value      : unsigned int number of data bytes yet to be received    *
******************************************************************************/

unsigned int GetsUART1(unsigned int length,
                        unsigned int *buffer,
                        unsigned int uart_data_wait)
{
  int wait = 0;
  char *temp_ptr = (char *) buffer;
  // read till length is 0
  while(length)
  {
    while(!DataRdyUART1())
    {
      if (wait < uart_data_wait)
      {
        // wait for more data
        wait++ ;
      }
      else
      {
        // Time out- Return words/bytes to be read
        return(length);
      }
    }
    wait=0;
    // check if TX/RX is 8bits or 9bits
    if (U1MODEbits.PDSEL == 3)
    {
      // data word from HW buffer to SW buffer
      *buffer++ = U1RXREG;
    }
    else
    {
      // data byte from HW buffer to SW buffer
      *temp_ptr++ = U1RXREG & 0xFF;
    }
    length--;
  }
  // number of data yet to be received i.e.,0
  return(length);
}



void OpenUART1(unsigned int config1,unsigned int config2, unsigned int ubrg)
{
  U1BRG = ubrg;       // baud rate
  U1MODE = config1;   // operation settings
  U1STA = config2;    // TX & RX interrupt modes
}


void PutsUART1(unsigned int *buffer)
{
  //  Put the data string to be transmitted into the transmit buffer (till NULL character)
  //  Parameter = unsigned int *address of the string buffer to be transmitted
  char * temp_ptr = (char *) buffer;
  // transmit till NULL character is encountered
  // check if TX is 8bits or 9bits
  if (U1MODEbits.PDSEL == 3)
  {
    while(*buffer != '\0')
    {
      // wait if the buffer is full
      while(U1STAbits.UTXBF);
      // transfer data word to TX reg
      U1TXREG = *buffer++;
    }
  }
  else
  {
    while(*temp_ptr != '\0')
    {
      // wait if the buffer is full
      while(U1STAbits.UTXBF);
      // transfer data word to TX reg
      U1TXREG = *temp_ptr++;
    }
  }
}


unsigned int ReadUART1(void)
{
  if (U1MODEbits.PDSEL == 3)
  {
    return (U1RXREG);
  }
  else
  {
    return (U1RXREG & 0xFF);
  }
}


void WriteUART1(unsigned int data)
{
  // Write data into the UxTXREG
  while(U1STAbits.UTXBF);
  if (U1MODEbits.PDSEL == 3)
  {
    U1TXREG = data;
  }
  else
  {
    U1TXREG = data & 0xFF;
  }
}

// ------------

char BusyUART2(void)
{
  // Return TRUE if UART1 is busy Transmitting
  return(!U2STAbits.TRMT);
}


void CloseUART2(void)
{
  /// Disable UART1 and Interrupt;  Clear Flags
  U2MODEbits.UARTEN = 0;    // Disable UART1
  U2STAbits.UTXEN = 0;      // Disable UART1 TX
  IEC1bits.U2RXIE = 0;      // Disable UART1 RX Interrupt
  IEC1bits.U2TXIE = 0;
  IFS1bits.U2RXIF = 0;      // Clear UART1 RX Flag
  IFS1bits.U2TXIF = 0;
}


void ConfigIntUART2(unsigned int config)
{
  /// UART2 Received Data Variables
  R2xRdy = 0;        // Data Bytes Received Counter; Clear this when we process the Received Data
  R2xIndex;           // Data Array Buffer Index pointer
  R2xBufMax = 4;
  R2xData[5];       // Received Data Buffer
  R2xByte = 0x0000;   // Data Byte working variable

  // clear IF flags
  IFS1bits.U2RXIF = 0;
  IFS1bits.U2TXIF = 0;
  // set priority
  IPC6bits.U2RXIP = 0x0007 & config;
  IPC6bits.U2TXIP = (0x0070 & config) >> 4;
  // enable/disable interrupt
  IEC1bits.U2RXIE = (0x0008 & config) >> 3;
  IEC1bits.U2TXIE = (0x0080 & config) >> 7;
}



char DataRdyUART2(void)
{
  // Return true if Data is in the Read buffer
  return(U2STAbits.URXDA);
}


/******************************************************************************
* Description       : This function gets a string of data of specified length *
*                     if available in the UxRXREG buffer into the buffer      *
*                     specified.                                              *
* Parameters        : unsigned int length the length expected                 *
*                     unsigned int *buffer  the received data to be           *
*                                  recorded to this array                     *
*                     unsigned int uart_data_wait timeout value               *
* Return Value      : unsigned int number of data bytes yet to be received    *
******************************************************************************/

unsigned int GetsUART2(unsigned int length,
                        unsigned int *buffer,
                        unsigned int uart_data_wait)
{
  int wait = 0;
  char *temp_ptr = (char *) buffer;
  // read till length is 0
  while(length)
  {
    while(!DataRdyUART2())
    {
      if (wait < uart_data_wait)
      {
        // wait for more data
        wait++ ;
      }
      else
      {
        // Time out- Return words/bytes to be read
        return(length);
      }
    }
    wait=0;
    // check if TX/RX is 8bits or 9bits
    if (U2MODEbits.PDSEL == 3)
    {
      // data word from HW buffer to SW buffer
      *buffer++ = U2RXREG;
    }
    else
    {
      // data byte from HW buffer to SW buffer
      *temp_ptr++ = U2RXREG & 0xFF;
    }
    length--;
  }
  // number of data yet to be received i.e.,0
  return(length);
}



void OpenUART2(unsigned int config1,unsigned int config2, unsigned int ubrg)
{
  U2BRG = ubrg;       // baud rate
  U2MODE = config1;   // operation settings
  U2STA = config2;    // TX & RX interrupt modes
}


void PutsUART2(unsigned int *buffer)
{
  //  Put the data string to be transmitted into the transmit buffer (till NULL character)
  //  Parameter = unsigned int *address of the string buffer to be transmitted
  char * temp_ptr = (char *) buffer;
  // transmit till NULL character is encountered
  // check if TX is 8bits or 9bits
  if (U2MODEbits.PDSEL == 3)
  {
    while(*buffer != '\0')
    {
      // wait if the buffer is full
      while(U2STAbits.UTXBF);
      // transfer data word to TX reg
      U2TXREG = *buffer++;
    }
  }
  else
  {
    while(*temp_ptr != '\0')
    {
      // wait if the buffer is full
      while(U2STAbits.UTXBF);
      // transfer data word to TX reg
      U2TXREG = *temp_ptr++;
    }
  }
}


unsigned int ReadUART2(void)
{
  if (U2MODEbits.PDSEL == 3)
  {
    return (U2RXREG);
  }
  else
  {
    return (U2RXREG & 0xFF);
  }
}


void WriteUART2(unsigned int data)
{
  // Write data into the UxTXREG
  while(U2STAbits.UTXBF);
  if (U2MODEbits.PDSEL == 3)
  {
    U2TXREG = data;
  }
  else
  {
    U2TXREG = data & 0xFF;
  }
}


