//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// ASN.1(BER) Length Info
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil {
  public class LengthInfo {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public bool IsInfinite { get; private set;}
    public int  Length     { get; private set;}
    public int  ByteSize   { get; private set; }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public LengthInfo (bool isInfinite, int length, int byteSize) {
      IsInfinite = IsInfinite;
      Length     = length;
      ByteSize   = byteSize;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      return string.Format("IsInfinite : {0}, Length : {1}, ByteSize : {2}", IsInfinite, Length, ByteSize);
    }

    // Read length data from buffered bytes
    // @Param :
    //  [in] bytes - buffered byte array
    // @Return :
    //  length data for current content
    public static LengthInfo ReadLength (byte[] bytes) {
      byte readCode       = bytes[0];
      bool isMsbEnabled   = ((readCode & 0x80) != 0x00);
      byte bit7to1        = (byte)(readCode & 0x7f);

      if (isMsbEnabled) {
        if (bit7to1 == 0x00) {
          return new LengthInfo(true, 0, 1);
        }
        else {
          // System.Console.WriteLine("[PemReader] LENGTH BYTE SIZE : " + bit7to1);
          if (bit7to1 > 4) {
            throw new System.NotSupportedException("Long definite length is not supported!");
          }
          else {
            int length = 0;
            for (int i = 0;i < bit7to1;i++) {
              length = ((length << 8) | (bytes[1 + i]));
            }
            return new LengthInfo(false, length, 1 + bit7to1);
          }
        }
      }
      else {
        return new LengthInfo(false, (int)bit7to1, 1);
      }
    }
  }
}
