using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTosPMU
{
  public class Crc
  {
    // CRC-CCITT Calculation
    // f(x) = x^16 + x^12 + x^5 + 1
    //
    // Derived from IEEE Std C37.118-2005 sample code 

    public static UInt16 Calc_CCITT(string sData, UInt16 iDataLen)
    {
      // Example:  cout << “CRC of “ << “Arnold” << “ = “ << Calc_CRC((unsigned char*)"Arnold") << endl;
      UInt16 iCrc = 0xFFFF;   // 0xFFFF is specific for SynchroPhasor Data CRC
      UInt16 iCalc1;
      UInt16 iCalc2;
      UInt16 ii;
      for (ii = 0; ii < iDataLen; ii++)
      {
        iCalc1 = (UInt16)((iCrc >> 8) ^ (byte)sData[ii]);
        iCrc <<= 8;
        iCalc2 = (UInt16)(iCalc1 ^ (iCalc1 >> 4));
        iCrc ^= iCalc2;
        iCalc2 <<= 5;
        iCrc ^= iCalc2;
        iCalc2 <<= 7;
        iCrc ^= iCalc2;
      }
      return iCrc;
    }


    public static UInt16 Calc_CCITT(byte[] bData, UInt16 iStart, UInt16 iDataLen)
    {
      // Example:  cout << “CRC of “ << “Arnold” << “ = “ << Calc_CRC((unsigned char*)"Arnold") << endl;
      UInt16 iCrc = 0xFFFF;   // 0xFFFF is specific for SynchroPhasor Data CRC
      UInt16 iCalc1;
      UInt16 iCalc2;
      UInt16 ii;
      for (ii = iStart; ii < iStart + iDataLen; ii++)
      {
        iCalc1 = (UInt16)((iCrc >> 8) ^ bData[ii]);
        iCrc <<= 8;
        iCalc2 = (UInt16)(iCalc1 ^ (iCalc1 >> 4));
        iCrc ^= iCalc2;
        iCalc2 <<= 5;
        iCrc ^= iCalc2;
        iCalc2 <<= 7;
        iCrc ^= iCalc2;
      }
      return iCrc;
    }


  }
}
