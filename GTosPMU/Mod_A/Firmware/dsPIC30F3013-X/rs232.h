#ifndef RS232_H
#define	RS232_H

#ifdef	__cplusplus
extern "C" {
#endif

/// UART1 Received Data Variables
extern volatile unsigned char R1xRdy;       // Data Bytes Received Counter; Clear this when we process the Received Data
extern volatile unsigned int R1xIndex;      // Data Array Buffer Index pointer
extern volatile unsigned int R1xBufMax;
extern volatile unsigned char R1xData[];    // Received Data Buffer
extern volatile unsigned int R1xByte;       // Data Byte working variable

extern void PutsUART1(unsigned int *buffer);
extern void CloseUART1(void);

/// UART2 Received Data Variables
extern volatile unsigned char R2xRdy;       // Data Bytes Received Counter; Clear this when we process the Received Data
extern volatile unsigned int R2xIndex;      // Data Array Buffer Index pointer
extern volatile unsigned int R2xBufMax;
extern volatile unsigned char R2xData[];    // Received Data Buffer
extern volatile unsigned int R2xByte;       // Data Byte working variable

extern void PutsUART2(unsigned int *buffer);
extern void CloseUART2(void);

#ifdef	__cplusplus
}
#endif

#endif	/* RS232_H */

