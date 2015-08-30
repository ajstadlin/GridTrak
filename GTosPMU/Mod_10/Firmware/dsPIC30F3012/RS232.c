/// RS232.c
/// p30f3012 customized implementation
// Custom Serial Communications
// 01/14/10 0.0000  GridTrak Project, PHzSensor_3013
// 07/31/09 9.0  Added to PHz9_3012 project
// 12/22/06  From MicroChip C30 Library (I should look for an update!)

#include <p30f3012.h>

//// UART 
// Received Packet Counter
volatile unsigned char R1xRdy = 0;    // Clear this when we process the packet
volatile unsigned int R1xIndex;            // 
volatile unsigned char R1xData[10];
volatile unsigned int R1xByte = 0x0000;

volatile unsigned char R2xRdy = 0;    // Clear this when we process the packet
volatile unsigned int R2xIndex;            // 
volatile unsigned char R2xData[10];
volatile unsigned int R2xByte = 0x0000;


void __attribute__((interrupt, no_auto_psv)) _U1RXInterrupt(void)
{
  /// UART2 Receive Interrupt Service Routine
  IFS0bits.U1RXIF = 0;
  if (U1STAbits.URXDA)
  {
    R1xByte = U1RXREG & 0x00FF;
    R1xData[0] = R1xByte;
    R1xRdy++;
  }
}


//// Standard UART Routines
//   Note:  These are derived from the MicroChip library.
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


