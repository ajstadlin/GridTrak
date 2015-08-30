// RS232.h
// Custom Serial Communications for the GridTrak Project
// 11/09/11  FW ver 8a - buffer managemement revised

#ifndef __RS232_H
#define __RS232_H

extern volatile unsigned int R1xBufMax;
extern volatile unsigned char R1xRdy;
extern volatile unsigned int R1xIndex;
extern volatile unsigned char R1xData[];
extern volatile unsigned char R1xByte;

void PutsUART1(unsigned int *buffer);
void CloseUART1(void);

extern volatile unsigned int R2xBufMax;
extern volatile unsigned char R2xRdy;
extern volatile unsigned int R2xIndex;
extern volatile unsigned char R2xData[];
extern volatile unsigned char R2xByte;

void PutsUART2(unsigned int *buffer);
void CloseUART2(void);

#endif
