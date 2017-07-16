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

    public LengthInfo (int length, int byteSize) {
      IsInfinite = (length == 0);
      Length     = length;
      ByteSize   = byteSize;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      return string.Format("IsInfinite : {0}, Length : {1}, ByteSize : {2}", IsInfinite, Length, ByteSize);
    }
  }
}
