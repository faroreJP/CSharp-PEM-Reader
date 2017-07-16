//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
// BER(Basic Encoding Rule) Value : Octet String
// @Author : Farore
// @Date   : 2017/07/16
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace FaroreUtil.BER {
  public class ValueOctetString : ValueBase {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public ValueOctetString (byte[] bytes) : base (bytes) {}

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Usage
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public override string ToString () {
      var bytes   = (byte[])Value;
      var builder = new System.Text.StringBuilder();

      builder.Append(bytes[0]);
      for (int i = 1;i < bytes.Length;i++) {
        builder.Append(":");
        builder.Append(bytes[i]);
      }

      return builder.ToString();
    }
  }
}


