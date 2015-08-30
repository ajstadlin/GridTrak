// RS232.h
// Custom Serial Communications
// 01/14/10 0.0000  GridTrak Project
#ifndef __RS232_H
#define __RS232_H

extern volatile unsigned char R1xRdy;
extern volatile unsigned int R1xIndex;
extern volatile unsigned char R1xData[10];
extern volatile unsigned char R1xByte;

void PutsUART1(unsigned int *buffer);
void CloseUART1(void);


extern volatile unsigned char R2xRdy;
extern volatile unsigned int R2xIndex;
extern volatile unsigned char R2xData[10];
extern volatile unsigned char R2xByte;

void PutsUART2(unsigned int *buffer);
void CloseUART2(void);

#endif
